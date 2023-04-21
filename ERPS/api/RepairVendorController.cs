using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;

namespace ERPS.api
{
    public class RepairVendorController : ApiController
    {
        // GET api/<controller>
        public List<RepairVendor> Get()
        {
            List<RepairVendor> repairVendors = new List<RepairVendor>();

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return repairVendors;
            }
            string zipCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["zip"]);

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM RepairVendors WITH(NOLOCK) ");
                sql.AppendLine("ORDER BY PostalCode");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        RepairVendor cat = new RepairVendor(r);
                        repairVendors.Add(cat);
                    }
                    r.Close();
                }
            }

            List<RepairVendor> vendors = repairVendors.Where(x => x.PostalCode.StartsWith(zipCode.Substring(0, 3))).ToList<RepairVendor>();
            if (vendors.Count < 3)
            {
                vendors = repairVendors.Where(x => x.PostalCode.StartsWith(zipCode.Substring(0, 2))).ToList<RepairVendor>();
            }
            return vendors;
        }
    }
}
