using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class CCTransaction
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CardCode { get; set; }
        public long RSCustomerID { get; set; }   
        public long RSContactID { get; set; }     
        public long InvoiceID { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public string TransactionID { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string AuthCode { get; set; }
        public int ClaimID { get; set; }
        public int CustomerID { get; set; }

        public CCTransaction()
        {
            ID = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            CardNumber = string.Empty;
            ExpDate = string.Empty;
            CardCode = string.Empty;
            Amount = 0;
            RSCustomerID = 0;
            RSContactID = 0;
            InvoiceID = 0;
            TransactionDateTime = null;
            TransactionID = string.Empty;
            Response = string.Empty;
            Status = string.Empty;
            AuthCode = string.Empty;
            ClaimID = 0;
        }
    }
}