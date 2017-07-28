using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.DAL.Models
{
	[Table("AflProducts_Categories", Schema="Liman")]
	public class AflProducts_Categorie
	{
		public AflProducts_Categorie()
		{
			AflProduct = new AflProduct();
		}
		[Key]
		[Column("ID")]
		public long ID { get; set; }

		[ForeignKey("ProdID")]
		public virtual AflProduct AflProduct { get; set; }
		[Column("ProductID", TypeName = "long")]
		[ForeignKey("AflProduct")]
		public virtual long ProdID { get; set; }

		[ForeignKey("AflProductCategorie")]
		public virtual long CategoryID { get; set; }
		[ForeignKey("CategoryID")]
		public virtual AflProductCategorie AflProductCategorie { get; set; }
	}
}
