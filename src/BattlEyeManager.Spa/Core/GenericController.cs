using AutoMapper;
using BattlEyeManager.DataLayer.Repositories;
using BattlEyeManager.Spa.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BattlEyeManager.Spa.Core
{
    public class GenericController<T, TKey, TModel> : BaseController
            where T : class, new()
    {
        private readonly IGenericRepository<T, TKey> _repository;

        public GenericController(IGenericRepository<T, TKey> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dbItems = await _repository.GetItemsAsync();

            var items = dbItems
                .Select(Mapper.Map<TModel>)
                .ToArray();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(TKey id)
        {
            var item = await _repository.GetItemByIdAsync(id);
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

            var item = new T();

            Mapper.Map(model, item);

            await _repository.UpdateItemAsync(item);

            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(201, Type = typeof(BanReasonModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Put([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = Mapper.Map<T>(model);

            await _repository.AddItemAsync(item);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TKey id)
        {
            await _repository.DeleteItemByIdAsync(id);
            return NoContent();
        }
    }
}