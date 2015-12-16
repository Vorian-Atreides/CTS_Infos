using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public class ConfigurationViewModel : AViewModel
	{
		#region Properties

		public Model.Configuration						Configuration	{ get; private set; }

		private List<WebService.Models.Stop>			_stops;
		public List<WebService.Models.Stop>				Stops
		{
			get { return (_stops); }
			private set
			{
				if (_stops != value)
				{
					_stops = value;
					OnNotifyPropertyChanged("Stops");
				}
			}
		}

		private ServiceAgent.ConfigurationServiceAgent	_serviceAgent;

		#endregion //Properties

		public ConfigurationViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_serviceAgent	= new ServiceAgent.ConfigurationServiceAgent();
			Configuration	= new Model.Configuration();
			Stops			= new List<WebService.Models.Stop>();
		}

		public void Save()
		{
			_serviceAgent.Serialize(Configuration);
			_serviceAgent.Serialize(Stops);
		}

		#region AViewModel#IVIewModel

		public async override Task<bool> Initialize(params string[] args)
		{
			List<WebService.Models.Stop> stops = null;

			try
			{
				var conf = await _serviceAgent.UnserializeConfigurationAsync();
				if (conf != null)
					Configuration = conf;
				if (Configuration != null && Configuration.LastUpdate.HasValue && (Configuration.LastUpdate.Value - DateTime.Now).TotalDays <= 10)
					stops = await _serviceAgent.UnserializeStopsAsync();
				if (stops == null || stops.Count == 0)
				{
					stops = await new WebService.Request.StopCode().GetAllStops();
					Configuration.LastUpdate = DateTime.Now;
				}
				if (stops != null)
					Stops = stops;
				if (Configuration != null && !Configuration.LocationAlreadyRequested)
				{
					MessageBoxResult result = MessageBox.Show("L'application peut elle utiliser vos coordonnées ?", "Droit", MessageBoxButton.OKCancel);
					if (result == MessageBoxResult.OK)
						Configuration.CanUseLocation = true;
					Configuration.LocationAlreadyRequested = true;
				}
				return (true);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (false);
		}

		#endregion //AViewModel#IVIewModel
	}
}
