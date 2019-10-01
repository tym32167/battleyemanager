using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
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
    public class ServerModeratorController : BaseController
    {
        private readonly ServerModeratorRepository _serverModeratorRepository;
        private readonly IServerRepository _serverRepository;

        public ServerModeratorController(ServerModeratorRepository serverModeratorRepository, IServerRepository serverRepository)
        {
            _serverModeratorRepository = serverModeratorRepository;
            _serverRepository = serverRepository;
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var dbItems =
                new HashSet<int>(
                    (await _serverModeratorRepository.GetByUserId(userId))
                    .Select(x => x.Id));

            var items =
                (await _serverRepository.GetItemsAsync())
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
    }
}