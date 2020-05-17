using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Spa.Model
{
    public class OnlineServerModel
    {
        public int Id { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int SteamPort { get; set; }



        [Required]
        public int PlayersCount { get; set; }
        [Required]
        public int AdminsCount { get; set; }
        [Required]
        public int BansCount { get; set; }
        [Required]
        public bool IsConnected { get; set; }
    }
}