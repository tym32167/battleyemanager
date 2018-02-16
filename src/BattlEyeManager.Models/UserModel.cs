using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattlEyeManager.Models
{
    public class UserModel : IdentityUser<Guid>
    {
        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "FirstNameRequired")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "LastNameRequired")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "PasswordRequired"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "UserNameRequired")]
        public override string UserName { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "EmailRequired"), DataType(DataType.EmailAddress)]
        public override string Email { get; set; }

        [NotMapped]
        public bool IsEmailConfirmed
        {
            get { return EmailConfirmed; }
        }
        public System.DateTime? EmailConfirmationDate { get; set; }
        public int Score { get; set; }

        public override Guid Id { get; set; } = Guid.NewGuid();
    }
}