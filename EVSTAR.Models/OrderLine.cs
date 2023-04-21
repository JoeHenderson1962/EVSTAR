using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class OrderLine
    {
        public int ID { get; set; }
        public int SalesOrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityDelivered { get; set; }
        public decimal UnitCost { get; set; }
        public bool Taxable { get; set; }
        public decimal TaxAmount { get; set; }
        public string StudentName { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public decimal LineTotal { get; set; }
        public DateTime? DateAgreedToWaiver { get; set; }
        public string StudentID { get; set; }

        public OrderLine()
        {
            ID = 0;
            SalesOrderID = 0;
            ProductID = 0;
            ProductName = "";
            Quantity = 0;
            QuantityDelivered = 0;
            UnitCost = 0;
            Taxable = false;
            TaxAmount = 0;
            StudentName = String.Empty;
            AssetTag = String.Empty;
            SerialNumber = String.Empty;
            LineTotal = 0;
            DateAgreedToWaiver = null;
            StudentID = string.Empty;
        }

        public OrderLine(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            SalesOrderID = DBHelper.GetInt32Value(r["SalesOrderID"]);
            ProductID = DBHelper.GetInt32Value(r["ProductID"]);
            ProductName = DBHelper.GetStringValue(r["ProductName"]);
            Quantity = DBHelper.GetDecimalValue(r["Quantity"]);
            QuantityDelivered = DBHelper.GetDecimalValue(r["QuantityDelivered"]);
            UnitCost = DBHelper.GetDecimalValue(r["UnitCost"]);
            Taxable = DBHelper.GetBooleanValue(r["Taxable"]);
            TaxAmount = DBHelper.GetDecimalValue(r["TaxAmount"]);
            StudentName = DBHelper.GetStringValue(r["StudentName"]);
            AssetTag = DBHelper.GetStringValue(r["AssetTag"]);
            SerialNumber = DBHelper.GetStringValue(r["SerialNumber"]);
            DateAgreedToWaiver = DBHelper.GetDateTimeValue(r["DateAgreedToWaiver"]);
            StudentID = DBHelper.GetStringValue(r["StudentID"]);
        }
    }
}
