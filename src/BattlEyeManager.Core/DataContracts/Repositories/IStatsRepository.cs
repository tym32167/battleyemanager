using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IStatsRepository : IRepository
    {
        Task PushServerUserCount(ServerUserCount[] records);
    }

}
