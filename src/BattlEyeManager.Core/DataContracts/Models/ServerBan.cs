using System;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class ServerBan
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public int ServerId { get; set; }
        public int Num { get; set; }
        public string GuidIp { get; set; }
        public int Minutes { get; set; }
        public int MinutesLeft { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool IsActive { get; set; }
    }
}