using AutoMapper;
using BattlEyeManager.DataLayer.Context;
using BattlEyeManager.DataLayer.Models;
using BattlEyeManager.Spa.Core;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Api.Admin
{
    [Route("api/[controller]")]
    public class BanReasonController : BaseController
    {
        private readonly AppDbContext _dbContext;

        public BanReasonController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbItems = await _dbContext.BanReasons
                .OrderBy(x => x.Id)
                .ToArrayAsync();

            var items = dbItems
                .Select(Mapper.Map<BanReasonModel>)
                .ToArray();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return NotFound();

            var item = await _dbContext.BanReasons.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var ret = Mapper.Map<BanReasonModel>(item);
            return Ok(ret);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id, [FromBody] BanReasonModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id <= 0) return NotFound();

            var item = await _dbContext.BanReasons.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            Mapper.Map(model, item);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(201, Type = typeof(BanReasonModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] BanReasonModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = Mapper.Map<BanReason>(model);
            _dbContext.BanReasons.Add(item);
            await _dbContext.SaveChangesAsync();


            return CreatedAtAction(nameof(Get), new { id = item.Id }, await Get(item.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return NotFound();

            var item = await _dbContext.BanReasons.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _dbContext.BanReasons.Remove(item);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}