using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiffertGaston.CTSInfos.WebService.Request
{
	public class NextArrival : ARequest
	{
		#region Properties

		private const string	_url	= "http://opendata.cts-strasbourg.fr/webservice_v4/Service.asmx?op=rechercheProchainesArriveesWeb";
		private const string	_name	= "ListeArrivee";

		#endregion //Properties

		public async Task<List<Models.Arrival>> GetAllTram(params string[] args)
		{
			return (await SendRequest(args[0], "1", args[1]));
		}

		public async Task<List<Models.Arrival>> GetAllBus(params string[] args)
		{
			return (await SendRequest(args[0], "2", args[1]));
		}

		public async Task<List<Models.Arrival>> SendRequest(params string[] args)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					StringContent request		= new StringContent(RequestBuilder(args));
					request.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
					using (HttpResponseMessage response = await client.PostAsync(_url, request))
					{
						if (response.IsSuccessStatusCode)
						{
							XDocument xDoc	= XDocument.Load(await response.Content.ReadAsStreamAsync());
							XNamespace soap = XNamespace.Get(_soapNamespace);
							var items		= xDoc.Descendants(soap + _name).Elements();

							List<Models.Arrival>	arrivals = new List<Models.Arrival>();

							foreach (var item in items)
							{
								try
								{
									var arrival = Hydrate(item, soap);
									arrivals.Add(arrival);
								}
								catch { }
							}
							return (arrivals);
						}
						throw new Exception(response.StatusCode + " " + await response.Content.ReadAsStringAsync());
					}
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
			throw new Exception(_internalError);
		}

		private Models.Arrival	Hydrate(XElement element, XNamespace space)
		{
			Models.Arrival	arrival = new Models.Arrival()
			{
				Destination	= GetValue(element, space, "Destination"),
				Mode		= GetValue(element, space, "Mode"),
				Time		= DateTime.Parse(GetValue(element, space, "Horaire")).ToString("HH:mm")
			};
			return (arrival);
		}

		#region ARequest#IRequest

		public override string RequestBuilder(params string[] args)
		{
			StringBuilder builder = new StringBuilder(CredentialHeader);

			builder.Append("<soap:Body>")
                .Append("<rechercheProchainesArriveesWeb xmlns=\"http://www.cts-strasbourg.fr/\">")
                .Append("<CodeArret>").Append(args[0]).Append("</CodeArret>")
                .Append("<Mode>").Append(args[1]).Append("</Mode>")
                .Append("<Heure>").Append(DateTime.Now.ToString("HH:mm")).Append("</Heure>")
                .Append("<NbHoraires>").Append(args[2]).Append("</NbHoraires>")
                .Append("</rechercheProchainesArriveesWeb>")
                .Append("</soap:Body>")
                .Append("</soap:Envelope>");
			return (builder.ToString());
		}

		#endregion //ARequest#IRequest
	}
}
