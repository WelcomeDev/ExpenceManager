﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiagramControls;
using DiagramModel;
using ExpenceManager.AdditionalContrils;
using Microsoft.Win32;
using Model;
using Model.DataBase;
using Utils.InputUtils;

namespace ExpenceManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string SampleExamplesDir = @"C:\Users\aleks\source\repos\ExpenceManager\Purchase txt samples";
		private readonly SolidColorBrush[] brushes = new SolidColorBrush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Purple, Brushes.Cyan, Brushes.Orange };

		private Purchase purchase;
		private PieDiagram pie;

		private readonly Func<IEnumerable<GoodType>> typesProvider;
		private readonly Func<GoodType, DateTime, DateTime?, IEnumerable<PurchaseItem>> dataProvider;

		public DateTime InitialDate => new DateTime(2018, 1, 1);

		public MainWindow()
		{
			InitializeComponent();

			typesProvider = PurchaseDB.GetAllGoodTypes;
			dataProvider = PurchaseDB.GetTypeAtDateRange;

			Initialize();
		}

		private void Initialize()
		{
			var lastDate = PurchaseDB.GetLastPurchaseDate();
			ManagerCalendar.DisplayDateEnd = DateTime.Today;
			ManagerCalendar.DisplayDateStart = InitialDate;

			if (lastDate != null)
			{
				InitializeDiagram();
				ManagerCalendar.SelectedDate = lastDate;
				Task.Run(() => CrossOutUnavailableDates());
			}

			//TODO: drop loading selection
		}

		private void CrossOutUnavailableDates()
		{
			var availableDates = PurchaseDB.GetAvailableDates();
			Dispatcher.Invoke(() => ManagerCalendar.BlackoutDates.Clear());

			var currentDate = DateTime.Today;
			while (currentDate != InitialDate)
			{
				if (availableDates.Count(x => x.Year == currentDate.Year &&
										x.Month == currentDate.Month &&
										x.Day == currentDate.Day) == 0)     // if there's no such date
				{
					Dispatcher.Invoke(() => ManagerCalendar.BlackoutDates.Add(new CalendarDateRange(currentDate)));
				}

				currentDate = currentDate.AddDays(-1);
			}
		}

		private void InitializeDiagram()
		{
			var scopes = new Scopes<GoodType, PurchaseItem>(typesProvider, dataProvider, DateTime.Today, null);
			pie = new PieDiagram(scopes, brushes);

			PlacePie();
		}

		private void PlacePie()
		{
			Grid.SetColumn(pie, 2);
			Grid.SetRow(pie, 0);
			MainGrid.Children.Add(pie);
		}

		private void LoadNewPie(DateTime initialDate, DateTime? finalDate)
		{
			Task.Run(() => CrossOutUnavailableDates());

			if (pie is null)
			{
				var scopes = new Scopes<GoodType, PurchaseItem>(typesProvider, dataProvider, initialDate, finalDate);
				pie = new PieDiagram(scopes, brushes);

				PlacePie();
			}
			else
			{
				var scopes = new Scopes<GoodType, PurchaseItem>(typesProvider, dataProvider, initialDate, finalDate);
				pie = new PieDiagram(scopes, brushes);
			}	
		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			var purchaseWin = new AddPurchaseWindow();
			purchaseWin.PurchaseCreated += PurchaseWin_PurchaseCreated;
			purchaseWin.ShowDialog();
		}

		private void PurchaseWin_PurchaseCreated(Purchase obj)
		{
			purchase = obj;
			LoadNewPie(purchase.Date, null);
		}

		private void LoadFromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (LoadFromComboBox.SelectedItem != null)
			{
				var itemTag = ((ComboBoxItem)LoadFromComboBox.SelectedItem).Tag;

				switch (itemTag)
				{
					case "TXT":
						OpenTxtFileDialog();
						break;

					case "EXCEL":
						OpenExcelFileDialog();
						break;

					default:
						throw new ArgumentException("Can't recognize the tag");
				}
			}
		}

		private void OpenExcelFileDialog()
		{
			//throw new NotImplementedException();
		}

		private void OpenTxtFileDialog()
		{
			const string txtExtension = ".txt";

			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				InitialDirectory = SampleExamplesDir,
			};
			openFileDialog.ShowDialog();

			var fileName = openFileDialog.FileName;
			if (fileName.EndsWith(txtExtension))
			{
				ReadTxtAsync(fileName);
			}
			else
			{
				MessageBox.Show("Please select txt file.");
			}
		}

		private async void ReadTxtAsync(string fileName)
		{
			await Task.Run(() => ReadTxtFile(fileName));
		}

		private void ReadTxtFile(string fileName)
		{
			try
			{
				var date = File.GetLastWriteTime(fileName);
				purchase = new Purchase(date);

				foreach (var item in GoodsTextParser.Read(fileName))
				{
					purchase.Add(item);
				}

				PurchaseDB.Add(purchase);
				Thread.CurrentThread.Join();

				LoadNewPie(date, null);
			}
			catch (Exception ex)
			{
				Dispatcher.Invoke(() => MessageBox.Show(ex.Message));
			}
		}

		private void ExportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ManagerCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{
			if (pie != null)
			{
				Mouse.Capture(null);

				var dates = ManagerCalendar.SelectedDates.OrderBy(x => x.Date);

				DateTime initialDate = dates.First();
				DateTime? finalDate = dates.Last();

				if (initialDate == finalDate.Value)
					finalDate = null;

				var sc = new Scopes<GoodType, PurchaseItem>(typesProvider, dataProvider, initialDate, finalDate);
				pie.LoadNew(sc);
			}
			else
			{
				Initialize();
			}
		}
	}
}
