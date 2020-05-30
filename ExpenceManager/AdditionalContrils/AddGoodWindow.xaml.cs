using Model;
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
using System.Windows.Shapes;

namespace ExpenceManager.AdditionalContrils
{
	/// <summary>
	/// Логика взаимодействия для AddGoodWindow.xaml
	/// </summary>
	public partial class AddGoodWindow : Window
	{
		private readonly SolidColorBrush InvalidBrush = new SolidColorBrush(Colors.Red);
		private readonly SolidColorBrush ValidBrush = new SolidColorBrush(Colors.Green);

		private bool amountValid = false;
		private bool priceValid = false;
		private bool nameValid = false;
		private bool typeValid = false;

		public event Action<Good> GoodCreated;

		//TODO: make binding in xaml to default props
		public AddGoodWindow()
		{
			InitializeComponent();


		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (amountValid && priceValid && nameValid && typeValid)
			{
				//GoodCreated?.Invoke(new Good(NameTextBox.Text,
				//	decimal.Parse(PriceTextBox.Text), ));
				Close();
				return;
			}

			MessageBox.Show("Invalid data, please check input");
		}

		private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void PriceTextBox_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void AmountTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsAmountValid(AmountTextBox.Text))
			{

			}
		}

		private void ToInvalidView(Control control)
		{
			control.BorderThickness = new Thickness(2);
			control.BorderBrush = InvalidBrush;
		}

		private void ToValidView(Control control)
		{
			control.BorderThickness = new Thickness(2);
			control.BorderBrush = ValidBrush;
		}

		private void ToDefaultView(Control control)
		{
			//TODO: add default thickness and Brush, initialized in ctor
		}


	}
}
