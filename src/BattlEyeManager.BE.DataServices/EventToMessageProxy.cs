using BattlEyeManager.BE.Services;
using BattlEyeManager.Core.MessageBus;

namespace BattlEyeManager.BE.DataServices
{
    public class EventToMessageProxy
    {
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly IMessageBus _messageBus;

        public EventToMessageProxy(IBeServerAggregator beServerAggregator,
            IMessageBus messageBus)
        {
            _beServerAggregator = beServerAggregator;
            _messageBus = messageBus;
        }

        public void Init()
        {
            _beServerAggregator.AdminHandler += _beServerAggregator_AdminHandler;
            _beServerAggregator.BanHandler += _beServerAggregator_BanHandler;
            _beServerAggregator.BanLog += _beServerAggregator_BanLog;
            _beServerAggregator.ChatMessageHandler += _beServerAggregator_ChatMessageHandler;
            _beServerAggregator.ConnectingHandler += _beServerAggregator_ConnectingHandler;
            _beServerAggregator.DisconnectHandler += _beServerAggregator_DisconnectHandler;
            _beServerAggregator.MissionHandler += _beServerAggregator_MissionHandler;
            _beServerAggregator.PlayerHandler += _beServerAggregator_PlayerHandler;
            _beServerAggregator.PlayerLog += _beServerAggregator_PlayerLog;
            _beServerAggregator.RConAdminLog += _beServerAggregator_RConAdminLog;
        }

        private void _beServerAggregator_RConAdminLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_PlayerLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_PlayerHandler(object sender, BEServerEventArgs<System.Collections.Generic.IEnumerable<Models.Player>> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_MissionHandler(object sender, BEServerEventArgs<System.Collections.Generic.IEnumerable<Models.Mission>> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_DisconnectHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_ConnectingHandler(object sender, BEServerEventArgs<ServerInfo> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_ChatMessageHandler(object sender, BEServerEventArgs<Models.ChatMessage> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_BanLog(object sender, BEServerEventArgs<Models.LogMessage> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_BanHandler(object sender, BEServerEventArgs<System.Collections.Generic.IEnumerable<Models.Ban>> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }

        private void _beServerAggregator_AdminHandler(object sender, BEServerEventArgs<System.Collections.Generic.IEnumerable<Models.Admin>> e)
        {
            _messageBus.Publish(MessageFactory.Create(e));
        }
    }
}
