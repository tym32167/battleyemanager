using BattlEyeManager.Web.Core;
using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Web.Controllers
{
    public class ActiveServerController : BaseController
    {
        private readonly ServerStateService _serverStateService;

        public ActiveServerController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        // GET: ActiveServer
        public ActionResult Index()
        {
            return View(_serverStateService.GetConnectedServers());
        }
    }
}