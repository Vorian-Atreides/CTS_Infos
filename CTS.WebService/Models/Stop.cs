using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.WebService.Models
{
	public class Stop : AModel
	{
		#region Properties

		public string	Wording	{ get; set; }
		public string	Code	{ get; set; }

		#endregion //Properties

		public Stop()
		{
			Wording = string.Empty;
			Code	= string.Empty;
		}
	}
}
