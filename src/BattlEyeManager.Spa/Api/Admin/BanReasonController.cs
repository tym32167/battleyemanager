using BattlEyeManager.Core.DataContracts.Models.Values;
using BattlEyeManager.Core.DataContracts.Repositories;
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
    public class BanReasonController : GenericController<BanReason, int, BanReasonModel>
    {
        public BanReasonController(IBanReasonRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}