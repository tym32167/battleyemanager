using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BattlEyeManager.Web.Controllers
{
    public class OnlineChatController : Controller
    {
        private readonly ServerStateService _serverStateService;

        public OnlineChatController(ServerStateService serverStateService)
        {
            _serverStateService = serverStateService;
        }

        // GET: OnlineChat
        public ActionResult Index(Guid serverId)
        {
            return View(_serverStateService.GetChat(serverId));
        }
    }
}