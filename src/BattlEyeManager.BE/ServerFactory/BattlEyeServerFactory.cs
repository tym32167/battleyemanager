using BattleNET;
using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.ServerDecorators;
using BattlEyeManager.Core;

namespace BattlEyeManager.BE.ServerFactory
{
    public class BattlEyeServerFactory : IBattlEyeServerFactory
    {
        private readonly ILog _log;

        public BattlEyeServerFactory(ILog log)
        {
            _log = log;
        }

        public IBattlEyeServer Create(BattlEyeLoginCredentials credentials)
        {
            return new ThreadSafeBattleEyeServer(new BattlEyeServerProxy(new BattlEyeClient(credentials)
            {
                ReconnectOnPacketLoss = true
            }, $"{credentials.Host}:{credentials.Port}", _log), _log);
        }
    }
}