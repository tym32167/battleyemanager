using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BattlEyeManager.Spa.Core;

namespace BattlEyeManager.Spa.Api
{
    public class OnlinePlayerController : BaseController
    {
        private readonly ServerStateService _serverStateService;

        public OnlinePlayerController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        [HttpGet("api/onlineserver/{serverId}/players")]
        public IActionResult Get(int serverId)
        {
            return Ok(_serverStateService.GetPlayers(serverId)
                .OrderBy(x => x.Num)
                .ToArray());
        }
    }
}