using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Spa.Model
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}