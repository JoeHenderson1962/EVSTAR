using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{

	[XmlRoot(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobAckResponseServiceJob
	{

		[XmlElement(ElementName = "serviceJobID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceJobID { get; set; }

		[XmlElement(ElementName = "success", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string Success { get; set; }
	}

	[XmlRoot(ElementName = "serviceJobs", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobAckResponseServiceJobs
	{

		[XmlElement(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public List<ServiceJobAckResponseServiceJob> ServiceJob { get; set; }
	}

	[XmlRoot(ElementName = "statusDetails", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobAckStatusDetails
	{

		[XmlElement(ElementName = "serviceJobs", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobAckResponseServiceJobs ServiceJobs { get; set; }
	}

	[XmlRoot(ElementName = "serviceJobAckResponse", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobAckResponse
	{

		[XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public DateTime Version { get; set; }

		[XmlElement(ElementName = "msgStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string MsgStatus { get; set; }

		[XmlElement(ElementName = "statusDetails", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobAckStatusDetails StatusDetails { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobAckResponseBody
	{

		[XmlElement(ElementName = "serviceJobAckResponse", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobAckResponse ServiceJobAckResponse { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobAckResponseEnvelope
	{

		[XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public ServiceJobAckResponseBody Body { get; set; }

		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }

		[XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soapenv { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

}
