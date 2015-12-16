using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.ViewModel
{
	public class StationViewModel : AViewModel
	{
		#region Properties

		private WebService.Models.Stop							_selectedItem;
		public WebService.Models.Stop							SelectedItem
		{
			get { return (_selectedItem); }
			set
			{
				if (_selectedItem != value)
				{
					_selectedItem = value;
					OnNotifyPropertyChanged("SelectedItem");
				}
			}
		}

		private ObservableCollection<WebService.Models.Stop>	_favoris;
		public ObservableCollection<WebService.Models.Stop>		Favoris
		{
			get { return (_favoris); }
			private set
			{
				if (_favoris != value)
				{
					_favoris = value;
					OnNotifyPropertyChanged("Favoris");
				}
			}
		}

		private ServiceAgent.StationServiceAgent				_serviceAgent;

		#endregion //Properties

		#region Commands

		public ICommand Search			{ get; private set; }
		public ICommand SearchTaped		{ get; private set; }
		public ICommand AddToFavoris	{ get; private set; }
		public ICommand Delete			{ get; private set; }
		public ICommand Pin				{ get; private set; }

		#endregion //Commands

		#region Actions

		private void navigate()
		{
			if (SelectedItem != null)
			{
				SimpleIoc.Default.GetInstance<ScheduleStationViewModel>().OnNavigate(SelectedItem.Code, SelectedItem.Wording);
				_navigationService.NavigateTo("/Graphical/View/ScheduleStationView.xaml");
			}
		}

		private void navigateTaped(object sender)
		{
			WebService.Models.Stop	stop = sender as WebService.Models.Stop;

			if (stop == null)
				return;
			SimpleIoc.Default.GetInstance<ScheduleStationViewModel>().OnNavigate(stop.Code, stop.Wording);
			_navigationService.NavigateTo("/Graphical/View/ScheduleStationView.xaml");
		}

		private void addToFavoris()
		{
			if (Favoris.Where(item => item.Code.Equals(SelectedItem.Code)).SingleOrDefault() == null)
				Favoris.Add(SelectedItem);
		}

		private void delete(object sender)
		{
			WebService.Models.Stop stop = sender as WebService.Models.Stop;

			if (stop == null)
				return;
			Favoris.Remove(stop);
		}

		private void pin(object sender)
		{
			WebService.Models.Stop stop = sender as WebService.Models.Stop;

			if (stop == null)
				return;
			try
			{
				IconicTileData tile = new IconicTileData()
				{
					Title			= stop.Wording,
					SmallIconImage	= new Uri("/Graphical/Assets/tram.png", UriKind.Relative),
					IconImage		= new Uri("/Graphical/Assets/tram.png", UriKind.Relative)
				};
				ShellTile.Create(new Uri(string.Format("/Graphical/View/Tiles/ScheduleStationView.xaml?code={0}&wording={1}", stop.Code, stop.Wording),
					UriKind.Relative), tile, false);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		#endregion //Actions

		public StationViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Favoris			= new ObservableCollection<WebService.Models.Stop>();
			SelectedItem	= null;
			_serviceAgent	= new ServiceAgent.StationServiceAgent();

			Search			= new Commands.SimpleCommand(navigate);
			SearchTaped		= new Commands.CommandSender(navigateTaped);
			AddToFavoris	= new Commands.SimpleCommand(addToFavoris);
			Delete			= new Commands.CommandSender(delete);
			Pin				= new Commands.CommandSender(pin);
		}

		public void Save()
		{
			_serviceAgent.Serialize(Favoris.ToList());
		}

		public async override Task<bool> Initialize(params string[] args)
		{
			try
			{
				var favoris = await _serviceAgent.UnserializeStationAsync();
				if (favoris != null)
					Favoris = new ObservableCollection<WebService.Models.Stop>(favoris);
				return (true);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (false);
		}
	}
}
