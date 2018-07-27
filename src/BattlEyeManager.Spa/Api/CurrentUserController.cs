using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    public class CurrentUserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usr = await _userManager.FindByNameAsync(User.Identity.Name);

            if (usr == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(usr);
            var isAdmin = roles.Any(r => r == RoleConstants.Administrator);

            var ret = new ApplicationUserModel()
            {
                Id = usr.Id,
                UserName = usr.UserName,
                Email = usr.Email,
                LastName = usr.LastName,
                FirstName = usr.FirstName,
                IsAdmin = isAdmin
            };


            return Ok(ret);
        }
    }
}