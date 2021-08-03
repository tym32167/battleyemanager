using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Models.Values;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IRepository : IDisposable
    {
    }

    public interface IGenericRepository<TItem, TKey> : IDisposable
    {
        Task<TItem[]> GetAll();
        Task<TItem> GetById(TKey key);
        Task Delete(TKey key);
        Task<TItem> Add(TItem item);
        Task<TItem> Update(TItem item);
    }

    public interface IBanReasonRepository : IGenericRepository<BanReason, int>
    {

    }

    public interface IKickReasonRepository : IGenericRepository<KickReason, int>
    {

    }

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


    public interface IPlayerRepository : IRepository
    {
        Task<Player[]> RegisterJoinedPlayers(Player[] joined);
        Task<int> PlayersTotalCount();
        Task<Player[]> GetPlayers(int skip, int take);
        Task ImportPlayers(Player[] request);
    }

    public interface IBanRepository : IRepository
    {
        Task RegisterActualBans(int serverId, ServerBan[] actualBans);
    }

    public interface IPlayerNoteRepository : IRepository
    {

    }


    public interface IChatRepository : IRepository
    {
        Task AddAsync(ChatMessage chatMessage);
        Task<ChatMessage[]> GetLastMessages(int serverId, int count);
    }

    public interface IStatsRepository : IRepository
    {
        Task PushServerUserCount(ServerUserCount[] records);
    }


    public interface IServerRepository : IGenericRepository<Server, int>
    {
        Task<Server[]> GetActiveServers();
    }

    public interface IServerModeratorRepository : IRepository
    {
        Task<ServerModerator[]> GetServerModerators();
        Task UpdateServerModeratorForUser(string userId, HashSet<int> update);
    }

    public interface IUtilRepository : IRepository
    {
        Task InitStore();
    }

}
