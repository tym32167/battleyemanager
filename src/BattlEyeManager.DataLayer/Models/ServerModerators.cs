using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class ServerModerators
    {
        public int Id { get; set; }

        [Required]
        public int ServerId { get; set; }
        public Server Server { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}