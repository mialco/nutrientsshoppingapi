using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.Models
{
    public class AflProductsPage
    {
		public int pages { get; set; }
		public int page { get; set; }
		public int totalRecords { get; set; }
		public int pageSize { get; set; }
		public int thisPageSize { get; set; }
		public long? categoryId { get; set; }
		public string categoryName { get; set; }
		public IEnumerable<AflProductItem> aflProducts { get; set; } 
    }
}
