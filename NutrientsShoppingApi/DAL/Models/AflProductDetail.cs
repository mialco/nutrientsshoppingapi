using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NutrientsShoppingApi.DAL.Models
{
	[Table("AflProductDetail", Schema = "Liman")]
	public class AflProductDetail
	{
		[Column("ProdId")]
		[Key,ForeignKey("AflProduct")]
		public virtual long ProdID { get; set; }

		public string Description1 { get; set; }
		public string Description2 { get; set; }
		public bool? IsActive { get; set; }

		[ForeignKey("ProdID")]
		public virtual AflProduct AflProduct { get; set; }

	}

}
