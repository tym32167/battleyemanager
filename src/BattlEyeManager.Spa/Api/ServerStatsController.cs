using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Extensions;
using BattlEyeManager.Spa.Infrastructure.Services;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public async Task<IActionResult> GetServersStats()
        {
            var set = _moderatorService.GetServersByUser(User);
            var admin = User.IsAdmin();
            var onlineServers = (await _onlineServerService.GetOnlineServers())
                .Where(s => admin || set.Contains(s.Id))
                .Select(s => s.Id)
                .ToArray();

            var end = DateTime.UtcNow.Date;
            var start = end.AddDays(-14);


            var data = await _repository.GetServersStats(onlineServers, start, end);
            var labels = data.Data.Select(x => x.Date).Distinct().OrderBy(x => x).Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray();
            var servers = data.Servers.ToDictionary(x => x.Id, x => x.Name);

            var dataset = data.Data
                .GroupBy(x => x.ServerId)
                .Select(x => new DataSet()
                {
                    Label = servers[x.Key],
                    Data = x.OrderBy(z => z.Date).Select(z => z.PlayerCount).ToList()
                }).ToList();

            var ret = new ServerStatsModel()
            {
                Labels = labels,
                DataSets = dataset
            };

            return Ok(ret);
        }
    }
}