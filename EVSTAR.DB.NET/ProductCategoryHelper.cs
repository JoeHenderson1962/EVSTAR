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
    public class ProductCategoryHelper
    {
        public List<ProductCategory> Select(int id, string clientCode, out string errorMsg)
        {
            List<ProductCategory> result = new List<ProductCategory>();

            ClientHelper clientHelper = new ClientHelper();
            ProgramHelper programHelper = new ProgramHelper();
            
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM ProductCategories WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ProductCategory category = new ProductCategory(r);
                            if (category != null)
                            {
                                if (category.ClientID > 0)
                                {
                                    List<Client> clients = clientHelper.Select(category.ClientID, clientCode, out errorMsg);
                                    if (clients != null && clients.Count > 0)
                                        category.ProductCategoryClient = clients[0];
                                }

                                if (category.ProgramID > 0)
                                {
                                    List<Program> programs = programHelper.Select(category.ProgramID, clientCode, out errorMsg);
                                    if (programs != null && programs.Count > 0)
                                        category.ProductCategoryProgram = programs[0];
                                }
                            }

                            result.Add(category);
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

        public ProductCategory Insert(ProductCategory data, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO ProductCategories ");
                        sql.AppendLine("(CategoryName, Description, ClientID, ProgramID, ServiceFee, ProductType, LogoFile, MaxAmountPerClaim, ");
                        sql.AppendLine("MaxAmountPer12Month, FulfillmentType, Coverage, SortOrder) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@CategoryName, @Description, @ClientID, @ProgramID, @ServiceFee, @ProductType, @LogoFile, @MaxAmountPerClaim, ");
                        sql.AppendLine("@MaxAmountPer12Month, @FulfillmentType, @Coverage, @SortOrder) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
                            cmd.Parameters.AddWithValue("@ServiceFee", data.ServiceFee);
                            cmd.Parameters.AddWithValue("@ProductType", data.ProductType);
                            cmd.Parameters.AddWithValue("@LogoFile", data.LogoFile);
                            cmd.Parameters.AddWithValue("@MaxAmountPerClaim", data.MaxAmountPerClaim);
                            cmd.Parameters.AddWithValue("@MaxAmountPer12Month", data.MaxAmountPer12Month);
                            cmd.Parameters.AddWithValue("@FulfillmentType", data.FulfillmentType);
                            cmd.Parameters.AddWithValue("@Coverage", data.Coverage);
                            cmd.Parameters.AddWithValue("@SortOrder", data.SortOrder);
                            data.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return data;
        }

        public ProductCategory Update(ProductCategory data, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE ProductCategories SET CategoryName=@CategoryName, Description=@Description, ClientID=@ClientID, ");
                        sql.AppendLine("ProgramID=@ProgramID, ServiceFee=@ServiceFee, ProductType=@ProductType, LogoFile=@LogoFile, ");
                        sql.AppendLine("MaxAmountPerClaim=@MaxAmountPerClaim, MaxAmountPer12Month=@MaxAmountPer12Month, FulfillmentType=@FulfillmentType, ");
                        sql.AppendLine("Coverage=@Coverage, SortOrder=@SortOrder ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
                            cmd.Parameters.AddWithValue("@ServiceFee", data.ServiceFee);
                            cmd.Parameters.AddWithValue("@ProductType", data.ProductType);
                            cmd.Parameters.AddWithValue("@LogoFile", data.LogoFile);
                            cmd.Parameters.AddWithValue("@MaxAmountPerClaim", data.MaxAmountPerClaim);
                            cmd.Parameters.AddWithValue("@MaxAmountPer12Month", data.MaxAmountPer12Month);
                            cmd.Parameters.AddWithValue("@FulfillmentType", data.FulfillmentType);
                            cmd.Parameters.AddWithValue("@Coverage", data.Coverage);
                            cmd.Parameters.AddWithValue("@SortOrder", data.SortOrder);
                            cmd.Parameters.AddWithValue("@ID", data.ID);
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
            return data;
        }
    }
}
