using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Api.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task Import(PlayerSyncDto[] requestPlayers)
        {
            var impoerData = requestPlayers
                .Where(x => x.GUID?.Length == 32)
                .GroupBy(x => x.GUID)
                .Select(x => x.First())
                .ToDictionary(x => x.GUID);

            var ids = impoerData.Keys.ToArray();

            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var dbItems = await ctx.Players.Where(x => ids.Contains(x.GUID)).ToArrayAsync();

                    var actualData = dbItems.GroupBy(x => x.GUID)
                        .Select(x => x.First())
                        .ToDictionary(x => x.GUID);

                    foreach (var player in impoerData.Values)
                    {
                        if (actualData.ContainsKey(player.GUID))
                        {
                            var actual = actualData[player.GUID];
                            if (string.IsNullOrEmpty(actual.SteamId) && !string.IsNullOrEmpty(player.SteamId)) actual.SteamId = player.SteamId;
                            if (string.IsNullOrEmpty(actual.Comment) && !string.IsNullOrEmpty(player.Comment)) actual.Comment = player.Comment;
                        }

                        else
                        {
                            var dto = new Player();
                            dto.Name = player.Name;
                            dto.Comment = player.Comment;
                            dto.LastSeen = new DateTime(player.LastSeen.Ticks, DateTimeKind.Utc);
                            dto.SteamId = player.SteamId;
                            dto.GUID = player.GUID;
                            dto.IP = player.IP;
                            ctx.Players.Add(dto);
                        }
                    }

                    await ctx.SaveChangesAsync();
                }
            }
        }
    }
}