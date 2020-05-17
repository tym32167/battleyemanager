using System;
using System.Linq;
using System.Security.Claims;
using BattlEyeManager.Spa.Constants;

namespace BattlEyeManager.Spa.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            var isAdmin = ((ClaimsIdentity)user.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Any(x => String.Compare(RoleConstants.Administrator, x.Value, StringComparison.Ordinal) == 0);
            return isAdmin;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userId = ((ClaimsIdentity)user.Identity).Claims
                .Where(c => c.Type == "id")
                .Select(x => x.Value)
                .Single();
            return userId;
        }
    }
}