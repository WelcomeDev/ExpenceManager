using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Model;
using Model.DataBase;

namespace ExpenceManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly SolidColorBrush[] brushes = new SolidColorBrush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Purple, Brushes.Cyan, Brushes.Orange };

		public MainWindow()
		{
			InitializeComponent();
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

		}

		private void CrossOutUnavailableDates()
		{
			var availableDates = PurchaseDB.GetAvailableDates();
		}

		private void InitializeDiagram()
		{
			var scopes = new Scopes<GoodType, PurchaseItem>(PurchaseDB.GetAllGoodTypes, PurchaseDB.GetTypeAtDateRange, DateTime.Today, null);
			PieDiagram pie = new PieDiagram(scopes, brushes);

			Grid.SetColumn(pie, 2);
			Grid.SetRow(pie, 0);
			MainGrid.Children.Add(pie);
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

		}

		private void ExportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ManagerCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
