using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    public class KickReasonController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public KickReasonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reasons = await _appDbContext
                .KickReasons
                    .OrderBy(x => x.Text)
                    .ToArrayAsync();

            return Ok(reasons);
        }
    }
}