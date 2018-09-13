namespace BattlEyeManager.Core.MessageBus
{
    public interface IMessage<out T> : IMessage
    {
        T Data { get; }
    }

    public interface IMessage
    {
    }
}