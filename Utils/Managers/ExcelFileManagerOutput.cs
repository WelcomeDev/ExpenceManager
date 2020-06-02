using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using Microsoft.EntityFrameworkCore.Query;
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
		private const string DirPath = @"C:\Users\aleks\source\repos\ExpenceManager\Purchase xlsx samples";
		private const string ExcelExtension = ".xlsx";

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

			onCreationTask.Result.CalculateMode = XLCalculateMode.Default;
			onCreationTask.Result.SaveAs(Path.Combine(DirPath, Guid.NewGuid().ToString() + ExcelExtension));
		}

		private static XLWorkbook OnCreation()
		{
			XLWorkbook workBook = new XLWorkbook();
			workBook.Worksheets.Add(FirstSheetName);
			workBook.Worksheets.Add(SecondSheetName);
			//TOOD: out to second one: here you can place your diagram :)

			return workBook;
		}

		private static IEnumerable<Purchase> GetDataTable(DateTime initialDate, DateTime? finalDate)
		{
			if (finalDate.HasValue && initialDate == finalDate.Value)
				finalDate = null;

			var items = PurchaseDB.GetPurchase(initialDate, finalDate);

			return items.OrderBy(x => x.Date);
		}

		private static void CreateTable(IEnumerable<Purchase> purchases, IXLWorksheet sheet)
		{
			int currentRow = 1;
			foreach (var purchase in purchases)
			{
				int tableRowBegin = currentRow;

				OutDate(sheet, ref currentRow, purchase);
				CreateHeader(sheet, currentRow++);

				var pItems = purchase.GetPurchaseItems().OrderBy(x => x.Type).ThenBy(x => x.GetTotal);
				int rowDataBegin = currentRow;
				foreach (var item in pItems)
				{
					OutGood(sheet, item, currentRow++);
				}

				OutPurchaseSum(sheet, ref currentRow, rowDataBegin);

				EditRange(sheet, currentRow, tableRowBegin, rowDataBegin);
			}

			sheet.Columns().AdjustToContents();
		}

		private static void OutPurchaseSum(IXLWorksheet sheet, ref int currentRow, int rowDataBegin)
		{
			var cell = sheet.Cell(currentRow++, 5);
			cell.Value = $"=СУММ($E{rowDataBegin}:$E{currentRow - 2})";
			cell.Style.Fill.BackgroundColor = XLColor.FromColor(Color.LightPink);
		}

		private static void OutDate(IXLWorksheet sheet, ref int currentRow, Purchase purchase)
		{
			var dateCell = sheet.Cell(currentRow++, 1);
			dateCell.Value = purchase.Date.ToShortDateString();
			dateCell.Style.Fill.BackgroundColor = XLColor.FromColor(Color.LightGreen);
		}

		private static void EditRange(IXLWorksheet sheet, int currentRow, int tableRowBegin, int rowDataBegin)
		{
			var tableRange = sheet.Range($"A{tableRowBegin}:E{currentRow}");

			tableRange.Style.Border.LeftBorder =
			tableRange.Style.Border.TopBorder =
			tableRange.Style.Border.RightBorder =
			tableRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

			var formulasRange = tableRange.Range($"E{rowDataBegin}:E{currentRow}");
			formulasRange.Style.NumberFormat.Format = "###,##0.00 P";
		}

		private static void OutGood(IXLWorksheet sheet, PurchaseItem item, int row)
		{
			int column = 1;
			sheet.Cell(row, column++).Value = item.Type.ToString();
			sheet.Cell(row, column++).Value = item.Name;
			sheet.Cell(row, column++).Value = item.Price;
			sheet.Cell(row, column++).Value = item.Amount;
			sheet.Cell(row, column++).Value = $"=$C{row}*$D{row}";
		}

		/// <summary>
		/// Creates line - Type Name Price Amount
		/// And Total Amount with formula
		/// </summary>
		private static void CreateHeader(IXLWorksheet sheet, int row)
		{
			int column = 1;
			sheet.Cell(row, column++).Value = "Type";
			sheet.Cell(row, column++).Value = "Name";
			sheet.Cell(row, column++).Value = "Price";
			sheet.Cell(row, column++).Value = "Amount";
			sheet.Cell(row, column++).Value = "Total price, P";

			var range = sheet.Range($"A{row}:E{row}");
			range.Style.Fill.BackgroundColor = XLColor.FromColor(Color.LightBlue);
			range.Style.Font.Bold = true;
		}
	}
}
