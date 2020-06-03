using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpenceManager.AdditionalContrils
{
	/// <summary>
	/// Логика взаимодействия для AddNewGoodTypePage.xaml
	/// </summary>
	public partial class AddNewGoodTypePage : Page
	{
		public AddNewGoodTypePage()
		{
			InitializeComponent();
		}

		private void MainPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainPanel.Focus();
		}
	}
}
