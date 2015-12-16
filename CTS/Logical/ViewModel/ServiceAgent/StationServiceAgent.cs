using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiffertGaston.CTSInfos.Client.WindowsPhone.Ads.Logical.ViewModel.ServiceAgent
{
	public class StationServiceAgent : AServiceArgent
	{
		private const string	_stationPath = "station.xml";

		public StationServiceAgent()
		{
		}

		public void Serialize(List<WebService.Models.Stop> stations)
		{
			Tools.XML<List<WebService.Models.Stop>> serializer = new Tools.XML<List<WebService.Models.Stop>>();
			Serialize<List<WebService.Models.Stop>>(serializer, _stationPath, stations);
		}

		public async Task<List<WebService.Models.Stop>> UnserializeStationAsync()
		{
			Tools.XML<List<WebService.Models.Stop>> serializer = new Tools.XML<List<WebService.Models.Stop>>();
			return (await Unserialize<List<WebService.Models.Stop>>(serializer, _stationPath));
		}
	}
}
