using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServerController : Controller
    {
        private readonly AppDbContext _appContext;
        private readonly IBeServerAggregator _beServerAggregator;

        public ServerController(AppDbContext appContext, IBeServerAggregator beServerAggregator)
        {
            _appContext = appContext;
            _beServerAggregator = beServerAggregator;
        }

        // GET: Server
        public async Task<IActionResult> Index()
        {
            var item = await _appContext.Servers.ToListAsync();
            return View(item.ToArray());
        }

        // GET: Server/Create
        public ActionResult Create()
        {
            return View(new Server());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Server server)
        {
            if (ModelState.IsValid)
            {
                await _appContext.Servers.AddAsync(server);
                await _appContext.SaveChangesAsync();

                if (server.Active)
                    _beServerAggregator.AddServer(new ServerInfo()
                    {
                        Id = server.Id,
                        Password = server.Password,
                        Port = server.Port,
                        Host = server.Host,
                        Name = server.Name
                    });

                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(server);
        }

        // GET: Server/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _appContext.Servers.FindAsync(id);
            return View(item);
        }

        // POST: Server/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Server server)
        {
            if (ModelState.IsValid)
            {
                _appContext.Servers.Update(server);
                await _appContext.SaveChangesAsync();

                if (server.Active)
                    _beServerAggregator.AddServer(new ServerInfo()
                    {
                        Id = server.Id,
                        Password = server.Password,
                        Port = server.Port,
                        Host = server.Host,
                        Name = server.Name
                    });
                else
                {
                    _beServerAggregator.RemoveServer(server.Id);
                }

                return RedirectToAction("Index");
            }

            return View(server);
        }

        // GET: Server/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            _beServerAggregator.RemoveServer(id);
            _appContext.Servers.Remove(_appContext.Servers.Find(id));
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}