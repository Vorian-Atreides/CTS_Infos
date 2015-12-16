using Cimbalino.Phone.Toolkit.Converters;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Graphical.View.Converter
{
	public class DistanceToBackground : MultiValueConverterBase
	{
		private static Dictionary<double, SolidColorBrush>	_converter = new Dictionary<double, SolidColorBrush>()
		{
			{ 0,	new SolidColorBrush(Colors.Blue)	},
			{ 500,	new SolidColorBrush(Colors.Green)	},
			{ 1500, new SolidColorBrush(Colors.Orange)	},
			{ 2500, new SolidColorBrush(Colors.Red)		},
			{ 3500, new SolidColorBrush(Colors.Black)	}
		};

		public override object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (values == null || values.Count() != 2 || !SimpleIoc.Default.GetInstance<Logical.ViewModel.ConfigurationViewModel>().Configuration.CanUseLocation)
				return (_converter[3500]);
			GeoCoordinate					position = values[0] as GeoCoordinate;
			WebService.Models.VelhopStop	item	 = values[1] as WebService.Models.VelhopStop;
			if (position == null || item == null)
				return (_converter[3500]);
			double dist = distance(position, item);
			try
			{
				var color = (from value in _converter
							 orderby value.Key
							 where value.Key < dist
							 select value.Value).LastOrDefault();
				if (color == null)
					return (_converter[3500]);
				return (color);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (_converter[3500]);
		}

		public override object[] ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (null);
		}

		private double distance(GeoCoordinate position, WebService.Models.VelhopStop stop)
		{
			double[]		latitude;
			double[]		longitude;
			double			value;

			latitude		=	new double[2];
			longitude		=	new double[2];
			latitude[0]		=	stop.Latitude			* Math.PI / 180;
			latitude[1]		=	position.Latitude		* Math.PI / 180;
			longitude[0]	=	stop.Longitude			* Math.PI / 180;
			longitude[1]	=	position.Longitude		* Math.PI / 180;
			value			=	Math.Cos(latitude[0])	* Math.Cos(latitude[1]) * Math.Cos(longitude[1] - longitude[0]);
			value			+=	Math.Sin(latitude[0])	* Math.Sin(latitude[1]);
			value			=	Math.Acos(value)		* 6371000;
			return (value);
		}
	}
}
