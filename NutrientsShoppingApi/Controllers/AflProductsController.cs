using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NutrientsShoppingApi.Repositories;
using NutrientsShoppingApi.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using NutrientsShoppingApi.Models;

namespace NutrientsShoppingApi.Controllers
{
	/// <summary>
	/// In order to identify the authorization, we will use for the begining User.Claim(clientNname) 
	/// User.Claims.Where(x=>x.Type=="client_id").FirstOrDefault().Value
	/// With this we can retrieve the client name and decide based on the user if we allow the respective resource or not
	/// User subject can identify the user id with this code
	/// User.Claims.Where(x=>x.Type=="sub").Select(y=>y.Value).FirstOrDefault()
	/// </summary>
	[Produces("application/json")]
	//[Route("api/[controller]/count")]
	[Route("api/[controller]")]
	[Authorize]
	public class AflProductsController : Controller
	{
		IAflProductRepository _aflProductRepository;
		public AflProductsController(IAflProductRepository aflProductReposittory)
		{
			_aflProductRepository = aflProductReposittory;
		}

		[HttpGet]
		public IEnumerable<AflProduct> GetAll() {
			var v = _aflProductRepository.GetAll();
			return v;
		}

		[HttpGet("count", Name ="AflProductsCount", Order =0)]
		public int Count()
		{
			return 45;
		}


		[HttpGet("{id:int}", Name = "GetAflProduct", Order = 1)]
		public IActionResult GetById(long id)
		{
			var filter = new AflProductFilter { IsActive = true, DateActive = new DateTime(2017, 1, 3) };
			var count = _aflProductRepository.Count(filter);

			var v = _aflProductRepository.Find(id);
			if (v == null)
			{
				return NotFound();
			}
			return new ObjectResult(v);
		}

		[HttpPost(Name="Insert New Product", Order =10)]
		public IActionResult InsertProduct(AflProductItem aflProductItem)
		{
			var x =Request.Form["ClientID"].ToString();
			return new JsonResult("{\"new Object\":\"new object value\" }}");
		}

		[HttpGet("{categoryName}/{page:int}/{pageSize:int}", Name = "AflProductByCategoryName", Order = 1)]
		public IActionResult GetWithFilterCategoryName(string categoryName, int page, int pageSize)
		{
		
			try
			{
				var filter = new AflProductFilter { CategoryName = categoryName, IsActive = true, DateActive = DateTime.Today };
				var productsPage = _aflProductRepository.GetAflProductsPage(page, pageSize, filter);
				if (productsPage == null)
				{
					return new NoContentResult();
				}
				return new JsonResult(productsPage);

			}
			catch (Exception)
			{
				//todo: Log Exception
				return new BadRequestResult();
			}
		}




		/// <summary>
		/// Gets a page with products 
		/// </summary>
		/// <param name="categoryId"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpGet("{categoryId:int}/{page:int}/{pageSize:int}",Name ="AflProductByCategory",Order =1)]
		
		public IActionResult GetWithFilterCategoryId(int categoryId, int page, int pageSize)
		{

			try
			{
				var filter = new AflProductFilter { CategoryId = categoryId, IsActive = true, DateActive = DateTime.Today };
				var productsPage = _aflProductRepository.GetAflProductsPage(page, pageSize, filter);
				if (productsPage == null)
				{
					return new NoContentResult();
				}
				return new JsonResult(productsPage);

			}
			catch (Exception)
			{
				//todo: Log Exception
				return new BadRequestResult();
			}
		}




	}
}