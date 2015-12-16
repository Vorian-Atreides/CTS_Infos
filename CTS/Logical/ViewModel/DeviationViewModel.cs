using Cimbalino.Phone.Toolkit.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public class DeviationViewModel : AViewModel
	{
		#region Properties

		private ObservableCollection<WebService.Models.Deviation>	_deviations;
		public ObservableCollection<WebService.Models.Deviation>	Deviations
		{
			get { return (_deviations); }
			set
			{
				if (_deviations != value)
				{
					_deviations = value;
					OnNotifyPropertyChanged("Deviations");
				}
			}
		}

		#endregion //Properties

		public DeviationViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			Deviations = new ObservableCollection<WebService.Models.Deviation>();
		}

		#region AViewModel#IViewModel

		public async override Task<bool> Initialize(params string[] args)
		{
			WebService.Request.DeviationInfo deviations = new WebService.Request.DeviationInfo();
			try
			{
				var items = await deviations.SendRequest();
				Deviations = new ObservableCollection<WebService.Models.Deviation>(items);
				return (true);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (false);
		}

		#endregion //AViewModel#IViewModel
	}
}
