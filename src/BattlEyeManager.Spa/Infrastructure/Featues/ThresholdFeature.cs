using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Infrastructure.State;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Infrastructure.Featues
{
    public class ThresholdFeature
    {        
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<int, ServerInfoDto> _enabledServers = new Dictionary<int, ServerInfoDto>();

        private Dictionary<int, HashSet<string>> _welcomeFeatureBlackLists = new Dictionary<int, HashSet<string>>();        

        public ThresholdFeature(OnlinePlayerStateService playerStateService,
            ServerStateService serverStateService,
            IServiceScopeFactory serviceScopeFactory)
        {            
            _serverStateService = serverStateService;
            _serviceScopeFactory = serviceScopeFactory;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public void SetEnabled(ServerInfoDto server)
        {
            if (server.ThresholdFeatureEnabled)
            {
                _enabledServers[server.Id] = server;

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        var blackList =
                            ctx.WelcomeFeatureBlackList
                                .Where(x => x.ServerId == server.Id)
                                .Select(x => x.Guid)
                                .Distinct();

                        _welcomeFeatureBlackLists[server.Id] = new HashSet<string>(blackList);
                    }
                }
            }

            else
            {
                _enabledServers.Remove(server.Id);
                _welcomeFeatureBlackLists.Remove(server.Id);
            }
        }     
       
        private async void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<BE.Models.Player>> e)
        {
            if (!_enabledServers.ContainsKey(e.Server.Id)) return;

            var server = _enabledServers[e.Server.Id];
            var blackList = _welcomeFeatureBlackLists[server.Id];

            var players = e.Data.ToArray();            
            if (players.Length == 0) return;

            var serverId = e.Server.Id;

            var whitelistedPlayers = players.Where(p => !blackList.Contains(p.Guid)).ToArray();
            var blackListedPlayers = players.Where(p => blackList.Contains(p.Guid)).ToArray();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    var playerService = scope.ServiceProvider.GetService<OnlinePlayerService>();

                    foreach (var player in whitelistedPlayers)
                    {
                        var sessions = await ctx.PlayerSessions
                            .Where(x => x.EndDate != null && player.Guid == x.Player.GUID && x.ServerId == serverId)
                            .ToArrayAsync();

                        int hours = (int)sessions.Select(s => ((s.EndDate ?? s.StartDate) - s.StartDate).TotalHours).Sum();

                        if (hours < server.ThresholdMinHoursCap)
                        {
                            await playerService.KickAsync(server.Id, player.Num, player.Guid, server.ThresholdFeatureMessageTemplate, "bot");
                        }                        
                    }                    
                }
            }
        }
    }
}