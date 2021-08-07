using System.Threading.Tasks;

namespace BattlEyeManager.Core.DataContracts.Repositories
{
    public interface IUtilRepository : IRepository
    {
        Task InitStore();
    }

}
