using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Permissions;

namespace EVSTAR.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int CoverageID { get; set; }
        public int CoveredProductID { get; set; }
        public int CoveredPerilID { get; set; }
        public int RepairVendorID { get; set; }
        public int AddressID { get; set; }
        public bool LocalRepair { get; set; }
        public DateTime DateOfLoss { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateReceivedAtTCS { get; set; }
        public DateTime DateReturnedToCustomer { get; set; }
        public bool PassCodeDisabled { get; set; }
        public string PassCode { get; set; }
        public string InboundTrackingNumber { get; set; }
        public string OutboundTrackingNumber { get; set; }
        public int PerilSubcategoryID { get; set; }
        public string DenialReason { get; set; }
        public DateTime DateDenied { get; set; }
        public long RepairShoprTicketID { get; set; }
        public bool SendToRS { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Deductible { get; set; }
        public List<ClaimStatusHistory> StatusHistory { get; set; }
        public Customer ClaimCustomer { get; set; }
        public CoveredProduct ClaimProduct { get; set; }
        public CoveredPeril ClaimPeril { get; set; }
        public RepairVendor ClaimVendor { get; set; }
        public Address ClaimAddress { get; set; }
        public string PerilSubCategory { get; set; }
        public Coverage ClaimCoverage { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? PoliceReportDate { get; set; }
        public string PoliceReportInfo { get; set; }
        public string EventDescription { get; set; }
        public string UserName { get; set; }
        public int DenialReasonID { get; set; }
        public DenialReason ClaimDenialReason { get; set; }
        public int StoreID { get; set; }
        public Store ClaimStore { get; set; }
        public string StoreRepID { get; set; }
        public DateTime? DeductiblePaidDate { get; set; }
        public int ReplacementProductID { get; set; }
        public Equipment ClaimReplacementProduct { get; set; }
        public SqlMoney ProgrammingFee { get; set; }
        public SqlMoney EquipmentCost { get; set; }
        public SqlMoney ActivationFee { get; set; }
        public string ReplacedESN { get; set; }
        public PerilSubCategory ClaimPerilSubCategory { get; set; }
        public string ReimbursementMethod { get; set; }
        public decimal ReimbursementAmount { get; set; }
        public string ReimbursementAccount { get; set; }
        public DateTime DateNoPaid { get; set; }
        public DateTime DateCancelled { get; set; }
        public DateTime OpenDate { 
            get
            {
                if (StatusHistory != null && StatusHistory.Count > 0)
                {
                    ClaimStatusHistory openStatus = StatusHistory.Find(x => x.Status.ToUpper() == "OPEN");
                    if (openStatus != null)
                        return openStatus.StatusDate;
                    else
                        return DateTime.Now;
                }
                else
                    return DateTime.Now;
            }
        }


        public Claim()
        {
            ID = 0;
            CustomerID = 0;
            CoveredProductID = 0;
            CoveredPerilID = 0;
            RepairVendorID = 0;
            AddressID = 0;
            LocalRepair = false;
            DateOfLoss = DateTime.Parse("1900-01-01");
            DateSubmitted = DateTime.Parse("1900-01-01");
            DateCompleted = DateTime.Parse("1900-01-01");
            DateReceivedAtTCS = DateTime.Parse("1900-01-01");
            DateReturnedToCustomer = DateTime.Parse("1900-01-01");
            PassCodeDisabled = false;
            PassCode = string.Empty;
            InboundTrackingNumber = string.Empty;
            OutboundTrackingNumber = string.Empty;
            PerilSubcategoryID = 0;
            DateDenied = DateTime.Parse("1900-01-01");
            DenialReason = string.Empty;
            RepairShoprTicketID = 0;
            SendToRS = false;
            Deductible = 0;
            StatusHistory = new List<ClaimStatusHistory>();
            PerilSubCategory = string.Empty;
            EventDate = null;
            PoliceReportDate = null;
            PoliceReportInfo = string.Empty;
            EventDescription = string.Empty;
            UserName = string.Empty;
            DenialReasonID = 0;
            StoreID = 0;
            StoreRepID = string.Empty;
            DeductiblePaidDate = null;
            ReplacementProductID= 0;
            ProgrammingFee = 0;
            EquipmentCost = 0;
            ActivationFee = 0;
            ReplacedESN = string.Empty;
            ReimbursementMethod = string.Empty;
            ReimbursementAmount = 0M;
            ReimbursementAccount = string.Empty;
            DateNoPaid = DateTime.MinValue;
            DateCancelled = DateTime.MinValue;
        }

        public Claim(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
            CoveredProductID = DBHelper.GetInt32Value(r["CoveredProductID"]);
            CoveredPerilID = DBHelper.GetInt32Value(r["CoveredPerilID"]);
            RepairVendorID = DBHelper.GetInt32Value(r["RepairVendorID"]);
            AddressID = DBHelper.GetInt32Value(r["AddressID"]);
            LocalRepair = DBHelper.GetBooleanValue(r["LocalRepair"]);
            DateOfLoss = DBHelper.GetDateTimeValue(r["DateOfLoss"]);
            DateSubmitted = DBHelper.GetDateTimeValue(r["DateSubmitted"]);
            DateCompleted = DBHelper.GetDateTimeValue(r["DateCompleted"]);
            DateReceivedAtTCS = DBHelper.GetDateTimeValue(r["DateReceivedAtTCS"]);
            DateReturnedToCustomer = DBHelper.GetDateTimeValue(r["DateReturnedToCustomer"]);
            PassCodeDisabled = DBHelper.GetBooleanValue(r["PasscodeDisabled"]);
            PassCode = DBHelper.GetStringValue(r["Passcode"]);
            InboundTrackingNumber = DBHelper.GetStringValue(r["InboundTrackingNumber"]);
            OutboundTrackingNumber = DBHelper.GetStringValue(r["OutboundTrackingNumber"]);
            PerilSubcategoryID = DBHelper.GetInt32Value(r["PerilSubcategoryID"]);
            DateDenied = DBHelper.GetDateTimeValue(r["DateDenied"]);
            DenialReason = DBHelper.GetStringValue(r["DenialReason"]);
            RepairShoprTicketID = DBHelper.GetInt64Value(r["RepairShoprTicketID"]);
            SendToRS = DBHelper.GetBooleanValue(r["SendToRS"]);
            Deductible = DBHelper.GetDecimalValue(r["Deductible"]);
            PerilSubCategory = DBHelper.GetStringValue(r["Subcategory"]);
            EventDate = DBHelper.GetNullableDateTimeValue(r["EventDate"]);
            PoliceReportDate = DBHelper.GetNullableDateTimeValue(r["PoliceReportDate"]);
            PoliceReportInfo = DBHelper.GetStringValue(r["PoliceReportInfo"]);
            EventDescription = DBHelper.GetStringValue(r["EventDescription"]);
            UserName = DBHelper.GetStringValue(r["UserName"]);
            DenialReasonID = DBHelper.GetInt32Value(r["DenialReasonID"]);
            StoreID = DBHelper.GetInt32Value(r["StoreID"]);
            StoreRepID = DBHelper.GetStringValue(r["StoreRepID"]);
            DeductiblePaidDate = DBHelper.GetNullableDateTimeValue(r["DeductiblePaidDate"]);
            ReplacementProductID = DBHelper.GetInt32Value(r["ReplacementProductID"]);
            ProgrammingFee = DBHelper.GetDecimalValue(r["ProgrammingFee"]);
            EquipmentCost = DBHelper.GetDecimalValue(r["EquipmentCost"]);
            ActivationFee = DBHelper.GetDecimalValue(r["ActivationFee"]);
            ReplacedESN = DBHelper.GetStringValue(r["ReplacedESN"]);
            ReimbursementMethod = DBHelper.GetStringValue(r["ReimbursementMethod"]);
            ReimbursementAmount = DBHelper.GetDecimalValue(r["ReimbursementAmount"]);
            ReimbursementAccount = DBHelper.GetStringValue(r["ReimbursementAccount"]);
            DateNoPaid = DBHelper.GetDateTimeValue(r["DateNoPaid"]);
            DateCancelled = DBHelper.GetDateTimeValue(r["DateCancelled"]);
        }
    }
}