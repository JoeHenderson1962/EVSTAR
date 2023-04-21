using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public string Color { get; set; }
        public int Memory { get; set; }
        public string ProductJSON { get; set; }

        public Product()
        {
            ID = 0;
            Make = string.Empty;
            Model = string.Empty;
            Cost = 0.0M;
            Description = string.Empty;
            ImageFile = string.Empty;
            UPC = string.Empty;
            ProductJSON = string.Empty;
            SKU = string.Empty;
            Color = string.Empty;
            Memory = 0;
        }

        public Product(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Make = DBHelper.GetStringValue(r["Make"]);
            Model = DBHelper.GetStringValue(r["Model"]);
            Cost = DBHelper.GetDecimalValue(r["Cost"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            ImageFile = DBHelper.GetStringValue(r["ImageFile"]);
            UPC = DBHelper.GetStringValue(r["UPC"]);
            ProductJSON = DBHelper.GetStringValue(r["ProductJSON"]);
            SKU = DBHelper.GetStringValue(r["SKU"]);
            Color = DBHelper.GetStringValue(r["Color"]);
            Memory = DBHelper.GetInt32Value(r["Memory"]);
        }
    }
}
