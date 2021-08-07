using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task UpdateUserDisplayName(string userId, string displayName);
    }
}
