using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BattlEyeManager.Models
{
    public interface IKeyValueStore<T, in TK> where T : IEntity<TK>
    {
        T Find(TK key);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Update(T item);
        void Delete(TK key);
        T Add(T item);


        Task<T> FindAsync(TK key);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(T item);
        Task DeleteAsync(TK key);
        Task<T> AddAsync(T item);
    }
}