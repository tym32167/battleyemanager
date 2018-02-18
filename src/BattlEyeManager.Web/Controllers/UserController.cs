using BattlEyeManager.Models;
using BattlEyeManager.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseController
    {
        private readonly UserManager<UserModel> _userManager;

        public UserController(UserManager<UserModel> userManager, RoleManager<RoleModel> roleManager)
        {
            _userManager = userManager;
        }

        // GET: User
        public ActionResult Index()
        {
            return View(_userManager.Users.ToArray());
        }


        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {

                var dbuser = await _userManager.FindByNameAsync(user.UserName);

                if (dbuser.Email != user.Email)
                {
                    var res = await _userManager.SetEmailAsync(dbuser, user.Email);

                    if (!res.Succeeded)
                    {
                        foreach (var identityError in res.Errors)
                        {
                            ModelState.AddModelError(String.Empty, identityError.Description);
                        }
                    }
                }

                dbuser = await _userManager.FindByNameAsync(user.UserName);

                if (!string.IsNullOrEmpty(user.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(dbuser);
                    var res = await _userManager.ResetPasswordAsync(dbuser, token, user.Password);

                    if (!res.Succeeded)
                    {
                        foreach (var identityError in res.Errors)
                        {
                            ModelState.AddModelError(String.Empty, identityError.Description);
                        }
                    }
                }

                return RedirectToAction("Index", "User");
            }

            return View();
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string username)
        {
            var usr = await _userManager.FindByNameAsync(username);
            if (usr != null)
                await _userManager.DeleteAsync(usr);

            return RedirectToAction("Index", "User");
        }


        [HttpGet]
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
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
    }
}