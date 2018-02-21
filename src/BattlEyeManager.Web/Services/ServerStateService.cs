using BattlEyeManager.BE.BeNet;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Services
{
    public class ServerStateService
    {
        private readonly IBeServerAggregator _aggregator;
        private readonly IKeyValueStore<ChatModel, Guid> _chatStore;

        private readonly ConcurrentDictionary<Guid, IEnumerable<Player>> _playerState = new ConcurrentDictionary<Guid, IEnumerable<Player>>();

        private readonly ConcurrentDictionary<Guid, ConcurrentQueue<ChatMessage>> _chat = new ConcurrentDictionary<Guid, ConcurrentQueue<ChatMessage>>();

        private Timer _timer;

        public ServerStateService(IBeServerAggregator aggregator, IKeyValueStore<ChatModel, Guid> chatStore)
        {
            _aggregator = aggregator;
            _chatStore = chatStore;

            _aggregator.PlayerHandler += _aggregator_PlayerHandler;
            _aggregator.ChatMessageHandler += _aggregator_ChatMessageHandler;
            _aggregator.DisconnectHandler += _aggregator_DisconnectHandler;
        }


        public async Task InitAsync()
        {
            var history = (await _chatStore.FindAsync(x => x.Date > DateTime.Today)).OrderByDescending(x => x.Date).Take(100).Reverse();

            foreach (var chat in history)
            {
                AddChatMessage(chat.ServerId, new ChatMessage() { Message = chat.Text, Date = chat.Date });
            }

            _timer = new Timer(PlayersUpdate, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        private void _aggregator_DisconnectHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            var emptyPlayer = Enumerable.Empty<Player>();
            _playerState.AddOrUpdate(e.Server.Id, guid => emptyPlayer, (guid, players) => emptyPlayer);

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

        private void PlayersUpdate(object state)
        {
            var servers = _aggregator.GetConnectedServers().ToArray();

            foreach (var server in servers)
            {
                _aggregator.Send(server.Id, BattlEyeCommand.Players);
            }
        }

        private async void _aggregator_ChatMessageHandler(object sender, BEServerEventArgs<ChatMessage> e)
        {
            AddChatMessage(e.Server.Id, e.Data);
            await _chatStore.AddAsync(new ChatModel { Date = e.Data.Date, ServerId = e.Server.Id, Text = e.Data.Message });
        }

        private void AddChatMessage(Guid serverId, ChatMessage message)
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
                    queue.TryDequeue(out ChatMessage _);
                    queue.TryDequeue(out ChatMessage _);
                }
                return queue;
            });
        }

        private void _aggregator_PlayerHandler(object sender, BEServerEventArgs<IEnumerable<Player>> e)
        {
            _playerState.AddOrUpdate(e.Server.Id, guid => e.Data, (guid, players) => e.Data);
        }

        public IEnumerable<Player> GetPlayers(Guid serverId)
        {
            if (_playerState.TryGetValue(serverId, out IEnumerable<Player> res))
            {
                return res;
            }
            return Enumerable.Empty<Player>();
        }

        public IEnumerable<ChatMessage> GetChat(Guid serverId)
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

        public void RefreshPlayers(Guid serverId)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Players);
        }

        public void PostChat(Guid serverId, string chatMessage)
        {
            _aggregator.Send(serverId, BattlEyeCommand.Say, $" -1 tim: {chatMessage}");
        }
    }
}