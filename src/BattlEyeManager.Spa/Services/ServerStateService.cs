using BattleNET;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class ServerStateService
    {
        private readonly IBeServerAggregator _aggregator;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly DataRegistrator _dataRegistrator;

        private readonly ConcurrentDictionary<int, IEnumerable<Player>> _playerState = new ConcurrentDictionary<int, IEnumerable<Player>>();
        private readonly ConcurrentDictionary<int, ConcurrentQueue<ChatMessage>> _chat = new ConcurrentDictionary<int, ConcurrentQueue<ChatMessage>>();

        private readonly ConcurrentDictionary<int, IEnumerable<Admin>> _adminState = new ConcurrentDictionary<int, IEnumerable<Admin>>();
        private readonly ConcurrentDictionary<int, IEnumerable<Ban>> _banState = new ConcurrentDictionary<int, IEnumerable<Ban>>();

        public ServerStateService(IBeServerAggregator aggregator, IServiceScopeFactory scopeFactory, DataRegistrator dataRegistrator)
        {
            _aggregator = aggregator;
            _scopeFactory = scopeFactory;
            _dataRegistrator = dataRegistrator;
        }

        public async Task InitAsync()
        {
            _aggregator.PlayerHandler += _aggregator_PlayerHandler;
            _aggregator.ChatMessageHandler += _aggregator_ChatMessageHandler;
            _aggregator.DisconnectHandler += _aggregator_DisconnectHandler;
            _aggregator.ConnectingHandler += _aggregator_ConnectingHandler;

            _aggregator.AdminHandler += _aggregator_AdminHandler;
            _aggregator.BanHandler += _aggregator_BanHandler;
        }

        private void _aggregator_BanHandler(object sender, BEServerEventArgs<IEnumerable<Ban>> e)
        {
            _banState.AddOrUpdate(e.Server.Id, guid =>
            {
                var ret = e.Data;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                _dataRegistrator.BansOnlineChangeRegister(e.Data.ToArray(), e.Server);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                return ret;
            }, (guid, players) =>
            {
                var ret = e.Data;

                var joined = ret.Where(r => !players.Any(p => p.Num == r.Num && p.GuidIp == r.GuidIp && p.Reason == r.Reason)).ToArray();
                var leaved = players.Where(r => !ret.Any(p => p.Num == r.Num && p.GuidIp == r.GuidIp && p.Reason == r.Reason)).ToArray();

#pragma warning disable 4014
                if (joined.Any() || leaved.Any())
                    _dataRegistrator.BansOnlineChangeRegister(e.Data.ToArray(), e.Server);
#pragma warning restore 4014
                return ret;
            });
        }

        private void _aggregator_AdminHandler(object sender, BEServerEventArgs<IEnumerable<Admin>> e)
        {
            _adminState.AddOrUpdate(e.Server.Id, guid =>
            {
                var ret = e.Data;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                _dataRegistrator.AdminsOnlineChangeRegister(e.Data.ToArray(), new Admin[0], e.Server);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                return ret;
            }, (guid, players) =>
            {
                var ret = e.Data;

                var joined = ret.Where(r => !players.Any(p => p.Num == r.Num && p.IP == r.IP && p.Port == r.Port)).ToArray();
                var leaved = players.Where(r => !ret.Any(p => p.Num == r.Num && p.IP == r.IP && p.Port == r.Port)).ToArray();

#pragma warning disable 4014
                if (joined.Any() || leaved.Any())
                    _dataRegistrator.AdminsOnlineChangeRegister(joined, leaved, e.Server);
#pragma warning restore 4014
                return ret;
            });
        }

        private void _aggregator_ConnectingHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            LoadChat(e.Server);
        }

        private async void LoadChat(ServerInfo server)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var history = await ctx.ChatMessages.Where(c => c.ServerId == server.Id)
                        .OrderByDescending(x => x.Date)
                        .Take(100).OrderBy(x => x.Date).ToListAsync();

                    foreach (var chat in history)
                    {
                        AddChatMessage(chat.ServerId, new ChatMessage { Message = chat.Text, Date = chat.Date });
                    }
                }
            }
        }

        private void _aggregator_DisconnectHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            var emptyPlayer = Enumerable.Empty<Player>();
            _playerState.AddOrUpdate(e.Server.Id, guid => emptyPlayer, (guid, players) =>
            {
                _dataRegistrator.UsersOnlineChangeRegister(new Player[0], players.ToArray(), e.Server);
                return emptyPlayer;
            });

            _chat.AddOrUpdate(e.Server.Id, guid =>
            {
                var queue = new ConcurrentQueue<ChatMessage>();
                queue.Clear();
                return queue;
            }, (guid, queue) =>
            {
                queue.Clear();
                return queue;
            });
        }

        private async void _aggregator_ChatMessageHandler(object sender, BEServerEventArgs<ChatMessage> e)
        {
            AddChatMessage(e.Server.Id, e.Data);
            await _dataRegistrator.RegisterChatMessage(e);

            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<IHubContext<FallbackHub>>();
                await ctx.Clients.All.SendAsync("send", e.Server.Id, e.Data);
            }
        }

        private void AddChatMessage(int serverId, ChatMessage message)
        {
            _chat.AddOrUpdate(serverId, guid =>
            {
                var queue = new ConcurrentQueue<ChatMessage>();
                queue.Enqueue(message);
                return queue;
            }, (guid, queue) =>
            {
                queue.Enqueue(message);
                if (queue.Count > 100)
                {
                    queue.TryDequeue(out ChatMessage _);
                }
                return queue;
            });
        }

        private void _aggregator_PlayerHandler(object sender, BEServerEventArgs<IEnumerable<Player>> e)
        {
            _playerState.AddOrUpdate(e.Server.Id, guid =>
            {
                var ret = e.Data;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                _dataRegistrator.UsersOnlineChangeRegister(ret.ToArray(), new Player[0], e.Server);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                return ret;
            }, (guid, players) =>
            {
                var ret = e.Data;

                var joined = ret.Where(r => players.All(p => p.Guid != r.Guid)).ToArray();
                var leaved = players.Where(r => ret.All(p => p.Guid != r.Guid)).ToArray();

#pragma warning disable 4014
                if (joined.Any() || leaved.Any())
                    _dataRegistrator.UsersOnlineChangeRegister(joined, leaved, e.Server);
#pragma warning restore 4014

                return ret;
            });
        }

        public IEnumerable<Player> GetPlayers(int serverId)
        {
            if (_playerState.TryGetValue(serverId, out IEnumerable<Player> res))
            {
                return res;
            }
            return Enumerable.Empty<Player>();
        }

        public IEnumerable<ChatMessage> GetChat(int serverId)
        {
            if (_chat.TryGetValue(serverId, out ConcurrentQueue<ChatMessage> res))
            {
                return res;
            }
            return Enumerable.Empty<ChatMessage>();
        }

        public IEnumerable<ServerInfo> GetConnectedServers()
        {
            return _aggregator.GetConnectedServers();
        }

        public void RefreshPlayers(int serverId)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Players);
        }

        public void PostChat(int serverId, string chatMessage)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Say, $" -1 tim: {chatMessage}");
        }
    }
}