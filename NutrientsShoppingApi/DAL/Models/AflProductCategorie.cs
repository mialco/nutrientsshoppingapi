using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.DAL.Models
{
	[Table("aflProductCategories",Schema="Liman")]
	public class AflProductCategorie
    {
		[Key]
		[Column("ProductCategoryID",TypeName="Int64" )]
		public virtual long CategoryID { get;set; }
		public string ProductCategoryName { get; set; }


		//public virtual IEnumerable< AflProducts_Categorie> AflProducts_Categorie { get; set; }
	}
}
