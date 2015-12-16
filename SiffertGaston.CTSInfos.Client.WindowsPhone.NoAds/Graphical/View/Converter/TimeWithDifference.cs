using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View.Converter
{
	public class TimeWithDifference : Cimbalino.Phone.Toolkit.Converters.MultiValueConverterBase
	{
		public override object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			DateTime	time;
			bool		show;

			if (values == null || values.Count() != 2 ||
				values[0] == null || values[1] == null ||
				!DateTime.TryParse(values[0].ToString(), out time) ||
				!bool.TryParse(values[1].ToString(), out show))
				return ("Erreur");
			StringBuilder builder = new StringBuilder(time.ToString("HH:mm"));
			if (show)
			{
				builder.Append(" (")
				.Append(Math.Abs((int) (time - DateTime.Now).TotalMinutes))
				.Append(")");
			}
			return (builder.ToString());
		}

		public override object[] ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
