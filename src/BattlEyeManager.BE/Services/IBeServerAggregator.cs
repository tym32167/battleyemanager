using BattleNET;
using BattlEyeManager.BE.Models;
using System;
using System.Collections.Generic;

namespace BattlEyeManager.BE.Services
{
    public interface IBeServerAggregator
    {
        event EventHandler<BEServerEventArgs<IEnumerable<Admin>>> AdminHandler;
        event EventHandler<BEServerEventArgs<IEnumerable<Ban>>> BanHandler;
        event EventHandler<BEServerEventArgs<LogMessage>> BanLog;
        event EventHandler<BEServerEventArgs<ChatMessage>> ChatMessageHandler;
        event EventHandler<BEServerEventArgs<ServerInfo>> ConnectingHandler;
        event EventHandler<BEServerEventArgs<ServerInfo>> DisconnectHandler;
        event EventHandler<BEServerEventArgs<IEnumerable<Mission>>> MissionHandler;
        event EventHandler<BEServerEventArgs<IEnumerable<Player>>> PlayerHandler;
        event EventHandler<BEServerEventArgs<LogMessage>> PlayerLog;
        event EventHandler<BEServerEventArgs<LogMessage>> RConAdminLog;

        event EventHandler<CommandArgs> CommandHandler;

        bool AddServer(ServerInfo info);
        bool RemoveServer(int serverId);
        bool IsConnected(int serverId);

        IEnumerable<ServerInfo> GetConnectedServers();
        void Send(int serverId, BattlEyeCommand command, string param = null);


    }
}