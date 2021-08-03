using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public abstract class GenericRepository<TItem, TKey, TModel, TModelKey> : IGenericRepository<TItem, TKey> where TModel : class
    {
        private readonly AppDbContext _context;

        protected abstract TModel ToModel(TItem item);
        protected abstract TItem ToItem(TModel model);

        protected abstract TModelKey ToModelKey(TKey itemKey);
        protected abstract TKey ToItemKey(TModelKey modelKey);


        protected GenericRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<TItem[]> GetAll()
        {
            var ret = await _context.Set<TModel>()
                    .ToArrayAsync();
            return ret.Select(ToItem).ToArray();

        }

        public async Task<TItem> GetById(TKey id)
        {
            var ret = await _context.Set<TModel>().FindAsync(ToModelKey(id));
            return ToItem(ret);
        }

        public async Task<TItem> Update(TItem item)
        {
            var ret = _context.Set<TModel>().Update(ToModel(item));
            await _context.SaveChangesAsync();
            return ToItem(ret.Entity);
        }

        public async Task<TItem> Add(TItem item)
        {
            var ret = _context.Set<TModel>().Add(ToModel(item));
            await _context.SaveChangesAsync();
            return ToItem(ret.Entity);
        }

        public async Task Delete(TKey id)
        {
            var item = await _context.Set<TModel>().FindAsync(ToModelKey(id));
            _context.Set<TModel>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            _context?.Dispose();
        }
    }
}
