using BattlEyeManager.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BattlEyeManager.MongoDB
{
    public class MongoDBStore<T, TK> : IKeyValueStore<T, TK> where T : IEntity<TK> where TK : class
    {
        public T Find(TK key)
        {
            var coll = GetCollection();
            return coll.Find(u => u.Id == key).FirstOrDefault();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var coll = GetCollection();
            return coll.Find(predicate).ToList().AsQueryable();
        }

        public void Update(T item)
        {
            var coll = GetCollection();
            coll.ReplaceOne(u => u.Id == item.Id, item);
        }

        public void Delete(TK key)
        {
            var coll = GetCollection();
            coll.DeleteOne(u => u.Id == key);
        }

        public T Add(T item)
        {
            var coll = GetCollection();
            coll.InsertOne(item);
            return Find(item.Id);
        }

        public async Task<T> FindAsync(TK key)
        {
            var coll = GetCollection();
            var res = await coll.FindAsync(u => u.Id == key);
            return res.FirstOrDefault();
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var coll = GetCollection();
            var res = await coll.FindAsync(predicate);
            var list = await res.ToListAsync();
            return list.AsQueryable();
        }

        public Task UpdateAsync(T item)
        {
            var coll = GetCollection();
            return coll.ReplaceOneAsync(u => u.Id == item.Id, item);
        }

        public Task DeleteAsync(TK key)
        {
            var coll = GetCollection();
            return coll.DeleteOneAsync(u => u.Id == key);
        }

        public async Task<T> AddAsync(T item)
        {
            var coll = GetCollection();
            await coll.InsertOneAsync(item);
            return await FindAsync(item.Id);
        }

        private static IMongoDatabase GetDatabase()
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("BattleyeManager");
            return database;
        }

        private static IMongoCollection<TC> GetCollection<TC>()
        {
            return GetDatabase().GetCollection<TC>(typeof(TC).Name);
        }

        private static IMongoCollection<T> GetCollection()
        {
            return GetDatabase().GetCollection<T>(typeof(T).Name);
        }
    }


    public class MongoDBStoreGuid<T> : IKeyValueStore<T, Guid> where T : IEntity<Guid>
    {
        public T Find(Guid key)
        {
            var coll = GetCollection();
            return coll.Find(u => u.Id == key).FirstOrDefault();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var coll = GetCollection();
            return coll.Find(predicate).ToList().AsQueryable();
        }

        public void Update(T item)
        {
            var coll = GetCollection();
            coll.ReplaceOne(u => u.Id == item.Id, item);
        }

        public void Delete(Guid key)
        {
            var coll = GetCollection();
            coll.DeleteOne(u => u.Id == key);
        }

        public T Add(T item)
        {
            var coll = GetCollection();
            coll.InsertOne(item);
            return Find(item.Id);
        }

        public async Task<T> FindAsync(Guid key)
        {
            var coll = GetCollection();
            var res = await coll.FindAsync(u => u.Id == key);
            return res.FirstOrDefault();
        }

        public async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var coll = GetCollection();
            var res = await coll.FindAsync(predicate);
            var list = await res.ToListAsync();
            return list.AsQueryable();
        }

        public Task UpdateAsync(T item)
        {
            var coll = GetCollection();
            return coll.ReplaceOneAsync(u => u.Id == item.Id, item);
        }

        public Task DeleteAsync(Guid key)
        {
            var coll = GetCollection();
            return coll.DeleteOneAsync(u => u.Id == key);
        }

        public async Task<T> AddAsync(T item)
        {
            var coll = GetCollection();
            await coll.InsertOneAsync(item);
            return await FindAsync(item.Id);
        }

        private static IMongoDatabase GetDatabase()
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("BattleyeManager");
            return database;
        }

        private static IMongoCollection<TC> GetCollection<TC>()
        {
            return GetDatabase().GetCollection<TC>(typeof(TC).Name);
        }

        private static IMongoCollection<T> GetCollection()
        {
            return GetDatabase().GetCollection<T>(typeof(T).Name);
        }
    }
}