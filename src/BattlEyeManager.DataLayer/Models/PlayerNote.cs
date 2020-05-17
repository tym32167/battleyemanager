using System;

namespace BattlEyeManager.DataLayer.Models
{
    public class PlayerNote
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}