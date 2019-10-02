using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class ServerModeratorService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private ConcurrentDictionary<string, HashSet<int>> _privilidges = new ConcurrentDictionary<string, HashSet<int>>();

        public ServerModeratorService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        public async Task Init()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dc = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var data = await dc.ServerModerators.ToArrayAsync();

                    foreach (var group in data.GroupBy(x => x.UserId))
                    {
                        var set = new HashSet<int>(group.Select(x => x.ServerId));
                        _privilidges.AddOrUpdate(group.Key, key => set, (key, value) => set);
                    }
                }
            }
        }

        public HashSet<int> GetServersByUserId(string userId)
        {
            if (_privilidges.TryGetValue(userId, out var v))
                return new HashSet<int>(v);
            return new HashSet<int>();
        }

        public async Task SetByUserId(string userId, HashSet<int> update)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var dc = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var data = await dc.ServerModerators.Where(x => x.UserId == userId).ToListAsync();
                    var actual = new HashSet<int>(data.Select(d => d.ServerId));
                    dc.ServerModerators.RemoveRange(data.Where(d => !update.Contains(d.ServerId)));

                    dc.ServerModerators.AddRange(
                        update.Where(u => !actual.Contains(u))
                            .Select(x => new ServerModerators()
                            {
                                ServerId = x,
                                UserId = userId
                            }));

                    await dc.SaveChangesAsync();
                    _privilidges.AddOrUpdate(userId, key => new HashSet<int>(update),
                        (key, value) => new HashSet<int>(update));
                }
            }
        }

        public bool CheckAccess(ClaimsPrincipal user, string userid, int serverId)
        {
            var isAdmin = ((ClaimsIdentity)user.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Any(x => String.Compare(RoleConstants.Administrator, x.Value, StringComparison.Ordinal) == 0);

            if (isAdmin) return true;

            if (_privilidges.TryGetValue(userid, out var v))
                return v.Contains(serverId);

            return false;
        }
    }
}