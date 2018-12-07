using System;

namespace BattlEyeManager.DataLayer.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ServerId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Server Server { get; set; }
    }
}