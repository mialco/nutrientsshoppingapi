using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NutrientsShoppingApi.DAL.Models;
using NutrientsShoppingApi.DAL;
using NutrientsShoppingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace NutrientsShoppingApi.Repositories
{
	public class AflProductRepository : IAflProductRepository
	{
		private LimanContext _context;

		public AflProductRepository(LimanContext context)
		{
			_context = context;
		}

		public AflProductItemWithDetails Find(long productId)
		{
			try
			{
				
					var v = _context.AflProducts.Where(x => x.ProdID == productId).
					Include(p => p.AflProductDetail)
					.Include(pc=>pc.AflProducts_Categories)
					.ThenInclude(c=>c.AflProductCategorie)
					.FirstOrDefault();

				if (v != null)
				{

					if (v.AflProductDetail == null)
					{
						v.AflProductDetail = new AflProductDetail();
					}
					var prod = new AflProductItemWithDetails
					{
						prodId = v.ProdID,
						productName = v.ProductName,
						description = v.Description,
						navigateUrl = v.NavigateUrl,
						imgUrl = v.ImgUrl,
						imgAlt = v.ImgAlt,
						imgDescription = v.ImgDescription,
						additionalInfoTitle = v.AdditionalInfoTitle,
						additionalInfoUrl = v.AdditionalInfoUrl,
						recordStatus = v.RecordStatus,
						linkCode = v.LinkCode,
						advertiser = v.Advertiser,
						price = v.Price,
						advertizerLinkID = v.AdvertizerLinkID,
						desc1 = v.AflProductDetail.Description1,
						desc2 = v.AflProductDetail.Description2,
						detailIsActive = v.AflProductDetail.IsActive.HasValue ? v.AflProductDetail.IsActive.Value : false,
						articleTitle = v.AflProductDetail.ArticleTitle,
						metaDescription = v.AflProductDetail.MetaDescription,
						metaKeywords = v.AflProductDetail.MetaKeywords,
						title = v.AflProductDetail.Title	,
						categories = v.AflProducts_Categories == null ? null : v.AflProducts_Categories.Select(x=>x.AflProductCategorie.ProductCategoryName).ToList()
					};
					return prod;
				}
				return null;
			}
			catch (Exception ex)
			{
				//todo: log error
				Console.WriteLine(ex);
				return null;
			}

		}

		public IEnumerable<AflProduct> GetAll()
		{
			//var v1 = _context.AflProducts.Where(x => x.AflProducts_Categorie.CategoryID == 27).ToList();
			var v2 = _context.AflProducts_Categories.Where(x => x.CategoryID == 27).Select(x => x.AflProduct).ToList();

			var v3 = _context.AflProductCategories.ToList();

			return v2;

		}


		public AflProductsPage GetAflProductsPage(int page, int pageSize, AflProductFilter filter)
		{

			try
			{

				IQueryable<AflProducts_Categorie> qryProducts = _context.AflProducts_Categories;
				if (filter != null)
				{
					qryProducts = qryProducts.Where(x => x.AflProduct.IsActive == filter.IsActive);
					if (filter.CategoryId > 0)
					{
						qryProducts = qryProducts.Where(x => x.CategoryID == filter.CategoryId);
					}
					if (!string.IsNullOrEmpty(filter.CategoryName))
					{
						qryProducts = qryProducts.Where(x =>  (x.AflProductCategorie.ProductCategoryName ?? string.Empty).ToLower() == filter.CategoryName.ToLower()); 
					}
					//TODO: Filter by active dates
				}
				var totalRecords = qryProducts.Count();

				if (totalRecords == 0)
				{
					return null;
				}

				//If page <= 0 is passed we respond with page 1
				page = page <= 0 ? 1 : page;
				// If page size  is 0 or less we will make it 10
				//TODO: Get the default page size from thr configuration
				pageSize = pageSize <= 0 ? 10 : pageSize;
				//if pagesize is larger than total records make it equal witj total records
				pageSize = pageSize >= totalRecords ? totalRecords : pageSize;
				// We limit the page size to a default max value
				//todo: Get the max page size from the configuration
				pageSize = pageSize > 100 ? 100 : pageSize;
				var totalPages = Convert.ToInt32(Math.Ceiling((decimal)totalRecords / pageSize));
				//if page is more than total pages  we'll make page equeal with total pages
				page = page > totalPages ? totalPages : page;
				//Prvent taking more items on the last page than they exist
				var takeCount = page == totalPages ? totalRecords - ((page - 1) * pageSize) : pageSize;

				//todo:
				//Uncomment this line once the issue with SQL 2012 gets resolved
				//qryProducts = qryProducts.Skip((page - 1) * pageSize).Take(takeCount);

				//Swap the next 2 lines once the issue with SQL 2012 gets resolved and we can to the paging on the sql server directly
				var v1 = qryProducts.Include(c=>c.AflProductCategorie).Include(p => p.AflProduct).ThenInclude(c =>c.AflProductDetail).ToList<AflProducts_Categorie>();
				//var v1 = qryProducts.ToList< AflProducts_Categorie>().Skip((page - 1) * pageSize).Take(takeCount);
				var productsPage = new AflProductsPage
				{
					pageSize = pageSize,
					totalRecords = totalRecords,
					pages = totalPages,
					page = page,
					thisPageSize = v1.Count(),
					categoryId = filter.CategoryId,
					categoryName = qryProducts.FirstOrDefault().AflProductCategorie.ProductCategoryName,
					aflProducts = qryProducts.ToList().Skip((page - 1) * pageSize).Take(takeCount).Select(x => new AflProductItem
					{
						prodId = x.AflProduct.ProdID,
						productName = x.AflProduct.ProductName,
						description = x.AflProduct.Description,
						navigateUrl = x.AflProduct.NavigateUrl,
						imgUrl = x.AflProduct.ImgUrl,
						imgAlt = x.AflProduct.ImgAlt,
						imgDescription = x.AflProduct.ImgDescription,
						additionalInfoTitle = x.AflProduct.AdditionalInfoTitle,
						additionalInfoUrl = x.AflProduct.AdditionalInfoUrl,
						recordStatus = x.AflProduct.RecordStatus,
						linkCode = x.AflProduct.LinkCode,
						advertiser = x.AflProduct.Advertiser,
						price = x.AflProduct.Price,
						advertizerLinkID = x.AflProduct.AdvertizerLinkID,
						detailIsActive = x.AflProduct.AflProductDetail != null ? (x.AflProduct.AflProductDetail.IsActive.HasValue ? x.AflProduct.AflProductDetail.IsActive.Value : false)   : false
					}).ToList()
				};


				return productsPage;
			}
			catch (Exception ex)
			{
				//todo: Log Error
				Console.WriteLine(ex);
				return null;
			}
		}

		public AflProductsPage GetAflProductsPageSproc(int page, int pageSize, AflProductFilter filter)
		{

			try
			{

				IQueryable<AflProducts_Categorie> qryProducts = _context.AflProducts_Categories;
				if (filter != null)
				{
					qryProducts = qryProducts.Where(x => x.AflProduct.IsActive == filter.IsActive);
					if (filter.CategoryId > 0)
					{
						qryProducts = qryProducts.Where(x => x.CategoryID == filter.CategoryId);
					}
					if (!string.IsNullOrEmpty(filter.CategoryName))
					{
						qryProducts = qryProducts.Where(x => (x.AflProductCategorie.ProductCategoryName ?? string.Empty).ToLower() == filter.CategoryName.ToLower());
					}
					//TODO: Filter by active dates
				}
				var totalRecords = qryProducts.Count();

				if (totalRecords == 0)
				{
					return null;
				}

				//If page <= 0 is passed we respond with page 1
				page = page <= 0 ? 1 : page;
				// If page size  is 0 or less we will make it 10
				//TODO: Get the default page size from thr configuration
				pageSize = pageSize <= 0 ? 10 : pageSize;
				//if pagesize is larger than total records make it equal witj total records
				pageSize = pageSize >= totalRecords ? totalRecords : pageSize;
				// We limit the page size to a default max value
				//todo: Get the max page size from the configuration
				pageSize = pageSize > 100 ? 100 : pageSize;
				var totalPages = Convert.ToInt32(Math.Ceiling((decimal)totalRecords / pageSize));
				//if page is more than total pages  we'll make page equeal with total pages
				page = page > totalPages ? totalPages : page;
				//Prvent taking more items on the last page than they exist
				var takeCount = page == totalPages ? totalRecords - ((page - 1) * pageSize) : pageSize;

				//todo:
				//Uncomment this line once the issue with SQL 2012 gets resolved
				//qryProducts = qryProducts.Skip((page - 1) * pageSize).Take(takeCount);

				//Swap the next 2 lines once the issue with SQL 2012 gets resolved and we can to the paging on the sql server directly
				var v1 = qryProducts.Include(x => x.AflProduct).Include(x => x.AflProductCategorie).ToList<AflProducts_Categorie>();
				//var v1 = qryProducts.ToList< AflProducts_Categorie>().Skip((page - 1) * pageSize).Take(takeCount);
				var productsPage = new AflProductsPage
				{
					pageSize = pageSize,
					totalRecords = totalRecords,
					pages = totalPages,
					page = page,
					thisPageSize = v1.Count(),
					categoryId = filter.CategoryId,
					categoryName = qryProducts.FirstOrDefault().AflProductCategorie.ProductCategoryName,
					aflProducts = qryProducts.Select(x => new AflProductItem
					{
						prodId = x.AflProduct.ProdID,
						productName = x.AflProduct.ProductName,
						description = x.AflProduct.Description,
						navigateUrl = x.AflProduct.NavigateUrl,
						imgUrl = x.AflProduct.ImgUrl,
						imgAlt = x.AflProduct.ImgAlt,
						imgDescription = x.AflProduct.ImgDescription,
						additionalInfoTitle = x.AflProduct.AdditionalInfoTitle,
						additionalInfoUrl = x.AflProduct.AdditionalInfoUrl,
						recordStatus = x.AflProduct.RecordStatus,
						linkCode = x.AflProduct.LinkCode,
						advertiser = x.AflProduct.Advertiser,
						price = x.AflProduct.Price,
						advertizerLinkID = x.AflProduct.AdvertizerLinkID
					}).ToList()
				};
				return productsPage;
			}
			catch (Exception ex)
			{
				//todo: Log Error
				Console.WriteLine(ex);
				return null;
			}
		}


		public int Count(AflProductFilter filter)
		{
			IQueryable<AflProduct> qry = _context.AflProducts;
			if (filter != null)
			{
				if (filter.IsActive.HasValue)
				{
					qry = qry.Where(x => x.IsActive == filter.IsActive.Value);
				}
				if (filter.DateActive.HasValue)
				{
					qry = qry.Where(x => (!x.StartDate.HasValue) || x.StartDate.Value.Date <= filter.DateActive.Value);
					qry = qry.Where(x => (!x.EndDate.HasValue) || x.EndDate.Value.Date >= filter.DateActive.Value);
				}
			}
			return qry.Count();
		}
	}
}
