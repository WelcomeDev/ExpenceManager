using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExpenceManager.AdditionalContrils
{
	/// <summary>
	/// Логика взаимодействия для AddNewGoodTypePage.xaml
	/// </summary>
	public partial class AddNewGoodTypePage : Page
	{
		private Brush DefaultBrush { get; }
		private Thickness DefaultThickness { get; }
		private bool isValid = false;

		public AddNewGoodTypePage()
		{
			InitializeComponent();

			DefaultBrush = NewItemTextBox.BorderBrush;
			DefaultThickness = NewItemTextBox.BorderThickness;
		}

		private void MainPanel_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainPanel.Focus();
		}

		private void NewItemTextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (DataValidation.IsNameValid(NewItemTextBox.Text))
			{
				NewItemTextBox.ToValidView();
				isValid = true;
			}
			else
			{
				NewItemTextBox.ToInvalidView();
				isValid = false;
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if(isValid)
			{
				GoodTypeCreated?.Invoke(new GoodType(NewItemTextBox.Text));
				Clear();
			}
		}

		private void Clear()
		{
			NewItemTextBox.ToDefaultView(DefaultThickness, DefaultBrush);
			NewItemTextBox.Text = "";
			isValid = false;
		}
	}

	public partial class AddNewGoodTypePage : Page
	{
		public static event Action<GoodType> GoodTypeCreated;
	}
}
