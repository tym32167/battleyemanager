using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid ServerId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Server Server { get; set; }
    }

    public class Server
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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
    }
}