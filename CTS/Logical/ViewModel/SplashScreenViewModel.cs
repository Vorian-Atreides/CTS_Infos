using Cimbalino.Phone.Toolkit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public class SplashScreenViewModel : AViewModel
	{
		#region Properties

		private bool	_loaded;
		public bool		Loaded
		{
			get { return (_loaded); }
			set
			{
				if (_loaded != value)
				{
					_loaded = value;
					OnNotifyPropertyChanged("Loaded");
					if (Loaded == false && _navigationService.CurrentSource.ToString() == @"/Graphical\View\SplashScreenView.xaml")
					{
						_navigationService.Navigated += _navigationService_Navigated;
						_navigationService.NavigateTo("/Graphical/View/MainPage.xaml");
					}
				}
			}
		}

		void _navigationService_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			_navigationService.Navigated -= _navigationService_Navigated;
			_navigationService.RemoveBackEntry();
		}

		#endregion //Properties

		public SplashScreenViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			_loaded = true;
		}
	}
}
