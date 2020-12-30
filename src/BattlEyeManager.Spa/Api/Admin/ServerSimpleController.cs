using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Core.Mapping;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Authorize(Roles = RoleConstants.Administrator)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ServerSimpleController : BaseController
    {
        private readonly IServerRepository _repository;
        private readonly IMapper _mapper;

        public ServerSimpleController(IServerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var ret = _mapper.Map<ServerSimpleModel>(item);
            return Ok(ret);
        }
    }
}