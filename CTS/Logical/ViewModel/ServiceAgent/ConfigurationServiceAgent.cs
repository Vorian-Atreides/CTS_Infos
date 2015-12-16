using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel.ServiceAgent
{
	public class ConfigurationServiceAgent : AServiceArgent
	{
		private const string	_configurationPath	= "configuration.xml";
		private const string	_stopsPath			= "stops.xml";

		public ConfigurationServiceAgent()
		{
		}

		#region Configuration

		public void Serialize(Model.Configuration configuration)
		{
			Tools.XML<Model.Configuration> serializer = new Tools.XML<Model.Configuration>();
			Serialize<Model.Configuration>(serializer, _configurationPath, configuration);
		}

		public async Task<Model.Configuration> UnserializeConfigurationAsync()
		{
			Tools.XML<Model.Configuration> serializer = new Tools.XML<Model.Configuration>();
			return (await Unserialize<Model.Configuration>(serializer, _configurationPath));
		}

		#endregion //Configuration

		#region Stops

		public void Serialize(List<WebService.Models.Stop> stops)
		{
			Tools.XML<List<WebService.Models.Stop>> serializer = new Tools.XML<List<WebService.Models.Stop>>();
			Serialize<List<WebService.Models.Stop>>(serializer, _stopsPath, stops);
		}

		public async Task<List<WebService.Models.Stop>> UnserializeStopsAsync()
		{
			try
			{
				Tools.XML<List<WebService.Models.Stop>> serializer = new Tools.XML<List<WebService.Models.Stop>>();
				return (await Unserialize<List<WebService.Models.Stop>>(serializer, _stopsPath));
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			return (new List<WebService.Models.Stop>());
		}


		#endregion //Stops
	}
}
