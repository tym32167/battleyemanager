using BattlEyeManager.Core;
using BattlEyeManager.Spa.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    // public class UtilController : BaseController
    public class UtilController : Controller
    {
        private readonly IIpService ipService;

        public UtilController(IIpService service)
        {
            this.ipService = service;
        }

        [HttpGet("ip")]
        public async Task<IActionResult> CheckIP()
        {
            var remoteIpAddress = this.Request.HttpContext.Connection.RemoteIpAddress;

            var country = ipService.GetCountry(remoteIpAddress.ToString());

            return Ok(new { IP = remoteIpAddress.ToString(), country = country });
        }
    }
}
