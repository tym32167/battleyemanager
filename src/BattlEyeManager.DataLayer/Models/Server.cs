using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    [MySqlCharset("utf8")]
    public class Server
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string Password { get; set; }

        [Required]
        [MySqlCharset("utf8")]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int SteamPort { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}