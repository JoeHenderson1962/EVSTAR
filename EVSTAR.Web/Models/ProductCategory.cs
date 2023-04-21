using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class ProductCategory
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int ClientID { get; set; }
        public int ProgramID { get; set; }
        public decimal ServiceFee { get; set; }
        public string ProductType { get; set; }
        public string LogoFile { get; set; }
        public int ProductCount { get; set; 
        }
        public ProductCategory()
        {
            ID = 0;
            ClientID = 0;
            ProgramID = 0;
            CategoryName = string.Empty;
            Description = string.Empty;
            ProductType = string.Empty;
            ServiceFee = 0.0M;
            LogoFile = string.Empty;
            ProductCount = 0;
        }

        public ProductCategory(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            ProgramID = DBHelper.GetInt32Value(r["ProgramID"]);
            CategoryName = DBHelper.GetStringValue(r["CategoryName"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            ProductType = DBHelper.GetStringValue(r["ProductType"]);
            ServiceFee = DBHelper.GetDecimalValue(r["ServiceFee"]);
            LogoFile = DBHelper.GetStringValue(r["LogoFile"]);
        }
    }
}