using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Subscription
    {
        public DateTime Sale_Purchase_Date { get; set; }
        public DateTime Coverage_Start_Date { get; set; }
        public DateTime Cancellation_Date { get; set; }
        public string Cancel_Reason_Code { get; set; }
        public string Customer_First_Name { get; set; }
        public string Customer_Last_Name { get; set; }
        public string Customer_Address_1 { get; set; }
        public string Customer_Address_2 { get; set; }
        public string Customer_City { get; set; }
        public string Customer_State { get; set; }
        public string Customer_ZIP_Code { get; set; }
        public string Customer_Country { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Email { get; set; }
        public string SKU_Coverage_Code { get; set; }
        public string Subscribed_Phone { get; set; }
        public string Subscribed_Make { get; set; } 
        public string Subscribed_Model { get;set; }
        public string Subscribed_IMEI { get; set; }
        public string Subscribed_ESN { get; set; }
        public decimal Contract_Refund_Amount { get; set; }
        public decimal Contract_Price_Retail_Cost { get; set; }
        public string Carrier { get; set; }
        public string Action { get; set; }

        public Subscription()
        {
            Sale_Purchase_Date = DateTime.MinValue;
            Coverage_Start_Date = DateTime.MinValue;
            Cancellation_Date = DateTime.MinValue; 
            Cancel_Reason_Code = string.Empty;
            Customer_First_Name = string.Empty;
            Customer_Last_Name = string.Empty;
            Customer_Address_1 = String.Empty;
            Customer_Address_2 = String.Empty;
            Customer_City  = String.Empty;
            Customer_State = String.Empty;
            Customer_ZIP_Code = String.Empty;
            Customer_Country = String.Empty;
            Customer_Phone = String.Empty;
            Customer_Email = String.Empty;
            SKU_Coverage_Code = String.Empty;
            Subscribed_ESN = String.Empty;
            Subscribed_IMEI = String.Empty;
            Subscribed_Make = String.Empty;
            Subscribed_Model = String.Empty;
            Subscribed_Phone = String.Empty;
            Contract_Refund_Amount = 0;
            Contract_Price_Retail_Cost = 0;
            Carrier = String.Empty;
            Action = String.Empty;
        }

        public Subscription(SqlDataReader r) : base()
        {
            Sale_Purchase_Date = DBHelper.GetDateTimeValue(r["Sale_Purchase_Date"]);
            Coverage_Start_Date = DBHelper.GetDateTimeValue(r["Coverage_Start_Date"]);
            Cancellation_Date = DBHelper.GetDateTimeValue(r["Cancellation_Date"]);
            Cancel_Reason_Code = DBHelper.GetStringValue(r["Cancel_Reason_Code"]);
            Customer_First_Name = DBHelper.GetStringValue(r["Customer_First_Name"]);
            Customer_Last_Name = DBHelper.GetStringValue(r["Customer_Last_Name"]);
            Customer_Address_1 = DBHelper.GetStringValue(r["Customer_Address_1"]);
            Customer_Address_2 = DBHelper.GetStringValue(r["Customer_Address_2"]);
            Customer_City = DBHelper.GetStringValue(r["Customer_City"]);
            Customer_State = DBHelper.GetStringValue(r["Customer_State"]);
            Customer_ZIP_Code = DBHelper.GetStringValue(r["Customer_ZIP_Code"]);
            Customer_Country = DBHelper.GetStringValue(r["Customer_Country"]);
            Customer_Phone = DBHelper.GetStringValue(r["Customer_Phone"]);
            Customer_Email = DBHelper.GetStringValue(r["Customer_Email"]);
            SKU_Coverage_Code = DBHelper.GetStringValue(r["SKU_Coverage_Code"]);
            Subscribed_ESN = DBHelper.GetStringValue(r["Subscribed_ESN"]);
            Subscribed_IMEI = DBHelper.GetStringValue(r["Subscribed_IMEI"]);
            Subscribed_Make = DBHelper.GetStringValue(r["Subscribed_Make"]);
            Subscribed_Model = DBHelper.GetStringValue(r["Subscribed_Model"]);
            Subscribed_Phone = DBHelper.GetStringValue(r["Subscribed_Phone"]);
            Contract_Refund_Amount = DBHelper.GetDecimalValue(r["Contract_Refund_Amount"]);
            Contract_Price_Retail_Cost = DBHelper.GetDecimalValue(r["Contract_Price_Retail_Cost"]);
            Carrier = DBHelper.GetStringValue(r["Carrier"]);
            Action = DBHelper.GetStringValue(r["Action"]);
        }
    }

    public class SubscriptionResponse
    {
        public string status { get; set; }
        public int id { get; set; }
    }
}
