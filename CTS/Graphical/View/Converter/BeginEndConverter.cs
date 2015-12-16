using Cimbalino.Phone.Toolkit.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Graphical.View.Converter
{
	public class BeginEndConverter : MultiValueConverterBase
	{
		public override object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			StringBuilder	builder = new StringBuilder();
			DateTime		begin;
			DateTime		end;

			if (values[0] == null || values[1] == null)
				return (string.Empty);
			begin	= DateTime.Parse(values[0].ToString());
			end		= DateTime.Parse(values[1].ToString());
			builder.Append(begin.ToString("dd-MM-yy"))
			.Append(" au ")
			.Append(end.ToString("dd-MM-yy"));
			return (builder.ToString());
		}

		public override object[] ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}
	}
}
