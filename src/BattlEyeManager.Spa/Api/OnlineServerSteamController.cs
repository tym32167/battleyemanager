using BattlEyeManager.Core;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Steam;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OnlineServerSteamController : BaseController
    {
        private readonly OnlineServerService _onlineServerService;
        private readonly ServerModeratorService _moderatorService;
        private readonly IIpService _ipService;
        private readonly ISteamService _steamService;
        private readonly IMapper _mapper;

        public OnlineServerSteamController(OnlineServerService onlineServerService,
            ServerModeratorService moderatorService,
            IIpService ipService,
            ISteamService steamService,
            IMapper mapper)
        {
            _onlineServerService = onlineServerService;
            _moderatorService = moderatorService;
            _ipService = ipService;
            _steamService = steamService;
            _mapper = mapper;
        }


        [HttpGet("{id}/players")]
        public async Task<IActionResult> GetPlayers(int id)
        {
            _moderatorService.CheckAccess(User, id);
            if (id <= 0) return NotFound();

            var item = await _onlineServerService.GetOnlineServer(id);
            if (item == null)
            {
                return NotFound();
            }

            var host = item.Host;
            var ip = _ipService.GetIpAddress(host);
            var ipaddr = IPAddress.Parse(ip);
            var endpoint = new IPEndPoint(ipaddr, item.SteamPort);
            var players = _steamService.GetServerChallengeSync(endpoint);

            return Ok(new { players?.PlayerCount, Players = players.Players.Select(p => _mapper.Map<OnlineSteamPlayerModel>(p)).ToArray() });
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> GeInfo(int id)
        {
            _moderatorService.CheckAccess(User, id);
            if (id <= 0) return NotFound();

            var item = await _onlineServerService.GetOnlineServer(id);
            if (item == null)
            {
                return NotFound();
            }

            var host = item.Host;
            var ip = _ipService.GetIpAddress(host);
            var ipaddr = IPAddress.Parse(ip);
            var endpoint = new IPEndPoint(ipaddr, item.SteamPort);
            var ret = _steamService.GetServerInfoSync(endpoint);

            return Ok(ret);
        }

        [HttpGet("{id}/rules")]
        public async Task<IActionResult> GeRules(int id)
        {
            _moderatorService.CheckAccess(User, id);
            if (id <= 0) return NotFound();

            var item = await _onlineServerService.GetOnlineServer(id);
            if (item == null)
            {
                return NotFound();
            }

            var host = item.Host;
            var ip = _ipService.GetIpAddress(host);
            var ipaddr = IPAddress.Parse(ip);
            var endpoint = new IPEndPoint(ipaddr, item.SteamPort);
            var ret = _steamService.GetServerRulesSync(endpoint);

            return Ok(ret);
        }
    }
}