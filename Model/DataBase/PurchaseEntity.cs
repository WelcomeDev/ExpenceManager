using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.DataBase
{
	internal class PurchaseEntity
	{
		[Key]
		public int Id { get; set; }

		public DateTime Date { get; set; }

		///Need to store as list of classes.
		///'cause Json can't fork with any type of dictionary
		public List<PurchaseItemEntity> Goods { get; set; }

		public PurchaseEntity()
		{

		}

		public PurchaseEntity(Purchase items)
		{
			Date = items.Date;
			Goods = ConvertToEntityType(items.Goods);
		}

		private List<PurchaseItemEntity> ConvertToEntityType(Dictionary<Good, int> goods)
		{
			var res = new List<PurchaseItemEntity>();

			foreach (var item in goods)
			{
				res.Add(new PurchaseItemEntity(item));
			}

			return res;
		}
	}
}
