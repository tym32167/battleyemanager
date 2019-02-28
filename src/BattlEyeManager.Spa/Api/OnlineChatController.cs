using AutoMapper;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    public class OnlineChatController : BaseController
    {
        private readonly OnlineChatService _onlineChatService;

        public OnlineChatController(OnlineChatService onlineChatService)
        {
            _onlineChatService = onlineChatService;
        }

        [HttpGet("api/onlineserver/{serverId}/chat")]
        public IActionResult Get(int serverId)
        {
            var chatMessages = _onlineChatService.GetChat(serverId)
                .Select(Mapper.Map<ChatMessageModel>)
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

            _onlineChatService.PostChat(serverId, User.Identity.Name, model.Audience, model.Message);
            return Ok();
        }
    }
}