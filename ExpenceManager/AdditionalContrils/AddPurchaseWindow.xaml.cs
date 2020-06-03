using Model;
using Model.DataBase;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
			TotalPriceTextBlock.Text = 0.ToString("C2");

			AddGoodPage.GoodCreated += AddGoodPage_GoodCreated;
			TheAddPageFrame.Source = new Uri(@"AddGoodPage.xaml", UriKind.Relative);
		}

		private void AddGoodPage_GoodCreated(PurchaseItem obj)
		{
			purchase.Add(obj);

			Refresh();
		}

		private void Refresh()
		{
			GoodsDataGrid.ItemsSource = purchase.GetPurchaseItems();
			TotalPriceTextBlock.Text = purchase.Sum.ToString("C2");
		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void ConfirmButton_Click(object sender, RoutedEventArgs e)
		{
			if (purchase.ItemsAmount > 0)
			{
				PurchaseDB.Add(purchase);
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
