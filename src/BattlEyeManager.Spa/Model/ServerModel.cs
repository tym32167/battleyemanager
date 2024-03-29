﻿using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.Spa.Model
{
    public class ServerModel
    {
        public int Id { get; set; }

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

        public bool WelcomeFeatureEnabled { get; set; }
        public string WelcomeFeatureTemplate { get; set; }
        public string WelcomeFeatureEmptyTemplate { get; set; }
        public string WelcomeGreater50MessageTemplate { get; set; }

        public bool ThresholdFeatureEnabled { get; set; }
        public int ThresholdMinHoursCap { get; set; }        
        public string ThresholdFeatureMessageTemplate { get; set; }
    }

    public class ServerSimpleModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}