using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    public class OnlineServerController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public OnlineServerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _dbContext.Servers
                .Where(x => x.Active)
                .OrderBy(x => x.Name)
                .ToArray()
                .Select(x => Mapper.Map<ServerModel>(x))
                .ToArray();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return NotFound();

            var item = await _dbContext.Servers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var ret = Mapper.Map<ServerModel>(item);
            return Ok(ret);
        }        
    }
}