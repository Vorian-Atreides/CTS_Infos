using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View.Converter
{
	public class DeviationCategoryToColor : IValueConverter
	{
		private static readonly Dictionary<string, Color>	_stringToColor = new Dictionary<string, Color>()
		{
			{ "Travaux",		Color.FromArgb(0xFF, 0x08, 0x4F, 0x41)	},
			{ "Manifestation",	Color.FromArgb(0xFF, 0xE1, 0x36, 0x3E)	},
			{ "Autres",			Colors.Blue								}
		};

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return (Colors.Transparent);
			var selected = _stringToColor.Where(item => item.Key.Equals(value.ToString())).SingleOrDefault();
			return ((selected.Key != null) ? selected.Value : Colors.Transparent);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
