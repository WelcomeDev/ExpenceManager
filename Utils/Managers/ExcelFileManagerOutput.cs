using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Managers
{
	/// <summary>
	/// Output part
	/// </summary>
	public static partial class ExcelFileManager
	{
		///TODO: out order by Type then Name and add formuls in total line
		///add headers = Type, Name, Price, Amount, Total
		///Than Total for total
		public static void Create(DateTime initialDate, DateTime? finalDate)
		{
			//Select from db set and than take it in pdf
			//throw new NotImplementedException();
		}
	}
}
