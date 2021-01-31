using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BattlEyeManager.Spa.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            this._env = env;
        }
        public IActionResult Index()
        {
            var fname = Path.Combine(_env.WebRootPath, "index.html");
            if (System.IO.File.Exists(fname))
                return new PhysicalFileResult(fname, "text/html");
            return NotFound();
        }
    }
}