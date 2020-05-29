using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataBase
{
	internal class PurchaseDBContext : DbContext
	{
		internal DbSet<Purchase> Purchases { get; set; }

		internal PurchaseDBContext()
		{

		}
	}
}
