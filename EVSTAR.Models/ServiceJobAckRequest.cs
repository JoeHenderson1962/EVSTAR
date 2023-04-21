using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{

    [XmlRoot(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJobAckServiceJob
    {

        [XmlElement(ElementName = "serviceJobID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobID { get; set; }

    }

    [XmlRoot(ElementName = "serviceJobAckRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJobAckRequest
    {

        [XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public String Version { get; set; }

        [XmlElement(ElementName = "sourceSystemName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SourceSystemName { get; set; }

        [XmlElement(ElementName = "sourceSystemVersion", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public double SourceSystemVersion { get; set; }

        [XmlElement(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public ServiceJobAckServiceJob ServiceJob { get; set; }

        [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class ServiceJobAckBody
    {

        [XmlElement(ElementName = "serviceJobAckRequest", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public ServiceJobAckRequest ServiceJobAckRequest { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class ServiceJobAckEnvelope
    {

        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Header Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public ServiceJobAckBody Body { get; set; }

        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }

        [XmlAttribute(AttributeName = "soapenv", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soapenv { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}
