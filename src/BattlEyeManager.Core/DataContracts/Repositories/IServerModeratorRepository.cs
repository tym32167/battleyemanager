using BattlEyeManager.Core.DataContracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IServerModeratorRepository : IRepository
    {
        Task<ServerModerator[]> GetServerModerators();
        Task UpdateServerModeratorForUser(string userId, HashSet<int> update);
    }

}
