using BattlEyeManager.BE.Abstract;
using BattleNET;
using BattlEyeManager.BE.ServerDecorators;

namespace BattlEyeManager.BE.ServerFactory
{
    public class WatcherBEServerFactory : IBattlEyeServerFactory
    {
        public IBattlEyeServer Create(BattlEyeLoginCredentials credentials)
        {
            return new BEConnectedWatcher(new BattlEyeServerFactory(), credentials);
        }
    }
}