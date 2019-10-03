using BattlEyeManager.DataLayer.Repositories;
using System.Collections.Generic;

namespace BattlEyeManager.DataLayer.Models
{
    public class Server : ServerInfoDto
    {


        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<Admin> Admins { get; set; }

        public ICollection<ServerModerators> Servers { get; set; }
    }
}