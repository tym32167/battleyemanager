using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Core;

namespace BattlEyeManager.Spa.Api
{
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
    }
}