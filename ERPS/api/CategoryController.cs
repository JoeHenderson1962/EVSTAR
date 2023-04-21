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
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        public List<ProductCategory> Get(int id)
        {
            List<ProductCategory> products = new List<ProductCategory>();

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string programIDstr = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);
            int programID = DBHelper.GetInt32Value(programIDstr);

            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return products;
            }

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
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

        //// GET api/<controller>
        //public ProductCategory Get(int id)
        //{
        //    ProductCategory category = null;

        //    string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
        //    string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
        //    string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
        //    string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
        //    string provided = Encryption.MD5(code + address);
        //    if (hashed != provided)
        //    {
        //        provided = Encryption.MD5(code + phone);
        //        if (hashed != provided)
        //            return category;
        //    }

        //    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        con.Open();
        //        StringBuilder sql = new StringBuilder();
        //        sql.AppendLine("SELECT pc.* FROM ProductCategories pc WITH(NOLOCK) ");
        //        sql.AppendLine("WHERE pc.ID=@ID ");
        //        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@ID", id);
        //            SqlDataReader r = cmd.ExecuteReader();
        //            if (r.Read())
        //            {
        //                category = new ProductCategory(r);
        //            }
        //            r.Close();
        //        }
        //    }

        //    return category;
        //}

        //public Client GetClientByID(int id)
        //{
        //    Client client = null;

        //    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        con.Open();
        //        StringBuilder sql = new StringBuilder();
        //        sql.AppendLine("SELECT * FROM Client WITH(NOLOCK) ");
        //        sql.AppendLine("WHERE ID=@ID ");
        //        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@ID", id);
        //            SqlDataReader r = cmd.ExecuteReader();
        //            if (r.Read())
        //            {
        //                client = new Client(r);
        //                AddressController ac = new AddressController();
        //                client.MailingAddress = ac.Get(client.AddressID);
        //                client.Fulfillment = GetFulfillmentTypeByID(client.FulfillmentTypeID);
        //            }
        //            r.Close();
        //        }
        //    }

        //    return client;
        //}

        //public FulfillmentType GetFulfillmentTypeByID(int id)
        //{
        //    FulfillmentType ft = null;

        //    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        con.Open();
        //        StringBuilder sql = new StringBuilder();
        //        sql.AppendLine("SELECT * FROM FulfillmentTypes WITH(NOLOCK) ");
        //        sql.AppendLine("WHERE ID=@ID ");
        //        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            cmd.Parameters.AddWithValue("@ID", id);
        //            SqlDataReader r = cmd.ExecuteReader();
        //            if (r.Read())
        //            {
        //                ft = new FulfillmentType(r);
        //            }
        //            r.Close();
        //        }
        //    }

        //    return ft;
        //}
    }
}
