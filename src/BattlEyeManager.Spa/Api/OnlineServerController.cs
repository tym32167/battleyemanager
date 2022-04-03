using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Infrastructure.Extensions;
using BattlEyeManager.Spa.Infrastructure.Featues;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OnlineServerController : BaseController
    {
        private readonly OnlineServerService _onlineServerService;
        private readonly ServerModeratorService _moderatorService;
        private readonly IServerRepository serverRepository;
        private readonly IMapper mapper;
        private readonly ThresholdFeature thresholdFeature;

        public OnlineServerController(OnlineServerService onlineServerService, 
            ServerModeratorService moderatorService, IServerRepository serverRepository,
            IMapper mapper, ThresholdFeature thresholdFeature)
        {
            _onlineServerService = onlineServerService;
            _moderatorService = moderatorService;
            this.serverRepository = serverRepository;
            this.mapper = mapper;
            this.thresholdFeature = thresholdFeature;
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
        public async Task<IActionResult> Command(int serverId, [FromBody] OnlineServerCommandModel command)
        {
            _moderatorService.CheckAccess(User, serverId);
            if (serverId != command.ServerId) return BadRequest();
            await _onlineServerService.Execute(command);
            return Ok(new { serverId, command });
        }

        [HttpGet("{serverId}/sessions/count")]
        public async Task<IActionResult> GetPlayerSessionCount(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);
            var count = await _onlineServerService.GetPlayerSessionCount(serverId);
            return Ok(new { serverId = serverId, PlayerSessionCount = count });
        }

        [HttpGet("{serverId}/sessions")]
        public async Task<IActionResult> GetSessions(int serverId, int skip, int take)
        {
            _moderatorService.CheckAccess(User, serverId);

            if (skip < 0 || take > 5000 || take < 0) return BadRequest();

            var startSearch = DateTime.UtcNow.AddDays(-1);
            var endSearch = DateTime.UtcNow;

            var sessions = await _onlineServerService.GetPlayerSessions(serverId, skip, take, startSearch, endSearch);

            var ret = new PagedResult<PlayerSessionModel>()
            {
                Skip = skip,
                Take = take,
                Data = sessions
            };

            return Ok(ret);
        }


        [HttpGet("{serverId}/options")]
        public async Task<IActionResult> GetOptions(int serverId)
        {
            _moderatorService.CheckAccess(User, serverId);

            var server = await serverRepository.GetItemByIdAsync(serverId);
            var options = mapper.Map<ServerOptionsModel>(server);            

            return Ok(options);
        }

        [HttpPost("{serverId}/options")]
        public async Task<IActionResult> SaveOptions(ServerOptionsModel model)
        {
            _moderatorService.CheckAccess(User, model.Id);

            var server = await serverRepository.GetItemByIdAsync(model.Id);

            server.ThresholdFeatureEnabled = model.ThresholdFeatureEnabled; 
            server.ThresholdFeatureMessageTemplate = model.ThresholdFeatureMessageTemplate; 
            server.ThresholdMinHoursCap = model.ThresholdMinHoursCap;

            await serverRepository.UpdateItemAsync(server);
            server = await serverRepository.GetItemByIdAsync(model.Id);

            thresholdFeature.SetEnabled(mapper.Map<ServerInfoDto>(server));
            var options = mapper.Map<ServerOptionsModel>(server);

            return Ok(options);
        }
    }
}