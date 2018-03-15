using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Spa.Core
{
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}