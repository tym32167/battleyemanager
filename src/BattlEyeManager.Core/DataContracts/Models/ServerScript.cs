namespace BattlEyeManager.Core.DataContracts.Models
{
    public class ServerScript
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}