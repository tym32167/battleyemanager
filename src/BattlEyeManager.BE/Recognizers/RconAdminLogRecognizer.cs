using Arma3BE.Server.Abstract;
using Arma3BE.Server.Messaging;
using Arma3BE.Server.Models;

namespace Arma3BE.Server.Recognizers
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