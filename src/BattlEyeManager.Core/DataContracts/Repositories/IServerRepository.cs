using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IServerRepository : IGenericRepository<Server, int>
    {
        Task<Server[]> GetActiveServers();
    }

}
