using System.Threading.Tasks;
using BattlEyeManager.Web.Models;

namespace BattlEyeManager.Web.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserModel userModel);
    }
}