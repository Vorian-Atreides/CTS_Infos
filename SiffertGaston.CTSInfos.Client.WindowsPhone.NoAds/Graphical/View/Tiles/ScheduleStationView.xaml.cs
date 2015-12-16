using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Graphical.View.Tiles
{
	public partial class ScheduleStationView : PhoneApplicationPage
	{
		public ScheduleStationView()
		{
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Logical.ViewModel.ScheduleStationViewModel viewModel = DataContext as Logical.ViewModel.ScheduleStationViewModel;
			if (viewModel == null)
				return;
			viewModel.OnNavigateRefresh(NavigationContext.QueryString["code"], NavigationContext.QueryString["wording"]);
		}
	}
}
