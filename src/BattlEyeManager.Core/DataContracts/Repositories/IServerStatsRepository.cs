using BattlEyeManager.Core.DataContracts.Models;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IServerStatsRepository : IRepository
    {
        Task<ServerStatsResult> GetServersStats(int[] serverIds, DateTime start, DateTime end, TimeSpan step);
    }
}
