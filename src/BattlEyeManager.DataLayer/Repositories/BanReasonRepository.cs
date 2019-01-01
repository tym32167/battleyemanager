using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public interface IGenericRepository<T, in TKey> where T : class
    {
        Task<T[]> GetItemsAsync();
        Task<T> GetItemByIdAsync(TKey id);
        Task UpdateItemAsync(T item);
        Task AddItemAsync(T item);
        Task DeleteItemByIdAsync(TKey id);
    }

    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly AppDbContext _context;

        protected GenericRepository(AppDbContext context)
        {
            _context = context;
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

    public class BanReasonRepository : GenericRepository<BanReason, int>
    {
        public BanReasonRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class KickReasonRepository : GenericRepository<KickReason, int>
    {
        public KickReasonRepository(AppDbContext context) : base(context)
        {
        }
    }
}