namespace BattlEyeManager.Models
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}