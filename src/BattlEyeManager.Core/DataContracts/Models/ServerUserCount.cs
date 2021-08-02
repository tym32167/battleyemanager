using System;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class ServerUserCount
    {
        public int Id { get; set; }

        public int ServerId { get; set; }

        public int PlayersCount { get; set; }

        public DateTime Time { get; set; }
    }
}