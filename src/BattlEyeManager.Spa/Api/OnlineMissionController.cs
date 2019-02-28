using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Infrastructure.Services;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnlineMissionController : BaseController
    {
        private readonly OnlineMissionService _onlineMissionService;

        public OnlineMissionController(OnlineMissionService onlineMissionService)
        {
            _onlineMissionService = onlineMissionService;
        }

        [HttpGet("{serverId}/missions")]
        public async Task<IActionResult> Get(int serverId)
        {
            return Ok(await _onlineMissionService.GetOnlineMissions(serverId));
        }

        [HttpPost("{serverId}/missions")]
        public async Task<IActionResult> Post(int serverId, [FromBody]OnlineMissionModel mission)
        {
            if (serverId != mission.ServerId) return BadRequest();
            await _onlineMissionService.SetOnlineMission(mission);
            return Ok();
        }
    }
}