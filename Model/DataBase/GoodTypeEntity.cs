namespace Model.DataBase
{
	internal class GoodTypeEntity
	{
		public string Name { get; set; }

		public GoodTypeEntity()
		{ }

		public GoodTypeEntity(GoodType type)
		{
			Name = type.Item;
		}
	}
}
