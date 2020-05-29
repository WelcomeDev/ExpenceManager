using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Model.DataBase
{
	public static class PurchaseDB
	{
		public static DateTime? GetLastPurchaseDate()
		{
			using var db = new PurchaseDBContext();

			var items = db.Purchases;

			if (items.Count() > 0) return items.OrderBy(x => x.Date).Last().Date;

			return null;
		}

		public static IEnumerable<DateTime> GetAvailableDates()
		{
			using var db = new PurchaseDBContext();

			return db.Purchases.Select(x => x.Date);
		}

		public static IEnumerable<Good> GetTypeAtDateRange(GoodType type, DateTime initialDate, DateTime? finalDate)
		{
			using var db = new PurchaseDBContext();

			ApproximateToLower(ref initialDate);

			if(finalDate.HasValue)
			{
				ApproximateToUpper(ref finalDate);

				db.Purchases.Where(x => x.Date >= initialDate && x.Date <= finalDate).Select(x => x);
			}
			else
			{

			}
			
		}

		private static void ApproximateToLower(ref DateTime initialDate)
		{
			throw new NotImplementedException();
		}

		private static void ApproximateToUpper(ref DateTime? finalDate)
		{
			throw new NotImplementedException();
		}
	}
}
