using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Infrastructure.Extensions;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OnlineServerController : BaseController
    {
        private readonly OnlineServerService _onlineServerService;
        private readonly ServerModeratorService _moderatorService;

        public OnlineServerController(OnlineServerService onlineServerService, ServerModeratorService moderatorService)
        {
            _onlineServerService = onlineServerService;
            _moderatorService = moderatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var set = _moderatorService.GetServersByUser(User);
            var admin = User.IsAdmin();
            var servers = await _onlineServerService.GetOnlineServers();
            var ret = servers.Where(x => set.Contains(x.Id) || admin).ToArray();
            return Ok(ret);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _moderatorService.CheckAccess(User, id);
            if (id <= 0) return NotFound();

            var item = await _onlineServerService.GetOnlineServer(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost("{serverId}/command")]
        public async Task<IActionResult> Command(int serverId, [FromBody]OnlineServerCommandModel command)
        {
            _moderatorService.CheckAccess(User, serverId);
            if (serverId != command.ServerId) return BadRequest();
            await _onlineServerService.Execute(command);
            return Ok(new { serverId, command });
        }
    }
}