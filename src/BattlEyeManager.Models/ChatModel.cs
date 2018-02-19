using System;

namespace BattlEyeManager.Models
{
    public class ChatModel : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; }
        public Guid ServerId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}