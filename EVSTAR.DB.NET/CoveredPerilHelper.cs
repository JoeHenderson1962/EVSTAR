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
    public class CoveredPerilHelper
    {
        public List<CoveredPeril> SelectFull(int id, int prodCategoryId, string program, string clientCode, out string errorMsg)
        {
            List<CoveredPeril> result = new List<CoveredPeril>();

            ProductCategoryHelper productCategoryHelper = new ProductCategoryHelper();
            ProgramHelper programHelper = new ProgramHelper();

            errorMsg = string.Empty;
            try
            {
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
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ProductCategoryID", prodCategoryId);
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CoveredPeril cat = new CoveredPeril(r);
                            if (cat.Program.ToLower() == program.ToLower() || id > 0)
                                result.Add(cat);
                        }
                        r.Close();
                    }
                    if (id > 0)
                        sql.AppendLine("WHERE c.ID=@ID ");
                    if (prodCategoryId > 0)
                        sql.AppendLine("WHERE ProductCategoryID=@ProductCategoryID ");
                    sql.AppendLine("ORDER BY c.ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);
                        if (prodCategoryId > 0)
                            cmd.Parameters.AddWithValue("@ProductCategoryID", prodCategoryId);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CoveredPeril peril = new CoveredPeril(r);
                            if (peril.ProgramID > 0)
                            {
                                List<Program> programResults = programHelper.Select(peril.ProgramID, clientCode, out errorMsg);
                                if (programResults != null && programResults.Count > 0)
                                {
                                    peril.PerilProgram = programResults[0];
                                    peril.Program = peril.PerilProgram.ProgramName;
                                }
                            }
                            if (peril.ProductCategoryID > 0)
                            {
                                List<ProductCategory> categoryResults = productCategoryHelper.Select(peril.ProductCategoryID, clientCode, out errorMsg);
                                if (categoryResults != null && categoryResults.Count > 0)
                                {
                                    peril.PerilProductCategory = categoryResults[0];
                                }
                            }
                            result.Add(peril);
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

        public List<CoveredPeril> Select(int id, int prodCategoryId, string program, string clientCode, out string errorMsg)
        {
            List<CoveredPeril> result = new List<CoveredPeril>();

            ProductCategoryHelper productCategoryHelper = new ProductCategoryHelper();
            ProgramHelper programHelper = new ProgramHelper();

            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT c.ID, c.Peril, c.Description, 0 as SubcategoryID, '' as Subcategory, p.ProgramName, c.ProgramID, c.ProductCategoryID ");
                    sql.AppendLine("FROM CoveredPerils c WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN Program p WITH(NOLOCK) ON p.ID = c.ProgramID ");

                    if (id > 0)
                        sql.AppendLine("WHERE c.ID=@ID ");
                    if (prodCategoryId > 0)
                        sql.AppendLine("WHERE ProductCategoryID=@ProductCategoryID ");
                    sql.AppendLine("ORDER BY c.ID");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);
                        if (prodCategoryId > 0)
                            cmd.Parameters.AddWithValue("@ProductCategoryID", prodCategoryId);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CoveredPeril peril = new CoveredPeril(r);
                            if (peril.ProgramID > 0)
                            {
                                List<Program> programResults = programHelper.Select(peril.ProgramID, clientCode, out errorMsg);
                                if (programResults != null && programResults.Count > 0)
                                {
                                    peril.PerilProgram = programResults[0];
                                    peril.Program = peril.PerilProgram.ProgramName;
                                }
                            }
                            if (peril.ProductCategoryID > 0)
                            {
                                List<ProductCategory> categoryResults = productCategoryHelper.Select(peril.ProductCategoryID, clientCode, out errorMsg);
                                if (categoryResults != null && categoryResults.Count > 0)
                                {
                                    peril.PerilProductCategory = categoryResults[0];
                                }
                            }
                            if (peril.Program.ToLower() == program.ToLower() || id > 0)
                                result.Add(peril);
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

        public CoveredPeril Insert(CoveredPeril data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("INSERT INTO CoveredPerils ");
                        sql.AppendLine("(Peril, ProductCategoryID, Description, ProgramID) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@Peril, @ProductCategoryID, @Description, @ProgramID) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Peril", data.Peril);
                            cmd.Parameters.AddWithValue("@ProductCategoryID", data.ProductCategoryID);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
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

        public CoveredPeril Update(CoveredPeril data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("UPDATE CoveredPerils SET Peril=@Peril, ProductCategoryID=@ProductCategoryID, Description=@Description, ProgramID=@ProgramID ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Peril", data.Peril);
                            cmd.Parameters.AddWithValue("@ProductCategoryID", data.ProductCategoryID);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
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
