using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExpenceManager.AdditionalContrils
{
	internal static class Helper
	{
		public static readonly SolidColorBrush InvalidBrush = new SolidColorBrush(Colors.Red);
		public static readonly SolidColorBrush ValidBrush = new SolidColorBrush(Colors.Green);

		public static void ToInvalidView(this Control control)
		{
			control.BorderThickness = new Thickness(2);
			control.BorderBrush = InvalidBrush;
		}

		public static void ToValidView(this Control control)
		{
			control.BorderThickness = new Thickness(2);
			control.BorderBrush = ValidBrush;
		}

		public static void ToDefaultView(this Control control, Thickness defaultThickness, Brush defaultBrush)
		{
			control.BorderThickness = defaultThickness;
			control.BorderBrush = defaultBrush;
		}
	}
}
