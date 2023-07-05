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
    public class RSCommentHelper
    {
        public List<Comment> Select(long ticket_id, string clientCode)
        {
            List<Comment> comments = new List<Comment>();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM TicketComments WITH(NOLOCK) ");
                    if (ticket_id > 0)
                    {
                        sql.AppendLine("WHERE ticket_id=@ID ");
                    }

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (ticket_id > 0)
                            cmd.Parameters.AddWithValue("@ID", ticket_id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Comment comment = new Comment(r);
                            comments.Add(comment);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Comment comment = new Comment();
                comment.subject = "DB ERROR";
                comment.body = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
                comment.hidden = true;
                comments.Add(comment);
            }
            return comments;
        }
    }
}

