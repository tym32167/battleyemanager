namespace BattlEyeManager.Core.MessageBus
{
    public class Message<T> : Message, IMessage<T>
    {
        public T Data { get; }

        public Message(T data)
        {
            Data = data;
        }
    }

    public class MessageFactory
    {
        public static Message<T> Create<T>(T payload) => new Message<T>(payload);
    }

    public class Message : IMessage
    {

    }
}