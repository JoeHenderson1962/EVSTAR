using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address()
        {
            ID = 0;
            Line1 = string.Empty;
            Line2 = string.Empty;
            Line3 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            Country = string.Empty;
        }

        public Address(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Line1 = DBHelper.GetStringValue(r["Line1"]);
            Line2 = DBHelper.GetStringValue(r["Line2"]);
            Line3 = DBHelper.GetStringValue(r["Line3"]);
            City = DBHelper.GetStringValue(r["City"]);
            State = DBHelper.GetStringValue(r["State"]);
            PostalCode = DBHelper.GetStringValue(r["PostalCode"]);
            Country = DBHelper.GetStringValue(r["Country"]);
        }
    }
}