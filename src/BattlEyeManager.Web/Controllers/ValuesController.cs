using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BattlEyeManager.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/items/GetLatest/5         
        [HttpGet("GetLatest/{num}")]
        public JsonResult GetLatest(int num)
        {
            var arr = new List<ItemViewModel>();
            for (int i = 1; i <= num; i++) arr.Add(new ItemViewModel()
            {
                Id = i,
                Title = String.Format("Item {0} Title", i),
                Description = String.Format("Item {0} Description", i)
            });

            var settings = new JsonSerializerSettings()
            {
               Formatting = Formatting.Indented
            }; return new JsonResult(arr, settings);
        }

        [JsonObject(MemberSerialization.OptOut)]
        public class ItemViewModel
        {
            #region Constructor         
            public ItemViewModel() { }
            #endregion Constructor          

            #region Properties        


            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Text { get; set; }
            public string Notes { get; set; }

            [DefaultValue(0)]
            public int Type { get; set; }

            [DefaultValue(0)]
            public int Flags { get; set; }
            public string UserId { get; set; }

            [JsonIgnore]
            public int ViewCount { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime LastModifiedDate { get; set; }
            #endregion Properties     
        }
    }
}