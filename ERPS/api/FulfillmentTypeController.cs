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
    public class FulfillmentTypeController : ApiController
    {
        // GET api/<controller>
        public List<FulfillmentType> Get()
        {
            List<FulfillmentType> fulfillmentTypes = new List<FulfillmentType>();

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return fulfillmentTypes;
            }

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM FulfillmentTypes c WITH(NOLOCK) ");
                sql.AppendLine("ORDER BY Description");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        FulfillmentType cat = new FulfillmentType(r);
                        fulfillmentTypes.Add(cat);
                    }
                    r.Close();
                }
            }

            return fulfillmentTypes;
        }

        // GET api/<controller>/{id}
        public FulfillmentType Get(int id)
        {
            FulfillmentType fulfillmentType = null;

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return fulfillmentType;
            }

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM ProductCategories c WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID=@ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        fulfillmentType = new FulfillmentType(r);
                    }
                    r.Close();
                }
            }

            return fulfillmentType;
        }
    }
}
