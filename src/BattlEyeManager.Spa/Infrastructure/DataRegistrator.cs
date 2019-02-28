using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BattlEyeManager.Spa.Infrastructure
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

                    foreach (var session in ctx.Admins.Where(x => x.EndDate == null).ToArray())
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

        private static readonly SemaphoreSlim SemaphoreSlimPlayers = new SemaphoreSlim(1, 1);

        public async Task UsersOnlineChangeRegisterJoined(BE.Models.Player[] joined, ServerInfo server)
        {
            await SemaphoreSlimPlayers.WaitAsync();

            joined = joined.GroupBy(x => x.Guid).Select(x => x.First()).ToArray();

            _logger.LogInformation($"Register JOINED:{joined.Length}");

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
                    }
                }
            }
            finally
            {
                SemaphoreSlimPlayers.Release();
            }
        }

        public async Task UsersOnlineChangeRegisterLeaved(BE.Models.Player[] leaved, ServerInfo server)
        {
            await SemaphoreSlimPlayers.WaitAsync();

            leaved = leaved.GroupBy(x => x.Guid).Select(x => x.First()).ToArray();

            _logger.LogInformation($"Register LEAVED:{leaved.Length}");

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        if (leaved.Any())
                        {

                            var leavingPlayersIds = leaved.Select(x => x.Guid).ToArray();

                            var threeDaysAgo = DateTime.UtcNow.AddDays(-3);

                            var sessionsToClose =
                                await ctx.PlayerSessions
                                    .Where(x => x.EndDate == null && leavingPlayersIds.Contains(x.Player.GUID) &&
                                                x.StartDate > threeDaysAgo)
                                    .ToListAsync();

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
                SemaphoreSlimPlayers.Release();
            }
        }


        private static readonly SemaphoreSlim SemaphoreSlimAdmins = new SemaphoreSlim(1, 1);

        public async Task AdminsOnlineChangeRegister(BE.Models.Admin[] joined, BE.Models.Admin[] leaved, ServerInfo server)
        {
            await SemaphoreSlimAdmins.WaitAsync();

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        await ctx.Admins.AddRangeAsync(
                            joined.Select(x => new Admin
                            {
                                IP = x.IP,
                                Num = x.Num,
                                Port = x.Port,
                                ServerId = server.Id,
                                StartDate = DateTime.UtcNow
                            })
                        );

                        var leavedHosts = leaved.Select(x => x.IP).ToArray();

                        foreach (var adm in await ctx.Admins.Where(a => a.EndDate == null && a.ServerId == server.Id && leavedHosts.Contains(a.IP)).ToListAsync())
                        {
                            if (leaved.Any(a => a.IP == adm.IP && a.Port == adm.Port && a.Num == adm.Num))
                            {
                                adm.EndDate = DateTime.UtcNow;
                            }
                        }

                        await ctx.SaveChangesAsync();
                    }
                }
            }
            finally
            {
                SemaphoreSlimAdmins.Release();
            }
        }

        private static readonly SemaphoreSlim SemaphoreSlimBans = new SemaphoreSlim(1, 1);

        public async Task BansOnlineChangeRegister(BE.Models.Ban[] all, ServerInfo server)
        {
            await SemaphoreSlimBans.WaitAsync();

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        var dbBans = await ctx.ServerBans.Where(x => x.IsActive && x.ServerId == server.Id)
                            .ToListAsync();


                        foreach (var serverBan in dbBans.Where(b => !all.Any(r => r.GuidIp == b.GuidIp && r.Reason == b.Reason && r.Num == b.Num)))
                        {
                            serverBan.IsActive = false;
                            serverBan.CloseDate = DateTime.UtcNow;
                        }


                        var toAdd = all.Where(b => !dbBans.Any(r => r.GuidIp == b.GuidIp && r.Reason == b.Reason && r.Num == b.Num))
                            .Select(b => new ServerBan
                            {
                                Date = DateTime.UtcNow,
                                GuidIp = b.GuidIp,
                                IsActive = true,
                                Minutes = b.Minutesleft,
                                MinutesLeft = b.Minutesleft,
                                Num = b.Num,
                                Reason = b.Reason,
                                ServerId = server.Id
                            });

                        await ctx.ServerBans.AddRangeAsync(toAdd);
                        await ctx.SaveChangesAsync();
                    }
                }
            }
            finally
            {
                SemaphoreSlimBans.Release();
            }
        }
    }
}