using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View.Converter
{
	public class CBToImage : IValueConverter
	{
		private static readonly string[]	_paths = new string[]
		{
			"/Graphical/Pictures/cb_refuse.jpg",
			"/Graphical/Pictures/cb.jpg"
		};

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			int tmp;

			if (value == null || !int.TryParse(value.ToString(), out tmp) || tmp > 1 || tmp < 0)
				return (string.Empty);
			return (_paths[tmp]);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
