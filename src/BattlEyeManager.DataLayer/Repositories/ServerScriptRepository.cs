using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerScriptRepository
    {
        private readonly AppDbContext _context;

        public ServerScriptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServerScript[]> GetByServerAsync(int serverId)
        {
            return await _context.ServerScripts.Where(s => s.ServerId == serverId).ToArrayAsync();
        }

        public async Task<ServerScript> GetByIdAsync(int id)
        {
            return await _context.ServerScripts.FindAsync(id);
        }

        public async Task UpdateAsync(ServerScript item)
        {
            _context.ServerScripts.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync(ServerScript item)
        {
            _context.ServerScripts.Attach(item);
            _context.Entry(item).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.ServerScripts.FindAsync(id);
            _context.ServerScripts.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}