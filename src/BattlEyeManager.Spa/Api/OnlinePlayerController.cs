﻿using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    [Produces("application/json")]
    public class OnlinePlayerController : BaseController
    {
        private readonly OnlinePlayerService _onlinePlayerService;
        private readonly ServerModeratorService _moderatorService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OnlinePlayerController(OnlinePlayerService onlinePlayerService,
            ServerModeratorService moderatorService,
            UserManager<ApplicationUser> userManager)
        {
            _onlinePlayerService = onlinePlayerService;
            _moderatorService = moderatorService;
            _userManager = userManager;
        }

        [HttpGet("api/onlineserver/{serverId}/players")]
        public async Task<IActionResult> Get(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);
            var ret = await _onlinePlayerService.GetOnlinePlayers(serverId);
            return Ok(ret
                .OrderBy(x => x.Num)
                .ToArray());
        }
                
        [HttpPost("api/onlineserver/{serverId}/kick")]
        public async Task<IActionResult> Kick(int serverId, [FromBody] KickPlayerModel model)
        {
            _moderatorService.CheckAccess(User, serverId);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return BadRequest();

            await _onlinePlayerService.KickAsync(serverId, model.Player.Num, model.Player.Guid, model.Reason,
                user.DisplayName);
            return Ok(model);
        }

        [HttpPost("api/onlineserver/{serverId}/ban/online")]
        public async Task<IActionResult> OnlineBan(int serverId, [FromBody] BanPlayerModel model)
        {
            _moderatorService.CheckAccess(User, serverId);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return BadRequest();

            await _onlinePlayerService.BanGuidOnlineAsync(serverId, model.Player.Num, model.Player.Guid, model.Reason,
                model.Minutes,
                user.DisplayName);
            return Ok(model);
        }

        [HttpPost("api/onlineserver/{serverId}/ban/offline")]
        public async Task<IActionResult> OfflineBan(int serverId, [FromBody] OfflineBanPlayerModel model)
        {
            _moderatorService.CheckAccess(User, serverId);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return BadRequest();

            await _onlinePlayerService.BanOfflineAsync(serverId, model.PlayerId, model.Reason,
                model.Minutes,
                user.DisplayName);
            return Ok(model);
        }
    }
}