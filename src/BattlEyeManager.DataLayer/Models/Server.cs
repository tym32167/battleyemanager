using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class Server
    {
        public int Id { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int SteamPort { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<PlayerSession> PlayerSessions { get; set; }
        public ICollection<ServerBan> ServerBans { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }
}