using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattlEyeManager.Spa.Api
{
    public class OnlineBanController : BaseController
    {
        private readonly ServerStateService _serverStateService;

        public OnlineBanController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        [HttpGet("api/onlineserver/{serverId}/bans")]
        public IActionResult Get(int serverId)
        {
            return Ok(_serverStateService.GetBans(serverId)
                .OrderBy(x => x.Num)
                .ToArray());
        }
    }
}
