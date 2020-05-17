using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BattlEyeManager.Spa.Infrastructure.State
{
    public class OnlineChatStateService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConcurrentDictionary<int, ConcurrentQueue<ChatMessage>> _chat = new ConcurrentDictionary<int, ConcurrentQueue<ChatMessage>>();

        public event EventHandler<BEServerEventArgs<ChatMessage>> ChatMessageHandler;

        public OnlineChatStateService(IBeServerAggregator serverAggregator, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            serverAggregator.ConnectingHandler += ServerAggregator_ConnectingHandler;
            serverAggregator.DisconnectHandler += ServerAggregator_DisconnectHandler;
            serverAggregator.ChatMessageHandler += ServerAggregator_ChatMessageHandler;
        }

        private void ServerAggregator_ConnectingHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            LoadChat(e.Server);
        }

        private void ServerAggregator_ChatMessageHandler(object sender, BEServerEventArgs<ChatMessage> e)
        {
            AddChatMessage(e.Server.Id, e.Data);
            OnChatMessageHandler(new BEServerEventArgs<ChatMessage>(e.Server, e.Data));
        }

        private void ServerAggregator_DisconnectHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
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

        public IEnumerable<ChatMessage> GetChat(int serverId)
        {
            if (_chat.TryGetValue(serverId, out ConcurrentQueue<ChatMessage> res))
            {
                return res;
            }
            return Enumerable.Empty<ChatMessage>();
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

        protected virtual void OnChatMessageHandler(BEServerEventArgs<ChatMessage> e)
        {
            ChatMessageHandler?.Invoke(this, e);
        }
    }
}