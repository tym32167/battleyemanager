using BattlEyeManager.BE.Abstract;
using BattlEyeManager.BE.BeNet;
using BattlEyeManager.BE.ServerDecorators;

namespace BattlEyeManager.BE.ServerFactory
{
    public class BattlEyeServerFactory : IBattlEyeServerFactory
    {
        public IBattlEyeServer Create(BattlEyeLoginCredentials credentials)
        {
            return new ThreadSafeBattleEyeServer(new BattlEyeServerProxy(new BattlEyeClient(credentials)
            {
                ReconnectOnPacketLoss = true
            }, $"{credentials.Host}:{credentials.Port}"));
        }
    }
}