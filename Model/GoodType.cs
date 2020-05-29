using System;
using System.Diagnostics.CodeAnalysis;

namespace Model
{
	public partial class GoodType
	{
		public string Name { get; }

		public GoodType(GoodTypes type)
		{
			Name = type.ToString();
		}

		public GoodType(string newType)
		{
			if (DataValidation.IsCharCapital(newType) == false)
			{
				newType = string.Concat(newType[0].ToString().ToUpper(), newType.Substring(1));
			}

			Name = newType;
		}
	}

	public partial class GoodType : IEquatable<GoodType>, IEquatable<GoodTypes>, IComparable<GoodType>
	{
		public override bool Equals(object obj)
		{
			return obj switch
			{
				GoodType goodType => Equals(goodType),

				GoodTypes goodType => Equals(goodType),

				_ => false,
			};
		}

		public override int GetHashCode() => HashCode.Combine(Name);

		public override string ToString() => Name;

		public bool Equals([AllowNull] GoodType other)
		{
			if (other is null)
				return false;

			return Name.Equals(other.Name);
		}

		public bool Equals(GoodTypes other)
		{
			return Name.Equals(other.ToString());
		}

		public int CompareTo(GoodType other)
		{
			if (other is null)
				throw new ArgumentNullException($"Comparable value can't be null.");

			return Name.CompareTo(other.Name);
		}
	}
}
