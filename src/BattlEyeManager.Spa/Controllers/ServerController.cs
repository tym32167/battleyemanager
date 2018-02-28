using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ServerController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //[HttpGet("[action]")]
        public Task<ServerInfo[]> Index()
        {
            return _appDbContext
                .Servers
                .OrderBy(x => x.Name)
                .Select(x => new ServerInfo()
                {
                    Id = x.Id,
                    Host = x.Host,
                    Name = x.Name,
                    Port = x.Port
                }).ToArrayAsync();
        }
    }
}