using AutoMapper;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/server/{serverId}/scripts")]
    [Produces("application/json")]
    public class ServerScriptController : BaseController
    {
        private readonly ServerScriptRepository _repository;

        public ServerScriptController(ServerScriptRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int serverId)
        {
            var dbItems = await _repository.GetByServerAsync(serverId);

            var items =
                dbItems.Select(Mapper.Map<ServerScript, ServerScriptModel>)
                .OrderBy(x => x.Name)
                .ToArray();

            return Ok(items);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int serverId, int id, [FromBody] ServerScriptModel item)
        {
            item.Id = id;
            item.ServerId = serverId;
            var db = Mapper.Map<ServerScriptModel, ServerScript>(item);
            await _repository.UpdateAsync(db);
            var refreshed = await _repository.GetByIdAsync(db.Id);
            var ret = Mapper.Map<ServerScript, ServerScriptModel>(refreshed);
            return Ok(ret);
        }


        [HttpPut]
        public async Task<IActionResult> Put(int serverId, [FromBody] ServerScriptModel item)
        {
            item.ServerId = serverId;
            var db = Mapper.Map<ServerScriptModel, ServerScript>(item);
            await _repository.AddAsync(db);
            var refreshed = await _repository.GetByIdAsync(db.Id);
            var ret = Mapper.Map<ServerScript, ServerScriptModel>(refreshed);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }

        public static string Bash(string cmd)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = cmd,
                    Arguments = "",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        [HttpPost("{id}/run")]
        public async Task<IActionResult> Post(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            var ret = await Task.Run(() => Bash(item.Path));
            return Ok(ret);
        }
    }
}