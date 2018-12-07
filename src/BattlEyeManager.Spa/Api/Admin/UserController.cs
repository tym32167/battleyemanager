using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected UserController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = new List<ApplicationUserModel>();

            foreach (var user in _userManager.Users) users.Add(await ToModel(user));

            var result = users
                .OrderBy(x => x.UserName)
                .ToArray();

            return Ok(result);
        }

        private async Task<ApplicationUserModel> ToModel(ApplicationUser usr)
        {
            var ret = new ApplicationUserModel
            {
                Id = usr.Id,
                UserName = usr.UserName,
                Email = usr.Email,
                LastName = usr.LastName,
                FirstName = usr.FirstName,
                IsAdmin = await _userManager.IsInRoleAsync(usr, RoleConstants.Administrator)
            };

            return ret;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var usr = await _userManager.FindByIdAsync(id);
            if (usr == null) return NotFound();

            var ret = await ToModel(usr);

            return Ok(ret);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] ApplicationUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (user.Email != model.Email)
            {
                var res = await _userManager.SetEmailAsync(user, model.Email);

                if (!res.Succeeded)
                {
                    foreach (var identityError in res.Errors)
                        ModelState.AddModelError("errors", identityError.Description);
                    return BadRequest(ModelState);
                }
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                user = await _userManager.FindByIdAsync(id);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var res = await _userManager.ResetPasswordAsync(user, token, model.Password);

                if (!res.Succeeded)
                {
                    foreach (var identityError in res.Errors)
                        ModelState.AddModelError("errors", identityError.Description);
                    return BadRequest(ModelState);
                }
            }

            if (model.IsAdmin)
                await _userManager.AddToRoleAsync(user, RoleConstants.Administrator);
            else
                await _userManager.RemoveFromRoleAsync(user, RoleConstants.Administrator);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(ApplicationUserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] ApplicationUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = model.Password
            };


            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                if (model.IsAdmin) await _userManager.AddToRoleAsync(user, RoleConstants.Administrator);

                return CreatedAtAction(nameof(Get), new { id = user.Id }, await Get(user.Id));
            }

            foreach (var identityError in result.Errors) ModelState.AddModelError("errors", identityError.Description);
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var usr = await _userManager.FindByIdAsync(id);
            if (usr == null) return NotFound();

            if (await _userManager.IsInRoleAsync(usr, RoleConstants.Administrator)
                &&
                (await _userManager.GetUsersInRoleAsync(RoleConstants.Administrator)).Count == 1)
                return BadRequest(new { error = "It is not possible to delete last Administrator" });

            await _userManager.DeleteAsync(usr);
            return NoContent();
        }
    }
}