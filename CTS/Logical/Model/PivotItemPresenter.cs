using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.Model
{
	public class PivotItemPresenter
	{
		#region Properties

		public string		Header	{ get; set; }
		public UserControl	Content	{ get; set; }

		#endregion //Properties

		public PivotItemPresenter()
		{
			Header	= string.Empty;
			Content	= null;
		}
	}
}
