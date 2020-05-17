using BattlEyeManager.BE.Abstract;
using BattleNET;
using BattlEyeManager.BE.ServerDecorators;
using BattlEyeManager.Core;

namespace BattlEyeManager.BE.ServerFactory
{
    public class WatcherBEServerFactory : IBattlEyeServerFactory
    {
        private readonly ILog _log;

        public WatcherBEServerFactory(ILog log)
        {
            _log = log;
        }

        public IBattlEyeServer Create(BattlEyeLoginCredentials credentials)
        {
            return new BEConnectedWatcher(new BattlEyeServerFactory(_log), credentials, _log);
        }
    }
}