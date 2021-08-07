using BattlEyeManager.Core.DataContracts.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IChatRepository : IRepository
    {
        Task AddAsync(ChatMessage chatMessage);
        Task<ChatMessage[]> GetLastMessages(int serverId, int count);
    }

}
