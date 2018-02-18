using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;

namespace BattlEyeManager.BE.Recognizers
{
    public class BanLogRecognizer : IServerMessageRecognizer
    {
        public ServerMessageType GetMessageType(ServerMessage message)
        {
            return ServerMessageType.BanLog;
        }

        public bool CanRecognize(ServerMessage serverMessage)
        {
            return serverMessage.Message.StartsWith("Player") && (
                serverMessage.Message.Contains("kicked") && serverMessage.Message.Contains("BattlEye: Admin Ban")
                );
        }
    }
}