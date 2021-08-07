using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task<Player[]> RegisterJoinedPlayers(Player[] joined);
        Task<int> PlayersTotalCount();
        Task<Player[]> GetPlayers(int skip, int take);
        Task ImportPlayers(Player[] request);
    }

}
