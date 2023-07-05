using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;

namespace EVSTAR.Web.api
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public User Get()
        {
            User user = null;
            try
            {
                string username = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["username"]);
                string authentication = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["auth"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                return AuthenticateUser(username, authentication, clientCode);
            }
            catch (Exception ex)
            {
                user = new User()
                {
                    Error = String.Format("{0}<br />{1}...", ex.Message, ex.StackTrace.Substring(0, 250))
                };
            }
            return user;
        }

        public User AuthenticateUser(string username, string auth, string clientCode)
        {
            User user = null;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Users WITH(NOLOCK) ");
                    sql.AppendLine("WHERE UserName = @UserName AND Authentication=@Auth AND Active=1");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserName", username);
                        cmd.Parameters.AddWithValue("@Auth", auth);
                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            user = new User(r);
                        }
                        r.Close();
                    }
                    con.Close();
                }
                if (user == null)
                    user = new User()
                    {
                        Error = "NOTFOUND"
                    };
            }
            catch (Exception ex)
            {
                user = new User()
                {
                    Error = String.Format("{0}<br />{1}...", ex.Message, ex.StackTrace.Substring(0, 250))
                };
            }
            return user;
        }

        private User GetUser(int id, string clientCode)
        {
            User user = null;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Users WITH(NOLOCK) ");
                    sql.AppendLine("WHERE ID=@ID");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", id);
                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            user = new User(r);
                        }
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                user = new User()
                {
                    Error = String.Format("{0}<br />{1}...", ex.Message, ex.StackTrace.Substring(0, 250))
                };
            }
            return user;
        }

        // GET api/<controller>/id
        public User Get(int id)
        {
            User user = null;
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
            try
            {
                user = GetUser(id, clientCode);
            }
            catch (Exception ex)
            {
            }
            return user;
        }
    }
}
