using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;

namespace BattlEyeManager.BE.Recognizers
{
    public class MissionsListRecognizer : IServerMessageRecognizer
    {
        public ServerMessageType GetMessageType(ServerMessage message)
        {
            return ServerMessageType.MissionList;
        }

        public bool CanRecognize(ServerMessage serverMessage)
        {
            return serverMessage.Message.StartsWith("Missions on server:");
        }
    }
}