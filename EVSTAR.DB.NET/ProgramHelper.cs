using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EVSTAR.DB.NET
{
    public class ProgramHelper
    {
        public List<Program> Select(int id, string clientCode, out string errorMsg)
        {
            List<Program> programs = new List<Program>();
            errorMsg= string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Program WITH(NOLOCK) ");
                    if (id > 0)
                    {
                        sql.AppendLine("WHERE ID=@ID ");
                    }

                    sql.AppendLine("ORDER BY ID");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Program program = new Program(r);
                            programs.Add(program);

                            ClientHelper ch = new ClientHelper();
                            List<Client> clients = ch.Select(program.ClientID, out errorMsg);
                            if (clients != null && clients.Count > 0)
                            {
                                program.ProgramClient = clients[0];
                            }
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return programs;
        }
    }
}

