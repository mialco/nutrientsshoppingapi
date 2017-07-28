using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.Models
{
    public class AflProductItem
    {
		public long prodId { get; set; }
		public string productName { get; set; }
		public string description { get; set; }
		public string navigateUrl { get; set; }
		public string imgUrl { get; set; }
		public string imgAlt { get; set; }
		public string imgDescription { get; set; }
		public string additionalInfoTitle { get; set; }
		public string additionalInfoUrl { get; set; }
		public string recordStatus { get; set; }
		public string linkCode { get; set; }
		public string advertiser { get; set; }
		public decimal price { get; set; }
		public string advertizerLinkID { get; set; }
		public long categoryId { get; set; }
		public string categoryName { get; set; }
		//public DateTime? StartDate { get; set; }
		//public DateTime? EndDate { get; set; }
		//public bool IsActive { get; set; }
	}
}
