using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerModeratorRepository : IDisposable
    {
        private readonly AppDbContext _context;

        public ServerModeratorRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<ServerInfoDto>> GetByUserId(string userId)
        {
            var list = await _context.ServerModerators.Where(x => x.UserId == userId)
                .Select(x => x.Server)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return list.Cast<ServerInfoDto>().ToList();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}