using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Model;
using Model.DataBase;

namespace Utils.Managers
{
	/// <summary>
	/// Output part
	/// </summary>
	public static partial class ExcelFileManager
	{
		private const string FirstSheetName = "Purchases";
		private const string SecondSheetName = "Diagram";

		///TODO: out order by Type then Name and add formuls in total line
		///add headers = Type, Name, Price, Amount, Total
		///Than TotalSum of total
		public static async void Create(DateTime initialDate, DateTime? finalDate)
		{
			var getDataTask = Task.Run(() => GetDataTable(initialDate, finalDate));
			var onCreationTask = Task.Run(() => OnCreation());


			Task.WaitAll(getDataTask, onCreationTask);

			await Task.Run(() => CreateTable(
							getDataTask.Result,
							onCreationTask.Result.Worksheet(FirstSheetName))
											);
		}

		private static XLWorkbook OnCreation()
		{
			XLWorkbook workBook = new XLWorkbook();
			workBook.Worksheets.Add(FirstSheetName);
			workBook.Worksheets.Add(SecondSheetName);

			return workBook;
		}

		private static IEnumerable<PurchaseItem> GetDataTable(DateTime initialDate, DateTime? finalDate)
		{
			var types = PurchaseDB.GetAllGoodTypes();
			var items = new List<PurchaseItem>();
			foreach (var type in types)
			{
				items.AddRange(PurchaseDB.GetItemsByTypeAtDateRange(type, initialDate, finalDate));
			}

			return items.OrderBy(x => x.Type).ThenBy(x => x.GetTotal);
		}

		private static void CreateTable(IEnumerable<PurchaseItem> result, IXLWorksheet iXLWorksheet)
		{
			
		}

		/// <summary>
		/// Creates line - Type Name Price Amount
		/// And Total Amount with formula
		/// </summary>
		private static void CreateHeader(IXLWorksheet iXLWorksheet, int row)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates pie diagram
		/// </summary>
		private static void CreateDiagram()
		{
			//make it in separate thread after data's been received
		}

		private static void CreateExcelFile()
		{
			throw new NotImplementedException();
		}
	}
}
