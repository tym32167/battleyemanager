using AutoMapper;
using BattlEyeManager.Spa.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class OnlineMissionService
    {
        private readonly ServerStateService _stateService;

        public OnlineMissionService(ServerStateService stateService)
        {
            _stateService = stateService;
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
    }
}