using AutoMapper;
using BattlEyeManager.BE.Services;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Featues;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ServerController : GenericController<Server, int, ServerModel>
    {
        private readonly IServerRepository _repository;
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly WelcomeFeature _welcomeFeature;

        public ServerController(IServerRepository repository, IBeServerAggregator beServerAggregator, WelcomeFeature welcomeFeature) : base(repository)
        {
            _repository = repository;
            _beServerAggregator = beServerAggregator;
            _welcomeFeature = welcomeFeature;
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var dbItems = await _repository.GetItems()
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            var items = dbItems
                .Select(Mapper.Map<ServerModel>)
                .ToArray();

            return Ok(items);
        }

        [HttpPost("{id}")]
        public override async Task<IActionResult> Post(int id, [FromBody] ServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isActive = model.Active;
            if (id <= 0) return NotFound();

            var ret = await base.Post(id, model);

            if (isActive)
                _beServerAggregator.AddServer(Mapper.Map<ServerInfo>(model));
            else
                _beServerAggregator.RemoveServer(model.Id);

            _welcomeFeature.SetEnabled(Mapper.Map<ServerInfoDto>(model));

            return ret;
        }


        [HttpPut]
        [ProducesResponseType(201, Type = typeof(ServerModel))]
        [ProducesResponseType(400)]
        public override async Task<IActionResult> Put([FromBody] ServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = Mapper.Map<Server>(model);

            await base.Put(model);

            if (item.Active)
                _beServerAggregator.AddServer(Mapper.Map<ServerInfo>(model));

            _welcomeFeature.SetEnabled(Mapper.Map<ServerInfoDto>(model));

            return CreatedAtAction(nameof(Get), new { id = item.Id }, await Get(item.Id));
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();

            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.WelcomeFeatureEnabled = false;

            _welcomeFeature.SetEnabled(item);
            _beServerAggregator.RemoveServer(id);
            return await base.Delete(id);
        }
    }
}