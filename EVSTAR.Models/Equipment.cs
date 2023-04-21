using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Equipment
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }
        public decimal ReturnValue { get; set; }
        public int ParentID { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string SpecificationsFile { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SKU { get; set; }
        public decimal Deductible { get; set; }

        public Equipment()
        {
            ID = 0;
            ClientID = 0;
            Make = string.Empty;
            Model = string.Empty;
            Cost = 0.0M;
            ReturnValue = 0.0M;
            ParentID = 0;
            Description = string.Empty;
            ImageFile = string.Empty;
            SpecificationsFile = string.Empty;
            StartDate = null;
            EndDate = null;
            SKU = string.Empty;
            Deductible = 0.0M;
        }

        public Equipment(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            Make = DBHelper.GetStringValue(r["Make"]);
            Model = DBHelper.GetStringValue(r["Model"]);
            Cost = DBHelper.GetDecimalValue(r["Cost"]);
            ReturnValue = DBHelper.GetDecimalValue(r["ReturnValue"]);
            ParentID = DBHelper.GetInt32Value(r["ParentID"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            ImageFile = DBHelper.GetStringValue(r["ImageFile"]);
            SpecificationsFile = DBHelper.GetStringValue(r["SpecificationsFile"]);
            StartDate = DBHelper.GetNullableDateTimeValue(r["StartDate"]);
            EndDate = DBHelper.GetNullableDateTimeValue(r["EndDate"]);
            SKU = DBHelper.GetStringValue(r["SKU"]);
            Deductible = DBHelper.GetDecimalValue(r["Deductible"]);
        }
    }
}
