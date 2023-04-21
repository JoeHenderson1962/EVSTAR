using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EVSTAR.Models

{
    /* 
	 Licensed under the Apache License, Version 2.0

	 http://www.apache.org/licenses/LICENSE-2.0
	 */
    [XmlRoot(ElementName = "model", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class Model
    {
        [XmlElement(ElementName = "productRowNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductRowNumber { get; set; }
        [XmlElement(ElementName = "brand", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string Brand { get; set; }
        [XmlElement(ElementName = "modelID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ModelID { get; set; }
        [XmlElement(ElementName = "productName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductName { get; set; }
        [XmlElement(ElementName = "purchaseDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PurchaseDate { get; set; }
        [XmlElement(ElementName = "purchasedFrom", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PurchasedFrom { get; set; }
        [XmlElement(ElementName = "authorizationNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string AuthorizationNumber { get; set; }
        [XmlElement(ElementName = "serviceContractNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceContractNumber { get; set; }
        [XmlElement(ElementName = "serviceContractExpiryDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceContractExpiryDate { get; set; }
        [XmlElement(ElementName = "serialNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SerialNumber { get; set; }
        [XmlElement(ElementName = "pexDeliveredDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PexDeliveredDate { get; set; }
        [XmlElement(ElementName = "productLine", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductLine { get; set; }
        [XmlElement(ElementName = "productLineDescription", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductLineDescription { get; set; }
        [XmlElement(ElementName = "purchasePrice", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PurchasePrice { get; set; }
        [XmlElement(ElementName = "authorizedAmount", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string AuthorizedAmount { get; set; }
        [XmlElement(ElementName = "productStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductStatus { get; set; }
        [XmlElement(ElementName = "productSubStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProductSubStatus { get; set; }
        [XmlElement(ElementName = "checkedInDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CheckedInDate { get; set; }
        [XmlElement(ElementName = "checkedOutDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CheckedOutDate { get; set; }
        [XmlElement(ElementName = "serialNumberElectronic", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SerialNumberElectronic { get; set; }
    }

    [XmlRoot(ElementName = "models", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class Models
    {
        [XmlElement(ElementName = "model", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<Model> Model { get; set; }
    }

    [XmlRoot(ElementName = "entitlement", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class Entitlement
    {
        [XmlElement(ElementName = "entitlementType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string EntitlementType { get; set; }
        [XmlElement(ElementName = "entitlementDescription", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string EntitlementDescription { get; set; }
    }

    [XmlRoot(ElementName = "entitlements", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class Entitlements
    {
        [XmlElement(ElementName = "entitlement", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<Entitlement> Entitlement { get; set; }
    }

    [XmlRoot(ElementName = "mailIn", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class MailIn
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
    public class MailIns
    {
        [XmlElement(ElementName = "mailIn", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public MailIn MailIn { get; set; }
    }

    [XmlRoot(ElementName = "comment", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJobComment
    {
        [XmlElement(ElementName = "commentText", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CommentText { get; set; }
        [XmlElement(ElementName = "commentDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CommentDate { get; set; }
        [XmlElement(ElementName = "commentTime", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CommentTime { get; set; }
        [XmlElement(ElementName = "commentUserID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CommentUserID { get; set; }
    }

    [XmlRoot(ElementName = "comments", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class Comments
    {
        [XmlElement(ElementName = "comment", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<ServiceJobComment> Comment { get; set; }
    }

    [XmlRoot(ElementName = "supportInformation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class SupportInformation
    {
        [XmlElement(ElementName = "supportData", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SupportData { get; set; }
        [XmlElement(ElementName = "supportDataDescription", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SupportDataDescription { get; set; }
    }

    [XmlRoot(ElementName = "supportingInformation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class SupportingInformation
    {
        [XmlElement(ElementName = "supportInformation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<SupportInformation> SupportInformation { get; set; }
    }

    [XmlRoot(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJob
    {
        [XmlElement(ElementName = "serviceJobID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobID { get; set; }
        [XmlElement(ElementName = "serviceJobType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobType { get; set; }
        [XmlElement(ElementName = "serviceJobDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobDate { get; set; }
        [XmlElement(ElementName = "customerNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerNumber { get; set; }
        [XmlElement(ElementName = "crmNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CrmNumber { get; set; }
        [XmlElement(ElementName = "originalCrmNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string OriginalCrmNumber { get; set; }
        [XmlElement(ElementName = "serviceType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceType { get; set; }
        [XmlElement(ElementName = "proofOfPurchase", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProofOfPurchase { get; set; }
        [XmlElement(ElementName = "paymentType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PaymentType { get; set; }
        [XmlElement(ElementName = "paymentTerms", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PaymentTerms { get; set; }
        [XmlElement(ElementName = "specialInstructions", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SpecialInstructions { get; set; }
        [XmlElement(ElementName = "scheduledServiceDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ScheduledServiceDate { get; set; }
        [XmlElement(ElementName = "slotStartTime", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SlotStartTime { get; set; }
        [XmlElement(ElementName = "slotLength", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SlotLength { get; set; }
        [XmlElement(ElementName = "serviceJobStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobStatus { get; set; }
        [XmlElement(ElementName = "serviceJobSubStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobSubStatus { get; set; }
        [XmlElement(ElementName = "serviceJobSubType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobSubType { get; set; }
        [XmlElement(ElementName = "dateAccepted", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string DateAccepted { get; set; }
        [XmlElement(ElementName = "serviceProviderReferenceNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceProviderReferenceNumber { get; set; }
        [XmlElement(ElementName = "serviceExplanation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceExplanation { get; set; }
        [XmlElement(ElementName = "problemDescription", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ProblemDescription { get; set; }
        [XmlElement(ElementName = "rejectReason", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string RejectReason { get; set; }
        [XmlElement(ElementName = "renotifyReason", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string RenotifyReason { get; set; }
        [XmlElement(ElementName = "locationID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string LocationID { get; set; }
        [XmlElement(ElementName = "zoneID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ZoneID { get; set; }
        [XmlElement(ElementName = "zoneNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ZoneNumber { get; set; }
        [XmlElement(ElementName = "complaintCode", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ComplaintCode { get; set; }
        [XmlElement(ElementName = "technicianID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string TechnicianID { get; set; }
        [XmlElement(ElementName = "routeNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string RouteNumber { get; set; }
        [XmlElement(ElementName = "mailingLabelMethod", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string MailingLabelMethod { get; set; }
        [XmlElement(ElementName = "contractPurchaseDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ContractPurchaseDate { get; set; }
        [XmlElement(ElementName = "posStoreNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PosStoreNumber { get; set; }
        [XmlElement(ElementName = "posTransactionNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PosTransactionNumber { get; set; }
        [XmlElement(ElementName = "posTerminalNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PosTerminalNumber { get; set; }
        [XmlElement(ElementName = "posRetailerRefNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PosRetailerRefNumber { get; set; }
        [XmlElement(ElementName = "posSaleDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PosSaleDate { get; set; }
        [XmlElement(ElementName = "resolutionCode", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ResolutionCode { get; set; }
        [XmlElement(ElementName = "serviceJobOutcome", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceJobOutcome { get; set; }
        [XmlElement(ElementName = "diagnosticProgram", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string DiagnosticProgram { get; set; }
        [XmlElement(ElementName = "planType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PlanType { get; set; }
        [XmlElement(ElementName = "models", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public Models Models { get; set; }
        [XmlElement(ElementName = "entitlements", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public Entitlements Entitlements { get; set; }
        [XmlElement(ElementName = "mailIns", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public MailIns MailIns { get; set; }
        [XmlElement(ElementName = "comments", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public Comments Comments { get; set; }
        [XmlElement(ElementName = "supportingInformation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public SupportingInformation SupportingInformation { get; set; }
        [XmlElement(ElementName = "shippingNotes", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ShippingNotes { get; set; }
        [XmlElement(ElementName = "adminContactNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string AdminContactNumber { get; set; }
        [XmlElement(ElementName = "serviceRequestType", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceRequestType { get; set; }
        [XmlElement(ElementName = "returnId", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ReturnId { get; set; }
        [XmlElement(ElementName = "ofrNumber", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string OfrNumber { get; set; }
        [XmlElement(ElementName = "firstTimeSent", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string FirstTimeSent { get; set; }
        [XmlElement(ElementName = "serviceCategory", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceCategory { get; set; }
        [XmlElement(ElementName = "retailerInformation", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string RetailerInformation { get; set; }
    }

    [XmlRoot(ElementName = "serviceJobs", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJobs
    {
        [XmlElement(ElementName = "serviceJob", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<ServiceJob> ServiceJob { get; set; }
    }

    [XmlRoot(ElementName = "serviceOrder", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceOrder
    {
        [XmlElement(ElementName = "serviceAdministratorID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceAdministratorID { get; set; }
        [XmlElement(ElementName = "serviceAdministratorName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceAdministratorName { get; set; }
        [XmlElement(ElementName = "serviceOrderID", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceOrderID { get; set; }
        [XmlElement(ElementName = "serviceOrderDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceOrderDate { get; set; }
        [XmlElement(ElementName = "serviceOrderStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceOrderStatus { get; set; }
        [XmlElement(ElementName = "serviceOrderStatusDate", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceOrderStatusDate { get; set; }
        [XmlElement(ElementName = "companyName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CompanyName { get; set; }
        [XmlElement(ElementName = "customerTitle", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerTitle { get; set; }
        [XmlElement(ElementName = "customerLastName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerLastName { get; set; }
        [XmlElement(ElementName = "customerFirstName", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerFirstName { get; set; }
        [XmlElement(ElementName = "customerAddressLine1", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressLine1 { get; set; }
        [XmlElement(ElementName = "customerAddressLine2", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressLine2 { get; set; }
        [XmlElement(ElementName = "customerAddressLine3", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressLine3 { get; set; }
        [XmlElement(ElementName = "customerAddressCity", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressCity { get; set; }
        [XmlElement(ElementName = "customerAddressState", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressState { get; set; }
        [XmlElement(ElementName = "customerAddressZip", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAddressZip { get; set; }
        [XmlElement(ElementName = "customerPhone", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerPhone { get; set; }
        [XmlElement(ElementName = "customerWorkPhone", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerWorkPhone { get; set; }
        [XmlElement(ElementName = "customerAlternatePhone", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerAlternatePhone { get; set; }
        [XmlElement(ElementName = "customerFax", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerFax { get; set; }
        [XmlElement(ElementName = "mobilePhone", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string MobilePhone { get; set; }
        [XmlElement(ElementName = "customerEmail", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string CustomerEmail { get; set; }
        [XmlElement(ElementName = "preferredContactMethod", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string PreferredContactMethod { get; set; }
        [XmlElement(ElementName = "smsOptIn", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string SmsOptIn { get; set; }
        [XmlElement(ElementName = "directionsCrossStreets", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string DirectionsCrossStreets { get; set; }
        [XmlElement(ElementName = "serviceDescription", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string ServiceDescription { get; set; }
        [XmlElement(ElementName = "serviceJobs", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public ServiceJobs ServiceJobs { get; set; }
    }

    [XmlRoot(ElementName = "serviceOrders", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceOrders
    {
        [XmlElement(ElementName = "serviceOrder", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public List<ServiceOrder> ServiceOrder { get; set; }
    }

    [XmlRoot(ElementName = "serviceJobListResponse", Namespace = "http://servicebench.com/serviceOrder/service/types")]
    public class ServiceJobListResponse
    {
        [XmlElement(ElementName = "version", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string Version { get; set; }
        [XmlElement(ElementName = "msgStatus", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string MsgStatus { get; set; }
        [XmlElement(ElementName = "totalCount", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string TotalCount { get; set; }
        [XmlElement(ElementName = "more", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public string More { get; set; }
        [XmlElement(ElementName = "serviceOrders", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public ServiceOrders ServiceOrders { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class JobListResponseBody
    {
        [XmlElement(ElementName = "serviceJobListResponse", Namespace = "http://servicebench.com/serviceOrder/service/types")]
        public ServiceJobListResponse ServiceJobListResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class JobListResponseEnvelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public JobListResponseBody Body { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

}
