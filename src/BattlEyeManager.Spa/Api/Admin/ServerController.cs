using BattlEyeManager.BE.Services;
using BattlEyeManager.Core.DataContracts.Repositories;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Infrastructure.Featues;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ServerController : BaseController // : GenericController<Server, int, ServerModel>
    {
        private readonly IServerRepository _repository;
        private readonly IBeServerAggregator _beServerAggregator;
        private readonly WelcomeFeature _welcomeFeature;
        private readonly IMapper _mapper;

        public ServerController(IServerRepository repository, IBeServerAggregator beServerAggregator, WelcomeFeature welcomeFeature, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _beServerAggregator = beServerAggregator;
            _welcomeFeature = welcomeFeature;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbItems = (await _repository.GetAllServers())
                .OrderBy(x => x.Name)
                .ToArray();

            var items = dbItems
                .Select(_mapper.Map<ServerModel>)
                .ToArray();

            return Ok(items);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromBody] ServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isActive = model.Active;
            if (id <= 0) return NotFound();

            var ret = await _repository.Update(_mapper.Map<BattlEyeManager.Core.DataContracts.Models.Server>(model));

            if (isActive)
                _beServerAggregator.AddServer(_mapper.Map<ServerInfo>(model));
            else
                _beServerAggregator.RemoveServer(model.Id);

            await _welcomeFeature.SetEnabled(_mapper.Map<WelcomeServerSettings>(model));

            return Ok(ret);
        }


        [HttpPut]
        [ProducesResponseType(201, Type = typeof(ServerModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] ServerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _mapper.Map<Server>(model);

            await base.Put(model);

            if (item.Active)
                _beServerAggregator.AddServer(_mapper.Map<ServerInfo>(model));

            await _welcomeFeature.SetEnabled(_mapper.Map<ServerInfoDto>(model));

            return CreatedAtAction(nameof(Get), new { id = item.Id }, await Get(item.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();

            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.WelcomeFeatureEnabled = false;

            await _welcomeFeature.SetEnabled(item);
            _beServerAggregator.RemoveServer(id);
            return await base.Delete(id);
        }
    }
}