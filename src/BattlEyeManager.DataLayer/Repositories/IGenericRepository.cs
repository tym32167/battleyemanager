using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public interface IGenericRepository<T, in TKey> where T : class
    {
        Task<T[]> GetItemsAsync();

        IQueryable<T> GetItems();

        Task<T> GetItemByIdAsync(TKey id);
        Task UpdateItemAsync(T item);
        Task AddItemAsync(T item);
        Task DeleteItemByIdAsync(TKey id);
    }
}