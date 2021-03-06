﻿using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Recognizers;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.BE.Models
{
    public class ServerMessage
    {
        private static readonly IEnumerable<IServerMessageRecognizer> Recognizers = new IServerMessageRecognizer[]
        {
            new AdminListRecognizer(),
            new BanListRecognizer(),
            new BanLogRecognizer(),
            new ChatMessageRecognizer(),
            new MissionsListRecognizer(),
            new PlayerListRecognizer(),
            new PlayerLogRecognizer(),
            new RconAdminLogRecognizer(),
        };

        public ServerMessage(int messageId, string message)
        {
            MessageId = messageId;
            Message = message;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int MessageId { get; }

        public string Message { get; }

        public ServerMessageType Type
        {
            get
            {
                var matches = Recognizers.Where(x => x.CanRecognize(this)).ToArray();
                if (matches.Length == 1) return matches[0].GetMessageType(this);
                return ServerMessageType.Unknown;
            }
        }
    }
}