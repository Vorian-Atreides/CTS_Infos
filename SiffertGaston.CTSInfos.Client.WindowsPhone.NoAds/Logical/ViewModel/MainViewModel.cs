using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Windows.Storage;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.ViewModel
{
	public class MainViewModel : AViewModel
	{
		#region Commands

		public ICommand NavigateToConfiguration { get; private set; }
		public ICommand NavigateToImage			{ get; private set; }
		public ICommand SelectionChanged		{ get; private set; }
		public ICommand RateApplication			{ get; private set; }

		#endregion //Commands

		#region Actions

		private void navigateToConfiguration()
		{
			_navigationService.NavigateTo("/Graphical/View/ConfigurationView.xaml");
		}

		private void selectionChanged(object sender)
		{
			IViewModel	viewModel;

			viewModel = sender as IViewModel;
			if (viewModel == null)
				return;
			viewModel.OnNavigate();
		}

		private void rateApplication()
		{
			MarketplaceDetailTask task = new MarketplaceDetailTask()
			{
				ContentIdentifier = "9a9f44c5-37cf-4ef3-9a3f-b7fabd9517b8",
				ContentType = MarketplaceContentType.Applications
			};
			task.Show();
		}

		private void navigateToImage(object sender)
		{
			if (sender == null)
				return;
			string source = sender.ToString();
			_navigationService.NavigateTo(new Uri(string.Format("/Graphical/View/ZoomableImage.xaml?source={0}", Uri.EscapeUriString(source)), UriKind.Relative));
		}

		#endregion //Actions

		public MainViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			NavigateToConfiguration = new Commands.SimpleCommand(navigateToConfiguration);
			NavigateToImage			= new Commands.CommandSender(navigateToImage);
			SelectionChanged		= new Commands.CommandSender(selectionChanged);
			RateApplication			= new Commands.SimpleCommand(rateApplication);
		}
	}
}
