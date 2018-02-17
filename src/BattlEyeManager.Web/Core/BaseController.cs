using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Web.Core
{
    [Authorize]
    public abstract class BaseController : Controller
    {

    }
}