using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Spa.Core
{
    [Authorize]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
    }
}