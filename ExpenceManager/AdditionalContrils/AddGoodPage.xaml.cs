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
		private readonly SolidColorBrush InvalidBrush = new SolidColorBrush(Colors.Red);
		private readonly SolidColorBrush ValidBrush = new SolidColorBrush(Colors.Green);
		private Brush DefaultBrush { get; }
		private Thickness DefaultThichness { get; }

		private bool amountValid = false;
		private bool priceValid = false;
		private bool nameValid = false;
		private bool typeValid = false;
		private GoodType goodType;

		public AddGoodPage()
		{
			InitializeComponent();

			DefaultBrush = AmountTextBox.BorderBrush;
			DefaultThichness = AmountTextBox.BorderThickness;

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
				ToValidView(NameTextBox);
				return;
			}

			nameValid = false;
			ToInvalidView(NameTextBox);
		}

		private void PriceTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsPriceValid(PriceTextBox.Text))
			{
				priceValid = true;
				ToValidView(PriceTextBox);
				return;
			}

			priceValid = false;
			ToInvalidView(PriceTextBox);
		}

		private void AmountTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsAmountValid(AmountTextBox.Text))
			{
				amountValid = true;
				ToValidView(AmountTextBox);
				return;
			}

			amountValid = false;
			ToInvalidView(AmountTextBox);
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
			control.BorderThickness = DefaultThichness;
			control.BorderBrush = DefaultBrush;
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
			ToDefaultView(AmountTextBox);
			AmountTextBox.Text = "";

			ToDefaultView(PriceTextBox);
			PriceTextBox.Text = "";

			ToDefaultView(NameTextBox);
			NameTextBox.Text = "";

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
