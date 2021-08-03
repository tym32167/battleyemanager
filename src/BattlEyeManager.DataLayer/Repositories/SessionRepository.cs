using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class SessionRepository : DisposeObject, ISessionRepository
    {
        private readonly AppDbContext context;

        public SessionRepository(AppDbContext context)
        {
            this.context = context;
        }


        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            context.Dispose();
        }

        public async Task EndOpenedPlayerSessions()
        {
            var opened = await context.PlayerSessions.Where(x => x.EndDate == null).ToArrayAsync();

            foreach (var session in opened)
                session.EndDate = session.StartDate;

            await context.SaveChangesAsync();
        }

        public async Task EndOpenedAdminSessions()
        {
            var opened = await context.Admins.Where(x => x.EndDate == null).ToArrayAsync();

            foreach (var session in opened)
                session.EndDate = session.StartDate;

            await context.SaveChangesAsync();
        }

        public Task CreateSessions(Core.DataContracts.Models.PlayerSession[] playerSessions)
        {
            throw new NotImplementedException();
        }

        public Task CreateSessions(Core.DataContracts.Models.AdminSession[] adminSessions)
        {
            throw new NotImplementedException();
        }

        public Task EndPlayerSessions(string[] guids)
        {
            throw new NotImplementedException();
        }

        public Task EndAdminSessions(Core.DataContracts.Models.AdminSession[] adminSessions)
        {
            throw new NotImplementedException();
        }

        public Task<Core.DataContracts.Models.PlayerSession[]> GetPlayerSessions(int serverId, DateTime startSearch, DateTime endSearch, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<Core.DataContracts.Models.PlayerSession[]> GetComletedPlayerSessions(int serverId, string playerGuid)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPlayerSessionsCount(int serverId)
        {
            throw new NotImplementedException();
        }
    }
}
