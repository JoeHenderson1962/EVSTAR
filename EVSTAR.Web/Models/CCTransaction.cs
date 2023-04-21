using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class CCTransaction
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string CardCode { get; set; }
        public int ClientID { get; set; }         // Client points to RS Customer (School district)
        public int CustomerID { get; set; }     // Customer points to RS Contact (Parent)
        public DateTime? TransactionDateTime { get; set; }
        public string TransactionID { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string AuthCode { get; set; }

        public CCTransaction()
        {
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
            ClientID = 0;
            CustomerID = 0;
            TransactionDateTime = null;
            TransactionID = string.Empty;
            Response = string.Empty;
            Status = string.Empty;
            AuthCode = string.Empty;
        }
    }
}