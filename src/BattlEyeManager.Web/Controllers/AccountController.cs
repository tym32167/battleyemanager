using BattlEyeManager.Models;
using BattlEyeManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<RoleModel> _roleManager;

        public AccountController(UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager, RoleManager<RoleModel> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            await CheckAdminUser();

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, identityError.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }


        private async Task CheckAdminUser()
        {
            var admin = await _userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                var adminModel = new UserModel() { UserName = "admin", Password = "12qw!@QW", LastName = "admin", FirstName = "admin", Email = "admin@admin.sample" };
                var result = await _userManager.CreateAsync(adminModel, adminModel.Password);

                var role = new RoleModel() { Name = "Administrator" };
                await _roleManager.CreateAsync(role);

                await _userManager.AddToRoleAsync(adminModel, "Administrator");
            }
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {


            if (ModelState.IsValid)
            {
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}