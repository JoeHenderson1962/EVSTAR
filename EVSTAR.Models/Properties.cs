using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace EVSTAR.Models
{
    public class Properties
    {
        public string Rework { get; set; }
        public string Passcode { get; set; }

        [JsonProperty("Repair Type")]
        public string RepairType { get; set; }

        [JsonProperty("Bin Location")]
        public string BinLocation { get; set; }

        [JsonProperty("Assigned Cart")]
        public string AssignedCart { get; set; }

        [JsonProperty("Date Received")]
        public string DateReceived { get; set; }

        [JsonProperty("Repair Reason (UBIF Only)")]
        public string RepairReasonUBIFOnly { get; set; }

        [JsonProperty("Customer Ref. Number (PO, Ticket #, Work Order, etc.)")]
        public string CustomerRefNumberPOTicketWorkOrderEtc { get; set; }
        public string Warranty { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Service Tag")]
        public string ServiceTag { get; set; }
        [JsonProperty("Sales Rep")]
        public string SalesRep { get; set; }
        public string notification_billing { get; set; }
        public string notification_reports { get; set; }
        public string notification_marketing { get; set; }
        public string title { get; set; }
        [JsonProperty("Portal PW To TCS")]
        public string PortalPWToTCS { get; set; }
        [JsonProperty("Box Contents")]
        public string BoxContents { get; set; }
        [JsonProperty("Asurion Box Contents")]
        public string AsurionBoxContents { get; set; }
        public string Originator { get; set; }
        [JsonProperty("SB Product Line Description")]
        public string Product_Line_Description { get; set; }
        public bool Admin { get; set; }
        [JsonProperty("Warranty Type")]
        public string WarrantyType { get; set; }
        [JsonProperty("Warranty Period")]
        public string WarrantyPeriod { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
    }
}