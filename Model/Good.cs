using Model.DataBase;
using System;
using System.Diagnostics.CodeAnalysis;

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

		public Good(string name, decimal price, GoodType type) : this(name, price)
		{
			Type = type;
		}

		internal Good(GoodEntity goodEntity)
		{
			Name = goodEntity.Name;
			Price = goodEntity.Price;
			Type = new GoodType(goodEntity.Type);
		}
	}

	public partial class Good : IEquatable<Good>
	{
		public override bool Equals(object obj)
		{
			if (obj is Good good)
				return Equals(good);

			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, Price, Type.GetHashCode());
		}

		public bool Equals([AllowNull] Good other)
		{
			if (other is null)
				return false;

			return Name.Equals(other.Name) && Price == other.Price && Type.Equals(other.Type);
		}

		public override string ToString()
		{
			return $"{Name} - {Price:C2}";
		}
	}
}
