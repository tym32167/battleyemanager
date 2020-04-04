using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Api.Sync;
using BattlEyeManager.Spa.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class PlayerSyncService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PlayerSyncService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<int> GetPlayersCount()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    return await ctx.Players.CountAsync();
                }
            }
        }

        public async Task<PlayerSyncDto[]> GetPlayers(int offset, int count)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var dbItems = await ctx.Players
                        
                        .OrderBy(x => x.Id)
                        .Skip(offset)
                        .Take(count)
                        .ToArrayAsync();

                    var items = dbItems
                        .Select(x => Mapper.Map<PlayerSyncDto>(x))
                        .ToArray();

                    return items;
                }
            }
        }
    }
}