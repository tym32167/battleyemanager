using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IBanRepository : IRepository
    {
        Task RegisterActualBans(int serverId, ServerBan[] actualBans);
    }

}
