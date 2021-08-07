using BattlEyeManager.DataLayer.Context;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class AdminRepository : BaseRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {
        }
    }
}