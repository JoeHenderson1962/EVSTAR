using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class CoveredProduct
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string IMEI { get; set; }
        public string Color { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime CoverageDate { get; set; }
        public string Features { get; set; }
        public string MemorySize { get; set; }
        public string WiFiMobileData { get; set; }
        public string DriveType { get; set; }
        public string ScreenSize { get; set; }
        public string DriveSize { get; set; }
        public string Resolution { get; set; }
        public string Processor { get; set; }
        public string YearVersion { get; set; }
        public string ProductAdded { get; set; }
        public ProductCategory ProdCategory { get; set; }

        public CoveredProduct()
        {
            ID = 0;
            CustomerID = 0;
            ProductCategoryID = 0;
            CategoryName = string.Empty;
            Manufacturer = string.Empty;
            Model = string.Empty;
            SerialNumber = string.Empty;
            IMEI = string.Empty;
            Color = string.Empty;
            PurchaseDate = DateTime.MaxValue;
            CoverageDate = DateTime.MaxValue;
            Features = string.Empty;
            MemorySize = string.Empty;
            WiFiMobileData = string.Empty;
            DriveType = string.Empty;
            ScreenSize = string.Empty;
            DriveSize = string.Empty;
            Resolution = string.Empty;
            Processor = string.Empty;
            YearVersion = string.Empty;
            Description = string.Empty;
            ProductAdded = CoverageDate.ToShortDateString();
            ProdCategory = new ProductCategory();
        }

        public CoveredProduct(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CustomerID = DBHelper.GetInt32Value(r["CustomerID"]);
            ProductCategoryID = DBHelper.GetInt32Value(r["ProductCategoryID"]);
            CategoryName = DBHelper.GetStringValue(r["CategoryName"]);
            Manufacturer = DBHelper.GetStringValue(r["Manufacturer"]);
            Model = DBHelper.GetStringValue(r["Model"]);
            SerialNumber = DBHelper.GetStringValue(r["SerialNumber"]);
            IMEI = DBHelper.GetStringValue(r["IMEI"]);
            Color = DBHelper.GetStringValue(r["Color"]);
            PurchaseDate = DBHelper.GetDateTimeValue(r["PurchaseDate"]);
            CoverageDate = DBHelper.GetDateTimeValue(r["CoverageDate"]);
            Features = DBHelper.GetStringValue(r["Features"]);
            MemorySize = DBHelper.GetStringValue(r["MemorySize"]);
            WiFiMobileData = DBHelper.GetStringValue(r["WiFiMobileData"]);
            DriveType = DBHelper.GetStringValue(r["DriveType"]);
            ScreenSize = DBHelper.GetStringValue(r["ScreenSize"]);
            DriveSize = DBHelper.GetStringValue(r["DriveSize"]);
            Resolution = DBHelper.GetStringValue(r["Resolution"]);
            Processor = DBHelper.GetStringValue(r["Processor"]);
            YearVersion = DBHelper.GetStringValue(r["YearVersion"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            ProductAdded = CoverageDate.ToShortDateString();
        }
    }
}