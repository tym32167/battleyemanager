using BattlEyeManager.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BattlEyeManager.RavenDB
{
    public class RavenDBStore<T, TK> : IKeyValueStore<T, TK> where T : IEntity<TK>
    {
        public T Find(TK key)
        {
            using (var session = CreateSession())
            {
                return session.Load<T>(key.ToString());
            }
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            using (var session = CreateSession())
            {
                return session.Query<T>().Where(predicate).ToArray().AsQueryable();
            }
        }

        public IQueryable<T> All()
        {
            using (var session = CreateSession())
            {
                return session.Query<T>();
            }
        }

        public void Update(T item)
        {
            using (var session = CreateSession())
            {
                session.Store(item, item.Id.ToString());
                session.SaveChanges();
            }
        }

        public void Delete(TK key)
        {
            using (var session = CreateSession())
            {
                session.Delete(key.ToString());
                session.SaveChanges();
            }
        }

        public T Add(T item)
        {
            using (var session = CreateSession())
            {
                session.Store(item, item.Id.ToString());
                session.SaveChanges();
                return Find(item.Id);
            }
        }

        public Task<IQueryable<T>> AllAsync()
        {
            return Task.FromResult(All());
        }

        public Task<T> FindAsync(TK key)
        {
            return Task.FromResult(Find(key));
        }

        public Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
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


        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                Urls = new[] { "http://192.168.0.17:8080", "http://192.168.0.10:8080", "http://192.168.0.14:8080" },
                Database = "BattleyeManager"
            };

            return store;
        }

        private static IDocumentSession CreateSession()
        {
            var store = CreateStore();



            store.Conventions.RegisterAsyncIdConvention<T>(
                (s, item) => Task.FromResult(item.Id.ToString()));

            store.Initialize();
            var session = store.OpenSession();

            //session.Advanced.DocumentStore.Conventions.RegisterAsyncIdConvention<T>(
            //    (s, item) => Task.FromResult(item.Id.ToString()));

            return session;
        }
    }
}