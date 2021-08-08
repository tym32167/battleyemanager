using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IPlayerNoteRepository : IRepository
    {
        Task AddNoteToPlayer(string playerGuid, string author, string note, string comment = null);
    }

}
