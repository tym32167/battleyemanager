using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BattlEyeManager.Steam
{
    public class SteamService : ISteamService
    {
        private readonly GetServerInfoSettings _settings;

        public SteamService(GetServerInfoSettings settings)
        {
            _settings = settings;
        }


        public ServerRulesResult GetServerRulesSync(IPEndPoint endpoint)
        {
            var localEndpoint = new IPEndPoint(IPAddress.Any, 0);
            using (var client = new UdpClient(localEndpoint))
            {
                client.Client.ReceiveTimeout = _settings.ReceiveTimeout;
                client.Client.SendTimeout = _settings.SendTimeout;

                client.Connect(endpoint);
                var requestPacket = new List<byte>();
                requestPacket.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x56 });
                requestPacket.AddRange(BitConverter.GetBytes(-1));
                client.Send(requestPacket.ToArray(), requestPacket.ToArray().Length);
                var responseData = client.Receive(ref localEndpoint);
                requestPacket.Clear();
                requestPacket.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x56 });
                requestPacket.AddRange(responseData.Skip(5).Take(4));
                client.Send(requestPacket.ToArray(), requestPacket.ToArray().Length);
                responseData = client.Receive(ref localEndpoint);
                return ServerRulesResult.Parse(responseData);
            }
        }


        public ServerPlayers GetServerChallengeSync(IPEndPoint endpoint)
        {
            var localEndpoint = new IPEndPoint(IPAddress.Any, 0);
            using (var client = new UdpClient(localEndpoint))
            {
                client.Client.ReceiveTimeout = _settings.ReceiveTimeout;
                client.Client.SendTimeout = _settings.SendTimeout;

                client.Connect(endpoint);

                var requestPacket = new List<byte>();
                requestPacket.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 });
                requestPacket.AddRange(BitConverter.GetBytes(-1));

                client.Send(requestPacket.ToArray(), requestPacket.ToArray().Length);
                var responseData = client.Receive(ref localEndpoint);
                requestPacket.Clear();
                requestPacket.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 });
                requestPacket.AddRange(responseData.Skip(5).Take(4));
                client.Send(requestPacket.ToArray(), requestPacket.ToArray().Length);
                responseData = client.Receive(ref localEndpoint);

                return ServerPlayers.Parse(responseData);
            }
        }


        public ServerInfoResult GetServerInfoSync(IPEndPoint endpoint)
        {
            var localEndPoint = new IPEndPoint(IPAddress.Any, 0);
            using (var client = new UdpClient(localEndPoint))
            {
                client.Client.ReceiveTimeout = _settings.ReceiveTimeout;
                client.Client.SendTimeout = _settings.SendTimeout;

                client.Connect(endpoint);
                var requestPacket = new List<byte>();
                requestPacket.AddRange(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
                requestPacket.Add(0x54);
                requestPacket.AddRange(Encoding.ASCII.GetBytes("Source Engine Query"));
                requestPacket.Add(0x00);
                var requestData = requestPacket.ToArray();
                client.Send(requestData, requestData.Length);
                var data = client.Receive(ref localEndPoint);
                return ServerInfoResult.Parse(data);
            }
        }
    }
}