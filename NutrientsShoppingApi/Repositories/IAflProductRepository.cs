using NutrientsShoppingApi.DAL.Models;
using NutrientsShoppingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.Repositories
{
    public interface IAflProductRepository
    {
		IEnumerable<AflProduct> GetAll();
		AflProductsPage GetAflProductsPage(int page, int pageSize, AflProductFilter filter);

		AflProductItemWithDetails Find(long productId);
		int Count(AflProductFilter filter);
    }
}
