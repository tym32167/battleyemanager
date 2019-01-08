using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly AppDbContext _context;

        protected GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetItems()
        {
            return _context.Set<T>();
        }

        public Task<T[]> GetItemsAsync()
        {
            return _context.Set<T>().ToArrayAsync();
        }

        public Task<T> GetItemByIdAsync(TKey id)
        {
            return _context.Set<T>().FindAsync(id);
        }

        public Task UpdateItemAsync(T item)
        {
            _context.Set<T>().Update(item);
            return _context.SaveChangesAsync();
        }

        public Task AddItemAsync(T item)
        {
            _context.Set<T>().Add(item);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteItemByIdAsync(TKey id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}