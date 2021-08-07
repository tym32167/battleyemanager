using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class StatsRepository : BaseRepository, IStatsRepository
    {
        public StatsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task PushServerUserCount(ServerUserCount[] records)
        {
            // throw new NotImplementedException();
        }
    }
}
