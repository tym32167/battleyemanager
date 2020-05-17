namespace BattlEyeManager.Spa.Model
{
    public class OnlineMissionModel
    {
        public string Name { get; set; }
        public int ServerId { get; set; }
    }

    public class OnlineServerCommandModel
    {
        public int ServerId { get; set; }
        public string Command { get; set; }
    }
}