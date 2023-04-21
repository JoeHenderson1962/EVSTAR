using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSTAR.Models.FedEx
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Alert
    {
        public string code { get; set; }
        public string alertType { get; set; }
        public string message { get; set; }
    }

    public class ShipmentDocument
    {
        public string contentKey { get; set; }
        public int copiesToPrint { get; set; }
        public string contentType { get; set; }
        public string trackingNumber { get; set; }
        public string docType { get; set; }
        public List<Alert> alerts { get; set; }
        public string encodedLabel { get; set; }
        public string url { get; set; }
    }

    public class TransactionDetail
    {
        public string transactionDetails { get; set; }
        public string transactionId { get; set; }
    }

    public class PackageDocument
    {
        public string contentKey { get; set; }
        public int copiesToPrint { get; set; }
        public string contentType { get; set; }
        public string trackingNumber { get; set; }
        public string docType { get; set; }
        public List<Alert> alerts { get; set; }
        public string encodedLabel { get; set; }
        public string url { get; set; }
    }

    public class PieceRespons
    {
        public double netChargeAmount { get; set; }
        public List<TransactionDetail> transactionDetails { get; set; }
        public List<PackageDocument> packageDocuments { get; set; }
        public string acceptanceTrackingNumber { get; set; }
        public string serviceCategory { get; set; }
        public string listCustomerTotalCharge { get; set; }
        public string deliveryTimestamp { get; set; }
        public string trackingIdType { get; set; }
        public double additionalChargesDiscount { get; set; }
        public double netListRateAmount { get; set; }
        public double baseRateAmount { get; set; }
        public int packageSequenceNumber { get; set; }
        public double netDiscountAmount { get; set; }
        public double codcollectionAmount { get; set; }
        public string masterTrackingNumber { get; set; }
        public string acceptanceType { get; set; }
        public string trackingNumber { get; set; }
        public bool successful { get; set; }
        public List<CustomerReference> customerReferences { get; set; }
    }

    public class BinaryBarcode
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class StringBarcode
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Barcodes
    {
        public List<BinaryBarcode> binaryBarcodes { get; set; }
        public List<StringBarcode> stringBarcodes { get; set; }
    }

    public class OperationalInstruction
    {
        public int number { get; set; }
        public string content { get; set; }
    }

    public class OperationalDetail
    {
        public string astraHandlingText { get; set; }
        public Barcodes barcodes { get; set; }
        public List<OperationalInstruction> operationalInstructions { get; set; }
        public string originServiceArea { get; set; }
        public string serviceCode { get; set; }
        public string airportId { get; set; }
        public string postalCode { get; set; }
        public string scac { get; set; }
        public string deliveryDay { get; set; }
        public string originLocationId { get; set; }
        public string countryCode { get; set; }
        public string astraDescription { get; set; }
        public int originLocationNumber { get; set; }
        public string deliveryDate { get; set; }
        public List<string> deliveryEligibilities { get; set; }
        public bool ineligibleForMoneyBackGuarantee { get; set; }
        public string maximumTransitTime { get; set; }
        public string destinationLocationStateOrProvinceCode { get; set; }
        public string astraPlannedServiceLevel { get; set; }
        public string destinationLocationId { get; set; }
        public string transitTime { get; set; }
        public string stateOrProvinceCode { get; set; }
        public int destinationLocationNumber { get; set; }
        public string packagingCode { get; set; }
        public string commitDate { get; set; }
        public string publishedDeliveryTime { get; set; }
        public string ursaSuffixCode { get; set; }
        public string ursaPrefixCode { get; set; }
        public string destinationServiceArea { get; set; }
        public string commitDay { get; set; }
        public string customTransitTime { get; set; }
    }

    public class TrackingId
    {
        public string formId { get; set; }
        public string trackingIdType { get; set; }
        public string uspsApplicationId { get; set; }
        public string trackingNumber { get; set; }
    }

    public class BillingWeight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class Surcharge
    {
        public object amount { get; set; }
        public string surchargeType { get; set; }
        public string level { get; set; }
        public string description { get; set; }
    }

    public class PackageRateDetail
    {
        public string ratedWeightMethod { get; set; }
        public double totalFreightDiscounts { get; set; }
        public double totalTaxes { get; set; }
        public string minimumChargeType { get; set; }
        public double baseCharge { get; set; }
        public double totalRebates { get; set; }
        public string rateType { get; set; }
        public BillingWeight billingWeight { get; set; }
        public double netFreight { get; set; }
        public List<Surcharge> surcharges { get; set; }
        public double totalSurcharges { get; set; }
        public double netFedExCharge { get; set; }
        public double netCharge { get; set; }
        public string currency { get; set; }
    }

    public class PackageRating
    {
        public int effectiveNetDiscount { get; set; }
        public string actualRateType { get; set; }
        public List<PackageRateDetail> packageRateDetails { get; set; }
    }

    public class Quantity
    {
        public string quantityType { get; set; }
        public double amount { get; set; }
        public string units { get; set; }
    }

    public class InnerReceptacle
    {
        public Quantity quantity { get; set; }
    }

    public class Options2
    {
        public string labelTextOption { get; set; }
        public string customerSuppliedLabelText { get; set; }
        public Quantity quantity { get; set; }
        public List<InnerReceptacle> innerReceptacles { get; set; }
        public List<string> options { get; set; }
        public Description description { get; set; }
    }

    public class PackingDetails
    {
        public string packingInstructions { get; set; }
        public bool cargoAircraftOnly { get; set; }
    }

    public class Description
    {
        public int sequenceNumber { get; set; }
        public List<string> processingOptions { get; set; }
        public List<string> subsidiaryClasses { get; set; }
        public string labelText { get; set; }
        public string technicalName { get; set; }
        public PackingDetails packingDetails { get; set; }
        public string authorization { get; set; }
        public bool reportableQuantity { get; set; }
        public double percentage { get; set; }
        public string id { get; set; }
        public string packingGroup { get; set; }
        public string properShippingName { get; set; }
        public string hazardClass { get; set; }
        public string packingInstructions { get; set; }
        public string tunnelRestrictionCode { get; set; }
        public string specialProvisions { get; set; }
        public string properShippingNameAndDescription { get; set; }
        public string symbols { get; set; }
        public List<string> attributes { get; set; }
    }

    public class NetExplosiveDetail
    {
        public int amount { get; set; }
        public string units { get; set; }
        public string type { get; set; }
    }

    public class HazardousCommodity
    {
        public Quantity quantity { get; set; }
        public List<string> options { get; set; }
        public Description description { get; set; }
        public NetExplosiveDetail netExplosiveDetail { get; set; }
        public int massPoints { get; set; }
    }

    public class Container
    {
        public int qvalue { get; set; }
        public List<HazardousCommodity> hazardousCommodities { get; set; }
    }

    public class HazardousPackageDetail
    {
        public string regulation { get; set; }
        public string accessibility { get; set; }
        public string labelType { get; set; }
        public List<Container> containers { get; set; }
        public bool cargoAircraftOnly { get; set; }
        public string referenceId { get; set; }
        public double radioactiveTransportIndex { get; set; }
    }

    public class CompletedPackageDetail
    {
        public int sequenceNumber { get; set; }
        public OperationalDetail operationalDetail { get; set; }
        public string signatureOption { get; set; }
        public List<TrackingId> trackingIds { get; set; }
        public int groupNumber { get; set; }
        public string oversizeClass { get; set; }
        public PackageRating packageRating { get; set; }
        public DryIceWeight dryIceWeight { get; set; }
        public HazardousPackageDetail hazardousPackageDetail { get; set; }
    }

    public class HoldingLocation
    {
        public Address address { get; set; }
        public Contact contact { get; set; }
    }

    public class CompletedHoldAtLocationDetail
    {
        public string holdingLocationType { get; set; }
        public HoldingLocation holdingLocation { get; set; }
    }

    public class UploadDocumentReferenceDetail
    {
        public string documentType { get; set; }
        public string documentReference { get; set; }
        public string description { get; set; }
        public string documentId { get; set; }
    }

    public class CompletedEtdDetail
    {
        public string folderId { get; set; }
        public string type { get; set; }
        public List<UploadDocumentReferenceDetail> uploadDocumentReferenceDetails { get; set; }
    }

    public class Name
    {
        public string type { get; set; }
        public string encoding { get; set; }
        public string value { get; set; }
    }

    public class ServiceDescription
    {
        public string serviceType { get; set; }
        public string code { get; set; }
        public List<Name> names { get; set; }
        public List<string> operatingOrgCodes { get; set; }
        public string astraDescription { get; set; }
        public string description { get; set; }
        public string serviceId { get; set; }
        public string serviceCategory { get; set; }
    }

    public class HazardousSummaryDetail
    {
        public int smallQuantityExceptionPackageCount { get; set; }
    }

    public class LicenseOrPermitDetail
    {
        public string number { get; set; }
        public string effectiveDate { get; set; }
        public string expirationDate { get; set; }
    }

    public class AdrLicense
    {
        public LicenseOrPermitDetail licenseOrPermitDetail { get; set; }
    }

    public class DryIceDetail
    {
        public TotalWeight totalWeight { get; set; }
        public int packageCount { get; set; }
        public ProcessingOptions processingOptions { get; set; }
    }

    public class HazardousShipmentDetail
    {
        public HazardousSummaryDetail hazardousSummaryDetail { get; set; }
        public AdrLicense adrLicense { get; set; }
        public DryIceDetail dryIceDetail { get; set; }
    }

    public class Tax
    {
        public int amount { get; set; }
        public string level { get; set; }
        public string description { get; set; }
        public string type { get; set; }
    }

    public class CurrencyExchangeRate
    {
        public double rate { get; set; }
        public string fromCurrency { get; set; }
        public string intoCurrency { get; set; }
    }

    public class TotalDimWeight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class TotalBillingWeight
    {
        public string units { get; set; }
        public int value { get; set; }
    }

    public class FreightDiscount
    {
        public double amount { get; set; }
        public string rateDiscountType { get; set; }
        public double percent { get; set; }
        public string description { get; set; }
    }

    public class ShipmentLegRateDetail
    {
        public string rateZone { get; set; }
        public string pricingCode { get; set; }
        public List<Tax> taxes { get; set; }
        public TotalDimWeight totalDimWeight { get; set; }
        public int totalRebates { get; set; }
        public int fuelSurchargePercent { get; set; }
        public CurrencyExchangeRate currencyExchangeRate { get; set; }
        public int dimDivisor { get; set; }
        public string rateType { get; set; }
        public string legDestinationLocationId { get; set; }
        public string dimDivisorType { get; set; }
        public int totalBaseCharge { get; set; }
        public string ratedWeightMethod { get; set; }
        public int totalFreightDiscounts { get; set; }
        public double totalTaxes { get; set; }
        public string minimumChargeType { get; set; }
        public double totalDutiesAndTaxes { get; set; }
        public int totalNetFreight { get; set; }
        public double totalNetFedExCharge { get; set; }
        public List<Surcharge> surcharges { get; set; }
        public int totalSurcharges { get; set; }
        public TotalBillingWeight totalBillingWeight { get; set; }
        public List<FreightDiscount> freightDiscounts { get; set; }
        public string rateScale { get; set; }
        public int totalNetCharge { get; set; }
        public double totalNetChargeWithDutiesAndTaxes { get; set; }
        public string currency { get; set; }
    }

    public class ShipmentRateDetail
    {
        public string rateZone { get; set; }
        public string ratedWeightMethod { get; set; }
        public double totalDutiesTaxesAndFees { get; set; }
        public string pricingCode { get; set; }
        public double totalFreightDiscounts { get; set; }
        public double totalTaxes { get; set; }
        public double totalDutiesAndTaxes { get; set; }
        public double totalAncillaryFeesAndTaxes { get; set; }
        public List<Tax> taxes { get; set; }
        public double totalRebates { get; set; }
        public double fuelSurchargePercent { get; set; }
        public CurrencyExchangeRate currencyExchangeRate { get; set; }
        public double totalNetFreight { get; set; }
        public double totalNetFedExCharge { get; set; }
        public List<ShipmentLegRateDetail> shipmentLegRateDetails { get; set; }
        public int dimDivisor { get; set; }
        public string rateType { get; set; }
        public List<Surcharge> surcharges { get; set; }
        public double totalSurcharges { get; set; }
        public TotalBillingWeight totalBillingWeight { get; set; }
        public List<FreightDiscount> freightDiscounts { get; set; }
        public string rateScale { get; set; }
        public double totalNetCharge { get; set; }
        public double totalBaseCharge { get; set; }
        public double totalNetChargeWithDutiesAndTaxes { get; set; }
        public string currency { get; set; }
    }

    public class ShipmentRating
    {
        public string actualRateType { get; set; }
        public List<ShipmentRateDetail> shipmentRateDetails { get; set; }
    }

    public class GenerationDetail
    {
        public string type { get; set; }
        public int minimumCopiesRequired { get; set; }
        public string letterhead { get; set; }
        public string electronicSignature { get; set; }
    }

    public class DocumentRequirements
    {
        public List<string> requiredDocuments { get; set; }
        public List<string> prohibitedDocuments { get; set; }
        public List<GenerationDetail> generationDetails { get; set; }
    }

    public class AccessorDetail
    {
        public string password { get; set; }
        public string role { get; set; }
        public string emailLabelUrl { get; set; }
        public string userId { get; set; }
    }

    public class AccessDetail
    {
        public List<AccessorDetail> accessorDetails { get; set; }
    }

    public class CompletedShipmentDetail
    {
        public List<CompletedPackageDetail> completedPackageDetails { get; set; }
        public OperationalDetail operationalDetail { get; set; }
        public string carrierCode { get; set; }
        public CompletedHoldAtLocationDetail completedHoldAtLocationDetail { get; set; }
        public CompletedEtdDetail completedEtdDetail { get; set; }
        public string packagingDescription { get; set; }
        public MasterTrackingId masterTrackingId { get; set; }
        public ServiceDescription serviceDescription { get; set; }
        public bool usDomestic { get; set; }
        public HazardousShipmentDetail hazardousShipmentDetail { get; set; }
        public ShipmentRating shipmentRating { get; set; }
        public DocumentRequirements documentRequirements { get; set; }
        public string exportComplianceStatement { get; set; }
        public AccessDetail accessDetail { get; set; }
    }

    public class Suggestion
    {
        public string description { get; set; }
        public string harmonizedCode { get; set; }
    }

    public class CommodityClarification
    {
        public int commodityIndex { get; set; }
        public List<Suggestion> suggestions { get; set; }
    }

    public class Parameter
    {
        public string id { get; set; }
        public string value { get; set; }
    }

    public class Advisory
    {
        public string code { get; set; }
        public string text { get; set; }
        public List<Parameter> parameters { get; set; }
        public string localizedText { get; set; }
    }

    public class Advisory2
    {
        public string code { get; set; }
        public string text { get; set; }
        public List<Parameter> parameters { get; set; }
        public string localizedText { get; set; }
    }

    public class Waiver
    {
        public List<Advisory> advisories { get; set; }
        public string description { get; set; }
        public string id { get; set; }
    }

    public class Prohibition
    {
        public string derivedHarmonizedCode { get; set; }
        public Advisory advisory { get; set; }
        public int commodityIndex { get; set; }
        public string source { get; set; }
        public List<string> categories { get; set; }
        public string type { get; set; }
        public Waiver waiver { get; set; }
        public string status { get; set; }
    }

    public class RegulatoryAdvisory
    {
        public List<CommodityClarification> commodityClarifications { get; set; }
        public List<Prohibition> prohibitions { get; set; }
    }

    public class ShipmentAdvisoryDetails
    {
        public RegulatoryAdvisory regulatoryAdvisory { get; set; }
    }

    public class TransactionShipment
    {
        public string serviceType { get; set; }
        public string shipDatestamp { get; set; }
        public string serviceCategory { get; set; }
        public List<ShipmentDocument> shipmentDocuments { get; set; }
        public List<PieceRespons> pieceResponses { get; set; }
        public string serviceName { get; set; }
        public List<Alert> alerts { get; set; }
        public CompletedShipmentDetail completedShipmentDetail { get; set; }
        public ShipmentAdvisoryDetails shipmentAdvisoryDetails { get; set; }
        public string masterTrackingNumber { get; set; }
    }

    public class Output
    {
        public List<TransactionShipment> transactionShipments { get; set; }
        public List<Alert> alerts { get; set; }
        public string jobId { get; set; }
    }

    public class ShipmentResponse
    {
        public string transactionId { get; set; }
        public string customerTransactionId { get; set; }
        public Output output { get; set; }
    }

}