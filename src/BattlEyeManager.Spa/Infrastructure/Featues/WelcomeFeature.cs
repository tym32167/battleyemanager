using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Player = BattlEyeManager.BE.Models.Player;

namespace BattlEyeManager.Spa.Infrastructure.Featues
{
    public class WelcomeFeature
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<WelcomeFeature> logger;
        private Dictionary<int, ServerInfoDto> _enabledServers = new Dictionary<int, ServerInfoDto>();

        private Dictionary<int, HashSet<string>> _welcomeFeatureBlackLists = new Dictionary<int, HashSet<string>>();

        private static string WelcomeGreater300MessageTemplate = "Welcome, {name}! More than 300 hours on server!";
        private static string WelcomeMessageTemplate = "Welcome, {name}! {sessions} sessions and {hours} hours on server!";
        private static string WelcomeEmptyMessageTemplate = "Welcome, {name}! First time on server!";

        private static string WelcomeNewNicknameTemplate = "Внимание! Игрок {newName} сменил ник. Его прошлый ник {oldName}!";

        private static string GetMessageString(string template, string name, int sessions, decimal points)
        {
            var templater = new StringTemplater();
            templater.AddParameter("name", name);           

            templater.AddParameter("sessions", sessions.ToString());
            templater.AddParameter("hours", ((int)points).ToString());

            templater.AddParameter("points", ((int)points).ToString());

            return templater.Template(template);
        }

        private static string GetNewNameMessageString(string template, string newName, string oldName)
        {
            var templater = new StringTemplater();
            templater.AddParameter("newName", newName);
            templater.AddParameter("oldName", oldName);

            return templater.Template(template);
        }

        public WelcomeFeature(OnlinePlayerStateService playerStateService,
            ServerStateService serverStateService,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<WelcomeFeature> logger)
        {
            _serverStateService = serverStateService;
            _serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public void SetEnabled(ServerInfoDto server)
        {
            if (server.WelcomeFeatureEnabled)
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

        private string GetWelcomeMessage(ServerInfoDto server, Player player, DataLayer.Models.Player playerDb, int sessionsCount, decimal points)
        {
            if (sessionsCount != 0)
            {
                if (points > 300)
                {
                    var message = GetMessageString(server.WelcomeGreater50MessageTemplate.DefaultIfNullOrEmpty(WelcomeGreater300MessageTemplate), player.Name, sessionsCount, points);
                    return message;
                }
                else
                {
                    var message = GetMessageString(server.WelcomeFeatureTemplate.DefaultIfNullOrEmpty(WelcomeMessageTemplate), player.Name, sessionsCount, points);
                    return message;
                }
            }
            else
            {
                var message = GetMessageString(server.WelcomeFeatureEmptyTemplate.DefaultIfNullOrEmpty(WelcomeEmptyMessageTemplate), player.Name, sessionsCount, 0);
                return message;
            }
        }

        private string GetNewNickameMessage(ServerInfoDto server, Player player, PlayerSession prevSession)
        {
            var newName = player.Name;
            var oldName = prevSession?.Name;

            if (!string.IsNullOrEmpty(oldName) &&
                string.Compare(newName, oldName, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                var message = GetNewNameMessageString(WelcomeNewNicknameTemplate, newName, oldName);
                return message;
            }

            return null;
        }

        private async void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<BE.Models.Player>> e)
        {
            if (!_enabledServers.ContainsKey(e.Server.Id)) return;

            var server = _enabledServers[e.Server.Id];
            var blackList = _welcomeFeatureBlackLists[server.Id];

            var players = e.Data.ToArray();
            if (players.Length > 5) return;
            if (players.Length == 0) return;

            var serverId = e.Server.Id;

            var whitelistedPlayers = players.Where(p => !blackList.Contains(p.Guid)).ToArray();
            var blackListedPlayers = players.Where(p => blackList.Contains(p.Guid)).ToArray();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var pointsRepo = scope.ServiceProvider.GetService<PlayerPointsRepository>())
                {
                    using (var ctx = scope.ServiceProvider.GetService<AppDbContext>())
                    {
                        foreach (var player in whitelistedPlayers)
                        {
                            var sessionsCount = await ctx.PlayerSessions
                                .Where(x => x.EndDate != null && player.Guid == x.Player.GUID && x.ServerId == serverId)
                                .CountAsync();

                            var prevSession = await ctx.PlayerSessions
                               .Where(x => x.EndDate != null && player.Guid == x.Player.GUID && x.ServerId == serverId)
                               .OrderByDescending(x => x.StartDate)
                               .FirstOrDefaultAsync();

                            var playerDb = await ctx.Players.Where(x => x.GUID == player.Guid).FirstOrDefaultAsync();
                            decimal points = 0;
                            if (playerDb != null) points = await pointsRepo.GetPlayerPointsAsync(serverId, playerDb.Id);                                                                       

                            var message = GetWelcomeMessage(server, player, playerDb, sessionsCount, points);
                            
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, message);

                            var newNameMessage = GetNewNickameMessage(server, player, prevSession);
                            if (!string.IsNullOrWhiteSpace(newNameMessage))
                            {                                
                                _serverStateService.PostChat(e.Server.Id, "bot", -1, newNameMessage);
                            }
                            
                        }

                        foreach (var player in blackListedPlayers)
                        {
                            var sessions = await ctx.PlayerSessions
                                .Where(x => x.EndDate != null && player.Guid == x.Player.GUID && x.ServerId == serverId && x.Name == player.Name)
                                .ToArrayAsync();

                            var points = await pointsRepo.GetPlayerPointsFromSessionsAsync(sessions);

                            var playerDb = await ctx.Players.Where(x => x.GUID == player.Guid).FirstOrDefaultAsync();

                            var message = GetWelcomeMessage(server, player, playerDb, sessions.Length, points);
                            
                            _serverStateService.PostChat(e.Server.Id, "bot", -1, message);
                        }
                    }
                }
            }
        }
    }
}