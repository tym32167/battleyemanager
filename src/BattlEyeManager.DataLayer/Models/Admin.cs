using System;

namespace BattlEyeManager.DataLayer.Models
{
    public class Admin
    {
        public int Id { get; set; }

        public string IP { get; set; }
        public int Num { get; set; }
        public int Port { get; set; }

        public int ServerId { get; set; }
        public Server Server { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}