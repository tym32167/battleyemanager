using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.Spa.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    public class BanReasonController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public BanReasonController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reasons = await _appDbContext
                .BanReasons
                .OrderBy(x => x.Text)
                .ToArrayAsync();

            return Ok(reasons);
        }
    }
}