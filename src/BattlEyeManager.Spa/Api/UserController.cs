using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userManager.Users.Select(x => new ApplicationUserModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                LastName = x.LastName,
                FirstName = x.FirstName,
            }).ToArray();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var usr = await _userManager.FindByIdAsync(id);
            if (usr == null)
            {
                return NotFound();
            }

            var ret = new ApplicationUserModel()
            {
                Id = usr.Id,
                UserName = usr.UserName,
                Email = usr.Email,
                LastName = usr.LastName,
                FirstName = usr.FirstName,
            };

            return Ok(ret);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(string id, [FromBody] ApplicationUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Email != model.Email)
            {
                var res = await _userManager.SetEmailAsync(user, model.Email);

                if (!res.Succeeded)
                {
                    foreach (var identityError in res.Errors)
                    {
                        ModelState.AddModelError(String.Empty, identityError.Description);
                    }
                    return BadRequest(ModelState);
                }
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                user = await _userManager.FindByIdAsync(id);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var res = await _userManager.ResetPasswordAsync(user, token, user.Password);

                if (!res.Succeeded)
                {
                    foreach (var identityError in res.Errors)
                    {
                        ModelState.AddModelError(String.Empty, identityError.Description);
                    }
                    return BadRequest(ModelState);
                }
            }

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(ApplicationUserModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] ApplicationUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
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
                return CreatedAtAction(nameof(Get), new { id = user.Id }, Get(user.Id));
            }
            else
            {
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var usr = await _userManager.FindByIdAsync(id);
            if (usr == null)
            {
                return NotFound();
            }

            if (await _userManager.IsInRoleAsync(usr, "Administrator"))
            {
                return BadRequest();
            }

            await _userManager.DeleteAsync(usr);
            return NoContent();
        }
    }
}