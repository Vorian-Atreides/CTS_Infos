using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SiffertGaston.CTSInfos.WebService.Models
{
	public class Deviation : AModel
	{
		public int			ID				{ get; set; }
		public string		Title			{ get; set; }
		public string		Description		{ get; set; }
		public DateTime?	Begin			{ get; set; }
		public DateTime?	End				{ get; set; }
		public string		Category		{ get; set; }
		public bool			Exergue			{ get; set; }
		public int			Position		{ get; set; }

		public Deviation()
		{
			ID			= 0;
			Title		= string.Empty;
			Description = string.Empty;
			Begin		= null;
			End			= null;
			Category	= string.Empty;
			Exergue		= false;
			Position	= 0;
		}
	}
}
