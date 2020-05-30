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

		public static IEnumerable<IEnumerable<Purchase.PurchaseItem>> GetTypeAtDateRange(GoodType type, DateTime initialDate, DateTime? finalDate)
		{
			using var db = new PurchaseDBContext();

			ApproximateToLower(ref initialDate);

			if (finalDate.HasValue)
			{
				ApproximateToUpper(ref finalDate);

				return db.Purchases.Where(x => x.Date >= initialDate && x.Date <= finalDate.Value)
									.Select(x => x.GetPurchaseItems().Where(x => x.Type == type));
			}
			else
			{
				return db.Purchases.Where(x => x.Date.Year == initialDate.Year &&
											x.Date.Month == initialDate.Month &&
											x.Date.Day == initialDate.Day)
									.Select(x => x.GetPurchaseItems().Where(x => x.Type == type));

			}

		}

		private static void ApproximateToLower(ref DateTime initialDate)
		{
			initialDate = initialDate.AddHours(-initialDate.Hour)
									.AddMinutes(-initialDate.Minute)
									.AddSeconds(-initialDate.Second);
		}

		private static void ApproximateToUpper(ref DateTime? finalDate)
		{
			var h = 23 - finalDate.Value.Hour;
			var m = 59 - finalDate.Value.Minute;
			var s = 59 - finalDate.Value.Second;

			finalDate = finalDate.Value.AddHours(h).AddMinutes(m).AddSeconds(s);
		}
	}
}
