using DiagramModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Model
{

	public partial class Purchase : EnumerableType<GoodType>
	{
		public DateTime Date { get; }

		public decimal Sum => Goods.Sum(x => x.Key.Price);

		public int ItemsAmount => Goods.Count;

		public Dictionary<Good, int> Goods { get; }

		public Purchase()
		{
			types = new List<GoodType>();
			Date = DateTime.Now;
		}

		public Purchase(DateTime date, Dictionary<Good, int> goods)
		{
			Date = date;
			Goods = goods;
			types = Goods.Select(x => x.Key.Type).Distinct().ToList();
		}

		public IEnumerable<PurchaseItem> GetPurchaseItems()
		{
			return Goods.Select(x => new PurchaseItem(x.Key.Name, x.Key.Price, x.Key.Type, x.Value));
		}

		public class PurchaseItem : IScopeSelectionItem
		{
			public string Name { get; }
			public decimal Price { get; }
			public int Amount { get; }
			public GoodType Type { get; }

			public decimal GetTotal => Price * Amount;

			public PurchaseItem(string name, decimal price, GoodType type, int amount)
			{
				Name = name;
				Price = price;
				Type = type;
				Amount = amount;
			}

			public override string ToString()
			{
				return $"{Amount} x {Name} - {Price:#0.00}";
			}
		}
	}

	public partial class Purchase : IEnumerable<Purchase.PurchaseItem>
	{

		//TODO: must return not good but PurchaseItem
		public IEnumerator<PurchaseItem> GetEnumerator()
		{
			return Goods.Select(x => new PurchaseItem(x.Key.Name, x.Key.Price, x.Key.Type, x.Value)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Goods.Select(x => new PurchaseItem(x.Key.Name, x.Key.Price, x.Key.Type, x.Value)).GetEnumerator();
		}
	}
}
