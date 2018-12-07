using BattlEyeManager.BE.Models;

namespace BattlEyeManager.Spa.Model
{
    public class OnlinePlayerModel
    {
        public int Num { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public int Ping { get; set; }

        public string Guid { get; set; }

        public string Name { get; set; }

        public Player.PlayerState State { get; set; }
    }
}