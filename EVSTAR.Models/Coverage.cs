using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace EVSTAR.Models
{
    public class Coverage
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ClientID { get; set; }
        public int ProgramID { get; set; }
        public int CoveredProductID { get; set; }
        public DateTime? TestCallDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? MaxClaimDate { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? DropDate { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Client CoverageClient { get; set; }
        public Customer CoverageCustomer { get; set; }
        public Program CoverageProgram { get; set; }
        public CoveredProduct CoverageProduct { get; set; }
        public string Status
        {
            get
            {
                if (DropDate.HasValue && DropDate.Value < DateTime.Now && DropDate.Value > Convert.ToDateTime("01/01/2022"))
                    return "DROPPED";
                if (CancelDate.HasValue && CancelDate.Value < DateTime.Now && CancelDate.Value > Convert.ToDateTime("01/01/2022"))
                    return "CANCELLED";
                if (MaxClaimDate.HasValue && MaxClaimDate.Value < DateTime.Now && MaxClaimDate.Value > Convert.ToDateTime("01/01/2022"))
                    return "MAXCLAIMS";
                return "ENROLLED";
            }
        }
        public int ClosedClaimsPastYear { get; set; }

        public Coverage()
        {
            ID = 0;
            CustomerID = 0;
            ClientID = 0;
            CoveredProductID = 0;
            ProgramID = 0;
            TestCallDate = null;
            EffectiveDate = null;
            CancelDate = null;
            MaxClaimDate = null;
            UploadDate = null;
            DropDate = null;
            UpdatedOn = null;
            UpdatedBy = string.Empty;
            CoverageClient = null;
            CoverageCustomer = null;
            CoverageProgram = null;
            CoverageProduct = null;
        }

        public Coverage(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            CoveredProductID = DBHelper.GetInt32Value(r["CoveredProductID"]);
            ProgramID = DBHelper.GetInt32Value(r["ProgramID"]);
            TestCallDate = DBHelper.GetNullableDateTimeValue(r["TestCallDate"]);
            EffectiveDate = DBHelper.GetNullableDateTimeValue(r["EffectiveDate"]);
            CancelDate = DBHelper.GetNullableDateTimeValue(r["CancelDate"]);
            MaxClaimDate = DBHelper.GetNullableDateTimeValue(r["MaxClaimDate"]);
            UploadDate = DBHelper.GetNullableDateTimeValue(r["UploadDate"]);
            DropDate = DBHelper.GetNullableDateTimeValue(r["DropDate"]);
            UpdatedOn = DBHelper.GetNullableDateTimeValue(r["UpdatedOn"]);
            UpdatedBy = DBHelper.GetStringValue(r["UpdatedBy"]);
        }

        public Coverage(DataRow row) : base()
        {
            if (row != null)
            {
                ID = DBHelper.GetInt32Value(row["ID"]);
                CustomerID = DBHelper.GetInt32Value(row["CustomerID"]);
                ClientID = DBHelper.GetInt32Value(row["ClientID"]);
                CoveredProductID = DBHelper.GetInt32Value(row["CoveredProductID"]);
                ProgramID = DBHelper.GetInt32Value(row["ProgramID"]);
                TestCallDate = DBHelper.GetNullableDateTimeValue(row["TestCallDate"]);
                EffectiveDate = DBHelper.GetNullableDateTimeValue(row["EffectiveDate"]);
                CancelDate = DBHelper.GetNullableDateTimeValue(row["CancelDate"]);
                MaxClaimDate = DBHelper.GetNullableDateTimeValue(row["MaxClaimDate"]);
                UploadDate = DBHelper.GetNullableDateTimeValue(row["UploadDate"]);
                DropDate = DBHelper.GetNullableDateTimeValue(row["DropDate"]);
                UpdatedOn = DBHelper.GetNullableDateTimeValue(row["UpdatedOn"]);
                UpdatedBy = DBHelper.GetStringValue(row["UpdatedBy"]);
            }
        }
    }
}