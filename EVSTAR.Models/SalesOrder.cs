using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class SalesOrder
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int CustomerID { get; set; }
        public int BillToAddressID { get; set; }
        public int ShipToAddressID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal TotalTax { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal AmountPaid { get; set; }
        public int CCTransactionID { get; set; }
        public CCTransaction CCTrans { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public Address BillToAddress { get; set; }
        public Address ShipToAddress { get; set; }
        public Client Client { get; set; }
        public Customer Customer { get; set; }

        public SalesOrder()
        {
            ID = 0;
            ClientID = 0;
            CustomerID = 0;
            Client = new Client();
            Customer = new Customer();
            BillToAddressID = 0;
            BillToAddress = new Address();
            ShipToAddress = new Address();
            ShipToAddressID = 0;
            OrderDateTime = DateTime.Now;
            TotalTax = 0;
            ItemTotal = 0;
            Shipping = 0;
            AmountPaid = 0;
            OrderTotal = 0;
            CCTransactionID = 0;
            CCTrans = new CCTransaction();
            OrderLines = new List<OrderLine>();
        }

        public SalesOrder(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
            BillToAddressID = DBHelper.GetInt32Value(r["BillToAddressID"]);
            ShipToAddressID = DBHelper.GetInt32Value(r["ShipToAddressID"]);
            OrderDateTime = DBHelper.GetDateTimeValue(r["OrderDateTime"]);
            TotalTax = DBHelper.GetDecimalValue(r["TotalTax"]);
            ItemTotal = DBHelper.GetDecimalValue(r["ItemTotal"]);
            Shipping = DBHelper.GetDecimalValue(r["Shipping"]);
            AmountPaid = DBHelper.GetDecimalValue(r["AmountPaid"]);
            OrderTotal = DBHelper.GetDecimalValue(r["OrderTotal"]);
            CCTransactionID = DBHelper.GetInt32Value(r["CCTransactionID"]);
        }

    }
}
