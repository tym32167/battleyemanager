using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Repositories
{
    public interface IServerRepository : IGenericRepository<Server, int>
    {

    }

    public class ServerRepository : GenericRepository<Server, int>, IServerRepository
    {
        private readonly AppDbContext _context;

        public ServerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }     
    }


    public class ServerInfoDto
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

        [MaxLength(255)]
        public string WelcomeFeatureTemplate { get; set; }

        [MaxLength(255)]
        public string WelcomeFeatureEmptyTemplate { get; set; }

        [MaxLength(255)]
        public string WelcomeGreater50MessageTemplate { get; set; }


        public bool ThresholdFeatureEnabled { get; set; }
        public int ThresholdMinHoursCap { get; set; }

        [MaxLength(255)]
        public string ThresholdFeatureMessageTemplate { get; set; }        
    }
}