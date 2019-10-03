using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnlineMissionController : BaseController
    {
        private readonly OnlineMissionService _onlineMissionService;
        private readonly ServerModeratorService _moderatorService;

        public OnlineMissionController(OnlineMissionService onlineMissionService, ServerModeratorService moderatorService)
        {
            _onlineMissionService = onlineMissionService;
            _moderatorService = moderatorService;
        }

        [HttpGet("{serverId}/missions")]
        public async Task<IActionResult> Get(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);
            return Ok(await _onlineMissionService.GetOnlineMissions(serverId));
        }

        [HttpPost("{serverId}/missions")]
        public async Task<IActionResult> Post(int serverId, [FromBody]OnlineMissionModel mission)
        {
            _moderatorService.CheckAccess(User, serverId);
            if (serverId != mission.ServerId) return BadRequest();
            await _onlineMissionService.SetOnlineMission(mission);
            return Ok();
        }
    }
}