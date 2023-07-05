using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public int ModelID { get; set; }
        public string ModelName{ get; set; }

        public Vehicle()
        {
            ID = 0;
            Make = string.Empty;
            ModelID = 0;
            ModelName = string.Empty;
        }

        public Vehicle(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Make = DBHelper.GetStringValue(r["Make"]);
            ModelID = DBHelper.GetInt32Value(r["ModelId"]);
            ModelName = DBHelper.GetStringValue(r["Model"]);
        }
    }
}