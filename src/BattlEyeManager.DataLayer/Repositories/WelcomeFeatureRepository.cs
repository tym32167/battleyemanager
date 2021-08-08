using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class WelcomeFeatureRepository : BaseRepository, IWelcomeFeatureRepository
    {
        private readonly AppDbContext context;

        public WelcomeFeatureRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<string[]> GetFeatureBlackList(int serverId)
        {
            return await context.WelcomeFeatureBlackList.Where(x => x.ServerId == serverId).Select(x => x.Guid).ToArrayAsync();
        }

        private static WelcomeServerSettings ToItem(Models.Server server)
        {
            return new WelcomeServerSettings()
            {
                ServerId = server.Id,
                WelcomeFeatureEnabled = server.WelcomeFeatureEnabled,
                WelcomeFeatureEmptyTemplate = server.WelcomeFeatureEmptyTemplate,
                WelcomeFeatureTemplate = server.WelcomeFeatureTemplate,
                WelcomeGreater50MessageTemplate = server.WelcomeGreater50MessageTemplate
            };
        }

        public async Task<WelcomeServerSettings[]> GetWelcomeServerSettings()
        {
            return await context.Servers.Select(x => ToItem(x)).ToArrayAsync();
        }
    }
}
