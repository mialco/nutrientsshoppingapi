using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.DAL.Models
{
	[Table("AflProducts",Schema="Liman")]
	public class AflProduct
	{
		[Column("ProdID")]
		//[Key,ForeignKey("AflProductDetail")]
		[Key]
		public long ProdID { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string NavigateUrl { get; set; }
		public string ImgUrl { get; set; }
		public string ImgAlt { get; set; }
		public string ImgDescription { get; set; }
		public string AdditionalInfoTitle { get; set; }
		public string AdditionalInfoUrl { get; set; }
		public string RecordStatus { get; set; }
		public string LinkCode { get; set; }
		public string Advertiser { get; set; }
		public decimal Price { get; set; }
		public string AdvertizerLinkID { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsActive { get; set; }

		//[ForeignKey("ProdID")]
		public virtual AflProductDetail AflProductDetail { get; set; }

		//[ForeignKey("ProdID")]
		//public virtual IEnumerable <AflProducts_Categorie> AflProducts_Categorie { get;set;}
	}
}
