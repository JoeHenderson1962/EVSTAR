using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public enum UserType { None, Web, API };
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int ClientID { get; set; }
        public int StoreID { get; set; }
        public string Authentication{ get; set; }
        public DateTime LastUpdated { get; set; }
        public int Department { get; set; }
        public int ReportsTo { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool Reset { get; set; } 

        public string ActiveStr
        {
            get { return Active ? "Y" : "N"; }
        }
        public UserType UserTypeID { get; set; }
        public string Error { get; set; }
        public Client ParentClient { get; set; }
        public int AddressID { get; set; }
        public Address UserAddress { get; set; }

        public User()
        {
            ID = 0;
            UserName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Title = string.Empty;
            ClientID = 0;
            StoreID = 0;
            Authentication = string.Empty;
            LastUpdated = DateTime.MinValue;
            Department = 0;
            ReportsTo = 0;
            Email = string.Empty;
            Phone = string.Empty;
            Active = false;
            Error = string.Empty;
            UserTypeID = UserType.Web;
            ParentClient = new Client();
            AddressID = 0;
            UserAddress = new Address();
            Reset = false;
        }

        public User(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            UserName = DBHelper.GetStringValue(r["UserName"]);
            LastName = DBHelper.GetStringValue(r["LastName"]);
            FirstName = DBHelper.GetStringValue(r["FirstName"]);
            Title = DBHelper.GetStringValue(r["Title"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            StoreID = DBHelper.GetInt32Value(r["StoreID"]);
            Authentication = DBHelper.GetStringValue(r["Authentication"]);
            LastUpdated = DBHelper.GetDateTimeValue(r["LastUpdated"]);
            Department = DBHelper.GetInt32Value(r["Department"]);
            ReportsTo = DBHelper.GetInt32Value(r["ReportsTo"]);
            Email = DBHelper.GetStringValue(r["Email"]);
            Phone = DBHelper.GetStringValue(r["Phone"]);
            Active = DBHelper.GetBooleanValue(r["Active"]);
            UserTypeID = (UserType)DBHelper.GetInt32Value(r["UserTypeID"]);
            AddressID = DBHelper.GetInt32Value(r["AddressID"]);
        }
    }
}
