using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.Models
{
    public class AflProductItemWithDetails : AflProductItem
    {
		public string desc1 { get; set; }
		public string desc2 { get; set; }
		public string title { get; set; }
		public string metaKeywords { get; set; }
		public string metaDescription { get; set; }
		public string articleTitle { get; set; }
		public List<string> categories { get; set; }

	}
}
