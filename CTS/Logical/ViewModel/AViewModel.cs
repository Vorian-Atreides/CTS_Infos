using Cimbalino.Phone.Toolkit.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel
{
	public interface IViewModel
	{
		Task<bool>	Initialize(params string[] args);
		void		OnNavigate(params string[] args);
	}

	public abstract class AViewModel : IViewModel, INotifyPropertyChanged
	{
		protected readonly INavigationService	_navigationService;

		public AViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		#region IVIewModel

		public virtual Task<bool> Initialize(params string[] args)
		{
			return (null);
		}

		public virtual void OnNavigate(params string[] args)
		{
		}

		#endregion //IViewModel

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnNotifyPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion //INotifyPropertyChanged
	}
}
