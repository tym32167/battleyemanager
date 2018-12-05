using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using BattlEyeManager.Spa.Services;
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
        private readonly ServerStateService _serverStateService;

        public OnlineServerController(AppDbContext dbContext, ServerStateService serverStateService)
        {
            _dbContext = dbContext;
            _serverStateService = serverStateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbItems = await _dbContext.Servers
                .Where(x => x.Active)
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            var items = dbItems
                .Select(x => Update(Mapper.Map<OnlineServerModel>(x)))
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
            return Ok(Update(ret));
        }

        private OnlineServerModel Update(OnlineServerModel input)
        {
            input.PlayersCount = _serverStateService.GetPlayersCount(input.Id);
            input.AdminsCount = _serverStateService.GetAdminsCount(input.Id);
            input.BansCount = _serverStateService.GetBansCount(input.Id);
            input.IsConnected = _serverStateService.IsConnected(input.Id);
            return input;
        }
    }
}