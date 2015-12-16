using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.ViewModel.Commands
{
	public class CommandSender : ICommand
	{
		private Action<object>	_action;

		public CommandSender(Action<object> action)
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
			_action(parameter);
		}

		#endregion //ICommand
	}
}
