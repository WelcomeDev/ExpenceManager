namespace Model
{
	public static class DataValidation
	{
		public static bool IsNameValid(string data)
		{
			if (string.IsNullOrWhiteSpace(data) == false)
			{
				return IsCharCapital(data.Trim());
			}

			return false;
		}

		public static bool IsCharCapital(string data) => data.Length > 0 && data.ToUpper()[0] == data[0];

		//public static bool DoesEnumContainMember(Type enumType, string data)
		//{
		//	if(enumType.IsEnum)
		//	{
		//		var members = Enum.GetValues(enumType);
		//		foreach(var member in members)
		//		{
		//			if (member.ToString().Equals(data))
		//				return true;
		//		}
		//	}

		//	return false;
		//}

		public static bool IsPriceValid(string input) => decimal.TryParse(input, out var res) && IsPriceValid(res);

		public static bool IsPriceValid(decimal num) => num > 0 && num < (decimal)1e9;

		public static bool IsAmountValid(string input) => int.TryParse(input, out var res) && IsAmountValid(res);

		public static bool IsAmountValid(int num) => num > 0 && num < 1000;
	}
}
