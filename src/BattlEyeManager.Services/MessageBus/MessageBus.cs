using BattlEyeManager.Core.MessageBus;
using Redbus;
using Redbus.Events;
using Redbus.Interfaces;
using System;

namespace BattlEyeManager.Services.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly IEventBus _bus = new EventBus();

        public void Subscribe<TMessage>(Action<TMessage> action) where TMessage : IMessage
        {
            var act = new Action<PayloadEvent<TMessage>>((e) => { action(e.Payload); });
            _bus.Subscribe(act);
        }

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            _bus.Publish(new PayloadEvent<TMessage>(message));
        }
    }
}
