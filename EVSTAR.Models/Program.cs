using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Program
    {
        public int ID { get; set; }
        public string ProgramName { get; set; }
        public int WaitingPeriodDays { get; set; }
        public bool ManufWarrantyDateKnown { get; set; }
        public int MaxClaims { get; set; }
        public decimal MaxAmountPerClaim { get; set; }
        public decimal MaxAmountPerYear { get; set; }
        public decimal MonthlyCost { get; set; }
        public int ClientID { get; set; }
        public Client ProgramClient{ get; set; }
        public bool RegisterWithCode { get; set; }
        public decimal OneTimeCost { get; set; }
        public decimal AnnualCost { get; set; }
        public DateTime CoverageEnds { get; set; }
        public string Description { get; set; }
        public int FulfillmentTypeID { get; set; }
        public FulfillmentType ProgramFulfillmentType { get; set; }


        public Program()
        {
            ID = 0;
            ProgramName = string.Empty;
            WaitingPeriodDays = 0;
            ManufWarrantyDateKnown = false;
            MaxClaims = 0;
            MaxAmountPerClaim = 0M;
            MaxAmountPerYear = 0M;
            MonthlyCost = 0M;
            ClientID = 0;
            ProgramClient = new Client();
            RegisterWithCode = false;
            OneTimeCost = 0M;
            AnnualCost = 0M;
            CoverageEnds = DateTime.MaxValue;
            Description= string.Empty;
            FulfillmentTypeID = 0;
            ProgramFulfillmentType = new FulfillmentType();
        }

        public Program(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ProgramName = DBHelper.GetStringValue(r["ProgramName"]);
            WaitingPeriodDays = DBHelper.GetInt32Value(r["WaitingPeriodDays"]);
            ManufWarrantyDateKnown = DBHelper.GetBooleanValue(r["ManufWarrantyDateKnown"]);
            MaxClaims = DBHelper.GetInt32Value(r["MaxClaims"]);
            MaxAmountPerClaim = DBHelper.GetDecimalValue(r["MaxAmountPerClaim"]);
            MaxAmountPerYear = DBHelper.GetDecimalValue(r["MaxAmountPerYear"]);
            MonthlyCost = DBHelper.GetDecimalValue(r["MonthlyCost"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            RegisterWithCode = DBHelper.GetBooleanValue(r["RegisterWithCode"]);
            OneTimeCost = DBHelper.GetDecimalValue(r["OneTimeCost"]);
            AnnualCost = DBHelper.GetDecimalValue(r["AnnualCost"]);
            CoverageEnds = DBHelper.GetDateTimeValue(r["CoverageEnds"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            FulfillmentTypeID = DBHelper.GetInt32Value(r["FulfillmentType"]);
        }
    }
}