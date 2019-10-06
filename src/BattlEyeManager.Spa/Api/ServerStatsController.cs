using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerStatsController : BaseController
    {
        private readonly ServerStatsRepository _repository;

        public ServerStatsController(ServerStatsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetServersStats()
        {
            var data = await _repository.GetServersStats(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
            var labels = data.Data.Select(x => x.Date).Distinct().OrderBy(x => x).Select(x => x.ToString()).ToArray();
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