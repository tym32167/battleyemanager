using BattleNET;
using BattlEyeManager.BE.Abstract;
using BattlEyeManager.Core;

namespace BattlEyeManager.BE.ServerDecorators
{
    public class BattlEyeServerProxy : DisposeObject, IBattlEyeServer
    {
        private BattlEyeClient _battlEyeClient;
        private readonly string _serverName;
        private readonly ILog _log;

        public BattlEyeServerProxy(BattlEyeClient client, string serverName, ILog log)
        {
            _log = log;
            _battlEyeClient = client;
            _serverName = serverName;

            _battlEyeClient.ReconnectOnPacketLoss = true;

            _battlEyeClient.BattlEyeConnected += OnBattlEyeConnected;
            _battlEyeClient.BattlEyeMessageReceived += OnBattlEyeMessageReceived;
            _battlEyeClient.BattlEyeDisconnected += OnBattlEyeDisconnected;
        }

        public event BattlEyeMessageEventHandler BattlEyeMessageReceived;
        public event BattlEyeConnectEventHandler BattlEyeConnected;
        public event BattlEyeDisconnectEventHandler BattlEyeDisconnected;

        public bool Connected => _battlEyeClient.Connected;


        private void OnBattlEyeMessageReceived(BattlEyeMessageEventArgs message)
        {
            _log.Info($"{_serverName}: Received\n{message.Id}\n{message.Message}");

            if (!string.IsNullOrEmpty(message.Message))
                BattlEyeMessageReceived?.Invoke(message);
        }

        private void OnBattlEyeConnected(BattlEyeConnectEventArgs args)
        {
            _log.Info($"{_serverName}: Connected");

            if (Connected)
                BattlEyeConnected?.Invoke(args);
        }

        private void OnBattlEyeDisconnected(BattlEyeDisconnectEventArgs args)
        {
            _log.Info($"{_serverName}: Disconnected");
            BattlEyeDisconnected?.Invoke(args);
        }

        public BattlEyeConnectionResult Connect()
        {
            _log.Info($"{_serverName}: Connect called");
            return _battlEyeClient.Connect();
        }

        public void Disconnect()
        {
            _log.Info($"{_serverName}: Disconnect called");
            _battlEyeClient?.Disconnect();
        }

        public bool ReconnectOnPacketLoss
        {
            get { return _battlEyeClient.ReconnectOnPacketLoss; }
            set { _battlEyeClient.ReconnectOnPacketLoss = value; }
        }

        public int SendCommand(BattlEyeCommand command, string parameters = "")
        {
            _log.Info($"{_serverName}: Send {command} with {parameters}");
            return _battlEyeClient.SendCommand(command, parameters);
        }

        public int SendCommand(string command)
        {
            _log.Info($"{_serverName}: Send {command}");
            return _battlEyeClient.SendCommand(command);
        }

        protected override void DisposeManagedResources()
        {
            _log.Info($"{_serverName}: Dispose");
            var local = _battlEyeClient;
            _battlEyeClient = null;
            if (local != null)
            {
                local.BattlEyeConnected -= OnBattlEyeConnected;
                local.BattlEyeMessageReceived -= OnBattlEyeMessageReceived;
                local.BattlEyeDisconnected -= OnBattlEyeDisconnected;

                if (local.Connected) local.Disconnect();
            }
            base.DisposeManagedResources();
        }
    }
}