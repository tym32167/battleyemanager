using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.DataLayer.Context;
using System.ComponentModel.DataAnnotations;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerRepository : GenericRepository<Server, int, ServerInfoDto, int>
    {
        public ServerRepository(AppDbContext context) : base(context)
        {
        }

        protected override Server ToItem(ServerInfoDto model)
        {
            return new Server()
            {
                Id = model.Id,
                Host = model.Host,
                Port = model.Port,
                SteamPort = model.SteamPort,
                Password = model.Password,
                Name = model.Name,
                Active = model.Active,
            };
        }

        protected override int ToItemKey(int modelKey)
        {
            return modelKey;
        }

        protected override ServerInfoDto ToModel(Server item)
        {
            return new ServerInfoDto()
            {
                Id = item.Id,
                Host = item.Host,
                Port = item.Port,
                SteamPort = item.SteamPort,
                Password = item.Password,
                Name = item.Name,
                Active = item.Active,
            };
        }

        protected override int ToModelKey(int itemKey)
        {
            return itemKey;
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
    }
}