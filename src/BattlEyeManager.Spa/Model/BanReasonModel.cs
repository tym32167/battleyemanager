using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Spa.Model
{
    public class BanReasonModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}