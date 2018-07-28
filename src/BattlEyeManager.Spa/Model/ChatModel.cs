using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Spa.Model
{
    public class ChatModel
    {
        [Required]
        public int Audience { get; set; }
        [Required]
        public string Message { get; set; }
    }
}