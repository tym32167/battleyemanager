using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Services.State;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Services.Featues
{
    public class WelcomeFeature
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private HashSet<int> _enabledServers = new HashSet<int>();

        public WelcomeFeature(OnlinePlayerStateService playerStateService,
            ServerStateService serverStateService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _serverStateService = serverStateService;
            _serviceScopeFactory = serviceScopeFactory;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public void SetEnabled(int serverId, bool enabled)
        {
            if (enabled) _enabledServers.Add(serverId);
            else _enabledServers.Remove(serverId);
        }

        private async void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<BE.Models.Player>> e)
        {
            if (!_enabledServers.Contains(e.Server.Id)) return;
            var players = e.Data.ToArray();
            if (players.Length > 5) return;
            var serverId = e.Server.Id;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                {
                    foreach (var player in players)
                    {
                        var sessions = await ctx.PlayerSessions
                            .Where(x => x.EndDate != null && player.Guid == x.Player.GUID && x.ServerId == serverId)
                            .ToArrayAsync();

                        if (sessions.Length != 0)
                        {
                            int hours = (int)sessions.Select(s => ((s.EndDate ?? s.StartDate) - s.StartDate).TotalHours).Sum();
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, $"Welcome, {player.Name}! {sessions.Length} sessions and {hours} hours on server!");
                        }
                        else
                        {
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, $"Welcome, {player.Name}! First time on server!");
                        }
                    }
                }
            }
        }
    }
}