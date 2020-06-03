using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExpenceManager.AdditionalContrils
{
	/// <summary>
	/// Логика взаимодействия для AddGoodPage.xaml
	/// </summary>
	public partial class AddGoodPage : Page
	{
		public readonly SolidColorBrush InvalidBrush = new SolidColorBrush(Colors.Red);
		public readonly SolidColorBrush ValidBrush = new SolidColorBrush(Colors.Green);
		private Brush DefaultBrush { get; }
		private Thickness DefaultThickness { get; }

		private bool amountValid = false;
		private bool priceValid = false;
		private bool nameValid = false;
		private bool typeValid = false;
		private GoodType goodType;

		public AddGoodPage()
		{
			InitializeComponent();

			DefaultBrush = AmountTextBox.BorderBrush;
			DefaultThickness = AmountTextBox.BorderThickness;

			InitializeComboBox();
			//SOLVE: add item to createNewType
		}

		private void InitializeComboBox()
		{
			var items = Enum.GetValues(typeof(GoodTypes));
			foreach (var item in items)
			{
				TypeComboBox.Items.Add(item);
			}
		}

		private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainGrid.Focus();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (amountValid && priceValid && nameValid && typeValid)
			{
				GoodCreated?.Invoke(new PurchaseItem(NameTextBox.Text,
													decimal.Parse(PriceTextBox.Text),
													goodType,
													int.Parse(AmountTextBox.Text)));
				Clear();
			}
			else
			{
				MessageBox.Show("Invalid data, please check input");
			}

		}

		private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsNameValid(NameTextBox.Text))
			{
				nameValid = true;
				NameTextBox.ToValidView();
				return;
			}

			nameValid = false;
			NameTextBox.ToInvalidView();
		}

		private void PriceTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsPriceValid(PriceTextBox.Text))
			{
				priceValid = true;
				PriceTextBox.ToValidView();
				return;
			}

			priceValid = false;
			PriceTextBox.ToInvalidView();
		}

		private void AmountTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsAmountValid(AmountTextBox.Text))
			{
				amountValid = true;
				AmountTextBox.ToValidView();
				return;
			}

			amountValid = false;
			AmountTextBox.ToInvalidView();
		}

		private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var objStr = TypeComboBox.SelectedItem.ToString();

			if (DataValidation.IsNameValid(objStr))
			{
				//TODO: add logic - such already exist
				if (Enum.TryParse(typeof(GoodTypes), objStr, out var result))
				{
					goodType = new GoodType((GoodTypes)result);
				}
				else
				{
					goodType = new GoodType(objStr);
				}


				typeValid = true;
				return;
			}

			typeValid = false;
		}

		private void Clear()
		{
			AmountTextBox.ToDefaultView(DefaultThickness, DefaultBrush);
			amountValid = false;
			AmountTextBox.Text = "";

			PriceTextBox.ToDefaultView(DefaultThickness, DefaultBrush);
			priceValid = false;
			PriceTextBox.Text = "";

			NameTextBox.ToDefaultView(DefaultThickness, DefaultBrush);
			nameValid = false;
			NameTextBox.Text = "";

			typeValid = false;
			TypeComboBox.SelectedIndex = -1;
		}

		private void NewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{

		}

		private void NewItem_GotFocus(object sender, RoutedEventArgs e)
		{

		}

		private void NewItem_LostFocus(object sender, RoutedEventArgs e)
		{

		}
	}

	public partial class AddGoodPage
	{
		public static event Action<PurchaseItem> GoodCreated;
	}
}
