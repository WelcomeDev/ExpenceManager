using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils.InputUtils
{
	public static class GoodsTextParser
	{
		public static IEnumerable<PurchaseItem> Read(string path)
		{
			if (File.Exists(path))
			{
				using var sr = new StreamReader(path);
				while (sr.EndOfStream is false)
				{
					var line = sr.ReadLine();
					Tuple<string, string, decimal, int> tuple = TryParse(line);
					if (tuple is null)
						continue;

					yield return new PurchaseItem(tuple.Item2, tuple.Item3, tuple.Item1, tuple.Item4);
				}
			}
			else
			{
				throw new ArgumentException($"Can't find path {path}");
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line">text line to parse</param>
		/// <returns>Type Name Price Amount</returns>
		private static Tuple<string, string, decimal, int> TryParse(string line)
		{
			var arr = line.Split();
			if (arr.Length >= 4 
								&& decimal.TryParse(arr[^2], out decimal price) && DataValidation.IsPriceValid(price)
								&& int.TryParse(arr.Last(), out var amount) && DataValidation.IsAmountValid(amount))
			{
				var t = arr[1..^2];

				//first item is GoodType, before the last one is price, the last one is amount. Words in the middlle are name
				var name = t.Aggregate((x, y) => x += " " + y);

				return new Tuple<string, string, decimal, int>(arr.First(), name, price, amount);
			}

			return null;
		}
	}
}
