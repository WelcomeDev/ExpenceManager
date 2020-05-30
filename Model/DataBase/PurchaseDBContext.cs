using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Model.DataBase
{
	
	internal class PurchaseDBContext : DbContext
	{
		internal DbSet<Purchase> Purchases { get; set; }

		internal PurchaseDBContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Purchase>(p => p.HasNoKey());

			modelBuilder.Entity<Purchase>(p =>
			p.Property(i => i.Goods).HasConversion(
				s=>JsonConvert.SerializeObject(s,Formatting.None),
				d=>JsonConvert.DeserializeObject<Dictionary<Good, int>>(d))
			);

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ExpenceManagerDB;Trusted_Connection=True;");
		}
	}
}
