using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class CustomerLookup
    {
        public int ID { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PrimaryFirstName { get; set; }
        public string PrimaryLastName { get; set; }
        public string RegistrationCode { get; set; }
        public string ClientCode { get; set; }
        public string Authentication { get; set; }
        public string StatusCode { get; set; }
        public int CustomerID { get; set; }

        public CustomerLookup()
        {
            ID = 0;
            MobileNumber = string.Empty;
            PrimaryFirstName = string.Empty;
            PrimaryLastName = string.Empty;
            Email = string.Empty;
            ClientCode = string.Empty;
            Authentication = string.Empty;
            CustomerID = 0;
            StatusCode = String.Empty;
        }

        public CustomerLookup(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            MobileNumber = DBHelper.GetStringValue(r["MobileNumber"]);
            PrimaryFirstName = DBHelper.GetStringValue(r["PrimaryFirstName"]);
            PrimaryLastName = DBHelper.GetStringValue(r["PrimaryLastName"]);
            Email = DBHelper.GetStringValue(r["Email"]);
            ClientCode = DBHelper.GetStringValue(r["ClientCode"]);
            StatusCode = DBHelper.GetStringValue(r["StatusCode"]);
            Authentication = DBHelper.GetStringValue(r["Authentication"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
        }
    }
}