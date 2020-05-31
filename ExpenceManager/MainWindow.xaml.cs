using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
		private readonly SolidColorBrush[] brushes = new SolidColorBrush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Purple, Brushes.Cyan, Brushes.Orange };

		private Purchase purchase;
		private PieDiagram pie;

		private readonly Func<IEnumerable<GoodType>> typesProvider;
		private readonly Func<GoodType, DateTime, DateTime?, IEnumerable<PurchaseItem>> dataProvider;

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

			if (lastDate != null)
			{
				InitializeDiagram();
				//Init selected date etc
				Task.Run(() => CrossOutUnavailableDates());
			}

			//TODO: drop loading selection
		}

		private void CrossOutUnavailableDates()
		{
			var availableDates = PurchaseDB.GetAvailableDates();
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
			var addWin = new AddGoodWindow();
			addWin.GoodCreated += AddWin_GoodCreated;
			addWin.ShowDialog();
		}

		private void AddWin_GoodCreated(Good obj)
		{
			throw new NotImplementedException();
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
				InitialDirectory = @"C:\Users\aleks\source\repos\ExpenceManager\Purchase txt samples",
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

				//TODO: remove that date from crossed out
				LoadNewPie(date, null);

				//TODO: select date in diagram and join main thread
			}
			catch (Exception ex)
			{
				Dispatcher.Invoke(() => MessageBox.Show(ex.Message));
			}
		}

		//SOLVE: set min win size
		private void ExportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//LoadNewPie();
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
