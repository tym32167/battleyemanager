using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories.Players
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private Core.DataContracts.Models.Player ToItem(Player model)
        {
            return new Core.DataContracts.Models.Player()
            {
                Id = model.Id,
                Comment = model.Comment,
                GUID = model.GUID,
                IP = model.IP,
                LastSeen = model.LastSeen,
                Name = model.Name,
                SteamId = model.SteamId
            };
        }

        public async Task RegisterJoinedPlayers(int serverId, Core.DataContracts.Models.Player[] joined)
        {
            if (!joined.Any()) return;

            var guidIds = joined.Select(x => x.GUID).ToArray();
            var dbPlayers = await _dbContext.Players.Where(x => guidIds.Contains(x.GUID)).ToListAsync();
            var pdict = joined.ToDictionary(x => x.GUID);

            var newPlayers = joined.Where(p => dbPlayers.All(z => z.GUID != p.GUID))
                .Select(p => new Player
                {
                    GUID = p.GUID,
                    IP = p.IP,
                    LastSeen = DateTime.UtcNow,
                    Name = p.Name,
                })
                .ToArray();

            await _dbContext.Players.AddRangeAsync(newPlayers);


            foreach (var dbPlayer in dbPlayers)
            {
                if (pdict.ContainsKey(dbPlayer.GUID))
                {
                    var p = pdict[dbPlayer.GUID];
                    dbPlayer.Name = p.Name;
                    dbPlayer.IP = p.IP;
                    dbPlayer.LastSeen = DateTime.UtcNow;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> PlayersTotalCount()
        {
            return await _dbContext.Players.CountAsync();
        }

        public async Task<Core.DataContracts.Models.Player[]> GetPlayers(int skip, int take)
        {
            return await _dbContext.Players.OrderBy(x => x.Id).Skip(skip).Take(take).Select(x => ToItem(x)).ToArrayAsync();
        }

        public Task ImportPlayers(Core.DataContracts.Models.Player[] request)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, Core.DataContracts.Models.Player>> GetPlayers(string[] guids)
        {
            throw new NotImplementedException();
            // return _playersCache.GetPlayers(guids);
        }

        public async Task<Core.DataContracts.Models.Player> GetById(int playerId)
        {
            var ret = await _dbContext.Players.FindAsync(playerId);
            return ToItem(ret);
        }
    }
}