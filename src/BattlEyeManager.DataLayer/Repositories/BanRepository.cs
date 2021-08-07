using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class BanRepository : BaseRepository, IBanRepository
    {
        private readonly AppDbContext context;

        public BanRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        private Models.ServerBan ToModel(ServerBan item)
        {
            return new Models.ServerBan()
            {
                Id = item.Id,
                CloseDate = item.CloseDate,
                Date = item.Date,
                GuidIp = item.GuidIp,
                IsActive = item.IsActive,
                Minutes = item.Minutes,
                MinutesLeft = item.MinutesLeft,
                Num = item.Num,
                PlayerId = item.PlayerId,
                Reason = item.Reason,
                ServerId = item.ServerId
            };
        }

        public async Task RegisterActualBans(int serverId, ServerBan[] actualBans)
        {
            var all = actualBans.Select(x => ToModel(x)).ToArray();
            var dbBans = await context.ServerBans.Where(x => x.IsActive && x.ServerId == serverId).ToListAsync();

            foreach (var serverBan in dbBans.Where(b => !all.Any(r => r.GuidIp == b.GuidIp && r.Reason == b.Reason && r.Num == b.Num)))
            {
                serverBan.IsActive = false;
                serverBan.CloseDate = DateTime.UtcNow;
            }

            var toAdd = all.Where(b => !dbBans.Any(r => r.GuidIp == b.GuidIp && r.Reason == b.Reason && r.Num == b.Num))
                .Select(b => new Models.ServerBan
                {
                    Date = DateTime.UtcNow,
                    GuidIp = b.GuidIp,
                    IsActive = true,
                    Minutes = b.MinutesLeft,
                    MinutesLeft = b.MinutesLeft,
                    Num = b.Num,
                    Reason = b.Reason,
                    ServerId = serverId
                });

            await context.ServerBans.AddRangeAsync(toAdd);
            await context.SaveChangesAsync();
        }
    }
}