using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
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
        public List<ClaimStatusHistory> StatusHistory { get; set; }

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
            StatusHistory = new List<ClaimStatusHistory>();
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
            PassCodeDisabled = DBHelper.GetBooleanValue(r["PassCodeDisabled"]);
            PassCode = DBHelper.GetStringValue(r["PassCode"]);
            InboundTrackingNumber = DBHelper.GetStringValue(r["InboundTrackingNumber"]);
            OutboundTrackingNumber = DBHelper.GetStringValue(r["OutboundTrackingNumber"]);
            PerilSubcategoryID = DBHelper.GetInt32Value(r["PerilSubcategoryID"]);
            DateDenied = DBHelper.GetDateTimeValue(r["DateDenied"]);
            DenialReason = DBHelper.GetStringValue(r["DenialReason"]);
            RepairShoprTicketID = DBHelper.GetInt64Value(r["RepairShoprTicketID"]);
            SendToRS = DBHelper.GetBooleanValue(r["SendToRS"]);
        }
    }
}