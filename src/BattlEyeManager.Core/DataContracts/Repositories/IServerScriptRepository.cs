using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IServerScriptRepository : IGenericRepository<ServerScript, int>
    {
        Task<ServerScript[]> GetByServerAsync(int serverId);
    }
}
