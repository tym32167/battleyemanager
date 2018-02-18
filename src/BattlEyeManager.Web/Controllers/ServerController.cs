﻿using BattlEyeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ServerController : Controller
    {
        private readonly IKeyValueStore<ServerModel, Guid> _store;

        public ServerController(IKeyValueStore<ServerModel, Guid> store)
        {
            _store = store;
        }

        // GET: Server
        public async Task<IActionResult> Index()
        {
            var item = await _store.AllAsync();
            return View(item.ToArray());
        }

        // GET: Server/Create
        public ActionResult Create()
        {
            return View(new ServerModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServerModel server)
        {
            if (ModelState.IsValid)
            {
                await _store.AddAsync(server);
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(server);
        }

        // GET: Server/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _store.FindAsync(id);
            return View(item);
        }

        // POST: Server/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServerModel server)
        {
            if (ModelState.IsValid)
            {
                await _store.UpdateAsync(server);
                return RedirectToAction("Index");
            }

            return View(server);
        }

        // GET: Server/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            await _store.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}