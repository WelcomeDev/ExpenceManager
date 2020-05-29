namespace Model
{
	public partial class Good
	{
		public string Name { get; }

		public decimal Price { get; }

		public GoodType Type { get; }

		private Good(string name, decimal price)
		{
			Name = name;
			Price = price;
		}

		public Good(string name, decimal price, GoodTypes type) : this(name, price)
		{
			Type = new GoodType(type);
		}

		public Good(string name, decimal price, string type) : this(name, price)
		{
			Type = new GoodType(type);
		}
	}

	public partial class Good
	{
		public override string ToString()
		{
			return $"{Name} - {Price:C2}";
		}
	}
}
