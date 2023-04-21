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
using static System.Collections.Specialized.BitVector32;

namespace EVSTAR.DB.NET
{
    public class CallActionHelper
    {
        public List<CallAction> Select(int id, out string errorMsg)
        {
            List<CallAction> result = new List<CallAction>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Actions WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE id=@ID ");

                    sql.AppendLine("ORDER BY SortOrder");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CallAction action = new CallAction(r);
                            result.Add(action);
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

        public CallAction Insert(CallAction action, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (action != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Actions ");
                        sql.AppendLine("(ClientID, ActionName, VisibleExisting, VisibleNonExisting, Description) ");
                        sql.AppendLine("VALUES (@ClientID, @ActionName, @VisibleExisting, @VisibleNonExisting, @Description); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", action.ClientID);
                            cmd.Parameters.AddWithValue("@ActionName", action.ActionName);
                            cmd.Parameters.AddWithValue("@VisibleExisting", action.VisibleExisting);
                            cmd.Parameters.AddWithValue("@VisibleNonExisting", action.VisibleNonExisting);
                            cmd.Parameters.AddWithValue("@Description", action.Description);
                            action.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return action;
        }

        public CallAction Update(CallAction action, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (action != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Actions SET ClientID=@ClientID, ActionName=@ActionName, VisibleExisting=@VisibleExisting, ");
                        sql.AppendLine("VisibleNonExisting=@VisibleNonExisting, Description=@Description ");
                        sql.AppendLine("WHERE ID=@ID");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", action.ClientID);
                            cmd.Parameters.AddWithValue("@ActionName", action.ActionName);
                            cmd.Parameters.AddWithValue("@VisibleExisting", action.VisibleExisting);
                            cmd.Parameters.AddWithValue("@VisibleNonExisting", action.VisibleNonExisting);
                            cmd.Parameters.AddWithValue("@Description", action.Description);
                            cmd.Parameters.AddWithValue("@ID", action.ID);
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
            return action;
        }
    }
}
