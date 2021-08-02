using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ServerModeratorController : BaseController
    {
        private readonly IServerRepository _serverRepository;
        private readonly ServerModeratorService _moderatorService;

        public ServerModeratorController(
            IServerRepository serverRepository,
            ServerModeratorService moderatorService)
        {
            _serverRepository = serverRepository;
            _moderatorService = moderatorService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var dbItems = _moderatorService.GetServersByUserId(userId);

            var items =
                (await _serverRepository.GetAllServers())
                .Select(x =>
                    new ServerModeratorItem()
                    {
                        ServerId = x.Id,
                        ServerName = x.Name,
                        IsChecked = dbItems.Contains(x.Id)
                    })
                .OrderBy(x => x.ServerName)
                .ToArray();

            return Ok(items);
        }


        [HttpPost("{userId}")]
        public async Task<IActionResult> Post(string userId, [FromBody] ServerModeratorItem[] data)
        {
            await _moderatorService.SetByUserId(userId,
               new HashSet<int>(data.Where(x => x.IsChecked).Select(x => x.ServerId)));
            return await Get(userId);
        }
    }
}