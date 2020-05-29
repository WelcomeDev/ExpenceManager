namespace Model
{
	public static class DataValidation
	{
		public static bool IsCharCapital(string data) => data.ToUpper()[0] == data[0];

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
	}
}
