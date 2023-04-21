using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class PerilSubCategory
    {
        public int ID { get; set; }
        public int CoveredPerilID { get; set; }
        public string Subcategory { get; set; }
        public CoveredPeril ParentCoveredPeril { get; set; }

        public PerilSubCategory()
        {
            ID = 0;
            CoveredPerilID = 0;
            Subcategory = string.Empty;
            ParentCoveredPeril = null;
        }

        public PerilSubCategory(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CoveredPerilID = DBHelper.GetInt32Value(r["CoveredPerilID"]);
            Subcategory = DBHelper.GetStringValue(r["Subcategory"]);
        }
    }
}