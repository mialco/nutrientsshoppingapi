using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NutrientsShoppingApi.DAL;

namespace NutrientsShoppingApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		LimanContext _limanContext;
		public ValuesController(LimanContext limanContext)
		{
			_limanContext = limanContext;
		}

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
			//var v1 =  await _limanContext.AflProducts.Select(x => x.ProductName).Take(20).ToListAsync();
			var v1 =  _limanContext.AflProducts.Select(x => x.ProductName).Take(20).ToList();

			return v1;
			//return new string[] { "value1", "value2" };
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
    }
}
