using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string PrimaryName { get; set; }
        public string AuthorizedName { get; set; }
        public int BillingAddressID { get; set; }
        public int ShippingAddressID { get; set; }
        public int MailingAddressID { get; set; }
        public string Email { get; set; }
        public string PrimaryFirstName { get; set; }
        public string PrimaryLastName { get; set; }
        public string AccountNumber { get; set; }
        public string SequenceNumber { get; set; }
        public string SubscriptionID { get; set; }
        public string StatusCode { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public Address MailingAddress { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public DateTime DateSubscriptionEmailSent { get; set; }
        public int ProgramID { get; set; }
        public Program WarrantyProgram { get; set; }
        public string Result { get; set; }
        public long RepairShoprCustomerID { get; set; }
        public DateTime? DateAgreedToWaiver { get; set; }
        public int ParentID { get; set; }
        public short NumClaimsLast12Months { get; set; }

        public Customer()
        {
            ID = 0;
            ClientID = 0;
            MobileNumber = string.Empty;
            HomeNumber = string.Empty;
            PrimaryFirstName = string.Empty;
            PrimaryLastName = string.Empty;
            PrimaryName = string.Empty;
            AuthorizedName = string.Empty;
            BillingAddressID = 0;
            ShippingAddressID = 0;
            MailingAddressID = 0;
            Email = string.Empty;
            AccountNumber = string.Empty;
            SequenceNumber = string.Empty;
            SubscriptionID = string.Empty;
            StatusCode = string.Empty;
            EnrollmentDate = DateTime.Parse("1900-01-01");
            CompanyName = string.Empty;
            Password = string.Empty;
            Result = string.Empty;
            ProgramID = 0;
            DateSubscriptionEmailSent = DateTime.Parse("1900-01-01");
            WarrantyProgram = new Program();
            RepairShoprCustomerID = 0;
            DateAgreedToWaiver = null;
            ParentID = 0;
            NumClaimsLast12Months = 0;
        }

        public Customer(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            MobileNumber = DBHelper.GetStringValue(r["MobileNumber"]);
            HomeNumber = DBHelper.GetStringValue(r["HomeNumber"]);
            PrimaryFirstName = DBHelper.GetStringValue(r["PrimaryFirstName"]);
            PrimaryLastName = DBHelper.GetStringValue(r["PrimaryLastName"]);
            PrimaryName = DBHelper.GetStringValue(r["PrimaryName"]);
            AuthorizedName = DBHelper.GetStringValue(r["AuthorizedName"]);
            BillingAddressID = DBHelper.GetInt32Value(r["BillingAddressID"]);
            ShippingAddressID = DBHelper.GetInt32Value(r["ShippingAddressID"]);
            MailingAddressID = DBHelper.GetInt32Value(r["MailingAddressID"]);
            Email = DBHelper.GetStringValue(r["Email"]);
            AccountNumber = DBHelper.GetStringValue(r["AccountNumber"]);
            SequenceNumber = DBHelper.GetStringValue(r["SequenceNumber"]);
            SubscriptionID = DBHelper.GetStringValue(r["SubscriptionID"]);
            StatusCode = DBHelper.GetStringValue(r["StatusCode"]);
            EnrollmentDate = DBHelper.GetDateTimeValue(r["EnrollmentDate"]);
            DateSubscriptionEmailSent = DBHelper.GetDateTimeValue(r["DateSubscriptionEmailSent"]);
            CompanyName = DBHelper.GetStringValue(r["CompanyName"]);
            try
            {
                Password = DBHelper.GetStringValue(r["Password"]);
            }
            catch
            {
                Password = DBHelper.GetStringValue(r["Authentication"]);
            }
            ProgramID = DBHelper.GetInt32Value(r["ProgramID"]);
            WarrantyProgram = new Program();
            RepairShoprCustomerID = DBHelper.GetInt64Value(r["RepairShoprCustomerID"]);
            DateAgreedToWaiver = DBHelper.GetDateTimeValue(r["DateAgreedToWaiver"]);
            ParentID = DBHelper.GetInt32Value(r["ParentID"]);
        }
    }
}