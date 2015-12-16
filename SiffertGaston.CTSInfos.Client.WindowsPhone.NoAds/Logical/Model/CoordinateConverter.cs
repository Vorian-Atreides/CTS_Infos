using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.Model
{
	public static class CoordinateConverter
	{
		public static GeoCoordinate GeocoordinateToGeoCoordinate(Geocoordinate coordinate)
		{
			return (new GeoCoordinate
				(
				coordinate.Latitude,
				coordinate.Longitude,
				coordinate.Altitude			?? double.NaN,
				coordinate.Accuracy,
				coordinate.AltitudeAccuracy ?? double.NaN,
				coordinate.Speed			?? double.NaN,
				coordinate.Heading			?? double.NaN
				));
		}
	}
}
