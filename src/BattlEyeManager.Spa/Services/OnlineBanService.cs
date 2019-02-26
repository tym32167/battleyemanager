using AutoMapper;
using BattleNET;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services.State;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class OnlineBanService
    {
        private readonly ServerStateService _serverStateService;
        private readonly IBeServerAggregator _serverAggregator;

        public OnlineBanService(ServerStateService serverStateService, IBeServerAggregator serverAggregator)
        {
            _serverStateService = serverStateService;
            _serverAggregator = serverAggregator;
        }

        public Task<OnlineBanViewModel[]> GetOnlineBans(int serverId)
        {
            var bans = _serverStateService.GetBans(serverId);
            var ret =
                bans.Select(x => Mapper.Map<Ban, OnlineBanViewModel>(x,
                        new OnlineBanViewModel { ServerId = serverId }))
                    .OrderBy(x => x.Num)
                    .ToArray();
            return Task.FromResult(ret);
        }

        public Task RemoveBan(int serverId, int banNumber)
        {
            _serverAggregator.Send(serverId, BattlEyeCommand.RemoveBan, banNumber.ToString());
            return Task.FromResult(true);
        }
    }
}