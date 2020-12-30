using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Constants;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Core.Mapping;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Spa.Api.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class KickReasonController : GenericController<KickReason, int, KickReasonModel>
    {
        public KickReasonController(IGenericRepository<KickReason, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}