﻿using BattleNET;
using System;

namespace BattlEyeManager.BE.Services
{
    public sealed class BELogic : IDisposable
    {
        private readonly IBeServerAggregator _serverAggregator;

        public BELogic(IBeServerAggregator serverAggregator)
        {
            _serverAggregator = serverAggregator;
        }

        public void Init()
        {
            _serverAggregator.PlayerLog += _serverAggregator_PlayerLog;
            _serverAggregator.BanLog += _serverAggregator_BanLog;
            _serverAggregator.RConAdminLog += _serverAggregator_RConAdminLog;
            _serverAggregator.ConnectingHandler += _serverAggregator_ConnectingHandler;
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