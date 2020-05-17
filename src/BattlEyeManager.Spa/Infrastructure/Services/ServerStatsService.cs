using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Infrastructure.State;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Infrastructure.Services
{
    public class ServerStatsService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly OnlinePlayerStateService _playerStateService;
        private readonly ServerStateService _serverStateService;

        public ServerStatsService(IServiceScopeFactory serviceScopeFactory,
            OnlinePlayerStateService playerStateService,
            ServerStateService serverStateService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _playerStateService = playerStateService;
            _serverStateService = serverStateService;
        }


        private CancellationTokenSource _source;

        public async void Start()
        {
            Stop();
            _source = new CancellationTokenSource();


            while (!_source.IsCancellationRequested)
            {
                var waitTask = Task.Delay(TimeSpan.FromMinutes(10));
                await SaveStats();
                await waitTask;
            }

        }

        private async Task SaveStats()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var dc = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var date = DateTime.UtcNow;
                    var servers = _serverStateService.GetConnectedServers() ?? Enumerable.Empty<ServerInfo>();

                    var records = servers.Select(s => new ServerUserCount
                    {
                        ServerId = s.Id,
                        PlayersCount = _playerStateService.GetPlayersCount(s.Id),
                        Time = date
                    }).ToArray();

                    dc.ServerUserCounts.AddRange(records);
                    await dc.SaveChangesAsync();
                }
            }
        }

        public void Stop()
        {
            _source?.Cancel();
            _source = null;
        }
    }
}