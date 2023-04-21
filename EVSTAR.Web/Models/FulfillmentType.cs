using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class FulfillmentType
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public FulfillmentType()
        {
            ID = 0;
            Description = string.Empty;
        }

        public FulfillmentType(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Description = DBHelper.GetStringValue(r["Description"]);
        }
    }
}