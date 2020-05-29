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

namespace ExpenceManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Initialize();
			
		}

		private void Initialize()
		{
			

			InitializeDiagram();
		}

		private void InitializeDiagram()
		{
			throw new NotImplementedException();
		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{

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
