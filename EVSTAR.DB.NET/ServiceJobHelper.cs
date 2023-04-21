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
    public class ServiceJobHelper
    {
        public List<ServiceBenchJob> Select(int id, string crmID, string serviceJobID, out string errorMsg)
        {
            List<ServiceBenchJob> result = new List<ServiceBenchJob>();
            
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM ServiceBenchJobs WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    if (!String.IsNullOrEmpty(crmID))
                        sql.AppendLine("WHERE CRM=@CRM ");

                    if (!String.IsNullOrEmpty(serviceJobID))
                        sql.AppendLine("WHERE ServiceJobID=@ServiceJobID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (!String.IsNullOrEmpty(crmID))
                            cmd.Parameters.AddWithValue("@CRM", crmID);

                        if (!String.IsNullOrEmpty(serviceJobID))
                            cmd.Parameters.AddWithValue("@ServiceJobID", serviceJobID);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ServiceBenchJob sbj = new ServiceBenchJob(r);
                            result.Add(sbj);
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

        public ServiceBenchJob Insert(ServiceBenchJob data, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO ServiceBenchJobs ");
                        sql.AppendLine("(CRM, ServiceOrderID, ServiceJobID, ServiceJobText, ServiceOrderText) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@CRM, @ServiceOrderID, @ServiceJobID, @ServiceJobText, @ServiceOrderText) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CRM", data.CRM);
                            cmd.Parameters.AddWithValue("@ServiceOrderID", data.ServiceOrderID);
                            cmd.Parameters.AddWithValue("@ServiceJobID", data.ServiceJobID);
                            cmd.Parameters.AddWithValue("@ServiceJobText", data.ServiceJobJSON);
                            cmd.Parameters.AddWithValue("@ServiceOrderText", data.ServiceOrderJSON);
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

        public ServiceBenchJob Update(ServiceBenchJob data, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE ServiceBenchJobs SET CRM=@CRM, ServiceOrderID=@ServiceOrderID, ServiceJobID=@ServiceJobID, " +
                            "ServiceJobText=@ServiceJobText, ServiceOrderText=@ServiceOrderText ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CRM", data.CRM);
                            cmd.Parameters.AddWithValue("@ServiceOrderID", data.ServiceOrderID);
                            cmd.Parameters.AddWithValue("@ServiceJobID", data.ServiceJobID);
                            cmd.Parameters.AddWithValue("@ServiceJobText", data.ServiceJobJSON);
                            cmd.Parameters.AddWithValue("@ServiceOrderText", data.ServiceOrderJSON);
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
