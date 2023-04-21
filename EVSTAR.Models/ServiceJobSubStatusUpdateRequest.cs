using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{

	[XmlRoot(ElementName = "mailin", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class Mailin
	{

		[XmlElement(ElementName = "shippedItem", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ShippedItem { get; set; }

		[XmlElement(ElementName = "status", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string Status { get; set; }

		[XmlElement(ElementName = "shippingVendor", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ShippingVendor { get; set; }

		[XmlElement(ElementName = "trackingNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string TrackingNumber { get; set; }

		[XmlElement(ElementName = "note", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string Note { get; set; }
	}

	[XmlRoot(ElementName = "mailIns", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobSubStatusUpdateRequestMailIns
	{

		[XmlElement(ElementName = "mailin", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public List<Mailin> Mailin { get; set; }
	}

	[XmlRoot(ElementName = "service", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobSubStatusUpdateRequestService
	{

		[XmlElement(ElementName = "serviceID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceID { get; set; }

		[XmlElement(ElementName = "serviceStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceStatus { get; set; }
	}

	[XmlRoot(ElementName = "services", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class Services
	{

		[XmlElement(ElementName = "service", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public List<ServiceJobSubStatusUpdateRequestService> Service { get; set; }
	}

	[XmlRoot(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobSubStatusUpdateRequestServiceJob
	{

		[XmlElement(ElementName = "serviceJobID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceJobID { get; set; }

		[XmlElement(ElementName = "serviceJobSubStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string ServiceJobSubStatus { get; set; }

		[XmlElement(ElementName = "technicianID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string TechnicianID { get; set; }

		[XmlElement(ElementName = "comments", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public Comments Comments { get; set; }

		[XmlElement(ElementName = "spServiceJobReferenceNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SpServiceJobReferenceNumber { get; set; }

		[XmlElement(ElementName = "mailIns", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public MailIns MailIns { get; set; }

		[XmlElement(ElementName = "services", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public Services Services { get; set; }
	}

	[XmlRoot(ElementName = "serviceJobSubStatusUpdateRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
	public class ServiceJobSubStatusUpdateRequest
	{

		[XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string Version { get; set; }

		[XmlElement(ElementName = "sourceSystemName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public string SourceSystemName { get; set; }

		[XmlElement(ElementName = "sourceSystemVersion", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public double SourceSystemVersion { get; set; }

		[XmlElement(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobSubStatusUpdateRequestServiceJob ServiceJob { get; set; }

		[XmlAttribute(AttributeName = "xmlns", Namespace = "")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobSubStatusUpdateRequestBody
	{

		[XmlElement(ElementName = "serviceJobSubStatusUpdateRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
		public ServiceJobSubStatusUpdateRequest ServiceJobSubStatusUpdateRequest { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ServiceJobSubStatusUpdateRequestEnvelope
	{

		[XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public Header Header { get; set; }

		[XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public ServiceJobSubStatusUpdateRequestBody Body { get; set; }

		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }

		[XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soapenv { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

}
