using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineServerController : BaseController
    {
        private readonly OnlineServerService _onlineServerService;

        public OnlineServerController(OnlineServerService onlineServerService)
        {
            _onlineServerService = onlineServerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_onlineServerService.GetOnlineServers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
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
            if (serverId != command.ServerId) return BadRequest();
            await _onlineServerService.Execute(command);
            return Ok(new { serverId, command });
        }
    }
}