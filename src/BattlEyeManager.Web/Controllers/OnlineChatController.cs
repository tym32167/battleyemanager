using BattlEyeManager.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public ActionResult Index(int serverId)
        {
            return View(_serverStateService.GetChat(serverId));
        }


        [HttpPost]
        public ActionResult Post(int serverId, string chatMessage)
        {
            _serverStateService.PostChat(serverId, chatMessage);

            Debug.WriteLine(serverId);
            Debug.WriteLine(chatMessage);

            return RedirectToAction("Index", new { serverId });
        }
    }
}