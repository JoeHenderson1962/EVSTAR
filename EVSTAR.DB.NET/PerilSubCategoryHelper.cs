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
    public class PerilSubCategoryHelper
    {
        public List<PerilSubCategory> Select(int id, string clientCode, out string errorMsg)
        {
            CoveredPerilHelper coveredPerilHelper = new CoveredPerilHelper();
            List<PerilSubCategory> result = new List<PerilSubCategory>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM PerilSubCategories WITH(NOLOCK) ");
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
                            PerilSubCategory data = new PerilSubCategory(r);
                            if (data.CoveredPerilID > 0)
                            {
                                List<CoveredPeril> perilResults = coveredPerilHelper.Select(data.CoveredPerilID, 0, string.Empty, clientCode, out errorMsg);
                                if (perilResults != null && perilResults.Count > 0)
                                {
                                    data.ParentCoveredPeril = perilResults[0];
                                }
                            }
                            result.Add(data);
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

        public PerilSubCategory Insert(PerilSubCategory data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("INSERT INTO PerilSubCategories ");
                        sql.AppendLine("(CoveredPerilID, Subcategory) ");
                        sql.AppendLine("VALUES (@CoveredPerilID, @Subcategory); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CoveredPerilID", data.CoveredPerilID);
                            cmd.Parameters.AddWithValue("@Subcategory", data.Subcategory);
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

        public PerilSubCategory Update(PerilSubCategory data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("UPDATE PerilSubCategories SET CoveredPerilID=@CoveredPerilID, Subcategory=@Subcategory ");
                        sql.AppendLine("WHERE ID=@ID");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CoveredPerilID", data.CoveredPerilID);
                            cmd.Parameters.AddWithValue("@Subcategory", data.Subcategory);
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
