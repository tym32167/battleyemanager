using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Services.State;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Services.Featues
{
    public class WelcomeFeature
    {
        private readonly ServerStateService _serverStateService;
        private HashSet<int> _enabledServers = new HashSet<int>();

        public WelcomeFeature(OnlinePlayerStateService playerStateService, ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        public void SetEnabled(int serverId, bool enabled)
        {
            if (enabled) _enabledServers.Add(serverId);
            else _enabledServers.Remove(serverId);
        }

        private void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<IEnumerable<BE.Models.Player>> e)
        {
            if (!_enabledServers.Contains(e.Server.Id)) return;
            var players = e.Data.ToArray();
            if (players.Length > 5) return;

            foreach (var player in players)
            {
                _serverStateService.PostChat(e.Server.Id, "bot", -1, $"Welcome, {player.Name}!");
            }
        }
    }
}