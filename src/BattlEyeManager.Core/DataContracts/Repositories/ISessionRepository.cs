using BattlEyeManager.Core.DataContracts.Models;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface ISessionRepository : IRepository
    {
        Task EndOpenedPlayerSessions();
        Task EndOpenedAdminSessions();
        Task CreateSessions(PlayerSession[] playerSessions);
        Task CreateSessions(AdminSession[] adminSessions);

        Task EndPlayerSessions(string[] guids);
        Task EndAdminSessions(AdminSession[] adminSessions);
        Task<PlayerSession[]> GetPlayerSessions(int serverId, DateTime startSearch, DateTime endSearch, int skip, int take);
        Task<PlayerSession[]> GetComletedPlayerSessions(int serverId, string playerGuid);
        Task<int> GetPlayerSessionsCount(int serverId);
    }

}
