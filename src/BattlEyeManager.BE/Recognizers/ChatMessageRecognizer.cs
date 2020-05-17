using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;

namespace BattlEyeManager.BE.Recognizers
{
    public class ChatMessageRecognizer : IServerMessageRecognizer
    {
        public ServerMessageType GetMessageType(ServerMessage message)
        {
            return ServerMessageType.ChatMessage;
        }

        public bool CanRecognize(ServerMessage serverMessage)
        {
            if (serverMessage.Message.StartsWith("(Side)") || serverMessage.Message.StartsWith("(Vehicle)") || serverMessage.Message.StartsWith("(Unknown)") ||
                (serverMessage.Message.StartsWith("(Global)") || serverMessage.Message.StartsWith("(Group)")) ||
                (serverMessage.Message.StartsWith("(Command)") || serverMessage.Message.StartsWith("(Direct)")))
                return true;
            if (serverMessage.Message.StartsWith("RCon") && !serverMessage.Message.EndsWith("logged in"))
                return true;

            return false;
        }
    }
}