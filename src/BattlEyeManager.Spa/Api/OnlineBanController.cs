using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Repositories.Players;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    [Produces("application/json")]
    public class OnlineBanController : BaseController
    {
        private readonly OnlineBanService _onlineBanService;
        private readonly ServerModeratorService _moderatorService;
        private readonly IPlayerRepository _playerRepository;        

        public OnlineBanController(OnlineBanService onlineBanService,
            ServerModeratorService moderatorService,
            IPlayerRepository playerRepository)
        {
            _onlineBanService = onlineBanService;
            _moderatorService = moderatorService;
            _playerRepository = playerRepository;
        }

        [HttpGet("api/onlineserver/{serverId}/bans")]
        public async Task<IActionResult> Get(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);

            var bans = await _onlineBanService.GetOnlineBans(serverId);

            var players = await _playerRepository.GetPlayers(bans.Select(b => b.GuidIp).ToArray());

            foreach (var ban in bans)
            {
                if (players.ContainsKey(ban.GuidIp))
                {
                    var p = players[ban.GuidIp];
                    ban.PlayerName = p.Name;
                    ban.PlayerComment = p.Comment;
                }
            }

            return Ok(bans);
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
