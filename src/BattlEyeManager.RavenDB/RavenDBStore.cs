using BattlEyeManager.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.RavenDB
{
    public class RavenDBStore<T, TK> : IKeyValueStore<T, TK> where T : IEntity<TK>
    {
        public T Find(TK key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(TK key)
        {
            throw new NotImplementedException();
        }

        public T Add(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(TK key)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> FindAsync(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TK key)
        {
            throw new NotImplementedException();
        }

        public Task<T> AddAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}