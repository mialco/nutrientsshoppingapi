using Microsoft.EntityFrameworkCore;
using NutrientsShoppingApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientsShoppingApi.DAL
{
	public class LimanContext : DbContext
	{
		public LimanContext(DbContextOptions<LimanContext> options) : base(options)
		{
		}

		public DbSet<AflProduct> AflProducts { get; set; }
		public DbSet<AflProductCategorie> AflProductCategories {get ; set;}
		public DbSet<AflProducts_Categorie> AflProducts_Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var x = 1;
			//modelBuilder.Entity<AflProduct>().ToTable("AflProducts");
		}

	}
}
