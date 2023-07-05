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
    public class AddressController : ApiController
    {
        // GET api/<controller>
        public Address Get(int id)
        {
            Address address = null;
            try 
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return address;
                }
                address = GetAddress(id);
            }
            catch (Exception ex)
            {
            }
            return address;
        }

        // POST api/<controller>
        public Address Post([FromBody] Address value)
        {
            Address address = null;
            try
            {
                //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                //string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                //string provided = Encryption.MD5(code + email);
                //if (hashed != provided)
                //{
                //    provided = Encryption.MD5(code + phone);
                //    if (hashed != provided)
                //        return null;
                //}

                address = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (address != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Addresses ");
                        sql.AppendLine("(Line1, Line2, Line3, City, State, PostalCode, Country) ");
                        sql.AppendLine("VALUES (@Line1, @Line2, @Line3, @City, @State, @PostalCode, @Country); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Line1", address.Line1);
                            cmd.Parameters.AddWithValue("@Line2", address.Line2);
                            cmd.Parameters.AddWithValue("@Line3", address.Line3);
                            cmd.Parameters.AddWithValue("@City", address.City);
                            cmd.Parameters.AddWithValue("@State", address.State);
                            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                            cmd.Parameters.AddWithValue("@Country", address.Country);
                            address.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return address;
        }

        private Address InsertAddress(Address address, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (address != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Addresses ");
                        sql.AppendLine("(Line1, Line2, Line3, City, State, PostalCode, Country) ");
                        sql.AppendLine("VALUES (@Line1, @Line2, @Line3, @City, @State, @PostalCode, @Country); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Line1", address.Line1);
                            cmd.Parameters.AddWithValue("@Line2", address.Line2);
                            cmd.Parameters.AddWithValue("@Line3", address.Line3);
                            cmd.Parameters.AddWithValue("@City", address.City);
                            cmd.Parameters.AddWithValue("@State", address.State);
                            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                            cmd.Parameters.AddWithValue("@Country", address.Country);
                            address.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            return address;
        }

        // POST api/<controller>
        public Address Put([FromBody] Address value)
        {
            Address address = null;
            try
            {
                //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                //string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                //string provided = Encryption.MD5(code + email);
                //if (hashed != provided)
                //{
                //    provided = Encryption.MD5(code + phone);
                //    if (hashed != provided)
                //        return null;
                //}

                address = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (address != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Addresses SET Line1=@Line1, Line2=@Line2, Line3=@Line3, City=@City, State=@State, ");
                        sql.AppendLine("PostalCode=@PostalCode, Country=@Country WHERE ID=@ID");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ID", address.ID);
                            cmd.Parameters.AddWithValue("@Line1", address.Line1);
                            cmd.Parameters.AddWithValue("@Line2", address.Line2);
                            cmd.Parameters.AddWithValue("@Line3", address.Line3);
                            cmd.Parameters.AddWithValue("@City", address.City);
                            cmd.Parameters.AddWithValue("@State", address.State);
                            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode);
                            cmd.Parameters.AddWithValue("@Country", address.Country);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return address;
        }

        private Address GetAddress(int addressID)
        {
            Address address = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Addresses WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", addressID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        address = new Address(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return address;
        }
    }
}
