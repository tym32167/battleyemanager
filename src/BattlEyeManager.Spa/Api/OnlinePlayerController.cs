using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    public class OnlinePlayerController : BaseController
    {
        private readonly OnlinePlayerService _onlinePlayerService;

        public OnlinePlayerController(OnlinePlayerService onlinePlayerService)
        {
            _onlinePlayerService = onlinePlayerService;
        }

        [HttpGet("api/onlineserver/{serverId}/players")]
        public async Task<IActionResult> Get(int serverId)
        {
            var ret = await _onlinePlayerService.GetOnlinePlayers(serverId);
            return Ok(ret
                .OrderBy(x => x.Num)
                .ToArray());
        }

        [HttpPost("{serverId}")]
        [Route("api/onlineserver/{serverId}/kick")]
        public async Task<IActionResult> Kick(int serverId, [FromBody] KickPlayerModel model)
        {
            await _onlinePlayerService.KickAsync(serverId, model.Player.Num, model.Player.Guid, model.Reason,
                User.Identity.Name);
            return Ok(model);
        }


        [HttpPost("{serverId}")]
        [Route("api/onlineserver/{serverId}/ban")]
        public async Task<IActionResult> Ban(int serverId, [FromBody] BanPlayerModel model)
        {
            await _onlinePlayerService.BanGuidOnlineAsync(serverId, model.Player.Num, model.Player.Guid, model.Reason,
                model.Minutes,
                User.Identity.Name);
            return Ok(model);
        }
    }
}