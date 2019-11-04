using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Extensions;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ServerStatsController : BaseController
    {
        private readonly ServerStatsRepository _repository;
        private readonly ServerModeratorService _moderatorService;
        private readonly OnlineServerService _onlineServerService;

        public ServerStatsController(ServerStatsRepository repository, ServerModeratorService moderatorService, OnlineServerService onlineServerService)
        {
            _repository = repository;
            _moderatorService = moderatorService;
            _onlineServerService = onlineServerService;
        }

        [HttpGet("lastday")]
        public async Task<IActionResult> GetServersStatsLastDay()
        {
            var cur = DateTime.UtcNow;
            cur = new DateTime(cur.Year, cur.Month, cur.Day, cur.Hour, cur.Minute - cur.Minute % 10, 0, DateTimeKind.Utc);

            var end = DateTime.UtcNow;
            var start = cur.AddDays(-1);
            var ret = await GetServersStats(start, end, TimeSpan.FromMinutes(10));
            return Ok(ret);
        }

        [HttpGet("lastweek")]
        public async Task<IActionResult> GetServersStatsLastWeek()
        {
            var end = DateTime.UtcNow;
            var start = end.Date.AddDays(-7);
            var ret = await GetServersStats(start, end, TimeSpan.FromHours(1));
            return Ok(ret);
        }

        private async Task<ServerStatsGraphModel> GetServersStats(DateTime start, DateTime end, TimeSpan step)
        {
            var set = _moderatorService.GetServersByUser(User);
            var admin = User.IsAdmin();
            var onlineServers = (await _onlineServerService.GetOnlineServers())
                .Where(s => admin || set.Contains(s.Id))
                .Select(s => s.Id)
                .ToArray();


            var data = await _repository.GetServersStats(onlineServers, start, end, step);
            var labels = data.Data.Select(x => x.Date).Distinct().OrderBy(x => x).ToArray();
            var servers = data.Servers.ToDictionary(x => x.Id, x => x.Name);

            var dataset = data.Data
                .GroupBy(x => x.ServerId)
                .Select(x => new DataSet()
                {
                    Label = servers[x.Key],
                    Data = x.OrderBy(z => z.Date).Select(z => z.PlayerCount).ToList()
                }).ToList();

            var ret = new ServerStatsGraphModel()
            {
                Dates = labels,
                DataSets = dataset
            };

            return ret;
        }
    }
}