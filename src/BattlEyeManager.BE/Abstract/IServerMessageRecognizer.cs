using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;

namespace BattlEyeManager.BE.Abstract
{
    public interface IServerMessageRecognizer
    {
        // ReSharper disable once UnusedParameter.Global
        ServerMessageType GetMessageType(ServerMessage message);
        bool CanRecognize(ServerMessage serverMessage);
    }
}

