using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BattlEyeManager.Web.Controllers
{
    public class OnlinePlayerController : Controller
    {
        private readonly ServerStateService _serverStateService;

        public OnlinePlayerController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        // GET: OnlinePlayer
        public ActionResult Index(int serverId)
        {
            var players = _serverStateService.GetPlayers(serverId);
            return View(players);
        }
    }
}