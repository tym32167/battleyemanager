using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Auth;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Auth([FromBody] TokenRequest tokenRequest)
        {
            var username = tokenRequest.UserName;
            var password = tokenRequest.Password;

            var principal = await GetIdentity(username, password);
            if (principal == null)
            {
                return StatusCode(400, "Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: principal.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                username = principal.Identity.Name
            };

            return Json(response);
        }

        private async Task<ClaimsPrincipal> GetIdentity(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var check = await _userManager.CheckPasswordAsync(user, password);
                if (check)
                {
                    var principal = await _signInManager.CreateUserPrincipalAsync(user);
                    return principal;
                }
            }
            return null;
        }
    }
}