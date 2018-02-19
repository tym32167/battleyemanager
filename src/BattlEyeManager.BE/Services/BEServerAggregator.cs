using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.BeNet;
using BattlEyeManager.BE.Core;
using BattlEyeManager.BE.Models;
using BattlEyeManager.BE.Net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace BattlEyeManager.BE.Services
{
    public class BeServerAggregator : IBeServerAggregator
    {
        private readonly IBattlEyeServerFactory _battlEyeServerFactory;
        private readonly IIpService _ipService;
        private readonly ConcurrentDictionary<Guid, ServerItem> _servers = new ConcurrentDictionary<Guid, ServerItem>();

        public BeServerAggregator(IBattlEyeServerFactory battlEyeServerFactory, IIpService ipService)
        {
            _battlEyeServerFactory = battlEyeServerFactory;
            _ipService = ipService;            
        }       

        public bool AddServer(ServerInfo info)
        {
            RemoveServer(info.Id);

            var host = _ipService.GetIpAddress(info.Host);

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

        public bool RemoveServer(Guid serverId)
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

        public event EventHandler<BEServerEventArgs<IEnumerable<Player>>> PlayerHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Ban>>> BanHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Admin>>> AdminHandler;
        public event EventHandler<BEServerEventArgs<IEnumerable<Mission>>> MissionHandler;

        public event EventHandler<BEServerEventArgs<ChatMessage>> ChatMessageHandler;

        public event EventHandler<BEServerEventArgs<LogMessage>> RConAdminLog;
        public event EventHandler<BEServerEventArgs<LogMessage>> PlayerLog;
        public event EventHandler<BEServerEventArgs<LogMessage>> BanLog;

        public event EventHandler<BEServerEventArgs<Guid>> ConnectingHandler;
        public event EventHandler<BEServerEventArgs<Guid>> DisconnectHandler;


        private class ServerItem : DisposeObject
        {
            private readonly ServerInfo _serverInfo;
            private readonly BeServerAggregator _aggregator;
            private IBattlEyeServer _server;

            public ServerItem(ServerInfo serverInfo, BeServerAggregator aggregator, IBattlEyeServer server)
            {
                _serverInfo = serverInfo;
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

            private void _server_BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
            {
                //System.Diagnostics.Debug.WriteLine(args.Message);
            }

            private void _server_BattlEyeDisconnected(BattlEyeDisconnectEventArgs args)
            {
                System.Diagnostics.Debug.WriteLine("DIS_CONNECTED");
            }

            private void _server_BattlEyeConnected(BattlEyeConnectEventArgs args)
            {
                System.Diagnostics.Debug.WriteLine("CONNECTED");
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
    }




    public class ServerInfo
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }

    public class BEServerEventArgs<T> : EventArgs
    {
        public BEServerEventArgs(Guid serverId, T data)
        {
            ServerId = serverId;
            Data = data;
        }

        public Guid ServerId { get; }
        public T Data { get; }
    }
}