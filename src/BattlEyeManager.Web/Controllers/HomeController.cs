using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}