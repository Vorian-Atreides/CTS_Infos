using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.Model
{
	public class Configuration
	{
		#region Properties

		public bool			LocationAlreadyRequested	{ get; set; }
		public bool			CanUseLocation				{ get; set; }
		public DateTime?	LastUpdate					{ get; set; }

		private int			_numberForecast;
		public int			NumberForecast
		{
			get { return (_numberForecast); }
			set
			{
				if (_numberForecast != value && value > 0 && value <= 5)
					_numberForecast = value;
			}
		}

		#endregion //Properties

		public Configuration()
		{
			LocationAlreadyRequested	= false;
			CanUseLocation				= false;
			NumberForecast				= 5;
			LastUpdate					= null;
		}
	}
}
