﻿using BattlEyeManager.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BattlEyeManager.Spa.Core.Mapping;

namespace BattlEyeManager.Spa.Core
{
    public class GenericController<T, TKey, TModel> : BaseController
            where T : class, new()
    {
        private readonly IGenericRepository<T, TKey> _repository;
        private readonly IMapper _mapper;

        public GenericController(IGenericRepository<T, TKey> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var dbItems = await _repository.GetItemsAsync();

            var items = dbItems
                .Select(_mapper.Map<TModel>)
                .ToArray();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var ret = _mapper.Map<TModel>(item);
            return Ok(ret);
        }

        [HttpPost("{id}")]
        public virtual async Task<IActionResult> Post(TKey id, [FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = new T();

            _mapper.Map(model, item);

            await _repository.UpdateItemAsync(item);

            return NoContent();
        }


        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> Put([FromBody] TModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _mapper.Map<T>(model);

            await _repository.AddItemAsync(item);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            await _repository.DeleteItemByIdAsync(id);
            return NoContent();
        }
    }
}