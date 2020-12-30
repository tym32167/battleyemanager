using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Core.Mapping;

namespace BattlEyeManager.Spa.Api
{
    [ApiController]
    [Produces("application/json")]
    public class OnlineChatController : BaseController
    {
        private readonly OnlineChatService _onlineChatService;
        private readonly ServerModeratorService _moderatorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OnlineChatController(OnlineChatService onlineChatService,
            ServerModeratorService moderatorService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _onlineChatService = onlineChatService;
            _moderatorService = moderatorService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("api/onlineserver/{serverId}/chat")]
        public IActionResult Get(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);
            var chatMessages = _onlineChatService.GetChat(serverId)
                .Select(_mapper.Map<ChatMessageModel>)
                .OrderBy(x => x.Date)
                .ToArray();
            return Ok(chatMessages);
        }

        [HttpPut("api/onlineserver/{serverId}/chat")]
        public async Task<IActionResult> Put(int serverId, [FromBody] ChatModel model)
        {
            _moderatorService.CheckAccess(User, serverId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
                return BadRequest();

            _onlineChatService.PostChat(serverId, user.DisplayName, model.Audience, model.Message);
            return Ok();
        }
    }
}