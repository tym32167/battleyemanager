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



        //private ServerInfoDto ToDto(Server info)
        //{
        //    return new ServerInfoDto()
        //    {
        //        Id = info.Id,
        //        Host = info.Host,
        //        Port = info.Port,
        //        SteamPort = info.SteamPort,
        //        Password = info.Password,
        //        Name = info.Name,
        //        Active = info.Active,
        //    };
        //}

        //private Server FromDto(ServerInfoDto info)
        //{
        //    return new Server()
        //    {
        //        Id = info.Id,
        //        Host = info.Host,
        //        Port = info.Port,
        //        SteamPort = info.SteamPort,
        //        Password = info.Password,
        //        Name = info.Name,
        //        Active = info.Active,
        //    };
        //}
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
    }
}