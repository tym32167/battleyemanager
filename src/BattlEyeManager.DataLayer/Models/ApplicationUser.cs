using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastNameRequired")]
        public string LastName { get; set; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserNameRequired")]
        public override string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "EmailRequired"), DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [Display(Name = "Display name")]
        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }

        public ICollection<ServerModerators> Servers { get; set; }
    }
}