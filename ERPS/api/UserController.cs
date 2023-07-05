using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;
using EVSTAR.DB.NET;
using System.Reflection.Emit;

namespace ERPS.api
{
    public class UserController : ApiController
    {
        public UserHelper userHelper = new UserHelper();

        // GET api/<controller>
        public User Get()
        {
            User user = null;
            try
            {
                string errorMsg = string.Empty;   
                string username = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["username"]);
                string authentication = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["auth"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                List<User> users = userHelper.Select(username, authentication, 0, clientCode, out errorMsg);
                if (users != null && users.Count > 0)
                {
                    user = users[0];
                }
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    if (user == null)
                    {
                        user = new User();
                    }
                    user.Error = errorMsg;
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

        private User AuthenticateUser(string username, string auth)
        {
            User user = null;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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

        private User GetUser(int id)
        {
            User user = null;
            try
            {
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                string errorMsg = string.Empty;
                List<User> users = userHelper.Select(string.Empty, string.Empty, id, clientCode, out errorMsg);
                if (users != null && users.Count > 0)
                {
                    user = users[0];
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
            try
            {
                user = GetUser(id);
            }
            catch (Exception ex)
            {
            }
            return user;
        }
    }
}
