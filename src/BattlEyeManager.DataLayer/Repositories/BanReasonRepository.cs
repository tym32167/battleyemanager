using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class BanReasonRepository : GenericRepository<BanReason, int>
    {
        public BanReasonRepository(AppDbContext context) : base(context)
        {
        }
    }
}