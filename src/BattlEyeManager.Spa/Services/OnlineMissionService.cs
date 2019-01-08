using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class OnlineMissionService
    {
        private readonly ServerStateService _stateService;
        private readonly BeServerAggregator _beServerAggregator;

        public OnlineMissionService(ServerStateService stateService, BeServerAggregator beServerAggregator)
        {
            _stateService = stateService;
            _beServerAggregator = beServerAggregator;
        }

        public async Task<IEnumerable<OnlineMissionModel>> GetOnlineMissions(int serverId)
        {
            var missions = _stateService.GetMissions(serverId);
            var ret = missions.Select(x => Mapper.Map(x,
                    new OnlineMissionModel() { ServerId = serverId }))
                .OrderBy(x => x.Name)
                .ToArray();
            return ret;
        }

        public async Task SetOnlineMission(OnlineMissionModel mission)
        {
            _beServerAggregator.Send(mission.ServerId, BattlEyeCommand.Mission, mission.Name);
        }
    }
}