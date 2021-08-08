using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories.Players
{
    public class PlayersCache
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private static ConcurrentDictionary<string, PlayerDto> _guidCache = new ConcurrentDictionary<string, PlayerDto>();

        public PlayersCache(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Reload(params string[] guids)
        {
            if (guids == null || guids.Length == 0) return;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var dc = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var players = await dc.Players
                        .AsNoTracking()
                        .Where(p => guids.Contains(p.GUID))
                        .ToArrayAsync();
                    await Reload(players);
                }
            }
        }

        public Task Reload(params PlayerDto[] players)
        {
            foreach (var player in players)
            {
                var copy = PlayerDto.Copy(player);
                _guidCache.AddOrUpdate(copy.GUID, g => copy, (g, p) => copy);
            }
            return Task.FromResult(true);
        }

        public async Task Reload()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var dc = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var players = await dc.Players
                        .AsNoTracking()
                        .ToArrayAsync();
                    _guidCache.Clear();
                    await Reload(players);
                }
            }
        }

        public Task<Dictionary<string, Core.DataContracts.Models.Player>> GetPlayers(string[] playerIds)
        {
            var ret = new Dictionary<string, Core.DataContracts.Models.Player>();

            foreach (var id in playerIds)
            {
                if (_guidCache.TryGetValue(id, out PlayerDto p))
                {
                    ret[id] = ToItem(p);
                }
            }

            return Task.FromResult(ret);
        }

        private Core.DataContracts.Models.Player ToItem(PlayerDto model)
        {
            return new Core.DataContracts.Models.Player()
            {
                Id = model.Id,
                Comment = model.Comment,
                GUID = model.GUID,
                IP = model.IP,
                LastSeen = model.LastSeen,
                Name = model.Name,
                SteamId = model.SteamId
            };
        }

    }
}