using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class PlayerNoteRepository : BaseRepository, IPlayerNoteRepository
    {
        private readonly AppDbContext context;

        public PlayerNoteRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddNoteToPlayer(string playerGuid, string author, string note, string comment = null)
        {
            var player = await context.Players
                    .FirstOrDefaultAsync(x => x.GUID == playerGuid);
            if (player != null)
            {
                context.PlayerNotes.Add(new PlayerNote()
                {
                    Author = author,
                    Date = DateTime.UtcNow,
                    PlayerId = player.Id,
                    Text = note
                });

                if (comment != null) player.Comment = $"{player.Comment} | {comment}";
            }

            await context.SaveChangesAsync();

            // TODO: add cache refresh 
            // await _playersCache.Reload(player);
        }
    }
}
