using AutoMapper;
using BattlEyeManager.BE.Models;
using BattlEyeManager.Spa.Model;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Services
{
    public class OnlinePlayerService
    {
        private readonly ServerStateService _serverStateService;

        public OnlinePlayerService(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        public async Task<OnlinePlayerModel[]> GetOnlinePlayers(int serverId)
        {
            var players = _serverStateService.GetPlayers(serverId);
            var ret =
                players.Select(Mapper.Map<Player, OnlinePlayerModel>)
                    .ToArray();
            return ret;
        }
    }
}

