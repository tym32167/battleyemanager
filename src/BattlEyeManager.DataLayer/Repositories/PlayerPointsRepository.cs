using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class PlayerPointsRepository : IDisposable
    {
        private readonly AppDbContext context;

        public PlayerPointsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context?.Dispose();
        }

        public async Task<decimal> GetPlayerPointsAsync(int serverId, int playerId)
        {
            var playerPoint = await context.PlayerPoints.Where(x => x.PlayerId == playerId && x.ServerId == serverId).FirstOrDefaultAsync();

            if (playerPoint != null) return playerPoint.Balance;

            decimal balance = await GenerateBalance(serverId, playerId);
            var pp = new PlayerPoints() { PlayerId = playerId, ServerId = serverId, Balance = balance };
            await context.PlayerPoints.AddAsync(pp);
            await context.SaveChangesAsync();
            return balance;
        }

        private async Task<decimal> GenerateBalance(int serverId, int playerId)
        {
            decimal balance = 0;

            var history = await context.PlayerPointsHistory.Where(x => x.PlayerId == playerId && x.ServerId == serverId).ToListAsync();
            foreach (var h in history) { balance += h.Balance; }

            var sessions = await context.PlayerSessions.Where(x => x.PlayerId == playerId && x.ServerId == serverId).ToListAsync();
            balance += await GetPlayerPointsFromSessionsAsync(sessions);


            return balance;
        }

        public Task<decimal> GetPlayerPointsFromSessionsAsync(IEnumerable<PlayerSession> sessions)
        {
            double ret = 0;

            foreach (var s in sessions)
            {
                if (!s.EndDate.HasValue) continue;
                var diff = s.EndDate.Value - s.StartDate;
                ret += diff.TotalHours;
            }

            return Task.FromResult(Convert.ToDecimal(ret));
        }

        public async Task RegisterSessionsAsync(IEnumerable<PlayerSession> sessions)
        {
            foreach(var s in sessions.Where(x => x.EndDate.HasValue))
            {
                await RegisterSessionAsync(s);
            }
        }

        private Task RegisterSessionAsync(PlayerSession session)
        {
            return AddBalanceSilent(session.PlayerId, session.ServerId, Convert.ToDecimal((session.EndDate.Value - session.StartDate).TotalHours));
        }

        private async Task AddBalanceSilent(int playerId, int serverId, decimal balance)
        {
            var playerPoint = await context.PlayerPoints.Where(x => x.PlayerId == playerId && x.ServerId == serverId).FirstOrDefaultAsync();
            if (playerPoint == null)
            {
                await GetPlayerPointsAsync(serverId, playerId);
                playerPoint = await context.PlayerPoints.Where(x => x.PlayerId == playerId && x.ServerId == serverId).FirstOrDefaultAsync();
            }
            playerPoint.Balance += balance;
            await context.SaveChangesAsync();            
        }
    }
}
