using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public class ScheduleStationViewModel : AViewModel
	{
		#region Properties

		private string										_code;

		public string										Station		{ get; private set; }

		private List<Model.NextArrivalContainer<string>>	_trams;
		public List<Model.NextArrivalContainer<string>>		Trams
		{
			get { return (_trams); }
			set
			{
				if (_trams != value)
				{
					_trams = value;
					OnNotifyPropertyChanged("Trams");
					OnNotifyPropertyChanged("TramIsEmpty");
				}
			}
		}

		private List<Model.NextArrivalContainer<string>>	_bus;
		public List<Model.NextArrivalContainer<string>>		Bus
		{
			get { return (_bus); }
			set
			{
				if (_bus != value)
				{
					_bus = value;
					OnNotifyPropertyChanged("Bus");
					OnNotifyPropertyChanged("BusIsEmpty");
				}
			}
		}

		private bool										_loaded;
		public bool											Loaded
		{
			get { return (_loaded); }
			set
			{
				if (_loaded != value)
				{
					_loaded = value;
					OnNotifyPropertyChanged("Loaded");
				}
			}
		}

		public bool											BusIsEmpty
		{
			get { return (!(Bus.Count == 0)); }
		}

		public bool											TramIsEmpty
		{
			get { return (!(Trams.Count == 0)); }
		}

		#endregion //Properties

		#region Commands

		public ICommand AddToFav	{ get; private set; }
		public ICommand Refresh		{ get; private set; }

		#endregion //Commands

		#region Actions

		private async void refresh()
		{
			Trams.Clear();
			Bus.Clear();
			Loaded = true;

			try
			{
				WebService.Request.NextArrival	nextArrival = new WebService.Request.NextArrival();
				int nb = SimpleIoc.Default.GetInstance<ViewModel.ConfigurationViewModel>().Configuration.NumberForecast;


				var trams	= await nextArrival.GetAllTram(_code, nb.ToString());
				var bus		= await nextArrival.GetAllBus(_code, nb.ToString());

				var tramGrouped = from item in trams
								  orderby item.Destination
								  group item by item.Destination into tramGroupedByDestination
								  select new Model.NextArrivalContainer<string>(tramGroupedByDestination.Key,
									  (from item in tramGroupedByDestination select item.Time).ToList());

				var busGrouped = from item in bus
								 orderby item.Destination
								 group item by item.Destination into tramGroupedByDestination
								 select new Model.NextArrivalContainer<string>(tramGroupedByDestination.Key,
									(from item in tramGroupedByDestination select item.Time).ToList());


				Trams = new List<Model.NextArrivalContainer<string>>(tramGrouped);
				Bus = new List<Model.NextArrivalContainer<string>>(busGrouped);
				Loaded = false;
			}
			catch
			{

			}
		}

		private void addToFav()
		{
		}

		#endregion //Actions

		public ScheduleStationViewModel()
			: base(null)
		{
			Loaded		= false;
			_code		= string.Empty;
			Station		= string.Empty;
			Trams		= new List<Model.NextArrivalContainer<string>>();
			Bus			= new List<Model.NextArrivalContainer<string>>();
			AddToFav	= new Commands.SimpleCommand(addToFav);
			Refresh		= new Commands.SimpleCommand(refresh);
		}

		[PreferredConstructorAttribute]
		public ScheduleStationViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Loaded		= false;
			_code		= string.Empty;
			Station		= string.Empty;
			Trams		= new List<Model.NextArrivalContainer<string>>();
			Bus			= new List<Model.NextArrivalContainer<string>>();
			AddToFav	= new Commands.SimpleCommand(addToFav);
			Refresh		= new Commands.SimpleCommand(refresh);

			_navigationService.Navigated += _navigationService_Navigated;
		}

		private void _navigationService_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			refresh();
		}

		public override void OnNavigate(params string[] args)
		{
			_code	= args[0];
			Station	= args[1];
		}

		public void OnNavigateRefresh(params string[] args)
		{
			OnNavigate(args);
			refresh();
		}
	}
}
