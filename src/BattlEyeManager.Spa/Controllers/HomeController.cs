using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BattlEyeManager.Spa.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment env;
        public HomeController(IHostingEnvironment env)
        {
            this.env = env;
        }
        public IActionResult Index()
        {
            var fname = Path.Combine(env.WebRootPath, "index.html");
            if (System.IO.File.Exists(fname))
                return new PhysicalFileResult(fname, "text/html");
            return NotFound();
        }
    }
}