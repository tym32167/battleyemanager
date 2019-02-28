using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Model;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class OnlineMissionService
    {
        private readonly ServerStateService _stateService;
        private readonly IBeServerAggregator _beServerAggregator;

        public OnlineMissionService(ServerStateService stateService, IBeServerAggregator beServerAggregator)
        {
            _stateService = stateService;
            _beServerAggregator = beServerAggregator;
        }

        public Task<IEnumerable<OnlineMissionModel>> GetOnlineMissions(int serverId)
        {
            var missions = _stateService.GetMissions(serverId);
            var ret = missions.Select(x => Mapper.Map(x,
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