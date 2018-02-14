using BattlEyeManager.Web.Models;
using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IUserService _userService;

        public UserRegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            await _userService.RegisterUser(userModel);
            return Content($"User {userModel.FirstName} { userModel.LastName} has been registered sucessfully");
        }
    }
}