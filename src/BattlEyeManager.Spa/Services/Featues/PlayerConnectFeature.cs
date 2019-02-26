using BattlEyeManager.BE.Services;
using BattlEyeManager.Spa.Services.State;

namespace BattlEyeManager.Spa.Services.Featues
{
    public class PlayerConnectFeature
    {
        private readonly ServerStateService _serverStateService;

        public PlayerConnectFeature(OnlinePlayerStateService playerStateService, ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
            playerStateService.PlayersJoined += _playerStateService_PlayersJoined;
        }

        private void _playerStateService_PlayersJoined(object sender, BEServerEventArgs<System.Collections.Generic.IEnumerable<BE.Models.Player>> e)
        {
            foreach (var player in e.Data)
            {
                _serverStateService.PostChat(e.Server.Id, "bot", -1, $"Welcome, {player.Name}!");
            }
        }
    }
}