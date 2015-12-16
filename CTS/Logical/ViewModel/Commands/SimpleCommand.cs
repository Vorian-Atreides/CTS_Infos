using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel.Commands
{
	public class SimpleCommand : ICommand
	{
		private Action	_action;

		public SimpleCommand(Action action)
		{
			_action = action;
		}

		#region ICommand

		public bool CanExecute(object parameter)
		{
			return (true);
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_action();
		}

		#endregion //ICommand
	}
}
