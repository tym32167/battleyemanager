using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;

namespace BattlEyeManager.BE.Recognizers
{
    public class RconAdminLogRecognizer : IServerMessageRecognizer
    {
        public ServerMessageType GetMessageType(ServerMessage message)
        {
            return ServerMessageType.RconAdminLog;
        }

        public bool CanRecognize(ServerMessage serverMessage)
        {
            return serverMessage.Message.StartsWith("RCon admin") && serverMessage.Message.EndsWith("logged in");
        }
    }
}