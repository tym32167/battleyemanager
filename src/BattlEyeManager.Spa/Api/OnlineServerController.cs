using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get()
        {
            var dbItems = await _dbContext.Servers
                .Where(x => x.Active)
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            var items = dbItems
                .Select(Mapper.Map<OnlineServerModel>)
                .ToArray();
            return Ok(items);
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

            var ret = Mapper.Map<OnlineServerModel>(item);

            return Ok(ret);
        }
    }
}