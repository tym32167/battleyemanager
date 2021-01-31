using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Core.Mapping;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class OnlineMissionService
    {
        private readonly ServerStateService _stateService;
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly IMapper _mapper;

        public OnlineMissionService(ServerStateService stateService, IBeServerAggregator beServerAggregator, IMapper mapper)
        {
            _stateService = stateService;
            _beServerAggregator = beServerAggregator;
            _mapper = mapper;
        }

        public Task<IEnumerable<OnlineMissionModel>> GetOnlineMissions(int serverId)
        {
            var missions = _stateService.GetMissions(serverId);
            var ret = missions.Select(x => _mapper.Map(x,
                    new OnlineMissionModel() { ServerId = serverId }))
                .OrderBy(x => x.Name)
                .ToArray();
            return Task.FromResult(ret.AsEnumerable());
        }

        public Task SetOnlineMission(OnlineMissionModel mission)
        {
            _beServerAggregator.Send(mission.ServerId, BattlEyeCommand.Mission, mission.Name);
            return Task.FromResult(true);
        }
    }
}