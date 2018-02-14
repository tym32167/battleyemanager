using BattlEyeManager.Web.Models;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Services
{
    public class UserService : IUserService
    {
        public Task<bool> RegisterUser(UserModel userModel)
        {
            return Task.FromResult(true);
        }
    }
}