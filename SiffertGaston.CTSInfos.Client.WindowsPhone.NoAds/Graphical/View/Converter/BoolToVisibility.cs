using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View.Converter
{
	public class BoolToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool visibility;

			if (value == null || !bool.TryParse(value.ToString(), out visibility))
				return (Visibility.Collapsed);
			return (visibility ? Visibility.Visible : Visibility.Collapsed);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
