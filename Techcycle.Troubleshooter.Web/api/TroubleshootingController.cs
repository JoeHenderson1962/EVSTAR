using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Techcycle.Troubleshooter.Web.api
{
    public class TroubleshootingController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            //List<Troubleshooting> troubleshootings = new List<Troubleshooting>();
            string make = DBHelper.GetStringValue(HttpContext.Current.Request.Params["make"]);
            string model = DBHelper.GetStringValue(HttpContext.Current.Request.Params["model"]);

            //string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    con.Open();
            //    StringBuilder sql = new StringBuilder();
            //    sql.AppendLine("SELECT * FROM Troubleshooting WHERE (ProductCategoryID=@PCID OR ISNULL(@PCID, 0) = 0) ");
            //    sql.AppendLine("AND (ProductID=@PID OR ISNULL(@PID, 0) = 0)");
            //    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@PCID", pcID);
            //        cmd.Parameters.AddWithValue("@PID", pID);
            //        SqlDataReader r = cmd.ExecuteReader();
            //        while (r.Read())
            //        {
            //            Troubleshooting ts = new Troubleshooting(r);
            //            troubleshootings.Add(ts);
            //        }
            //        r.Close();
            //    }
            //}
            string path = HttpContext.Current.Server.MapPath(string.Format("~/Content/{0}-{1}.html", make.Replace(" ", ""), model.Replace(" ", "")));
            path = path.Replace("/-", "/");
            StreamReader sr = new StreamReader(path);
            string troubleshootings = sr.ReadToEnd();
            sr.Close();
            return troubleshootings;
        }
    }
}
