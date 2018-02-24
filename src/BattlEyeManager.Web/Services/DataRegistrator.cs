using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Services
{
    public class DataRegistrator
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DataRegistrator> _logger;

        public DataRegistrator(IServiceScopeFactory scopeFactory, ILogger<DataRegistrator> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task Init()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var openedSessions = ctx.PlayerSessions.Where(x => x.EndDate == null).ToArray();

                    foreach (var session in openedSessions)
                    {
                        session.EndDate = session.StartDate;
                    }

                    await ctx.SaveChangesAsync();
                }
            }
        }

        public async Task RegisterChatMessage(BEServerEventArgs<BE.Models.ChatMessage> e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {

                    await ctx.ChatMessages.AddAsync(
                        new DataLayer.Models.ChatMessage
                        {
                            Date = e.Data.Date,
                            ServerId = e.Server.Id,
                            Text = e.Data.Message
                        });

                    await ctx.SaveChangesAsync();
                }
            }
        }

        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);


        public async Task UsersOnlineChangeRegister(BE.Models.Player[] joined, BE.Models.Player[] leaved, ServerInfo server)
        {
            await SemaphoreSlim.WaitAsync();

            joined = joined.GroupBy(x => x.Guid).Select(x => x.First()).ToArray();
            leaved = leaved.GroupBy(x => x.Guid).Select(x => x.First()).ToArray();

            _logger.LogInformation($"Register JOINED:{joined.Length}, LEAVED:{leaved.Length}");

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        if (joined.Any())
                        {

                            var guidIds = joined.Select(x => x.Guid).ToArray();
                            var dbPlayers = await ctx.Players.Where(x => guidIds.Contains(x.GUID)).ToListAsync();
                            var pdict = joined.ToDictionary(x => x.Guid);

                            var newPlayers = joined.Where(p => dbPlayers.All(z => z.GUID != p.Guid))
                                .Select(p => new Player
                                {
                                    GUID = p.Guid,
                                    IP = p.IP,
                                    LastSeen = DateTime.UtcNow,
                                    Name = p.Name,
                                })
                                .ToArray();

                            await ctx.Players.AddRangeAsync(newPlayers);


                            foreach (var dbPlayer in dbPlayers)
                            {
                                if (pdict.ContainsKey(dbPlayer.GUID))
                                {
                                    var p = pdict[dbPlayer.GUID];
                                    dbPlayer.Name = p.Name;
                                    dbPlayer.IP = p.IP;
                                    dbPlayer.LastSeen = DateTime.UtcNow;
                                }
                            }

                            await ctx.SaveChangesAsync();

                            var sessions = new List<PlayerSession>();

                            foreach (var p in newPlayers)
                            {
                                sessions.Add(new PlayerSession
                                {
                                    IP = p.IP,
                                    Name = p.Name,
                                    PlayerId = p.Id,
                                    ServerId = server.Id,
                                    StartDate = DateTime.UtcNow
                                });
                            }

                            foreach (var p in dbPlayers)
                            {
                                sessions.Add(new PlayerSession
                                {
                                    IP = p.IP,
                                    Name = p.Name,
                                    PlayerId = p.Id,
                                    ServerId = server.Id,
                                    StartDate = DateTime.UtcNow
                                });
                            }

                            await ctx.PlayerSessions.AddRangeAsync(sessions);

                            await ctx.SaveChangesAsync();
                        }

                        if (leaved.Any())
                        {

                            var leavingPlayersIds = leaved.Select(x => x.Guid).ToArray();

                            var threeDaysAgo = DateTime.UtcNow.AddDays(-3);

                            var sessionsToClose =
                                ctx.PlayerSessions
                                    .Where(x => x.EndDate == null && leavingPlayersIds.Contains(x.Player.GUID) && x.StartDate > threeDaysAgo)
                                    .ToArray();

                            foreach (var session in sessionsToClose)
                            {
                                session.EndDate = DateTime.UtcNow;
                            }

                            await ctx.SaveChangesAsync();
                        }
                    }
                }
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
    }
}