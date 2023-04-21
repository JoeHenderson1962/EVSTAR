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
    public class ClaimStatusHistoryHelper
    {
        public List<ClaimStatusHistory> Select(int id, int claimID, string clientCode, out string errorMsg)
        {
            List<ClaimStatusHistory> result = new List<ClaimStatusHistory>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT h.*, s.Name as StatusName FROM ClaimStatusHistory h WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN ClaimStatuses s WITH(NOLOCK) ON s.ID = h.StatusID ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");
                    if (claimID > 0)
                        sql.AppendLine("WHERE ClaimID=@ClaimID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (claimID > 0)
                            cmd.Parameters.AddWithValue("@ClaimID", claimID);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ClaimStatusHistory res = new ClaimStatusHistory(r);
                            result.Add(res);
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

        public ClaimStatusHistory Insert(ClaimStatusHistory data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("INSERT INTO ClaimStatusHistory ");
                        sql.AppendLine("(ClaimID, StatusID, StatusDate, UserName) ");
                        sql.AppendLine("VALUES (@ClaimID, @StatusID, @StatusDate, @UserName); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClaimID", data.ClaimID);
                            cmd.Parameters.AddWithValue("@StatusID", data.StatusID);
                            cmd.Parameters.AddWithValue("@StatusDate", data.StatusDate);
                            cmd.Parameters.AddWithValue("@UserName", data.UserName);

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
    }
}
