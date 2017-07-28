using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.DAL.Models
{
    public class AflProductFilter
    {
		public string CategoryName { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? DateActive { get; set; }
		public long CategoryId { get; set; }
    }
}
