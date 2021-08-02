
using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories.Players
{
    public class PlayerRepository : DisposeObject, IPlayerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly PlayersCache _playersCache;

        public PlayerRepository(AppDbContext dbContext, PlayersCache playersCache)
        {
            _dbContext = dbContext;
            _playersCache = playersCache;
        }

        public async Task<Player> GetById(int id)
        {
            var player = await _dbContext.Players
                    .FirstOrDefaultAsync(x => x.Id == id);
            return player;
        }


        public async Task AddNoteToPlayer(string playerGuid, string author, string note, string comment = null)
        {
            var player = await _dbContext.Players
                    .FirstOrDefaultAsync(x => x.GUID == playerGuid);
            if (player != null)
            {
                _dbContext.PlayerNotes.Add(new PlayerNote()
                {
                    Author = author,
                    Date = DateTime.UtcNow,
                    PlayerId = player.Id,
                    Text = note
                });

                if (comment != null) player.Comment = $"{player.Comment} | {comment}";
            }

            await _dbContext.SaveChangesAsync();

            await _playersCache.Reload(player);
        }


        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}