using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ServerStatsRepository : BaseRepository, IServerStatsRepository
    {
        private readonly AppDbContext _context;

        public ServerStatsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServerStatsResult> GetServersStats(int[] serverIds, DateTime start, DateTime end, TimeSpan step)
        {
            var ret = new ServerStatsResult() { Start = start, End = end };

            var servers = await _context.Servers
                    .Where(x => serverIds.Contains(x.Id))
                    .Select(x => new ServerStatInfo() { Id = x.Id, Name = x.Name })
                    .ToArrayAsync();

            ret.Servers.AddRange(servers);
            var sessions = await _context.PlayerSessions
                .Where(x =>
                    serverIds.Contains(x.ServerId)
                    &&
                    (
                    (x.StartDate > start && x.StartDate < end)
                    || (x.EndDate != null && x.EndDate > start && x.EndDate < end)
                    || (x.StartDate < start && x.EndDate != null && x.EndDate > end)
                    || (x.StartDate < end && x.EndDate == null)
                    )
                ).ToArrayAsync();

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
}