using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.WebService.Models
{
	public class VelhopStop : AModel
	{
		#region Properties

		public int		ID			{ get; set; }
		public string	Name		{ get; set; }
		public double	Latitude	{ get; set; }
		public double	Longitude	{ get; set; }
		public int		Available	{ get; set; }
		public int		Total		{ get; set; }
		public int		CB			{ get; set; }

		#endregion //Properties

		public VelhopStop()
		{
			ID			= 0;
			Name		= string.Empty;
			Latitude	= 0;
			Longitude	= 0;
			Available	= 0;
			Total		= 0;
			CB			= 0;
		}
	}
}
