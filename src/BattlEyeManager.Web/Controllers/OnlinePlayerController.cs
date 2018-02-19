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
        public ActionResult Index(Guid serverId)
        {
            var players = _serverStateService.GetPlayers(serverId);
            _serverStateService.RefreshPlayers(serverId);
            return View(players);
        }
    }
}