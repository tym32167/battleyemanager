using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateUserDisplayName(string userId, string displayName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.DisplayName = displayName;
                await _context.SaveChangesAsync();
            }
        }
    }
}