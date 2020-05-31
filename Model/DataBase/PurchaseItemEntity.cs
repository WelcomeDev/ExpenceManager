using System.Collections.Generic;

namespace Model.DataBase
{
	internal class PurchaseItemEntity
	{
		public GoodEntity Good { get; set; }

		public int Amount { get; set; }

		public PurchaseItemEntity()
		{

		}

		public PurchaseItemEntity(KeyValuePair<Good, int> pair)
		{
			Good = new GoodEntity(pair.Key);
			Amount = pair.Value;
		}
	}
}