using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SiffertGaston.CTSInfos.WebService.Request
{
	public class DeviationInfo : ARequest
	{
		#region Properties

		private const string	_url	= "http://opendata.cts-strasbourg.fr/webservice_v4/Service.asmx?op=deviations";
		private const string	_name	= "Deviations";

		#endregion //Properties

		public async Task<List<Models.Deviation>> SendRequest(params string[] args)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					StringContent request		= new StringContent(RequestBuilder());
					request.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
					using (HttpResponseMessage response = await client.PostAsync(_url, request))
					{
						if (response.IsSuccessStatusCode)
						{
							XDocument xDoc	= XDocument.Load(await response.Content.ReadAsStreamAsync());
							XNamespace soap = XNamespace.Get(_soapNamespace);
							var items		= xDoc.Descendants(soap + _name).Elements();

							List<Models.Deviation>	deviations = new List<Models.Deviation>();

							foreach (var item in items)
							{
								try
								{
									var deviation = Hydrate(item, soap);
									deviations.Add(deviation);
								}
								catch { }
							}
							return (deviations);
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

		private Models.Deviation Hydrate(XElement element, XNamespace space)
		{
			Models.Deviation	deviation = new Models.Deviation()
			{
				ID				= int.Parse(GetValue(element, space, "ID")),
				Title			= GetValue(element, space, "Titre"),
				Description		= GetValue(element, space, "Description"),
				Begin			= DateTime.Parse(GetValue(element, space, "Debut")),
				End				= DateTime.Parse(GetValue(element, space, "Fin")),
				Category		= GetValue(element, space, "Categorie"),
				Exergue			= bool.Parse(GetValue(element, space, "Exergue")),
				Position		= int.Parse(GetValue(element, space, "Position")),
			};
			return (deviation);
		}

		#region ARequest#IRequest

		public override string	RequestBuilder(params string[] args)
		{
			StringBuilder builder = new StringBuilder(CredentialHeader);

			builder.Append("<soap:Body>")
                .Append("<deviations xmlns=\"http://www.cts-strasbourg.fr/\" />")
                .Append("</soap:Body>")
                .Append("</soap:Envelope>");
			return (builder.ToString());
		}

		#endregion //ARequest#IRequest
	}
}
