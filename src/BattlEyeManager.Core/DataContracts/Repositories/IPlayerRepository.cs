using BattlEyeManager.Core.DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task RegisterJoinedPlayers(int serverId, Player[] joined);
        Task<int> PlayersTotalCount();
        Task<Player[]> GetPlayers(int skip, int take);
        Task ImportPlayers(Player[] request);

        Task<Dictionary<string, Player>> GetPlayers(string[] guids);
        Task<Player> GetById(int playerId);
    }

}
