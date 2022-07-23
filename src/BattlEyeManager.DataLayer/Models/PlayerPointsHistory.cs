using System;

namespace BattlEyeManager.DataLayer.Models
{
    public class PlayerPointsHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }
        public Decimal Balance { get; set; }
        public string Reason { get; set; }
    }
}