using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSTAR.Models.FedEx
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class TotalDeclaredValue
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Address
    {
        public List<string> streetLines { get; set; }
        public string city { get; set; }
        public string stateOrProvinceCode { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public bool residential { get; set; }
    }

    public class Contact
    {
        public string personName { get; set; }
        public string emailAddress { get; set; }
        public string phoneExtension { get; set; }
        public string phoneNumber { get; set; }
        public string companyName { get; set; }
        public string faxNumber { get; set; }
        public ParsedPersonName parsedPersonName { get; set; }
    }

    public class Tin
    {
        public string number { get; set; }
        public string tinType { get; set; }
        public string usage { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
    }

    public class Shipper
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<Tin> tins { get; set; }
    }

    public class AccountNumber
    {
        public string value { get; set; }
    }

    public class SoldTo
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<Tin> tins { get; set; }
        public AccountNumber accountNumber { get; set; }
    }

    public class Recipient
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<Tin> tins { get; set; }
        public string deliveryInstructions { get; set; }
        public string emailAddress { get; set; }
        public OptionsRequested optionsRequested { get; set; }
        public string role { get; set; }
        public string locale { get; set; }
    }

    public class Origin
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
    }

    public class ParsedPersonName
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string suffix { get; set; }
    }

    public class ResponsibleParty
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public AccountNumber accountNumber { get; set; }
        public List<Tin> tins { get; set; }
    }

    public class Payor
    {
        public ResponsibleParty responsibleParty { get; set; }
    }

    public class ShippingChargesPayment
    {
        public string paymentType { get; set; }
        public Payor payor { get; set; }
    }

    public class AttachedDocument
    {
        public string documentType { get; set; }
        public string documentReference { get; set; }
        public string description { get; set; }
        public string documentId { get; set; }
    }

    public class EtdDetail
    {
        public List<string> attributes { get; set; }
        public List<AttachedDocument> attachedDocuments { get; set; }
        public List<string> requestedDocumentTypes { get; set; }
    }

    public class ReturnEmailDetail
    {
        public string merchantPhoneNumber { get; set; }
        public List<string> allowedSpecialService { get; set; }
    }

    public class Rma
    {
        public string reason { get; set; }
    }

    public class ReturnAssociationDetail
    {
        public string shipDatestamp { get; set; }
        public string trackingNumber { get; set; }
    }

    public class ReturnShipmentDetail
    {
        public ReturnEmailDetail returnEmailDetail { get; set; }
        public Rma rma { get; set; }
        public ReturnAssociationDetail returnAssociationDetail { get; set; }
        public string returnType { get; set; }
    }

    public class Recipient2
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public List<Tin> tins { get; set; }
        public string deliveryInstructions { get; set; }
    }

    public class DeliveryOnInvoiceAcceptanceDetail
    {
        public Recipient recipient { get; set; }
    }

    public class InternationalTrafficInArmsRegulationsDetail
    {
        public string licenseOrExemptionNumber { get; set; }
    }

    public class ProcessingOptions
    {
        public List<string> options { get; set; }
    }

    public class RecommendedDocumentSpecification
    {
        public string types { get; set; }
    }

    public class OptionsRequested
    {
        public List<string> options { get; set; }
    }

    public class EmailLabelDetail
    {
        public List<Recipient> recipients { get; set; }
        public string message { get; set; }
    }

    public class PendingShipmentDetail
    {
        public string pendingShipmentType { get; set; }
        public ProcessingOptions processingOptions { get; set; }
        public RecommendedDocumentSpecification recommendedDocumentSpecification { get; set; }
        public EmailLabelDetail emailLabelDetail { get; set; }
        public List<AttachedDocument> attachedDocuments { get; set; }
        public string expirationTimeStamp { get; set; }
    }

    public class LocationContactAndAddress
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
    }

    public class HoldAtLocationDetail
    {
        public string locationId { get; set; }
        public LocationContactAndAddress locationContactAndAddress { get; set; }
        public string locationType { get; set; }
    }

    public class AddTransportationChargesDetail
    {
        public string rateType { get; set; }
        public string rateLevelType { get; set; }
        public string chargeLevelType { get; set; }
        public string chargeType { get; set; }
    }

    public class CodRecipient
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public AccountNumber accountNumber { get; set; }
        public List<Tin> tins { get; set; }
    }

    public class FinancialInstitutionContactAndAddress
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
    }

    public class CodCollectionAmount
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class ShipmentCodAmount
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class ShipmentCODDetail
    {
        public AddTransportationChargesDetail addTransportationChargesDetail { get; set; }
        public CodRecipient codRecipient { get; set; }
        public string remitToName { get; set; }
        public string codCollectionType { get; set; }
        public FinancialInstitutionContactAndAddress financialInstitutionContactAndAddress { get; set; }
        public CodCollectionAmount codCollectionAmount { get; set; }
        public string returnReferenceIndicatorType { get; set; }
        public ShipmentCodAmount shipmentCodAmount { get; set; }
    }

    public class TotalWeight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class ShipmentDryIceDetail
    {
        public TotalWeight totalWeight { get; set; }
        public int packageCount { get; set; }
    }

    public class InternationalControlledExportDetail
    {
        public string licenseOrPermitExpirationDate { get; set; }
        public string licenseOrPermitNumber { get; set; }
        public string entryNumber { get; set; }
        public string foreignTradeZoneCode { get; set; }
        public string type { get; set; }
    }

    public class PhoneNumber
    {
        public string areaCode { get; set; }
        public string localNumber { get; set; }
        public string extension { get; set; }
        public string personalIdentificationNumber { get; set; }
    }

    public class HomeDeliveryPremiumDetail
    {
        public PhoneNumber phoneNumber { get; set; }
        public string deliveryDate { get; set; }
        public string homedeliveryPremiumType { get; set; }
    }

    public class ShipmentSpecialServices
    {
        public List<string> specialServiceTypes { get; set; }
        public EtdDetail etdDetail { get; set; }
        public ReturnShipmentDetail returnShipmentDetail { get; set; }
        public DeliveryOnInvoiceAcceptanceDetail deliveryOnInvoiceAcceptanceDetail { get; set; }
        public InternationalTrafficInArmsRegulationsDetail internationalTrafficInArmsRegulationsDetail { get; set; }
        public PendingShipmentDetail pendingShipmentDetail { get; set; }
        public HoldAtLocationDetail holdAtLocationDetail { get; set; }
        public ShipmentCODDetail shipmentCODDetail { get; set; }
        public ShipmentDryIceDetail shipmentDryIceDetail { get; set; }
        public InternationalControlledExportDetail internationalControlledExportDetail { get; set; }
        public HomeDeliveryPremiumDetail homeDeliveryPremiumDetail { get; set; }
    }

    public class EmailNotificationRecipient
    {
        public string name { get; set; }
        public string emailNotificationRecipientType { get; set; }
        public string emailAddress { get; set; }
        public string notificationFormatType { get; set; }
        public string notificationType { get; set; }
        public string locale { get; set; }
        public List<string> notificationEventType { get; set; }
    }

    public class EmailNotificationDetail
    {
        public string aggregationType { get; set; }
        public List<EmailNotificationRecipient> emailNotificationRecipients { get; set; }
        public string personalMessage { get; set; }
        public string emailAddress { get; set; }
        public string type { get; set; }
        public string recipientType { get; set; }
    }

    public class ExpressFreightDetail
    {
        public string bookingConfirmationNumber { get; set; }
        public int shippersLoadAndCount { get; set; }
        public bool packingListEnclosed { get; set; }
    }

    public class FixedValue
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class VariableHandlingChargeDetail
    {
        public string rateType { get; set; }
        public double percentValue { get; set; }
        public string rateLevelType { get; set; }
        public FixedValue fixedValue { get; set; }
        public string rateElementBasis { get; set; }
    }

    public class Broker2
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public AccountNumber accountNumber { get; set; }
        public List<Tin> tins { get; set; }
        public string deliveryInstructions { get; set; }
    }

    public class Broker
    {
        public Broker broker { get; set; }
        public string type { get; set; }
    }

    public class CustomerReference
    {
        public string customerReferenceType { get; set; }
        public string value { get; set; }
    }

    public class TaxesOrMiscellaneousCharge
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class FreightCharge
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class PackingCosts
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class HandlingCosts
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class CommercialInvoice
    {
        public string originatorName { get; set; }
        public List<string> comments { get; set; }
        public List<CustomerReference> customerReferences { get; set; }
        public TaxesOrMiscellaneousCharge taxesOrMiscellaneousCharge { get; set; }
        public string taxesOrMiscellaneousChargeType { get; set; }
        public FreightCharge freightCharge { get; set; }
        public PackingCosts packingCosts { get; set; }
        public HandlingCosts handlingCosts { get; set; }
        public string declarationStatement { get; set; }
        public string termsOfSale { get; set; }
        public string specialInstructions { get; set; }
        public string shipmentPurpose { get; set; }
        public EmailNotificationDetail emailNotificationDetail { get; set; }
    }

    public class BillingDetails
    {
        public string billingCode { get; set; }
        public string billingType { get; set; }
        public string aliasId { get; set; }
        public string accountNickname { get; set; }
        public string accountNumber { get; set; }
        public string accountNumberCountryCode { get; set; }
    }

    public class DutiesPayment
    {
        public Payor payor { get; set; }
        public BillingDetails billingDetails { get; set; }
        public string paymentType { get; set; }
    }

    public class UnitPrice
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class AdditionalMeasure
    {
        public double quantity { get; set; }
        public string units { get; set; }
    }

    public class CustomsValue
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Weight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class UsmcaDetail
    {
        public string originCriterion { get; set; }
    }

    public class Commodity
    {
        public UnitPrice unitPrice { get; set; }
        public List<AdditionalMeasure> additionalMeasures { get; set; }
        public int numberOfPieces { get; set; }
        public int quantity { get; set; }
        public string quantityUnits { get; set; }
        public CustomsValue customsValue { get; set; }
        public string countryOfManufacture { get; set; }
        public string cIMarksAndNumbers { get; set; }
        public string harmonizedCode { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public Weight weight { get; set; }
        public string exportLicenseNumber { get; set; }
        public DateTime exportLicenseExpirationDate { get; set; }
        public string partNumber { get; set; }
        public string purpose { get; set; }
        public UsmcaDetail usmcaDetail { get; set; }
    }

    public class RecipientCustomsId
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class CustomsOption
    {
        public string description { get; set; }
        public string type { get; set; }
    }

    public class ImporterOfRecord
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public AccountNumber accountNumber { get; set; }
        public List<Tin> tins { get; set; }
    }

    public class DestinationControlDetail
    {
        public string endUser { get; set; }
        public string statementTypes { get; set; }
        public List<string> destinationCountries { get; set; }
    }

    public class ExportDetail
    {
        public DestinationControlDetail destinationControlDetail { get; set; }
        public string b13AFilingOption { get; set; }
        public string exportComplianceStatement { get; set; }
        public string permitNumber { get; set; }
    }

    public class TotalCustomsValue
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class UsmcaLowValueStatementDetail
    {
        public bool countryOfOriginLowValueDocumentRequested { get; set; }
        public string customsRole { get; set; }
    }

    public class DeclarationStatementDetail
    {
        public UsmcaLowValueStatementDetail usmcaLowValueStatementDetail { get; set; }
    }

    public class InsuranceCharge
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class CustomsClearanceDetail
    {
        public string regulatoryControls { get; set; }
        public List<Broker> brokers { get; set; }
        public CommercialInvoice commercialInvoice { get; set; }
        public string freightOnValue { get; set; }
        public DutiesPayment dutiesPayment { get; set; }
        public List<Commodity> commodities { get; set; }
        public bool isDocumentOnly { get; set; }
        public RecipientCustomsId recipientCustomsId { get; set; }
        public CustomsOption customsOption { get; set; }
        public ImporterOfRecord importerOfRecord { get; set; }
        public string generatedDocumentLocale { get; set; }
        public ExportDetail exportDetail { get; set; }
        public TotalCustomsValue totalCustomsValue { get; set; }
        public bool partiesToTransactionAreRelated { get; set; }
        public DeclarationStatementDetail declarationStatementDetail { get; set; }
        public InsuranceCharge insuranceCharge { get; set; }
    }

    public class SmartPostInfoDetail
    {
        public string ancillaryEndorsement { get; set; }
        public string hubId { get; set; }
        public string indicia { get; set; }
        public string specialServices { get; set; }
    }

    public class RegulatoryLabel
    {
        public string generationOptions { get; set; }
        public string type { get; set; }
    }

    public class AdditionalLabel
    {
        public string type { get; set; }
        public int count { get; set; }
    }

    public class DocTabZoneSpecification
    {
        public int zoneNumber { get; set; }
        public string header { get; set; }
        public string dataField { get; set; }
        public string literalValue { get; set; }
        public string justification { get; set; }
    }

    public class Zone001
    {
        public List<DocTabZoneSpecification> docTabZoneSpecifications { get; set; }
    }

    public class Specification
    {
        public int zoneNumber { get; set; }
        public string header { get; set; }
        public string dataField { get; set; }
        public string literalValue { get; set; }
        public string justification { get; set; }
    }

    public class Barcoded
    {
        public string symbology { get; set; }
        public Specification specification { get; set; }
    }

    public class DocTabContent
    {
        public string docTabContentType { get; set; }
        public Zone001 zone001 { get; set; }
        public Barcoded barcoded { get; set; }
    }

    public class CustomerSpecifiedDetail
    {
        public List<string> maskedData { get; set; }
        public List<RegulatoryLabel> regulatoryLabels { get; set; }
        public List<AdditionalLabel> additionalLabels { get; set; }
        public DocTabContent docTabContent { get; set; }
    }

    public class PrintedLabelOrigin
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
    }

    public class LabelSpecification
    {
        public string labelFormatType { get; set; }
        public string labelOrder { get; set; }
        public CustomerSpecifiedDetail customerSpecifiedDetail { get; set; }
        public PrintedLabelOrigin printedLabelOrigin { get; set; }
        public string labelStockType { get; set; }
        public string labelRotation { get; set; }
        public string imageType { get; set; }
        public string labelPrintingOrientation { get; set; }
        public bool returnedDispositionDetail { get; set; }
    }

    public class EMailRecipient
    {
        public string emailAddress { get; set; }
        public string recipientType { get; set; }
    }

    public class EMailDetail
    {
        public List<EMailRecipient> eMailRecipients { get; set; }
        public string locale { get; set; }
        public string grouping { get; set; }
    }

    public class Disposition
    {
        public EMailDetail eMailDetail { get; set; }
        public string dispositionType { get; set; }
    }

    public class DocumentFormat
    {
        public bool provideInstructions { get; set; }
        public OptionsRequested optionsRequested { get; set; }
        public string stockType { get; set; }
        public List<Disposition> dispositions { get; set; }
        public string locale { get; set; }
        public string docType { get; set; }
    }

    public class GeneralAgencyAgreementDetail
    {
        public DocumentFormat documentFormat { get; set; }
    }

    public class ReturnInstructionsDetail
    {
        public string customText { get; set; }
        public DocumentFormat documentFormat { get; set; }
    }

    public class CustomerImageUsage
    {
        public string id { get; set; }
        public string type { get; set; }
        public string providedImageType { get; set; }
    }

    public class Op900Detail
    {
        public List<CustomerImageUsage> customerImageUsages { get; set; }
        public string signatureName { get; set; }
        public DocumentFormat documentFormat { get; set; }
    }

    public class Producer
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
        public AccountNumber accountNumber { get; set; }
        public List<Tin> tins { get; set; }
    }

    public class BlanketPeriod
    {
        public string begins { get; set; }
        public string ends { get; set; }
    }

    public class UsmcaCertificationOfOriginDetail
    {
        public List<CustomerImageUsage> customerImageUsages { get; set; }
        public DocumentFormat documentFormat { get; set; }
        public string certifierSpecification { get; set; }
        public string importerSpecification { get; set; }
        public string producerSpecification { get; set; }
        public Producer producer { get; set; }
        public BlanketPeriod blanketPeriod { get; set; }
        public string certifierJobTitle { get; set; }
    }

    public class UsmcaCommercialInvoiceCertificationOfOriginDetail
    {
        public List<CustomerImageUsage> customerImageUsages { get; set; }
        public DocumentFormat documentFormat { get; set; }
        public string certifierSpecification { get; set; }
        public string importerSpecification { get; set; }
        public string producerSpecification { get; set; }
        public Producer producer { get; set; }
        public string certifierJobTitle { get; set; }
    }

    public class CertificateOfOrigin
    {
        public List<CustomerImageUsage> customerImageUsages { get; set; }
        public DocumentFormat documentFormat { get; set; }
    }

    public class CommercialInvoiceDetail
    {
        public List<CustomerImageUsage> customerImageUsages { get; set; }
        public DocumentFormat documentFormat { get; set; }
    }

    public class ShippingDocumentSpecification
    {
        public GeneralAgencyAgreementDetail generalAgencyAgreementDetail { get; set; }
        public ReturnInstructionsDetail returnInstructionsDetail { get; set; }
        public Op900Detail op900Detail { get; set; }
        public UsmcaCertificationOfOriginDetail usmcaCertificationOfOriginDetail { get; set; }
        public UsmcaCommercialInvoiceCertificationOfOriginDetail usmcaCommercialInvoiceCertificationOfOriginDetail { get; set; }
        public List<string> shippingDocumentTypes { get; set; }
        public CertificateOfOrigin certificateOfOrigin { get; set; }
        public CommercialInvoiceDetail commercialInvoiceDetail { get; set; }
    }

    public class MasterTrackingId
    {
        public string formId { get; set; }
        public string trackingIdType { get; set; }
        public string uspsApplicationId { get; set; }
        public string trackingNumber { get; set; }
    }

    public class DeclaredValue
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Dimensions
    {
        public int length { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string units { get; set; }
    }

    public class ContentRecord
    {
        public string itemNumber { get; set; }
        public int receivedQuantity { get; set; }
        public string description { get; set; }
        public string partNumber { get; set; }
    }

    public class PriorityAlertDetail
    {
        public List<string> enhancementTypes { get; set; }
        public List<string> content { get; set; }
    }

    public class SignatureOptionDetail
    {
        public string signatureReleaseNumber { get; set; }
    }

    public class AlcoholDetail
    {
        public string alcoholRecipientType { get; set; }
        public string shipperAgreementType { get; set; }
    }

    public class DangerousGoodsDetail
    {
        public string accessibility { get; set; }
        public List<string> options { get; set; }
    }

    public class PackageCODDetail
    {
        public CodCollectionAmount codCollectionAmount { get; set; }
    }

    public class BatteryDetail
    {
        public string batteryPackingType { get; set; }
        public string batteryRegulatoryType { get; set; }
        public string batteryMaterialType { get; set; }
    }

    public class DryIceWeight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class PackageSpecialServices
    {
        public List<string> specialServiceTypes { get; set; }
        public string signatureOptionType { get; set; }
        public PriorityAlertDetail priorityAlertDetail { get; set; }
        public SignatureOptionDetail signatureOptionDetail { get; set; }
        public AlcoholDetail alcoholDetail { get; set; }
        public DangerousGoodsDetail dangerousGoodsDetail { get; set; }
        public PackageCODDetail packageCODDetail { get; set; }
        public int pieceCountVerificationBoxCount { get; set; }
        public List<BatteryDetail> batteryDetails { get; set; }
        public DryIceWeight dryIceWeight { get; set; }
    }

    public class RequestedPackageLineItem
    {
        public string sequenceNumber { get; set; }
        public string subPackagingType { get; set; }
        public List<CustomerReference> customerReferences { get; set; }
        public DeclaredValue declaredValue { get; set; }
        public Weight weight { get; set; }
        public Dimensions dimensions { get; set; }
        public int groupPackageCount { get; set; }
        public string itemDescriptionForClearance { get; set; }
        public List<ContentRecord> contentRecord { get; set; }
        public string itemDescription { get; set; }
        public VariableHandlingChargeDetail variableHandlingChargeDetail { get; set; }
        public PackageSpecialServices packageSpecialServices { get; set; }
    }

    public class RequestedShipment
    {
        public string shipDatestamp { get; set; }
        public TotalDeclaredValue totalDeclaredValue { get; set; }
        public Shipper shipper { get; set; }
        public SoldTo soldTo { get; set; }
        public List<Recipient> recipients { get; set; }
        public string recipientLocationNumber { get; set; }
        public string pickupType { get; set; }
        public string serviceType { get; set; }
        public string packagingType { get; set; }
        public double totalWeight { get; set; }
        public Origin origin { get; set; }
        public ShippingChargesPayment shippingChargesPayment { get; set; }
        public ShipmentSpecialServices shipmentSpecialServices { get; set; }
        public EmailNotificationDetail emailNotificationDetail { get; set; }
        public ExpressFreightDetail expressFreightDetail { get; set; }
        public VariableHandlingChargeDetail variableHandlingChargeDetail { get; set; }
        public CustomsClearanceDetail customsClearanceDetail { get; set; }
        public SmartPostInfoDetail smartPostInfoDetail { get; set; }
        public bool blockInsightVisibility { get; set; }
        public LabelSpecification labelSpecification { get; set; }
        public ShippingDocumentSpecification shippingDocumentSpecification { get; set; }
        public List<string> rateRequestType { get; set; }
        public string preferredCurrency { get; set; }
        public int totalPackageCount { get; set; }
        public MasterTrackingId masterTrackingId { get; set; }
        public List<RequestedPackageLineItem> requestedPackageLineItems { get; set; }
    }

    public class Shipment
    {
        public string mergeLabelDocOption { get; set; }
        public RequestedShipment requestedShipment { get; set; }
        public string labelResponseOptions { get; set; }
        public AccountNumber accountNumber { get; set; }
        public string shipAction { get; set; }
        public string processingOptionType { get; set; }
        public bool oneLabelAtATime { get; set; }
    }

}