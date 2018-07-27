using BattlEyeManager.Web.Core;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}