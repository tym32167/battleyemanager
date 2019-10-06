using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerStatsRepository
    {
        private readonly AppDbContext _context;

        public ServerStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServerStatsResult> GetServersStats(DateTime start, DateTime end)
        {
            var ret = new ServerStatsResult() { Start = start, End = end };

            var servers = await _context.Servers
                    .Where(x => x.Active)
                    .Select(x => new ServerStatInfo() { Id = x.Id, Name = x.Name })
                    .ToArrayAsync();

            ret.Servers.AddRange(servers);
            var sessions = await _context.PlayerSessions
                .Where(x =>
                    (x.StartDate > start && x.StartDate < end)
                    || (x.EndDate != null && x.EndDate > start && x.EndDate < end)
                    || (x.StartDate < start && x.EndDate != null && x.EndDate > end)
                    || (x.StartDate < end && x.EndDate == null)
                ).ToArrayAsync();

            var step = TimeSpan.FromHours(1);

            for (var curr = start; curr < end; curr = curr += step)
            {

                foreach (var s in servers)
                {
                    var cnt = sessions
                        .Where(x => x.ServerId == s.Id)
                        .Count(x => x.StartDate < curr && (x.EndDate == null || x.EndDate > curr));

                    var record = new ServerStatItemResult()
                    {
                        ServerId = s.Id,
                        ServerName = s.Name,
                        PlayerCount = cnt,
                        Date = curr
                    };

                    ret.Data.Add(record);
                }
            }

            return ret;
        }
    }

    public class ServerStatsResult
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<ServerStatInfo> Servers { get; set; } = new List<ServerStatInfo>();

        public List<ServerStatItemResult> Data { get; set; } = new List<ServerStatItemResult>();
    }

    public class ServerStatItemResult
    {
        public DateTime Date { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public int PlayerCount { get; set; }
    }

    public class ServerStatInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}