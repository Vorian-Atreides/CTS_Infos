using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.WebService.Models
{
	public class Arrival : AModel
	{
		#region Properties

		public string	Mode			{ get; set; }
		public string	Destination		{ get; set; }
		public string	Time			{ get; set; }

		#endregion //Properties

		public Arrival()
		{
			Mode		= string.Empty;
			Destination = string.Empty;
			Time		= string.Empty;
		}
	}
}
