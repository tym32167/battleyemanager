using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
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
    }
}
