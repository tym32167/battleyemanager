using BattlEyeManager.Core;
using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Repositories.Players;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.DataLayer.Repositories
{
    public class ChatRepository : DisposeObject, IChatRepository
    {
        private readonly AppDbContext context;

        public ChatRepository(AppDbContext context)
        {
            this.context = context;
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            context.Dispose();
        }

        public async Task AddAsync(ChatMessage chatMessage)
        {
            await context.ChatMessages.AddAsync(
                    new Models.ChatMessage()
                    {
                        Date = chatMessage.Date,
                        ServerId = chatMessage.ServerId,
                        Text = chatMessage.Text
                    }
                );

            await context.SaveChangesAsync();
        }

        public Task<ChatMessage[]> GetLastMessages(int serverId, int count)
        {
            return context.ChatMessages.Where(c => c.ServerId == serverId)
                .OrderByDescending(x => x.Date)
                .Take(count)
                .OrderBy(x => x.Date)
                .Select(x => new ChatMessage()
                {
                    Date = x.Date,
                    Id = x.Id, 
                    ServerId = x.ServerId,
                    Text = x.Text
                }).ToArrayAsync();
        }
    }
}