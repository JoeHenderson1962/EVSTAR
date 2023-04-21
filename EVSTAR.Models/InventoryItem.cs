using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class InventoryItem
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public int LocationID { get; set; }
        public string ScannedBy { get; set; }
        public DateTime ScannedDateTime { get; set; }
        public string ReferenceNo { get; set; }
        public int ProductID { get; set; }
        public string IMEI { get; set; }
        public string SerialNumber { get; set; }
        public string GradeClass { get; set; }
        public DateTime ClearDateTime { get; set; }

        public InventoryItem()
        {
            ID = 0;
            ClientID = 0;
            Make = string.Empty;
            Model = string.Empty;
            Cost = 0.0M;
            Description = string.Empty;
            ImageFile = string.Empty;
            LocationID = 0;
            ScannedBy = string.Empty;
            ScannedDateTime = DateTime.Now;
            ReferenceNo = string.Empty;
            ProductID = 0;
            IMEI = string.Empty;
            SerialNumber = string.Empty;
            GradeClass = string.Empty;
            ClearDateTime = Convert.ToDateTime("1900-01-01");
        }

        public InventoryItem(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            Make = DBHelper.GetStringValue(r["Make"]);
            Model = DBHelper.GetStringValue(r["Model"]);
            Cost = DBHelper.GetDecimalValue(r["Cost"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            ImageFile = DBHelper.GetStringValue(r["ImageFile"]);
            LocationID = DBHelper.GetInt32Value(r["LocationID"]);
            ScannedBy = DBHelper.GetStringValue(r["ScannedBy"]);
            ScannedDateTime = DBHelper.GetDateTimeValue(r["ScannedDateTime"]);
            ReferenceNo = DBHelper.GetStringValue(r["ReferenceNo"]);
            ProductID = DBHelper.GetInt32Value(r["ProductID"]);
            IMEI = DBHelper.GetStringValue(r["IMEI"]);
            SerialNumber = DBHelper.GetStringValue(r["SerialNumber"]);
            GradeClass = DBHelper.GetStringValue(r["GradeClassification"]);
            ClearDateTime = DBHelper.GetDateTimeValue(r["ClearedDateTime"]);
        }
    }
}
