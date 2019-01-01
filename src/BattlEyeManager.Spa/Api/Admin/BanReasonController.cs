using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;

namespace BattlEyeManager.Spa.Api.Admin
{
    [Route("api/[controller]")]
    public class BanReasonController : GenericController<BanReason, int, BanReasonModel>
    {
        public BanReasonController(IGenericRepository<BanReason, int> repository) : base(repository)
        {
        }
    }
}