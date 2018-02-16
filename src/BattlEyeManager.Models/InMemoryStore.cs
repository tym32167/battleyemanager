using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Models
{
    public class InMemoryStore<T, TK> : IKeyValueStore<T, TK> where T : IEntity<TK>
    {
        private readonly ConcurrentDictionary<TK, T> _values = new ConcurrentDictionary<TK, T>();

        public T Find(TK key)
        {
            _values.TryGetValue(key, out var item);
            return item;
        }

        public IQueryable<T> Find(Func<T, bool> predicate)
        {
            var res = _values.Values.Where(predicate);
            return res.AsQueryable();
        }

        public void Update(T item)
        {
            var old = Find(item.Id);
            _values.TryUpdate(item.Id, item, old);
        }

        public void Delete(TK key)
        {
            _values.TryRemove(key, out _);
        }

        public T Add(T item)
        {
            if (!_values.TryAdd(item.Id, item)) return default(T);
            return item;
        }

        public Task<T> FindAsync(TK key)
        {
            return Task.FromResult(Find(key));
        }

        public Task<IQueryable<T>> FindAsync(Func<T, bool> predicate)
        {
            return Task.FromResult(Find(predicate));
        }

        public Task UpdateAsync(T item)
        {
            Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TK key)
        {
            Delete(key);
            return Task.CompletedTask;
        }

        public Task<T> AddAsync(T item)
        {
            return Task.FromResult(Add(item));
        }
    }
}