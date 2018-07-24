using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class ServerController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public ServerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _dbContext.Servers.Select(x => Mapper.Map<ServerModel>(x))
                .OrderBy(x => x.Name)
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


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromBody] ServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0) return NotFound();

            var item = await _dbContext.Servers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            Mapper.Map(model, item);

            return NoContent();
        }
    }
}