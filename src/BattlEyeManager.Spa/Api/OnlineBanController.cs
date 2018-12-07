using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    public class OnlineBanController : BaseController
    {
        private readonly OnlineBanService _onlineBanService;

        public OnlineBanController(OnlineBanService onlineBanService)
        {
            _onlineBanService = onlineBanService;
        }

        [HttpGet("api/onlineserver/{serverId}/bans")]
        public async Task<IActionResult> Get(int serverId)
        {
            return Ok(await _onlineBanService.GetOnlineBans(serverId));
        }

        [HttpDelete("api/onlineserver/{serverId}/bans/{banNumber}")]
        public async Task<IActionResult> Delete(int serverId, int banNumber)
        {
            await _onlineBanService.RemoveBan(serverId, banNumber);
            return NoContent();
        }
    }
}
