using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiffertGaston.CTSInfos.WebService.Request
{
	public class VelhopStopInfo : ARequest
	{

		#region Properties

		private const string	_url = "http://velhop.strasbourg.eu/tvcstations.xml";

		#endregion //Properties

		public async Task<List<Models.VelhopStop>>	SendRequest()
		{
			try
			{
				using (HttpClient			client		= new HttpClient())
				using (HttpResponseMessage	response	= await client.GetAsync(_url))
				{
					if (response.IsSuccessStatusCode)
					{
						XDocument xDoc = XDocument.Load(await response.Content.ReadAsStreamAsync());
						var items = xDoc.Elements().Elements("sl").Elements("si");

						List<Models.VelhopStop> stops = new List<Models.VelhopStop>();
						foreach (var item in items)
						{
							try
							{
								var velhop = Hydrate(item, XNamespace.None);
								stops.Add(velhop);
							}
							catch { }
						}
						return (stops);
					}
					throw new Exception(response.StatusCode + " " + await response.Content.ReadAsStringAsync());
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			throw new Exception(_internalError);
		}

		private Models.VelhopStop	Hydrate(XElement element, XNamespace space)
		{
			int					tmp;
			NumberFormatInfo	format = new NumberFormatInfo()
			{
				NumberDecimalSeparator = "."
			};
	
			Models.VelhopStop	velhopStop = new Models.VelhopStop()
			{
				ID			= int.Parse(GetAttrValue(element, "id")),
				Name		= GetAttrValue(element, "na"),
				Latitude	= double.Parse(GetAttrValue(element, "la"), format),
				Longitude	= double.Parse(GetAttrValue(element, "lg"), format),
				Available	= int.Parse(GetAttrValue(element, "av")),
				Total		= int.Parse(GetAttrValue(element, "to"))
			};
			velhopStop.CB = int.TryParse(GetAttrValue(element, "cb"), out tmp) ? tmp : 0;
			return (velhopStop);
		}

		private string GetAttrValue(XElement element, string name)
		{
			XAttribute attr = element.Attribute(name);
			return ((attr != null) ? attr.Value : null);
		}

		#region ARequest#IRequest

		public override string RequestBuilder(params string[] args)
		{
			return (string.Empty);
		}

		#endregion //ARequest#IRequest
	}
}
