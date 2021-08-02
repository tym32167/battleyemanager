using BattlEyeManager.BE.Services;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Infrastructure.State;
using BattlEyeManager.Spa.Infrastructure.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Player = BattlEyeManager.BE.Models.Player;

namespace BattlEyeManager.Spa.Infrastructure.Featues
{
    public class WelcomeFeature
    {
        private readonly ServerStateService _serverStateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<int, WelcomeServerSettings> _enabledServers = new Dictionary<int, WelcomeServerSettings>();

        private Dictionary<int, HashSet<string>> _welcomeFeatureBlackLists = new Dictionary<int, HashSet<string>>();

        private static string WelcomeGreater50MessageTemplate = "Welcome, {name}! More than 50 hours on server!";
        private static string WelcomeMessageTemplate = "Welcome, {name}! {sessions} sessions and {hours} hours on server!";
        private static string WelcomeEmptyMessageTemplate = "Welcome, {name}! First time on server!";

        private static string WelcomeNewNicknameTemplate = "Внимание! Игрок {newName} сменил ник. Его прошлый ник {oldName}!";

        private static string GetMessageString(string template, string name, int sessions, int hours)
        {
            var templater = new StringTemplater();
            templater.AddParameter("name", name);
            templater.AddParameter("sessions", sessions.ToString());
            templater.AddParameter("hours", hours.ToString());

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
            IServiceScopeFactory serviceScopeFactory)
        {
            _serverStateService = serverStateService;
            _serviceScopeFactory = serviceScopeFactory;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public async Task SetEnabled(WelcomeServerSettings server)
        {
            if (server.WelcomeFeatureEnabled)
            {
                _enabledServers[server.ServerId] = server;

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    using (var repo = scope.ServiceProvider.GetService<IWelcomeFeatureRepository>())
                    {
                        string[] guids = await repo.GetFeatureBlackList(server.ServerId);
                        _welcomeFeatureBlackLists[server.ServerId] = new HashSet<string>(guids);
                    }
                }
            }

            else
            {
                _enabledServers.Remove(server.ServerId);
                _welcomeFeatureBlackLists.Remove(server.ServerId);
            }
        }

        private string GetWelcomeMessage(WelcomeServerSettings server, Player player, BattlEyeManager.Core.DataContracts.Models.PlayerSession[] sessions)
        {
            if (sessions.Length != 0)
            {
                int hours = (int)sessions.Select(s => ((s.EndDate ?? s.StartDate) - s.StartDate).TotalHours).Sum();
                if (hours > 50)
                {
                    var message = GetMessageString(server.WelcomeGreater50MessageTemplate.DefaultIfNullOrEmpty(WelcomeGreater50MessageTemplate), player.Name, sessions.Length, hours);
                    return message;
                }
                else
                {
                    var message = GetMessageString(server.WelcomeFeatureTemplate.DefaultIfNullOrEmpty(WelcomeMessageTemplate), player.Name, sessions.Length, hours);
                    return message;
                }
            }
            else
            {
                var message = GetMessageString(server.WelcomeFeatureEmptyTemplate.DefaultIfNullOrEmpty(WelcomeEmptyMessageTemplate), player.Name, sessions.Length, 0);
                return message;
            }
        }

        private string? GetNewNickameMessage(Player player, BattlEyeManager.Core.DataContracts.Models.PlayerSession[] sessions)
        {

            var newName = player.Name;
            var oldName = sessions.OrderByDescending(x => x.StartDate).FirstOrDefault()?.Name;

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
            var blackList = _welcomeFeatureBlackLists[server.ServerId];

            var players = e.Data.ToArray();
            if (players.Length > 2) return;

            var serverId = e.Server.Id;

            var whitelistedPlayers = players.Where(p => !blackList.Contains(p.Guid)).ToArray();

            if (!whitelistedPlayers.Any()) return;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                using (var repo = scope.ServiceProvider.GetService<ISessionRepository>())
                {
                    foreach (var player in whitelistedPlayers)
                    {
                        var sessions = await repo.GetComletedPlayerSessions(serverId, player.Guid);

                        var message = GetWelcomeMessage(server, player, sessions);
                        _serverStateService.PostChat(e.Server.Id, "bot", -1, message);

                        var newNameMessage = GetNewNickameMessage(player, sessions);
                        if (!string.IsNullOrWhiteSpace(newNameMessage)) _serverStateService.PostChat(e.Server.Id, "bot", -1, newNameMessage);
                    }
                }
            }
        }
    }
}