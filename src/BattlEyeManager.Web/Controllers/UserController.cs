using BattlEyeManager.Models;
using BattlEyeManager.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> Index()
        {
            return View(_userManager.Users.ToArray());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
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