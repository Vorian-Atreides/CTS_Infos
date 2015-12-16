using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiffertGaston.CTSInfos.WebService.Request
{
	public interface IRequest
	{
		string RequestBuilder(params string[] args);
	}

	public abstract class ARequest : IRequest
	{
		protected const string	_id					= "63";
		protected const string	_password			= "epitech";
		protected const string	_soapNamespace		= "http://www.cts-strasbourg.fr/";
		protected const string	_internalError		= "Internal error";

		protected string		CredentialHeader	{ get; private set; }

		public abstract string	RequestBuilder(params string[] args);

		public ARequest()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
			.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">")
			.Append("<soap:Header>")
			.Append("<CredentialHeader xmlns=\"http://www.cts-strasbourg.fr/\">")
			.Append("<ID>").Append(_id).Append("</ID>")
			.Append("<MDP>").Append(_password).Append("</MDP>")
			.Append("</CredentialHeader>")
			.Append("</soap:Header>");
			CredentialHeader = builder.ToString();
		}

		protected string GetValue(XElement element, XNamespace space, string name)
		{
			XElement tmp = element.Element(space + name);
			return ((tmp != null) ? tmp.Value : null);
		}
	}
}
