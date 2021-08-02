using System;

namespace BattlEyeManager.Core.DataContracts.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ServerId { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}