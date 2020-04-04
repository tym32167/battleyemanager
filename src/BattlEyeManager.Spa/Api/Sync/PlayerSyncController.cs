using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Sync
{
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class PlayerSyncController : BaseController
    {
        private readonly PlayerSyncService _playerSyncService;

        public PlayerSyncController(PlayerSyncService playerSyncService)
        {
            _playerSyncService = playerSyncService;
        }

        [HttpGet("api/sync/players")]
        public async Task<IActionResult> Get(int offset, int count)
        {
            if (offset < 0) return BadRequest(nameof(offset));
            if (count < 0 || count > 1000) BadRequest(nameof(count));
            var players = await _playerSyncService.GetPlayers(offset, count);
            var cnt = await _playerSyncService.GetPlayersCount();

            var resp = new PlayerSyncResponse()
            {
                Count = cnt, 
                Players = players
            };



            return Ok(resp);
        }

    }

    public class PlayerSyncResponse
    {
        public int Count { get; set; }
        public PlayerSyncDto[] Players { get; set; }
    }

    public class PlayerSyncDto
    {
        public string SteamId { get; set; }
        public string GUID { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime LastSeen { get; set; }
    }
}