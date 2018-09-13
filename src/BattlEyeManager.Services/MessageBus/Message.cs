using BattlEyeManager.Core.MessageBus;

namespace BattlEyeManager.Services.MessageBus
{
    public class Message<T> : Message, IMessage<T>
    {
        public T Data { get; }

        public Message(T data)
        {
            Data = data;
        }
    }

    public class Message : IMessage
    {

    }
}