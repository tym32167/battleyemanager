using BattleNET;
using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.Messaging;
using BattlEyeManager.BE.Models;
using BattlEyeManager.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BattlEyeManager.BE.Services
{
    public class BeServerAggregator : IBeServerAggregator
    {
        private readonly IBattlEyeServerFactory _battlEyeServerFactory;
        private readonly IIpService _ipService;
        private readonly ConcurrentDictionary<int, ServerItem> _servers = new ConcurrentDictionary<int, ServerItem>();
        private readonly ILog _log;

        public BeServerAggregator(IBattlEyeServerFactory battlEyeServerFactory,
            IIpService ipService,
            ILog log)
        {
            _log = log;
            _battlEyeServerFactory = battlEyeServerFactory;
            _ipService = ipService;
        }

        public event EventHandler<CommandArgs> CommandHandler;

        public bool AddServer(ServerInfo info)
        {
            RemoveServer(info.Id);

            var host = _ipService.GetIpAddress(info.Host);
            if (string.IsNullOrEmpty(host)) return false;

            var credentials = new BattlEyeLoginCredentials(IPAddress.Parse(host), info.Port, info.Password);
            var server = _battlEyeServerFactory.Create(credentials);
            var item = new ServerItem(info, this, server);

            if (_servers.TryAdd(info.Id, item))
            {
                item.Subscribe();
                item.Run();
                return true;
            }

            return false;
        }

        public bool RemoveServer(int serverId)
        {
            ServerItem item;

            if (_servers.TryRemove(serverId, out item))
            {
                item.Stop();
                item.UnSubscribe();
                return true;
            }
            return false;
        }


        public void Send(int serverId, BattlEyeCommand command, string param = null)
        {
            if (_servers.TryGetValue(serverId, out ServerItem item))
            {
                item.Send(command, param);
                OnCommandHandler(new CommandArgs(serverId, command, param));
            }
        }

        public IEnumerable<ServerInfo> GetConnectedServers()
        {
            return _servers.Values.Select(x => x.ServerInfo).ToArray();
        }

        public bool IsConnected(int serverId)
        {
            if (_servers.TryGetValue(serverId, out ServerItem item))
            {
                return item.Connected;
            }

            return false;
        }

        private void ProcessMessage(ServerInfo server, ServerMessage message)
        {
            var logMessage = new LogMessage
            {
                Date = DateTime.UtcNow,
                Message = message.Message
            };

            switch (message.Type)
            {
                case ServerMessageType.PlayerList:
                    var list = new PlayerList(message);
                    OnPlayerHandler(new BEServerEventArgs<IEnumerable<Player>>(server, list));
                    break;
                case ServerMessageType.BanList:
                    var banList = new BanList(message);
                    OnBanHandler(new BEServerEventArgs<IEnumerable<Ban>>(server, banList));
                    break;

                case ServerMessageType.AdminList:
                    var adminList = new AdminList(message);
                    OnAdminHandler(new BEServerEventArgs<IEnumerable<Admin>>(server, adminList));
                    break;

                case ServerMessageType.MissionList:
                    var missinlist = new MissionList(message);
                    OnMissionHandler(new BEServerEventArgs<IEnumerable<Mission>>(server, missinlist));
                    break;

                case ServerMessageType.ChatMessage:
                    var chatMessage = new ChatMessage
                    {
                        Date = DateTime.UtcNow,
                        Message = message.Message
                    };

                    OnChatMessageHandler(new BEServerEventArgs<ChatMessage>(server, chatMessage));
                    break;

                case ServerMessageType.RconAdminLog:
                    OnRConAdminLog(new BEServerEventArgs<LogMessage>(server, logMessage));
                    break;

                case ServerMessageType.PlayerLog:
                    OnPlayerLog(new BEServerEventArgs<LogMessage>(server, logMessage));
                    break;

                case ServerMessageType.BanLog:
                    OnBanLog(new BEServerEventArgs<LogMessage>(server, logMessage));
                    break;

                case ServerMessageType.Unknown:
                    //var unknownMessage = new ChatMessage
                    //{
                    //    Date = DateTime.UtcNow,
                    //    Message = message.Message
                    //};

                    //OnChatMessageHandler(unknownMessage);
                    break;
            }

            RegisterMessage(server, message);
        }

        // ReSharper disable once UnusedParameter.Local
        private void RegisterMessage(ServerInfo server, ServerMessage message)
        {
            if (message.Type == ServerMessageType.Unknown)
            {
                _log.Info(
                    $"UNKNOWN MESSAGE: message [\nserver: {server.Host}\nmessageId:{message.MessageId}\n{message.Message}\n]");
            }
            // _log.Info($"message [\nserver ip: {_host}\nmessageId:{message.MessageId}\n{message.Message}\n]");
        }

        private void Connect(ServerInfo server)
        {
            OnConnectingHandler(new BEServerEventArgs<ServerInfo>(server, server));
        }

        private void Disconnect(ServerInfo server)
        {
            OnDisconnectHandler(new BEServerEventArgs<ServerInfo>(server, server));
        }

        public event EventHandler<BEServerEventArgs<IEnumerable<Player>>> PlayerHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Ban>>> BanHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Admin>>> AdminHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Mission>>> MissionHandler;

        public event EventHandler<BEServerEventArgs<ChatMessage>> ChatMessageHandler;

        public event EventHandler<BEServerEventArgs<LogMessage>> RConAdminLog;
        public event EventHandler<BEServerEventArgs<LogMessage>> PlayerLog;
        public event EventHandler<BEServerEventArgs<LogMessage>> BanLog;

        public event EventHandler<BEServerEventArgs<ServerInfo>> ConnectingHandler;
        public event EventHandler<BEServerEventArgs<ServerInfo>> DisconnectHandler;

        private class ServerItem : DisposeObject
        {
            public ServerInfo ServerInfo { get; }
            private readonly BeServerAggregator _aggregator;
            private IBattlEyeServer _server;

            public ServerItem(ServerInfo serverInfo, BeServerAggregator aggregator, IBattlEyeServer server)
            {
                ServerInfo = serverInfo;
                _aggregator = aggregator;
                _server = server;
            }

            public void Subscribe()
            {
                var local = _server;
                if (local != null)
                {
                    local.BattlEyeConnected += _server_BattlEyeConnected;
                    local.BattlEyeDisconnected += _server_BattlEyeDisconnected;
                    local.BattlEyeMessageReceived += _server_BattlEyeMessageReceived;
                }
            }

            public bool Connected => _server?.Connected == true;

            private void _server_BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
            {
                _aggregator.ProcessMessage(ServerInfo, new ServerMessage(args.Id, args.Message));
            }

            private void _server_BattlEyeDisconnected(BattlEyeDisconnectEventArgs args)
            {
                _aggregator.Disconnect(ServerInfo);
            }

            private void _server_BattlEyeConnected(BattlEyeConnectEventArgs args)
            {
                _aggregator.Connect(ServerInfo);
            }

            public void Send(BattlEyeCommand command, string param = null)
            {
                if (_server?.Connected == true)
                    _server?.SendCommand(command, param);
            }

            public void UnSubscribe()
            {
                var local = _server;
                if (local != null)
                {
                    local.BattlEyeConnected -= _server_BattlEyeConnected;
                    local.BattlEyeDisconnected -= _server_BattlEyeDisconnected;
                    local.BattlEyeMessageReceived -= _server_BattlEyeMessageReceived;
                }
            }

            public void Run()
            {
                _server?.Connect();
            }

            public void Stop()
            {
                _server?.Disconnect();
                _server?.Dispose();
            }

            protected override void DisposeManagedResources()
            {
                base.DisposeManagedResources();

                UnSubscribe();
                Stop();
                _server?.Dispose();
                _server = null;
            }
        }

        protected virtual void OnConnectingHandler(BEServerEventArgs<ServerInfo> e)
        {
            ConnectingHandler?.Invoke(this, e);
        }

        protected virtual void OnDisconnectHandler(BEServerEventArgs<ServerInfo> e)
        {
            DisconnectHandler?.Invoke(this, e);
        }

        protected virtual void OnPlayerHandler(BEServerEventArgs<IEnumerable<Player>> e)
        {
            PlayerHandler?.Invoke(this, e);
        }

        protected virtual void OnBanHandler(BEServerEventArgs<IEnumerable<Ban>> e)
        {
            BanHandler?.Invoke(this, e);
        }

        protected virtual void OnAdminHandler(BEServerEventArgs<IEnumerable<Admin>> e)
        {
            AdminHandler?.Invoke(this, e);
        }

        protected virtual void OnMissionHandler(BEServerEventArgs<IEnumerable<Mission>> e)
        {
            MissionHandler?.Invoke(this, e);
        }

        protected virtual void OnChatMessageHandler(BEServerEventArgs<ChatMessage> e)
        {
            ChatMessageHandler?.Invoke(this, e);
        }

        protected virtual void OnRConAdminLog(BEServerEventArgs<LogMessage> e)
        {
            RConAdminLog?.Invoke(this, e);
        }

        protected virtual void OnPlayerLog(BEServerEventArgs<LogMessage> e)
        {
            PlayerLog?.Invoke(this, e);
        }

        protected virtual void OnBanLog(BEServerEventArgs<LogMessage> e)
        {
            BanLog?.Invoke(this, e);
        }

        protected virtual void OnCommandHandler(CommandArgs e)
        {
            CommandHandler?.Invoke(this, e);
        }
    }
}