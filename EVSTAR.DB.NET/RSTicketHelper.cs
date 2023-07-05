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
    public class RSTicketHelper
    {
        public Tickets Select(long id, string clientCode)
        {
            Tickets tickets = new Tickets();
            tickets.tickets = new List<Ticket>();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Ticket WITH(NOLOCK) ");
                    if (id > 0)
                    {
                        sql.AppendLine("WHERE id=@ID ");
                    }
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Ticket ticket = new Ticket(r);
                            RSCommentHelper commentHelper = new RSCommentHelper();
                            ticket.comments = commentHelper.Select(ticket.id, clientCode);
                            tickets.tickets.Add(ticket);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                tickets.error = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return tickets;
        }
    }
}

