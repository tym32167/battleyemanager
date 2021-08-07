using BattlEyeManager.Core.DataContracts.Models;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IRepository : IDisposable
    {
    }

    public interface IServerScriptRepository : IGenericRepository<ServerScript, int>
    {
        Task<ServerScript[]> GetByServerAsync(int serverId);
    }
    public interface IServerStatsRepository : IRepository
    {
        Task<ServerStatsResult> GetServersStats(int[] serverIds, DateTime start, DateTime end, TimeSpan step);
    }

    public interface IUserRepository : IRepository
    {
        Task UpdateUserDisplayName(string userId, string displayName);
    }
}
