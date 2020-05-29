using System;
using System.Diagnostics.CodeAnalysis;
using DiagramModel;

namespace Model
{
	public partial class GoodType: IEnumType
	{
		public string Item { get; }

		public GoodType(GoodTypes type)
		{
			Item = type.ToString();
		}

		public GoodType(string newType)
		{
			if (DataValidation.IsCharCapital(newType) == false)
			{
				newType = string.Concat(newType[0].ToString().ToUpper(), newType.Substring(1));
			}

			Item = newType;
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

		public override int GetHashCode() => HashCode.Combine(Item);

		public override string ToString() => Item;

		public bool Equals([AllowNull] GoodType other)
		{
			if (other is null)
				return false;

			return Item.Equals(other.Item);
		}

		public bool Equals(GoodTypes other)
		{
			return Item.Equals(other.ToString());
		}

		public int CompareTo(GoodType other)
		{
			if (other is null)
				throw new ArgumentNullException($"Comparable value can't be null.");

			return Item.CompareTo(other.Item);
		}
	}
}
