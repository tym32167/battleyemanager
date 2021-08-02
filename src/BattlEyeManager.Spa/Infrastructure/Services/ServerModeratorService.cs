using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class ServerModeratorService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConcurrentDictionary<string, HashSet<int>> _priviledges = new ConcurrentDictionary<string, HashSet<int>>();

        public ServerModeratorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task Init()
        {
            _priviledges.Clear();
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IServerModeratorRepository>())
                {
                    var data = await repo.GetServerModerators();

                    foreach (var group in data.GroupBy(x => x.UserId))
                    {
                        var set = new HashSet<int>(group.Select(x => x.ServerId));
                        _priviledges.AddOrUpdate(group.Key, key => set, (key, value) => set);
                    }
                }
            }
        }

        public HashSet<int> GetServersByUserId(string userId)
        {
            if (_priviledges.TryGetValue(userId, out var v))
                return new HashSet<int>(v);
            return new HashSet<int>();
        }

        public HashSet<int> GetServersByUser(ClaimsPrincipal user)
        {
            var userId = user.GetUserId();

            if (_priviledges.TryGetValue(userId, out var v))
                return new HashSet<int>(v);
            return new HashSet<int>();
        }

        public async Task SetByUserId(string userId, HashSet<int> update)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IServerModeratorRepository>())
                {

                    await repo.UpdateServerModeratorForUser(userId, update);

                    _priviledges.AddOrUpdate(userId, key => new HashSet<int>(update),
                        (key, value) => new HashSet<int>(update));
                }
            }
        }

        public void CheckAccess(ClaimsPrincipal user, int serverId)
        {
            if (user.IsAdmin()) return;

            var userId = user.GetUserId();

            var ret = false;

            if (_priviledges.TryGetValue(userId, out var v))
                ret = v.Contains(serverId);

            if (!ret)
                throw new AuthenticationException();
        }
    }
}