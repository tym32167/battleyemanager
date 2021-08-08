using BattlEyeManager.BE.Services;
using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                using (var repo = scope.ServiceProvider.GetService<ISessionRepository>())
                {
                    await repo.EndOpenedPlayerSessions();
                    await repo.EndOpenedAdminSessions();
                }
            }
        }

        public async Task RegisterChatMessage(BEServerEventArgs<BE.Models.ChatMessage> e)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<IChatRepository>())
                {
                    await repo.AddAsync(new ChatMessage
                    {
                        Date = e.Data.Date,
                        ServerId = e.Server.Id,
                        Text = e.Data.Message
                    });
                }
            }
        }

        private static readonly SemaphoreSlim SemaphoreSlimPlayers = new SemaphoreSlim(1, 1);

        public async Task UsersOnlineChangeRegisterJoined(BE.Models.Player[] joined, ServerInfo server)
        {
            await SemaphoreSlimPlayers.WaitAsync();

            try
            {
                joined = joined.GroupBy(x => x.Guid).Select(x => x.First()).ToArray();

                if (!joined.Any()) return;

                _logger.LogInformation($"Server {server.Id}:{server.Name} Register JOINED:{joined.Length}");

                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var repo = scope.ServiceProvider.GetService<IPlayerRepository>())
                    {
                        var now = DateTime.UtcNow;

                        await repo.RegisterJoinedPlayers(server.Id,
                                joined.Select(x => new Player()
                                {
                                    GUID = x.Guid,
                                    IP = x.IP,
                                    Name = x.Name,
                                    LastSeen = now
                                }).ToArray());

                        var players = await repo.GetPlayers(joined.Select(x => x.Guid).ToArray());

                        using (var sessionRepo = scope.ServiceProvider.GetService<ISessionRepository>())
                        {
                            await sessionRepo.CreateSessions(players.Values.Select(x => new PlayerSession()
                            {
                                IP = x.IP,
                                Name = x.Name,
                                PlayerId = x.Id,
                                StartDate = now,
                                ServerId = server.Id
                            }).ToArray());
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

            if (!leaved.Any()) return;

            _logger.LogInformation($"Server {server.Id}:{server.Name} Register LEAVED:{leaved.Length}");

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var repo = scope.ServiceProvider.GetService<ISessionRepository>())
                    {
                        await repo.EndPlayerSessions(leaved.Select(x => x.Guid).ToArray());
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
                    using (var repo = scope.ServiceProvider.GetService<ISessionRepository>())
                    {
                        await repo.CreateSessions(
                            joined.Select(x => new AdminSession
                            {
                                IP = x.IP,
                                Num = x.Num,
                                Port = x.Port,
                                ServerId = server.Id,
                                StartDate = DateTime.UtcNow
                            }).ToArray()
                        );

                        await repo.EndAdminSessions(
                            leaved.Select(x => new AdminSession
                            {
                                IP = x.IP,
                                Num = x.Num,
                                Port = x.Port,
                                ServerId = server.Id,
                                StartDate = DateTime.UtcNow
                            }).ToArray()
                        );
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
                    using (var repo = scope.ServiceProvider.GetService<IBanRepository>())
                    {
                        await repo.RegisterActualBans(server.Id, all.Select(b => new ServerBan
                        {
                            Date = DateTime.UtcNow,
                            GuidIp = b.GuidIp,
                            IsActive = true,
                            Minutes = b.Minutesleft,
                            MinutesLeft = b.Minutesleft,
                            Num = b.Num,
                            Reason = b.Reason,
                            ServerId = server.Id
                        }).ToArray());
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