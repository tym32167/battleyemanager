using BattlEyeManager.BE.Models;
using BattlEyeManager.Spa.Services.State;
using System.Collections.Generic;

namespace BattlEyeManager.Spa.Services
{
    public class OnlineChatService
    {
        private readonly OnlineChatStateService _chatStateService;
        private readonly ServerStateService _serverStateService;

        public OnlineChatService(OnlineChatStateService chatStateService, ServerStateService serverStateService)
        {
            _chatStateService = chatStateService;
            _serverStateService = serverStateService;
        }

        public void PostChat(int serverId, string adminName, int audience, string chatMessage)
        {
            _serverStateService.PostChat(serverId, adminName, audience, chatMessage);
        }

        public IEnumerable<ChatMessage> GetChat(int serverId)
        {
            return _chatStateService.GetChat(serverId);
        }
    }
}