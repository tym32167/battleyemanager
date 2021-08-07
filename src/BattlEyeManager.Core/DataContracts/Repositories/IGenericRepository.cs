using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IGenericRepository<TItem, TKey> : IDisposable
    {
        Task<TItem[]> GetAll();
        Task<TItem> GetById(TKey key);
        Task Delete(TKey key);
        Task<TItem> Add(TItem item);
        Task<TItem> Update(TItem item);
    }

}
