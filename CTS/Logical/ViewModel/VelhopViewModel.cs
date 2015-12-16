using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public class VelhopViewModel : AViewModel
	{
		#region Properties

		public const double							_strasLatitude	= 48.585060;
		public const double							_strasLongitude	= 7.736420;

		private Geolocator							_geolocator;

		private GeoCoordinate						_position;
		public GeoCoordinate						Position
		{
			get { return (_position); }
			private set
			{
				if (_position != value)
				{
					_position = value;
					OnNotifyPropertyChanged("Position");
				}
			}
		}

		private List<WebService.Models.VelhopStop>	_stations;
		public List<WebService.Models.VelhopStop>	Stations
		{
			get { return (_stations); }
			set
			{
				if (_stations != value)
				{
					_stations = value;
					OnNotifyPropertyChanged("Stations");
				}
			}
		}

		private List<WebService.Models.VelhopStop>	_selectedItems;
		public List<WebService.Models.VelhopStop>	SelectedItems
		{
			get { return (_selectedItems); }
			set
			{
				if (_selectedItems != value)
				{
					_selectedItems = value;
					OnNotifyPropertyChanged("SelectedItems");
				}
			}
		}

		#endregion //Properties

		#region Commands

		public ICommand	Selected	{ get; private set; }

		#endregion //Commands

		#region Actions

		private void selected(object sender)
		{
			WebService.Models.VelhopStop	stop = sender as WebService.Models.VelhopStop;

			if (stop == null)
			{
				SelectedItems = null;
				return;
			}
			var items		= from item in Stations
							  where item.Name.Equals(stop.Name)
							  select item;
			SelectedItems	= new List<WebService.Models.VelhopStop>(items);
		}

		#endregion //Actions

		public VelhopViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Stations					= new List<WebService.Models.VelhopStop>();
			SelectedItems				= null;
			Position					= new GeoCoordinate(_strasLatitude, _strasLongitude);
			_geolocator					= new Geolocator();
			_geolocator.ReportInterval	= 120000;
			_geolocator.PositionChanged	+= _geolocator_PositionChanged;

			Selected					= new Commands.CommandSender(selected);
		}

		private void _geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
		{
			if (SimpleIoc.Default.GetInstance<ConfigurationViewModel>().Configuration.CanUseLocation)
			{
				if (args != null && args.Position != null && args.Position.Coordinate != null)
					Position = new GeoCoordinate(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude);
			}
		}

		public async override Task<bool> Initialize(params string[] args)
		{
			try
			{
				var stops = await new WebService.Request.VelhopStopInfo().SendRequest();
				if (stops != null)
					Stations = new List<WebService.Models.VelhopStop>(stops);
				return (true);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return (false);
			}
		}

		public override void OnNavigate(params string[] args)
		{
		}
	}
}
