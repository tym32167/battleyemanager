using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Models
{
    public class ServerModel : IEntity<Guid>
    {
        [ReadOnly(true)]
        [HiddenInput]
        [Display(AutoGenerateField = true)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int SteamPort { get; set; }
    }
}