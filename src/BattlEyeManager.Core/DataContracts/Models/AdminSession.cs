using System;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class AdminSession
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public int Num { get; set; }
        public int Port { get; set; }
        public int ServerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}