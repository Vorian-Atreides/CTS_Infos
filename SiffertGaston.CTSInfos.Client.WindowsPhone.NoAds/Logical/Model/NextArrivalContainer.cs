using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.NoAds.Logical.Model
{
	public class NextArrivalContainer<T> : List<T>
	{
		#region Properties

		public string	Destination		{ get; private set; }
		public string	Line			{ get; private set; }

		#endregion //Properties

		public NextArrivalContainer(string key, List<T> items)
			: base(items)
		{
			if (key.Contains(' '))
			{
				int index	= key.IndexOf(' ');
				Line		= key.Substring(0, index);
				Destination = key.Substring(index + 1);
			}
			else
			{
				Destination = string.Empty;
				Line		= string.Empty;
			}
		}
	}
}
