using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.ViewModel
{
	public class ViewModelLocator
	{
		#region Properties

		public MainViewModel			MainViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<MainViewModel>()); }
		}

		public ConfigurationViewModel	ConfigurationViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<ConfigurationViewModel>()); }
		}

		public DeviationViewModel		DeviationViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<DeviationViewModel>()); }
		}

		public StationViewModel			StationViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<StationViewModel>()); }
		}

		public ScheduleStationViewModel ScheduleStationViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<ScheduleStationViewModel>()); }
		}

		public VelhopViewModel			VelhopViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<VelhopViewModel>()); }
		}

		public SplashScreenViewModel	SplashScreenViewModel
		{
			get { return (ServiceLocator.Current.GetInstance<SplashScreenViewModel>()); }
		}

		#endregion //Properties

		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (!SimpleIoc.Default.IsRegistered<INavigationService>())
				SimpleIoc.Default.Register<INavigationService, NavigationService>();

			SimpleIoc.Default.Register<MainViewModel>();
			SimpleIoc.Default.Register<ConfigurationViewModel>();
			SimpleIoc.Default.Register<DeviationViewModel>();
			SimpleIoc.Default.Register<StationViewModel>();
			SimpleIoc.Default.Register<ScheduleStationViewModel>();
			SimpleIoc.Default.Register<VelhopViewModel>();
			SimpleIoc.Default.Register<SplashScreenViewModel>();
		}
	}
}
