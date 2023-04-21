using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Techcycle.Web.Models
{
    public class ClaimStatusHistory
    {
        public int ID { get; set; }
        public int ClaimID { get; set; }
        public int StatusID { get; set; }
        public DateTime StatusDate { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }

        public ClaimStatusHistory()
        {
            ID = 0;
            ClaimID = 0;
            StatusID = 0;
            UserName = string.Empty;
            StatusDate = DateTime.MinValue;
            Status = string.Empty;
        }

        public ClaimStatusHistory(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            ClaimID = DBHelper.GetInt32Value(r["ClaimID"]);
            StatusID = DBHelper.GetInt32Value(r["StatusID"]);
            StatusDate = DBHelper.GetDateTimeValue(r["StatusDate"]);
            UserName = DBHelper.GetStringValue(r["UserName"]);
            Status = DBHelper.GetStringValue(r["StatusName"]);
        }
    }
}