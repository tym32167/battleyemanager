using System;

namespace BattlEyeManager.Core.MessageBus
{
    public interface IMessageBus
    {
        void Subscribe<TMessage>(Action<TMessage> action) where TMessage : IMessage;
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }
}