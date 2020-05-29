using DiagramModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public class PurchaseItem : IScopeSelectionItem
		{
			public string Name { get; }
			public decimal Price { get; }
			public int Amount { get; }

			public decimal GetTotal => Price * Amount;

			public PurchaseItem(string name, decimal price, int amount)
			{
				Name = name;
				Price = price;
				Amount = amount;
			}

			public override string ToString()
			{
				return $"{Amount} x {Name} - {Price:#0.00}";
			}
		}
	}

	public partial class Purchase : IEnumerable<Good>
	{

		//TODO: must return not good but PurchaseItem
		public IEnumerator<Good> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
