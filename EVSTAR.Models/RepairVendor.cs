using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class RepairVendor
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public bool Curbside { get; set; }
        public bool MailIn { get; set; }
        public bool CarryIn { get; set; }
        public string Url { get; set; }

        public RepairVendor()
        {
            ID = 0;
            CompanyName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            Country = string.Empty;
            Phone = string.Empty;
            Curbside = true;
            MailIn = true;
            CarryIn = true;
            Url = string.Empty;
        }

        public RepairVendor(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CompanyName = DBHelper.GetStringValue(r["CompanyName"]);
            Address = DBHelper.GetStringValue(r["Address"]);
            City = DBHelper.GetStringValue(r["City"]);
            State = DBHelper.GetStringValue(r["State"]);
            PostalCode = DBHelper.GetStringValue(r["PostalCode"]);
            Country = DBHelper.GetStringValue(r["Country"]);
            Phone = DBHelper.GetStringValue(r["Phone"]);
            Curbside = DBHelper.GetBooleanValue(r["Curbside"]);
            MailIn = DBHelper.GetBooleanValue(r["MailIn"]);
            CarryIn = DBHelper.GetBooleanValue(r["CarryIn"]);
            Url = DBHelper.GetStringValue(r["Url"]);
        }
    }
}