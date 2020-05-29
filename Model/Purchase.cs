using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
	//TODO: out only total prise for each GoodType in GeneralInfo
	//SOLVE: color generation

	public partial class Purchase
	{
		public DateTime Date { get; }

		public decimal Sum => Goods.Sum(x => x.Price);

		private List<GoodType> Types => Goods.Select(x => x.Type).Distinct().ToList();

		public int ItemsAmount => Goods.Count;

		public List<Good> Goods { get; }
	}

	public partial class Purchase : IEnumerable, IEnumerable<Good>, IEnumerable<string>
	{
		//TODO: what if
		///SOLVE: make such thing:
		///foreach(var item in Purchase.GetTypes())
		///purchase[GoodType] - returns collection
		//add indexers by int and by string probablly
	}
}
