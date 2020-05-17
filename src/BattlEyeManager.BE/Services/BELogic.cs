using BattleNET;
using System;
using System.Linq;
using System.Threading;

namespace BattlEyeManager.BE.Services
{
    public sealed class BELogic : IDisposable
    {
        private readonly IBeServerAggregator _serverAggregator;
        private readonly Timer _playerTimer;
        private readonly Timer _stateTimer;

        private void PlayerTimed(object state)
        {
            var servers = _serverAggregator.GetConnectedServers().Select(x => x.Id).ToArray();
            foreach (var server in servers)
            {
                _serverAggregator.Send(server, BattlEyeCommand.Players);
            }
        }

        private void StateTimed(object state)
        {
            var servers = _serverAggregator.GetConnectedServers().Select(x => x.Id).ToArray();
            foreach (var server in servers)
            {
                _serverAggregator.Send(server, BattlEyeCommand.Admins);
                _serverAggregator.Send(server, BattlEyeCommand.Bans);
                _serverAggregator.Send(server, BattlEyeCommand.Missions);
            }
        }

        public BELogic(IBeServerAggregator serverAggregator)
        {
            _serverAggregator = serverAggregator;
            _playerTimer = new Timer(PlayerTimed, null, TimeSpan.Zero, TimeSpan.FromSeconds(40));
            _stateTimer = new Timer(StateTimed, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        }

        public void Init()
        {
            _serverAggregator.PlayerLog += _serverAggregator_PlayerLog;
            _serverAggregator.BanLog += _serverAggregator_BanLog;
            _serverAggregator.RConAdminLog += _serverAggregator_RConAdminLog;
            _serverAggregator.ConnectingHandler += _serverAggregator_ConnectingHandler;
            _serverAggregator.CommandHandler += _serverAggregator_CommandHandler;
        }

        private void _serverAggregator_CommandHandler(object sender, CommandArgs e)
        {
            switch (e.Command)
            {
                case BattlEyeCommand.Ban:
                case BattlEyeCommand.AddBan:
                case BattlEyeCommand.RemoveBan:
                    _serverAggregator.Send(e.ServerId, BattlEyeCommand.Bans);
                    break;
            }
        }

        private void _serverAggregator_ConnectingHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Players);
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Bans);
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Missions);
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Admins);
        }

        private void _serverAggregator_RConAdminLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Admins);
        }

        private void _serverAggregator_BanLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Players);
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Bans);
        }

        private void _serverAggregator_PlayerLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _serverAggregator.Send(e.Server.Id, BattlEyeCommand.Players);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serverAggregator.PlayerLog -= _serverAggregator_PlayerLog;
                _serverAggregator.BanLog -= _serverAggregator_BanLog;
                _serverAggregator.RConAdminLog -= _serverAggregator_RConAdminLog;
                _serverAggregator.ConnectingHandler -= _serverAggregator_ConnectingHandler;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}