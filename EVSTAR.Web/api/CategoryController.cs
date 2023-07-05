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
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        public List<ProductCategory> Get(int id)
        {
            List<ProductCategory> products = new List<ProductCategory>();

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string programIDstr = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);
            int programID = DBHelper.GetInt32Value(programIDstr);

            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return products;
            }

            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
            string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT pc.* FROM ProductCategories pc WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN ProductType pt WITH(NOLOCK) ON pt.[Name] = pc.ProductType ");
                if (programID > 0)
                    sql.AppendLine("WHERE ProgramID=@ProgramID ");
                else if (id > 0)
                    sql.AppendLine("WHERE pc.ID=@ID ");
                sql.AppendLine("ORDER BY pt.SortOrder, pc.SortOrder");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (programID > 0)
                        cmd.Parameters.AddWithValue("@ProgramID", programID);
                    else if (id > 0)
                        cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        ProductCategory cp = new ProductCategory(r);
                        products.Add(cp);
                    }
                    r.Close();
                }
            }

            return products;
        }
    }
}
