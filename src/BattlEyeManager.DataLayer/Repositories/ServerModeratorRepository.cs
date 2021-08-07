using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerModeratorRepository : BaseRepository, IServerModeratorRepository
    {
        private readonly AppDbContext _context;

        public ServerModeratorRepository(AppDbContext context) : base(context)
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

        private ServerModerator ModelToItem(Models.ServerModerators model)
        {
            return new ServerModerator()
            {
                Id = model.Id,
                ServerId = model.ServerId,
                UserId = model.UserId
            };
        }

        public async Task<ServerModerator[]> GetServerModerators()
        {
            return await _context.ServerModerators.Select(x => ModelToItem(x)).ToArrayAsync();
        }

        public async Task UpdateServerModeratorForUser(string userId, HashSet<int> update)
        {
            var data = await _context.ServerModerators.Where(x => x.UserId == userId).ToListAsync();
            var actual = new HashSet<int>(data.Select(d => d.ServerId));

            _context.ServerModerators.RemoveRange(data.Where(d => !update.Contains(d.ServerId)));

            _context.ServerModerators.AddRange(
                update.Where(u => !actual.Contains(u))
                    .Select(x => new Models.ServerModerators()
                    {
                        ServerId = x,
                        UserId = userId
                    }));

            await _context.SaveChangesAsync();
        }
    }
}