using MySql.Data.EntityFrameworkCore.DataAnnotations;
using System;

namespace BattlEyeManager.DataLayer.Models
{
    [MySqlCharset("utf8")]
    public class ChatMessage
    {
        public int Id { get; set; }

        [MySqlCharset("utf8")]
        public string Text { get; set; }

        public Guid ServerId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Server Server { get; set; }
    }
}