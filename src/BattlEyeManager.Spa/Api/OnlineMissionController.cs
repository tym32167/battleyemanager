using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    public class OnlineMissionController : BaseController
    {
        private readonly OnlineMissionService _onlineMissionService;

        public OnlineMissionController(OnlineMissionService onlineMissionService)
        {
            _onlineMissionService = onlineMissionService;
        }

        [HttpGet("api/onlineserver/{serverId}/missions")]
        public async Task<IActionResult> Get(int serverId)
        {
            return Ok(await _onlineMissionService.GetOnlineMissions(serverId));
        }
    }
}