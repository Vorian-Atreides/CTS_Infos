using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiffertGaston.CTSInfos.WebService.Request
{
	public class StopCode : ARequest
	{
		#region Properties

		private const string	_url	= "http://opendata.cts-strasbourg.fr/webservice_v4/Service.asmx?op=rechercherCodesArretsDepuisLibelle";
		private const string	_name	= "ListeArret";

		#endregion //Properties

		public async Task<List<Models.Stop>>	GetAllStops()
		{
			bool				next	= true;
			int					i		= 1;
			List<Models.Stop>	stops	= new List<Models.Stop>();
			
			while (next)
			{
				try
				{
					var tmp = await SendRequest("", i.ToString());
					++i;
					if (tmp.Count == 0)
						next = false;
					else
						stops.AddRange(tmp);
				}
				catch
				{
					next = false;
				}
			}
			return (stops);
		}

		public async Task<List<Models.Stop>> SendRequest(params string[] args)
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
							XNamespace soap	= XNamespace.Get(_soapNamespace);
							var items		= xDoc.Descendants(soap + _name).Elements();

							List<Models.Stop>	stops = new List<Models.Stop>();

							foreach (var item in items)
							{
								try
								{
									var stop = Hydrate(item, soap);
									stops.Add(stop);
								}
								catch { }
							}
							return (stops);
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

		private Models.Stop Hydrate(XElement element, XNamespace space)
		{
			Models.Stop		stop = new Models.Stop()
			{
				Code		= GetValue(element, space, "Code"),
				Wording		= GetValue(element, space, "Libelle")
			};
			return (stop);
		}

		#region ARequest#IRequest

		public override string RequestBuilder(params string[] args)
		{
			StringBuilder builder = new StringBuilder(CredentialHeader);
		
			builder.Append("<soap:Body>")
                .Append("<rechercherCodesArretsDepuisLibelle xmlns=\"http://www.cts-strasbourg.fr/\">")
                .Append("<Saisie>").Append(args[0]).Append("</Saisie>")
                .Append("<NoPage>").Append(args[1]).Append("</NoPage>")
                .Append("</rechercherCodesArretsDepuisLibelle>")
                .Append("</soap:Body>")
                .Append("</soap:Envelope>");
			return (builder.ToString());
		}

		#endregion //ARequest#IRequest
	}
}
