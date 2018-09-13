using System;
using System.Collections.Generic;

namespace BattlEyeManager.DataLayer.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string SteamId { get; set; }

        public string GUID { get; set; }
        public string Comment { get; set; }

        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime LastSeen { get; set; }

        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<PlayerNote> Notes { get; set; }
    }
}