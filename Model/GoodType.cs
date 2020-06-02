using System;
using System.Diagnostics.CodeAnalysis;
using DiagramModel;
using Model.DataBase;

namespace Model
{
	public partial class GoodType
	{
		public GoodType(GoodTypes type)
		{
			Item = type.ToString();
		}

		public GoodType(string newType)
		{
			if (string.IsNullOrWhiteSpace(newType))
				throw new ArgumentException($"Invalid type {newType}");

			if (DataValidation.IsCharCapital(newType) == false)
			{
				newType = string.Concat(newType[0].ToString().ToUpper(), newType.Substring(1));
			}

			Item = newType.Trim();
		}

		internal GoodType(GoodTypeEntity goodTypeEntity)
		{
			Item = goodTypeEntity.Name;
		}
	}

	public partial class GoodType : IEquatable<GoodType>, IEquatable<GoodTypes>, IComparable<GoodType>, IEnumType
	{
		public string Item { get; }

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
