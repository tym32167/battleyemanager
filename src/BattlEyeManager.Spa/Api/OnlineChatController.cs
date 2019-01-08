using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    public class OnlineChatController : BaseController
    {
        private readonly ServerStateService _serverStateService;

        public OnlineChatController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        [HttpGet("api/onlineserver/{serverId}/chat")]
        public IActionResult Get(int serverId)
        {
            var chatMessages = _serverStateService.GetChat(serverId)
                .OrderBy(x => x.Date)
                .ToArray();
            return Ok(chatMessages);
        }

        [HttpPut("api/onlineserver/{serverId}/chat")]
        public IActionResult Put(int serverId, [FromBody] ChatModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _serverStateService.PostChat(serverId, User.Identity.Name, model.Audience, model.Message);
            return Ok();
        }
    }
}