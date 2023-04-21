using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class CCReport
    {
        /*
        SELECT so.ID, so.OrderDateTime, so.OrderTotal, o.StudentName, o.AssetTag, o.SerialNumber, o.StudentID,
        s.[Name], c.FirstName, c.LastName, c.Address, c.City, c.State, c.PostalCode, c.Amount, c.AuthCode
        FROM[TechCycle].[dbo].[SalesOrders]
              so WITH(NOLOCK)
        LEFT JOIN OrderLines o WITH(NOLOCK) ON o.SalesOrderID = so.ID
        LEFT JOIN CCTransactions c WITH(NOLOCK) on c.ID = so.CCTransactionID
        LEFT JOIN SaleItems s WITH(NOLOCK) ON s.ID = o.ProductID
        WHERE ISNULL(c.AuthCode, '') <> ''
        */

        public int ID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Decimal OrderTotal { get; set; }
        public string StudentName { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public string StudentID { get; set; }
        public string Coverage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Decimal AmountCharged { get; set; }
        public string AuthCode { get; set; }

        public CCReport()
        {
            ID = 0;
            OrderDateTime = DateTime.MinValue;
            OrderTotal = Decimal.Zero;
            StudentName = string.Empty;
            AssetTag = string.Empty;
            SerialNumber = string.Empty;
            StudentID = string.Empty;
            Coverage = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            AmountCharged = Decimal.Zero;
            AuthCode = string.Empty;
        }

        public CCReport(SqlDataReader r): base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            OrderDateTime = DBHelper.GetDateTimeValue(r["OrderDateTime"]);
            OrderTotal = DBHelper.GetDecimalValue(r["OrderTotal"]);
            StudentName = DBHelper.GetStringValue(r["StudentName"]);
            AssetTag = DBHelper.GetStringValue(r["AssetTag"]);
            SerialNumber = DBHelper.GetStringValue(r["SerialNumber"]);
            StudentID = DBHelper.GetStringValue(r["StudentID"]);
            Coverage = DBHelper.GetStringValue(r["Coverage"]);
            FirstName = DBHelper.GetStringValue(r["FirstName"]);
            LastName = DBHelper.GetStringValue(r["LastName"]);
            Address = DBHelper.GetStringValue(r["Address"]);
            City = DBHelper.GetStringValue(r["City"]);
            State = DBHelper.GetStringValue(r["State"]);
            PostalCode = DBHelper.GetStringValue(r["PostalCode"]);
            AmountCharged = DBHelper.GetDecimalValue(r["Amount"]);
            AuthCode = DBHelper.GetStringValue(r["AuthCode"]);
        }
    }
}
