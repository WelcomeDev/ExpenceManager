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
	/// Логика взаимодействия для AddPurchaseWindow.xaml
	/// </summary>
	public partial class AddPurchaseWindow : Window
	{
		private readonly Purchase purchase = new Purchase();
		public event Action<Purchase> PurchaseCreated;

		public AddPurchaseWindow()
		{
			InitializeComponent();

			AddGoodPage.GoodCreated += AddGoodPage_GoodCreated;
			TheAddPageFrame.Source = new Uri(@"AddGoodPage.xaml", UriKind.Relative);
		}

		private void AddGoodPage_GoodCreated(PurchaseItem obj)
		{
			purchase.Add(obj);

			Refresh();
			//throw new NotImplementedException();
		}

		private void Refresh()
		{
			GoodsDataGrid.ItemsSource = purchase.GetPurchaseItems();
		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void ConfirmButton_Click(object sender, RoutedEventArgs e)
		{
			if (purchase.ItemsAmount > 0)
			{
				PurchaseCreated?.Invoke(purchase);
				Close();
			}
			else
			{
				MessageBox.Show("Goods list is empty!");
			}

		}
	}
}
