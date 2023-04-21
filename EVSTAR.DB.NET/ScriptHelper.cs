using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.Collections.Generic;

namespace EVSTAR.DB.NET
{
    public class ScriptHelper
    {
        public Script Select(string name, int languageID, int clientID, string clientCode, out string errorMsg)
        {
            Script result = null;
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Scripts WITH(NOLOCK) ");
                    sql.AppendLine("WHERE ScriptName=@ScriptName AND LanguageCode=@LanguageCode AND ClientID=@ClientID ");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ScriptName", name);
                        cmd.Parameters.AddWithValue("@LanguageCode", languageID);
                        cmd.Parameters.AddWithValue("@ClientID", clientID);

                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            result = new Script(r);
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
    }
}
