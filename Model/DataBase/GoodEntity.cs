namespace Model.DataBase
{
	internal class GoodEntity
	{
		public string Name { get; set; }

		public decimal Price { get; set; }

		public GoodTypeEntity Type { get; set; }

		public GoodEntity()
		{ }

		public GoodEntity(Good good)
		{
			Name = good.Name;
			Price = good.Price;
			Type = new GoodTypeEntity(good.Type);
		}
	}
}
