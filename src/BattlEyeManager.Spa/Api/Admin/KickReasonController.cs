using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Spa.Api.Admin
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class KickReasonController : GenericController<KickReason, int, KickReasonModel>
    {
        public KickReasonController(IGenericRepository<KickReason, int> repository) : base(repository)
        {
        }
    }
}