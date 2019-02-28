﻿using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Infrastructure.Featues
{
    public class WelcomeFeature
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<int, ServerInfoDto> _enabledServers = new Dictionary<int, ServerInfoDto>();

        private static string WelcomeMessageTemplate = "Welcome, {name}! {sessions} sessions and {hours} hours on server!";
        private static string WelcomeEmptyMessageTemplate = "Welcome, {name}! First time on server!";

        private static string GetMessageString(string template, string name, int sessions, int hours)
        {
            var templater = new StringTemplater();
            templater.AddParameter("name", name);
            templater.AddParameter("sessions", sessions.ToString());
            templater.AddParameter("hours", hours.ToString());

            return templater.Template(template);
        }

        public WelcomeFeature(OnlinePlayerStateService playerStateService,
            ServerStateService serverStateService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _serverStateService = serverStateService;
            _serviceScopeFactory = serviceScopeFactory;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public void SetEnabled(ServerInfoDto server)
        {
            if (server.WelcomeFeatureEnabled) _enabledServers[server.Id] = server;
            else _enabledServers.Remove(server.Id);
        }

        private async void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<BE.Models.Player>> e)
        {
            if (!_enabledServers.ContainsKey(e.Server.Id)) return;

            var server = _enabledServers[e.Server.Id];

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
                            var message = GetMessageString(server.WelcomeFeatureTemplate.DefaultIfNullOrEmpty(WelcomeMessageTemplate), player.Name, sessions.Length, hours);
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, message);
                        }
                        else
                        {
                            var message = GetMessageString(server.WelcomeFeatureEmptyTemplate.DefaultIfNullOrEmpty(WelcomeEmptyMessageTemplate), player.Name, sessions.Length, 0);
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, message);
                        }
                    }
                }
            }
        }
    }
}