using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EVSTAR.Models;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace EVSTAR.DB.NET
{
    public class AddressHelper
    {
        public List<Address> Select(int id, out string errorMsg)
        {
            List<Address> result = new List<Address>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Addresses WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE id=@ID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Address address = new Address(r);
                            result.Add(address);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public Address Insert(Address address, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (address != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
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
                            cmd.Parameters.AddWithValue("@Line1", address.Line1 != null ? address.Line1 : string.Empty);
                            cmd.Parameters.AddWithValue("@Line2", address.Line2 != null ? address.Line2 : string.Empty);
                            cmd.Parameters.AddWithValue("@Line3", address.Line3 != null ? address.Line3 : string.Empty);
                            cmd.Parameters.AddWithValue("@City", address.City != null ? address.City : string.Empty);
                            cmd.Parameters.AddWithValue("@State", address.State != null ? address.State : string.Empty);
                            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode != null ? address.PostalCode : string.Empty);
                            cmd.Parameters.AddWithValue("@Country", address.Country != null ? address.Country : string.Empty);
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

        public Address Update(Address address, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (address != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Addresses SET Line1=@Line1, Line2=@Line2, Line3=@Line3, City=@City, State=@State, ");
                        sql.AppendLine("PostalCode=@PostalCode, Country=@Country ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Line1", address.Line1 != null ? address.Line1 : string.Empty);
                            cmd.Parameters.AddWithValue("@Line2", address.Line2 != null ? address.Line2 : string.Empty);
                            cmd.Parameters.AddWithValue("@Line3", address.Line3 != null ? address.Line3 : string.Empty);
                            cmd.Parameters.AddWithValue("@City", address.City != null ? address.City : string.Empty);
                            cmd.Parameters.AddWithValue("@State", address.State != null ? address.State : string.Empty);
                            cmd.Parameters.AddWithValue("@PostalCode", address.PostalCode != null ? address.PostalCode : string.Empty);
                            cmd.Parameters.AddWithValue("@Country", address.Country != null ? address.Country : string.Empty);
                            cmd.Parameters.AddWithValue("@ID", address.ID);
                            cmd.ExecuteNonQuery();
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
    }
}
