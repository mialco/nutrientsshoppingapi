using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NutrientsShoppingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
		[HttpGet]
		public JsonResult GetAll()
		{
			return null;
		}

		[HttpPost (Name ="Insert New Category")]
		public JsonResult Create(string categoryName, string categoryDescription)
		{
			return null;
		}

		[HttpDelete]
		public JsonResult Delete(string categoryName)
		{
			return null;
		}
    }
}