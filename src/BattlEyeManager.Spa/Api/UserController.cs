using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BattlEyeManager.Spa.Api
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ApplicationUserModel[] Get()
        {
            return _userManager.Users.Select(x => new ApplicationUserModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
            }).ToArray();
        }

        [HttpGet("{id}")]
        public ApplicationUserModel Get(string id)
        {
            return _userManager.Users
            .Where(x => x.Id == id)
            .Select(x => new ApplicationUserModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
            }).FirstOrDefault();
        }
    }
}