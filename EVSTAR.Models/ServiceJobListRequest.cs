using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{
	[XmlRoot(ElementName = "Password", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
	public class Password
	{

		[XmlAttribute(AttributeName = "Type", Namespace = "")]
		public string Type { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "UsernameToken", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
	public class UsernameToken
	{

		[XmlElement(ElementName = "Username", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
		public string Username { get; set; }

		[XmlElement(ElementName = "Password", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
		public Password Password { get; set; }

		[XmlAttribute(AttributeName = "Id", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
		public string Id { get; set; }

		[XmlAttribute(AttributeName = "wsu", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Wsu { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
	public class Security
	{

		[XmlElement(ElementName = "UsernameToken", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
		public UsernameToken UsernameToken { get; set; }

		[XmlAttribute(AttributeName = "mustUnderstand", Namespace = "soapenv")]
		public int MustUnderstand { get; set; }

		[XmlAttribute(AttributeName = "wsse", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Wsse { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class Header
	{

		[XmlElement(ElementName = "Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
		public Security Security { get; set; }
	}

	[XmlRoot(ElementName = "serviceJobListRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobListRequest
	{

		[XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public String Version { get; set; }

		[XmlElement(ElementName = "sourceSystemName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SourceSystemName { get; set; }

		[XmlElement(ElementName = "sourceSystemVersion", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public double SourceSystemVersion { get; set; }

		[XmlElement(ElementName = "spDownloadMarked", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public int SpDownloadMarked { get; set; }

		[XmlElement(ElementName = "spDownloadMarkedDateFrom", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpDownloadMarkedDateFrom { get; set; }

		[XmlElement(ElementName = "spDownloadMarkedTimeFrom", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpDownloadMarkedTimeFrom { get; set; }

		[XmlElement(ElementName = "spDownloadMarkedDateTo", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpDownloadMarkedDateTo { get; set; }

		[XmlElement(ElementName = "spDownloadMarkedTimeTo", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpDownloadMarkedTimeTo { get; set; }

		[XmlElement(ElementName = "downloadLimit", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public int DownloadLimit { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class JobListBody
	{

		[XmlElement(ElementName = "serviceJobListRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobListRequest ServiceJobListRequest { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class JobListEnvelope
	{

		[XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public Header Header { get; set; }

		[XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public JobListBody Body { get; set; }

		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }

		[XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soapenv { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

}
