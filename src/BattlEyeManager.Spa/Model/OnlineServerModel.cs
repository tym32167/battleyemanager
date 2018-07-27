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
    }
}