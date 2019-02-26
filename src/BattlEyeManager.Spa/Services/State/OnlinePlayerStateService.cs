using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Services.State
{
    public class OnlinePlayerStateService
    {
        private readonly ConcurrentDictionary<int, IEnumerable<Player>> _playerState = new ConcurrentDictionary<int, IEnumerable<Player>>();

        public event EventHandler<BEServerEventArgs<IEnumerable<Player>>> PlayersJoined;
        public event EventHandler<BEServerEventArgs<IEnumerable<Player>>> PlayersLeaved;

        public OnlinePlayerStateService(IBeServerAggregator serverAggregator)
        {
            var serverAggregator1 = serverAggregator;
            serverAggregator1.PlayerHandler += _serverAggregator_PlayerHandler;
            serverAggregator1.DisconnectHandler += _serverAggregator_DisconnectHandler;
        }

        private void _serverAggregator_DisconnectHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            var emptyPlayer = Enumerable.Empty<Player>();
            IEnumerable<Player> leaved = null;

            _playerState.AddOrUpdate(e.Server.Id,
                guid => emptyPlayer,
                (guid, players) =>
                {
                    leaved = players;
                    return emptyPlayer;
                });

            if (leaved != null) OnPlayersLeaved(new BEServerEventArgs<IEnumerable<Player>>(e.Server, leaved));
        }

        private void _serverAggregator_PlayerHandler(object sender, BEServerEventArgs<IEnumerable<Player>> e)
        {
            Player[] joined = null;
            Player[] leaved = null;

            _playerState.AddOrUpdate(e.Server.Id,
                guid => e.Data, (guid, players) =>
                {
                    var ret = e.Data;
                    joined = ret.Where(r => players.All(p => p.Guid != r.Guid)).ToArray();
                    leaved = players.Where(r => ret.All(p => p.Guid != r.Guid)).ToArray();
                    return ret;
                });

            if (joined != null) OnPlayersJoined(new BEServerEventArgs<IEnumerable<Player>>(e.Server, joined));
            if (leaved != null) OnPlayersLeaved(new BEServerEventArgs<IEnumerable<Player>>(e.Server, leaved));
        }

        protected virtual void OnPlayersJoined(BEServerEventArgs<IEnumerable<Player>> e)
        {
            PlayersJoined?.Invoke(this, e);
        }

        protected virtual void OnPlayersLeaved(BEServerEventArgs<IEnumerable<Player>> e)
        {
            PlayersLeaved?.Invoke(this, e);
        }

        public IEnumerable<Player> GetPlayers(int serverId)
        {
            if (_playerState.TryGetValue(serverId, out IEnumerable<Player> res))
            {
                return res;
            }
            return Enumerable.Empty<Player>();
        }

        public int GetPlayersCount(int serverId)
        {
            if (_playerState.TryGetValue(serverId, out IEnumerable<Player> res))
            {
                return res.Count();
            }
            return 0;
        }
    }
}