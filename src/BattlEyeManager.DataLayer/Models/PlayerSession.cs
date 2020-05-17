using System;

namespace BattlEyeManager.DataLayer.Models
{
    public class PlayerSession
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}