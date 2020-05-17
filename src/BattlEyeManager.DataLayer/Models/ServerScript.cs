using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class ServerScript
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Path { get; set; }
    }
}