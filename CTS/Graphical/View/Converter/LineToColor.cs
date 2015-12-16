using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Graphical.View.Converter
{
	public class LineToColor : IValueConverter
	{
		private static readonly SolidColorBrush	_default = new SolidColorBrush(Colors.Gray);

		private static readonly Dictionary<string, SolidColorBrush>	_dico = new Dictionary<string, SolidColorBrush>()
		{
			{ "A",		new SolidColorBrush(Color.FromArgb(255, 226, 0, 26))		},
            { "B",		new SolidColorBrush(Color.FromArgb(255, 0, 157, 224))		},
            { "C",		new SolidColorBrush(Color.FromArgb(255, 242, 148, 0))		},
            { "D",		new SolidColorBrush(Color.FromArgb(255, 0, 152, 50))		},
            { "E",		new SolidColorBrush(Color.FromArgb(255, 144, 133, 186))		},
            { "F",		new SolidColorBrush(Color.FromArgb(255, 151, 190, 13))		},
			{ "G",		new SolidColorBrush(Color.FromArgb(255, 0, 68, 148))		},
            { "2",		new SolidColorBrush(Color.FromArgb(255, 226, 0, 26))		},
            { "4",		new SolidColorBrush(Color.FromArgb(255, 204, 0, 30))		},
            { "6",		new SolidColorBrush(Color.FromArgb(255, 125, 93, 159))		},
            { "7",		new SolidColorBrush(Color.FromArgb(255, 0, 144, 54))		},
            { "10",		new SolidColorBrush(Color.FromArgb(255, 250, 187, 0))		},
            { "12",		new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "22",		new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "13",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "14",		new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "24",		new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "15",		new SolidColorBrush(Color.FromArgb(255, 0, 74, 153))		},
            { "15a",	new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "17",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "19",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "21",		new SolidColorBrush(Color.FromArgb(255, 226, 0, 26))		},
            { "27",		new SolidColorBrush(Color.FromArgb(255, 159, 194, 4))		},
			{ "29",		new SolidColorBrush(Color.FromArgb(255, 0, 144, 54))		},
            { "30",		new SolidColorBrush(Color.FromArgb(255, 255, 221, 0))		},
            { "31",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "40",		new SolidColorBrush(Color.FromArgb(255, 255, 221, 0))		},
            { "50",		new SolidColorBrush(Color.FromArgb(255, 255, 221, 0))		},
            { "62",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "63",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "65",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		},
            { "66",		new SolidColorBrush(Color.FromArgb(255, 208, 170, 204))		},
            { "70",		new SolidColorBrush(Color.FromArgb(255, 250, 187, 0))		},
            { "71",		new SolidColorBrush(Color.FromArgb(255, 137, 204, 207))		},
            { "72",		new SolidColorBrush(Color.FromArgb(255, 247, 201, 221))		}
		};

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null || !_dico.ContainsKey(value.ToString()))
				return (_default);
			return (_dico[value.ToString()]);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
