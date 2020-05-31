using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Model.DataBase
{

	internal class PurchaseDBContext : DbContext
	{
		internal DbSet<PurchaseEntity> Purchases { get; set; }

		internal PurchaseDBContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PurchaseEntity>(p =>
			p.Property(i => i.Goods).HasConversion(
				s => JsonConvert.SerializeObject(s, Formatting.None),
				d => JsonConvert.DeserializeObject<List<PurchaseItemEntity>>(d))
			);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExpenceManagerDB;Trusted_Connection=True;");
		}
	}
}
