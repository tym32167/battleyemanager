using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}