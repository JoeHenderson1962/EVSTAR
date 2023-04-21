using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class CoveredPeril
    {
        public int ID { get; set; }
        public string Peril { get; set; }
        public string Description { get; set; }
        public int SubcategoryID { get; set; }
        public string Subcategory { get; set; }
        public string Program { get; set; }

        public CoveredPeril()
        {
            ID = 0;
            Peril = string.Empty;
            Description = string.Empty;
            SubcategoryID = 0;
            Subcategory = string.Empty;
            Program = string.Empty;
        }

        public CoveredPeril(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Peril = DBHelper.GetStringValue(r["Peril"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            SubcategoryID = DBHelper.GetInt32Value(r["SubcategoryID"]);
            Subcategory = DBHelper.GetStringValue(r["Subcategory"]);
            Program = DBHelper.GetStringValue(r["Code"]);
        }
    }
}