using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Store
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Client StoreClient { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public string Hours { get; set; }
        public string Directions { get; set; }


        public Store()
        {
            ID = 0;
            Name = string.Empty;
            ClientID= 0;
            StoreClient = null;
            ContactName = string.Empty;
            Line1 = string.Empty;
            Line2 = string.Empty;
            Line3 = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            ContactPhone = string.Empty;
            ContactFax = string.Empty;
            Hours = string.Empty;
            Directions = string.Empty;
            ContactEmail = string.Empty;
        }

        public Store(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Name = DBHelper.GetStringValue(r["Name"]);
            ContactName = DBHelper.GetStringValue(r["ContactName"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            Line1 = DBHelper.GetStringValue(r["Line1"]);
            Line2 = DBHelper.GetStringValue(r["Line2"]);
            Line3 = DBHelper.GetStringValue(r["Line3"]);
            City = DBHelper.GetStringValue(r["City"]);
            Country = DBHelper.GetStringValue(r["Country"]);
            State = DBHelper.GetStringValue(r["State"]);
            PostalCode = DBHelper.GetStringValue(r["PostalCode"]);
            ContactPhone = DBHelper.GetStringValue(r["ContactPhone"]);
            ContactFax = DBHelper.GetStringValue(r["ContactFax"]);
            Hours = DBHelper.GetStringValue(r["Hours"]);
            Directions = DBHelper.GetStringValue(r["Directions"]);
            ContactEmail = DBHelper.GetStringValue(r["ContactEmail"]);
        }
    }
}