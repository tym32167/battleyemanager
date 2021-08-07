using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class UtilRepository : BaseRepository, IUtilRepository
    {
        private readonly AppDbContext context;

        public UtilRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task InitStore()
        {
            await context.Database.MigrateAsync();
        }
    }
}
