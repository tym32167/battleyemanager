using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Infrastructure.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;

namespace BattlEyeManager.Spa.Infrastructure.Featues
{
    public class ChatBotFeature
    {
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly OnlineChatService _chatService;

        private readonly TelegramBotClient _botClient;


        private readonly Dictionary<int, long> _serverToChatIdMap;
        private readonly Dictionary<long, int> _chatToServerIdMap;

        public ChatBotFeature(IOptions<ChatBotFeatureConfig> options, IBeServerAggregator beServerAggregator,
            OnlineChatService chatService)
        {
            _beServerAggregator = beServerAggregator;
            _chatService = chatService;

            var telegramBotAccessToken = options.Value.TelegramBotAccessToken;

            if (string.IsNullOrWhiteSpace(telegramBotAccessToken)) return;

            _serverToChatIdMap = (options.Value?.ServerToChatMap ?? new Dictionary<string, string>()).ToDictionary(x => int.Parse(x.Key), x => long.Parse(x.Value));
            _chatToServerIdMap = (options.Value?.ChatToServerMap ?? new Dictionary<string, string>()).ToDictionary(x => long.Parse(x.Key), x => int.Parse(x.Value)); ;

            _beServerAggregator.ChatMessageHandler += _beServerAggregator_ChatMessageHandler;
            _botClient = new TelegramBotClient(telegramBotAccessToken);

            if (_chatToServerIdMap.Any())
            {
                _botClient.OnMessage += _botClient_OnMessage;
                _botClient.StartReceiving();
            }
        }

        private void _botClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            var chatId = e.Message.Chat.Id;
            if (_chatToServerIdMap.ContainsKey(chatId))
            {
                var serverId = _chatToServerIdMap[chatId];
                _chatService.PostChat(serverId, e.Message.From.Username, -1, e.Message.Text);
            }
        }

        private async void _beServerAggregator_ChatMessageHandler(object sender,
            BEServerEventArgs<BE.Models.ChatMessage> e)
        {

            if (_serverToChatIdMap.ContainsKey(e.Server.Id) &&
                e.Data.Message?.ToLowerInvariant().Contains("!admin") == true)
            {
                var message = $"server: {e.Server.Name}{Environment.NewLine}message: {e.Data.Message}";
                await _botClient.SendTextMessageAsync(
                    chatId: _serverToChatIdMap[e.Server.Id],
                    text: message
                );
            }
        }
    }

    public class ChatBotFeatureConfig
    {
        public string TelegramBotAccessToken { get; set; }
        public Dictionary<string, string> ServerToChatMap { get; set; }
        public Dictionary<string, string> ChatToServerMap { get; set; }
    }
}