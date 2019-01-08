using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class KickReasonRepository : GenericRepository<KickReason, int>
    {
        public KickReasonRepository(AppDbContext context) : base(context)
        {
        }
    }
}