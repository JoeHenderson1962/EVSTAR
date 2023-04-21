using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int SalesOrderID { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal QuoteAmount { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }

        public Invoice()
        {
            ID = 0;
            CustomerID = 0;
            SalesOrderID = 0;
            InvoiceNumber = String.Empty;
            QuoteAmount = 0;
            AmountDue = 0;
            InvoiceAmount = 0;
            DatePaid = DateTime.MinValue;
            AmountPaid = 0;
        }

        public Invoice(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
            SalesOrderID = DBHelper.GetInt32Value(r["SalesOrderID"]);
            InvoiceNumber = DBHelper.GetStringValue(r["InvoiceNumber"]);
            QuoteAmount = DBHelper.GetDecimalValue(r["QuoteAmount"]);
            InvoiceAmount = DBHelper.GetDecimalValue(r["InvoiceAmount"]);
            AmountDue = DBHelper.GetDecimalValue(r["AmountDue"]);
            AmountPaid = DBHelper.GetDecimalValue(r["AmountPaid"]);
            DatePaid = DBHelper.GetDateTimeValue(r["DatePaid"]);
        }
    }
}
