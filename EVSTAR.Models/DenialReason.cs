using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class DenialReason
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DenialReason()
        {
            ID = 0;
            Name = string.Empty;
            Description = string.Empty;
        }

        public DenialReason(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Name = DBHelper.GetStringValue(r["Name"]);
            Description = DBHelper.GetStringValue(r["Description"]);
        }
    }
}