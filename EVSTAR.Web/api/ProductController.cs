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
using EVSTAR.DB.NET;

namespace EVSTAR.Web.api
{
    public class ProductController : ApiController
    {
        // GET api/<controller>
        public List<CoveredProduct> Get()
        {
            List<CoveredProduct> products = new List<CoveredProduct>();

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string customer = DBHelper.GetStringValue(HttpContext.Current.Request.Params["customer"]);
            int customerID = 0;
            Int32.TryParse(customer, out customerID);

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
                sql.AppendLine("SELECT cp.*, pc.CategoryName FROM CoveredProducts cp WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN ProductCategories pc WITH(NOLOCK) ON pc.ID = cp.ProductCategoryID ");
                sql.AppendLine("WHERE (cp.CustomerID=@CustomerID OR @CustomerID = 0) ");
                sql.AppendLine("ORDER BY pc.SortOrder, Manufacturer, Model");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        CoveredProduct cp = new CoveredProduct(r);
                        CategoryController categoryController = new CategoryController();
                        List<ProductCategory> pcs = categoryController.Get(cp.ProductCategoryID);
                        if (pcs != null && pcs.Count > 0)
                            cp.ProdCategory = pcs[0];
                        products.Add(cp);
                    }
                    r.Close();
                }
            }

            return products;
        }

        // GET api/<controller>/id
        public CoveredProduct Get(int id)
        {
            CoveredProduct product = null;

            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                    return product;
            }

            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
            CoveredProductHelper cph = new CoveredProductHelper();
            List<CoveredProduct> prods = new List<CoveredProduct>();
            string errorMsg;
            prods = cph.Select(id, clientCode,out errorMsg);
            if (prods != null && prods.Count > 0)
            {
                product = prods[0];
            }

            return product;
        }

        // POST api/<controller>
        public string Post([FromBody] CoveredProduct value)
        {
            string result = string.Empty;
            string function = DBHelper.GetStringValue(HttpContext.Current.Request.Params["function"]);

            try
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return null;
                }

                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
                CoveredProduct product = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (product != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO CoveredProducts ");
                        sql.AppendLine("(CustomerID, ProductCategoryID, Manufacturer, Model, SerialNumber, IMEI, Color, PurchaseDate, CoverageDate, Description) ");
                        sql.AppendLine("VALUES (@CustomerID, @ProductCategoryID, @Manufacturer, @Model, @SerialNumber, @IMEI, @Color, @PurchaseDate, @CoverageDate, @Description) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CustomerID", product.CustomerID);
                            cmd.Parameters.AddWithValue("@ProductCategoryID", product.ProductCategoryID);
                            cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                            cmd.Parameters.AddWithValue("@Model", product.Model);
                            cmd.Parameters.AddWithValue("@SerialNumber", product.SerialNumber);
                            cmd.Parameters.AddWithValue("@IMEI", product.IMEI);
                            cmd.Parameters.AddWithValue("@Color", product.Color);
                            cmd.Parameters.AddWithValue("@PurchaseDate", product.PurchaseDate);
                            cmd.Parameters.AddWithValue("@CoverageDate", product.CoverageDate);
                            cmd.Parameters.AddWithValue("@Description", product.Description);
                            product.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
                result = JsonConvert.SerializeObject(product);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
