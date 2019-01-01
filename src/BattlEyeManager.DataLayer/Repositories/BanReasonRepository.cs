using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Services;
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
        private readonly IFactory<AppDbContext> _contextFactory;

        protected GenericRepository(IFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task<T[]> GetItemsAsync()
        {
            using (var dc = _contextFactory.GetService())
            {
                return dc.Set<T>().ToArrayAsync();
            }
        }

        public Task<T> GetItemByIdAsync(TKey id)
        {
            using (var dc = _contextFactory.GetService())
            {
                return dc.Set<T>().FindAsync(id);
            }
        }

        public Task UpdateItemAsync(T item)
        {
            using (var dc = _contextFactory.GetService())
            {
                dc.Set<T>().Update(item);
                return dc.SaveChangesAsync();
            }
        }

        public Task AddItemAsync(T item)
        {
            using (var dc = _contextFactory.GetService())
            {
                dc.Set<T>().Add(item);
                return dc.SaveChangesAsync();
            }
        }

        public async Task DeleteItemByIdAsync(TKey id)
        {
            using (var dc = _contextFactory.GetService())
            {
                var item = await dc.Set<T>().FindAsync(id);
                dc.Set<T>().Remove(item);
                await dc.SaveChangesAsync();
            }
        }
    }

    public interface IBanReasonRepository : IGenericRepository<BanReason, int>
    {
    }

    public class BanReasonRepository : GenericRepository<BanReason, int>, IBanReasonRepository
    {
        public BanReasonRepository(IFactory<AppDbContext> contextFactory) : base(contextFactory)
        {
        }
    }


    public interface IKickReasonRepository : IGenericRepository<KickReason, int>
    {
    }

    public class KickReasonRepository : GenericRepository<KickReason, int>, IKickReasonRepository
    {
        public KickReasonRepository(IFactory<AppDbContext> contextFactory) : base(contextFactory)
        {
        }
    }
}