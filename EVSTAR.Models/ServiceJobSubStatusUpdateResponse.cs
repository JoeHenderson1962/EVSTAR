using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (Envelope)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "serviceJob")]
    public class ServiceJobSubStatusUpdateResponseServiceJob
    {

        [XmlElement(ElementName = "serviceJobID")]
        public string ServiceJobID { get; set; }

        [XmlElement(ElementName = "success")]
        public string Success { get; set; }
    }

    [XmlRoot(ElementName = "serviceJobSubStatusUpdateResponse")]
    public class ServiceJobSubStatusUpdateResponse
    {

        [XmlElement(ElementName = "version")]
        public DateTime Version { get; set; }

        [XmlElement(ElementName = "msgStatus")]
        public string MsgStatus { get; set; }

        [XmlElement(ElementName = "serviceJob")]
        public ServiceJobSubStatusUpdateResponseServiceJob ServiceJob { get; set; }

        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Body")]
    public class ServiceJobSubStatusUpdateResponseBody
    {

        [XmlElement(ElementName = "serviceJobSubStatusUpdateResponse")]
        public ServiceJobSubStatusUpdateResponse ServiceJobSubStatusUpdateResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope")]
    public class ServiceJobSubStatusUpdateResponseEnvelope
    {

        [XmlElement(ElementName = "Body")]
        public ServiceJobSubStatusUpdateResponseBody Body { get; set; }

        [XmlAttribute(AttributeName = "soap")]
        public string Soap { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

}
