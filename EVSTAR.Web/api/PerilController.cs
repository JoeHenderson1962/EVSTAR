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

namespace EVSTAR.Web.api
{
    public class PerilController : ApiController
    {
        // GET api/<controller>
        public List<CoveredPeril> Get()
        {
            List<CoveredPeril> perils = new List<CoveredPeril>();

            string program = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string category = DBHelper.GetStringValue(HttpContext.Current.Request.Params["category"]);
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
            if (Int32.TryParse(category, out int categoryID))
            {
                string provided = Encryption.MD5(code + address);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return perils;
                }

                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT c.ID, c.Peril, c.Description, s.ID as SubcategoryID, s.Subcategory, p.ProgramName, c.ProgramID, c.ProductCategoryID ");
                    sql.AppendLine("FROM CoveredPerils c WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN PerilSubcategories s WITH(NOLOCK) ON s.CoveredPerilID = c.ID ");
                    sql.AppendLine("LEFT JOIN Program p WITH(NOLOCK) ON p.ID = c.ProgramID ");
                    sql.AppendLine("LEFT JOIN Client l WITH(NOLOCK) ON l.ID = p.ClientID ");
                    sql.AppendLine("WHERE c.ProductCategoryID=@ProductCategoryID ");
                    sql.AppendLine("ORDER BY c.ID, s.ID ");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ProductCategoryID", categoryID);
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CoveredPeril cat = new CoveredPeril(r);
                            if (cat.Program.ToLower() == program.ToLower())
                                perils.Add(cat);
                        }
                        r.Close();
                    }
                }
            }
            return perils;
        }
    }
}
