namespace BattlEyeManager.Core.DataContracts.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int SteamPort { get; set; }
    }
}