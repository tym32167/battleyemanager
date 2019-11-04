namespace BattlEyeManager.DataLayer.Models
{
    public class ServerScript
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}