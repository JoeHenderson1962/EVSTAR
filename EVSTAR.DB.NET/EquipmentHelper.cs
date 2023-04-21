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
    public class EquipmentHelper
    {
        public List<Equipment> Select(int id, int clientID, string clientCode, out string errorMsg)
        {
            List<Equipment> result = new List<Equipment>();
            
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Equipment WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    if (clientID > 0)
                        sql.AppendLine("WHERE ClientID=@ClientID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (clientID > 0)
                            cmd.Parameters.AddWithValue("@ClientID", clientID);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Equipment equipment = new Equipment(r);
                            result.Add(equipment);
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

        public Equipment Insert(Equipment data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("INSERT INTO Equipment ");
                        sql.AppendLine("(ClientID, Make, Model, Cost, ReturnValue, ParentID, Description, ImageFile, SpecificationsFile, StartDate, EndDate, SKU, Deductible) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@ClientID, @Make, @Model, @Cost, @ReturnValue, @ParentID, @Description, @ImageFile, @SpecificationsFile, @StartDate, @EndDate, @SKU, @Deductible) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@Make", data.Make);
                            cmd.Parameters.AddWithValue("@Model", data.Model);
                            cmd.Parameters.AddWithValue("@Cost", data.Cost);
                            cmd.Parameters.AddWithValue("@ReturnValue", data.ReturnValue);
                            cmd.Parameters.AddWithValue("@ParentID", data.ParentID);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ImageFile", data.ImageFile);
                            cmd.Parameters.AddWithValue("@SpecificationsFile", data.SpecificationsFile);
                            if (data.StartDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@StartDate", data.StartDate);
                            else
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);

                            if (data.EndDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EndDate", data.EndDate);
                            else
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@SKU", data.SKU);
                            cmd.Parameters.AddWithValue("@Deductible", data.Deductible);
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

        public Equipment Update(Equipment data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("UPDATE Equipment SET ClientID=@ClientID, Make=@Make, Model=@Model, Cost=@Cost, ");
                        sql.AppendLine("ReturnValue=@ReturnValue, ParentID=@ParentID, Description=@Description, ImageFile=@ImageFile, ");
                        sql.AppendLine("SpecificationsFile=@SpecificationsFile, StartDate=@StartDate, EndDate=@EndDate, SKU=@SKU, Deductible=@Deductible ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@Make", data.Make);
                            cmd.Parameters.AddWithValue("@Model", data.Model);
                            cmd.Parameters.AddWithValue("@Cost", data.Cost);
                            cmd.Parameters.AddWithValue("@ReturnValue", data.ReturnValue);
                            cmd.Parameters.AddWithValue("@ParentID", data.ParentID);
                            cmd.Parameters.AddWithValue("@Description", data.Description);
                            cmd.Parameters.AddWithValue("@ImageFile", data.ImageFile);
                            cmd.Parameters.AddWithValue("@SpecificationsFile", data.SpecificationsFile);
                            if (data.StartDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@StartDate", data.StartDate);
                            else
                                cmd.Parameters.AddWithValue("@StartDate", DBNull.Value);

                            if (data.EndDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EndDate", data.EndDate);
                            else
                                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@SKU", data.SKU);
                            cmd.Parameters.AddWithValue("@Deductible", data.Deductible);
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
