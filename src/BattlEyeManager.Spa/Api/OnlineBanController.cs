using BattlEyeManager.Spa.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Infrastructure.Services;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    public class OnlineBanController : BaseController
    {
        private readonly OnlineBanService _onlineBanService;
        private readonly ServerModeratorService _moderatorService;

        public OnlineBanController(OnlineBanService onlineBanService, ServerModeratorService moderatorService)
        {
            _onlineBanService = onlineBanService;
            _moderatorService = moderatorService;
        }

        [HttpGet("api/onlineserver/{serverId}/bans")]
        public async Task<IActionResult> Get(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);
            return Ok(await _onlineBanService.GetOnlineBans(serverId));
        }

        [HttpDelete("api/onlineserver/{serverId}/bans/{banNumber}")]
        public async Task<IActionResult> Delete(int serverId, int banNumber)
        {
            _moderatorService.CheckAccess(User, serverId);
            await _onlineBanService.RemoveBan(serverId, banNumber);
            return NoContent();
        }
    }
}
