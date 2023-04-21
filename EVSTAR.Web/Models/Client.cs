using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Code{ get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }
        public Address MailingAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone1 { get; set; }
        public string ContactPhone2 { get; set; }
        public string ContactFax { get; set; }
        public string ContactEmail { get; set; }
        public int FulfillmentTypeID { get; set; }
        public FulfillmentType Fulfillment { get; set; }
        public int RerepairDays { get; set; }
        public decimal LoanerBillableAmount { get; set; }
        public string AgentNumber { get; set; }
        public string DealerNumber { get; set; }
        public string DealerName { get; set; }
        public string DealerState { get; set; }
        public string DealerCountry { get; set; }
        public string LogoFile { get; set; }
        public bool RegisterWithCode { get; set; }

        public Client()
        {
            ID = 0;
            Code = string.Empty;
            Name = string.Empty;
            AddressID = 0;
            MailingAddress = new Address();
            ContactName = string.Empty;
            ContactPhone1 = string.Empty;
            ContactPhone2 = string.Empty;
            ContactFax = string.Empty;
            ContactEmail = string.Empty;
            FulfillmentTypeID = 0;
            Fulfillment = new FulfillmentType();
            RerepairDays = 0;
            LoanerBillableAmount = 0;
            AgentNumber = string.Empty;
            DealerNumber = string.Empty;
            DealerName = string.Empty;
            DealerState = string.Empty;
            DealerCountry = string.Empty;
            LogoFile = string.Empty;
            RegisterWithCode = false;
        }

        public Client(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            AddressID = DBHelper.GetInt32Value(r["AddressID"]);
            FulfillmentTypeID = DBHelper.GetInt32Value(r["FulfillmentTypeID"]);
            Code = DBHelper.GetStringValue(r["Code"]);
            Name = DBHelper.GetStringValue(r["Name"]);
            ContactName = DBHelper.GetStringValue(r["ContactName"]);
            ContactPhone1 = DBHelper.GetStringValue(r["ContactPhone1"]);
            ContactPhone2 = DBHelper.GetStringValue(r["ContactPhone2"]);
            ContactFax = DBHelper.GetStringValue(r["ContactFax"]);
            ContactEmail = DBHelper.GetStringValue(r["ContactEmail"]);
            RerepairDays = DBHelper.GetInt32Value(r["RerepairDays"]);
            LoanerBillableAmount = DBHelper.GetInt32Value(r["LoanerBillableAmount"]);
            AgentNumber = DBHelper.GetStringValue(r["Agent_Number"]);
            DealerNumber = DBHelper.GetStringValue(r["Dealer_Number"]);
            DealerName = DBHelper.GetStringValue(r["Dealer_Name"]);
            DealerState = DBHelper.GetStringValue(r["Dealer_State"]);
            DealerCountry = DBHelper.GetStringValue(r["Dealer_Country"]);
            LogoFile = DBHelper.GetStringValue(r["LogoFile"]);
            RegisterWithCode = DBHelper.GetBooleanValue(r["RegisterWithCode"]);
        }
    }
}