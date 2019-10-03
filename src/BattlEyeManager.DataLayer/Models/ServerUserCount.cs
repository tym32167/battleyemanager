using System;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class ServerUserCount
    {
        public int Id { get; set; }

        [Required]
        public int ServerId { get; set; }
        public Server Server { get; set; }

        public int PlayersCount { get; set; }

        public DateTime Time { get; set; }
    }
}