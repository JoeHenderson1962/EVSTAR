using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{

	[XmlRoot(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobStatusUpdateServiceJob
	{

		[XmlElement(ElementName = "serviceJobID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceJobID { get; set; }

		[XmlElement(ElementName = "serviceJobStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceJobStatus { get; set; }

		[XmlElement(ElementName = "spServiceJobReferenceNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpServiceJobReferenceNumber { get; set; }

		[XmlElement(ElementName = "rejectReason", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string RejectReason { get; set; }

		[XmlElement(ElementName = "rejectCode", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string RejectCode { get; set; }

		[XmlElement(ElementName = "serviceExplanation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceExplanation { get; set; }

		[XmlElement(ElementName = "serviceDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public DateTime ServiceDate { get; set; }

		[XmlElement(ElementName = "serviceTime", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceTime { get; set; }

		[XmlElement(ElementName = "externalUserId", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ExternalUserId { get; set; }

		[XmlElement(ElementName = "externalUserName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ExternalUserName { get; set; }
	}

	[XmlRoot(ElementName = "serviceJobStatusUpdateRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobStatusUpdateRequest
	{

		[XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public String Version { get; set; }

		[XmlElement(ElementName = "sourceSystemName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SourceSystemName { get; set; }

		[XmlElement(ElementName = "sourceSystemVersion", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public double SourceSystemVersion { get; set; }

		[XmlElement(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobStatusUpdateServiceJob ServiceJob { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobStatusUpdateBody
	{

		[XmlElement(ElementName = "serviceJobStatusUpdateRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobStatusUpdateRequest ServiceJobStatusUpdateRequest { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobStatusUpdateEnvelope
	{

		[XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public Header Header { get; set; }

		[XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public ServiceJobStatusUpdateBody Body { get; set; }

		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }

		[XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soapenv { get; set; }

		[XmlText]
		public string Text { get; set; }
	}


}
