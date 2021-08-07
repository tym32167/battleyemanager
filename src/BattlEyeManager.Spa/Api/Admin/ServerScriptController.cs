using BattlEyeManager.Core.DataContracts.Models;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
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
        private readonly IServerScriptRepository _repository;
        private readonly IMapper _mapper;

        public ServerScriptController(IServerScriptRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int serverId)
        {
            var dbItems = await _repository.GetByServerAsync(serverId);

            var items =
                dbItems.Select(_mapper.Map<ServerScript, ServerScriptModel>)
                .OrderBy(x => x.Name)
                .ToArray();

            return Ok(items);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int serverId, int id, [FromBody] ServerScriptModel item)
        {
            item.Id = id;
            item.ServerId = serverId;
            var db = _mapper.Map<ServerScriptModel, ServerScript>(item);
            await _repository.Update(db);
            var refreshed = await _repository.GetById(db.Id);
            var ret = _mapper.Map<ServerScript, ServerScriptModel>(refreshed);
            return Ok(ret);
        }


        [HttpPut]
        public async Task<IActionResult> Put(int serverId, [FromBody] ServerScriptModel item)
        {
            item.ServerId = serverId;
            var db = _mapper.Map<ServerScriptModel, ServerScript>(item);
            await _repository.Add(db);
            var refreshed = await _repository.GetById(db.Id);
            var ret = _mapper.Map<ServerScript, ServerScriptModel>(refreshed);
            return Ok(ret);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
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
            var item = await _repository.GetById(id);
            var ret = await Task.Run(() => Bash(item.Path));
            return Ok(ret);
        }
    }
}