using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattlEyeManager.Spa.Api
{
    public class OnlineChatController : BaseController
    {
        private readonly ServerStateService _serverStateService;

        public OnlineChatController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        [HttpGet("api/onlineserver/{serverId}/chat")]
        public IActionResult Get(int serverId)
        {
            var chatMessages = _serverStateService.GetChat(serverId)
                .OrderBy(x => x.Date)
                .ToArray();
            return Ok(chatMessages);
        }
    }
}