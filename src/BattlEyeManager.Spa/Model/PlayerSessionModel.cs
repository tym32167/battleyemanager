using System;

namespace BattlEyeManager.Spa.Model
{
    public class PlayerSessionModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }       
        public int ServerId { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
