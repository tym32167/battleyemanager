using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleNET;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace BattlEyeManager.Spa.Infrastructure.State
{
    public class ServerStateService
    {
        private readonly IBeServerAggregator _aggregator;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly DataRegistrator _dataRegistrator;
        private readonly OnlinePlayerStateService _onlinePlayerStateService;
        private readonly OnlineChatStateService _onlineChatStateService;

        private readonly ConcurrentDictionary<int, IEnumerable<Mission>> _missionsState = new ConcurrentDictionary<int, IEnumerable<Mission>>();
        private readonly ConcurrentDictionary<int, IEnumerable<Admin>> _adminState = new ConcurrentDictionary<int, IEnumerable<Admin>>();
        private readonly ConcurrentDictionary<int, IEnumerable<Ban>> _banState = new ConcurrentDictionary<int, IEnumerable<Ban>>();

        public ServerStateService(IBeServerAggregator aggregator, IServiceScopeFactory scopeFactory,
            DataRegistrator dataRegistrator,
            OnlinePlayerStateService onlinePlayerStateService,
            OnlineChatStateService onlineChatStateService)
        {
            _aggregator = aggregator;
            _scopeFactory = scopeFactory;
            _dataRegistrator = dataRegistrator;
            _onlinePlayerStateService = onlinePlayerStateService;
            _onlineChatStateService = onlineChatStateService;
        }

        public Task InitAsync()
        {
            _onlineChatStateService.ChatMessageHandler += _onlineChatStateService_ChatMessageHandler; ;

            _aggregator.AdminHandler += _aggregator_AdminHandler;
            _aggregator.BanHandler += _aggregator_BanHandler;
            _aggregator.MissionHandler += _aggregator_MissionHandler;

            _onlinePlayerStateService.PlayersJoined += _onlinePlayerStateService_PlayersJoined;
            _onlinePlayerStateService.PlayersLeaved += _onlinePlayerStateService_PlayersLeaved;

            return Task.FromResult(true);
        }

        private async void _onlineChatStateService_ChatMessageHandler(object sender, BEServerEventArgs<ChatMessage> e)
        {
            await _dataRegistrator.RegisterChatMessage(e);

            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<IHubContext<FallbackHub>>();
                await ctx.Clients.All.SendAsync("event", e.Server.Id, "chat");
            }
        }

        private async void _onlinePlayerStateService_PlayersLeaved(object sender,
            BEServerEventArgs<IEnumerable<Player>> e)
        {
            if (e.Data.Any())
                await _dataRegistrator.UsersOnlineChangeRegisterLeaved(e.Data.ToArray(), e.Server);

            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<IHubContext<FallbackHub>>();
                await ctx.Clients.All.SendAsync("event", e.Server.Id, "player");
            }
        }


        private async void _onlinePlayerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<Player>> e)
        {
            if (e.Data.Any())
                await _dataRegistrator.UsersOnlineChangeRegisterJoined(e.Data.ToArray(), e.Server);

            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<IHubContext<FallbackHub>>();
                await ctx.Clients.All.SendAsync("event", e.Server.Id, "player");
            }
        }

        private void _aggregator_MissionHandler(object sender, BEServerEventArgs<IEnumerable<Mission>> e)
        {
            _missionsState.AddOrUpdate(e.Server.Id, i => e.Data, (i, p) => e.Data);
        }

        private async void _aggregator_BanHandler(object sender, BEServerEventArgs<IEnumerable<Ban>> e)
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

            using (var scope = _scopeFactory.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<IHubContext<FallbackHub>>();
                await ctx.Clients.All.SendAsync("event", e.Server.Id, "banlist");
            }
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

        public IEnumerable<Admin> GetAdmins(int serverId)
        {
            if (_adminState.TryGetValue(serverId, out IEnumerable<Admin> res))
            {
                return res;
            }
            return Enumerable.Empty<Admin>();
        }

        public IEnumerable<Mission> GetMissions(int serverId)
        {
            if (_missionsState.TryGetValue(serverId, out IEnumerable<Mission> res))
            {
                return res;
            }
            return Enumerable.Empty<Mission>();
        }

        public void PostChat(int serverId, string adminName, int audience, string chatMessage)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Say, $" {audience} {adminName}: {chatMessage}");
        }

        public IEnumerable<ServerInfo> GetConnectedServers()
        {
            return _aggregator.GetConnectedServers();
        }

        public void RefreshPlayers(int serverId)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Players);
        }

        public bool IsConnected(int serverId)
        {
            return _aggregator.IsConnected(serverId);
        }

        public int GetAdminsCount(int serverId)
        {
            if (_adminState.TryGetValue(serverId, out IEnumerable<Admin> res))
            {
                return res.Count();
            }
            return 0;
        }

        public int GetBansCount(int serverId)
        {
            if (_banState.TryGetValue(serverId, out IEnumerable<Ban> res))
            {
                return res.Count();
            }
            return 0;
        }

        public IEnumerable<Ban> GetBans(int serverId)
        {
            if (_banState.TryGetValue(serverId, out IEnumerable<Ban> res))
            {
                return res;
            }
            return Enumerable.Empty<Ban>();
        }
    }
}