using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace ERPS.api
{
    public class UspsController : ApiController
    {
        // GET api/<controller>
        public async Task<Address> Get()
        {
            Address addr = new Address();
            try
            {
                string uspsUser = ConfigurationManager.AppSettings["USPSUser"];
                string uspsPassword = ConfigurationManager.AppSettings["USPSPassword"];

                string postalCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["postalCode"]);
                string street = DBHelper.GetStringValue(HttpContext.Current.Request.Params["street"]);

                StringBuilder xml = new StringBuilder();
                xml.Append(String.Format("<CityStateLookupRequest USERID=\"{0}\">", uspsUser));
                //xml.Append("<Revision>1</Revision>");
                xml.Append("<ZipCode ID=\"0\">");
                xml.Append(String.Format("<Zip5>{0}</Zip5>", postalCode.Substring(0, 5)));
                xml.Append("</ZipCode>");
                xml.Append("</CityStateLookupRequest>");
                string resp = String.Empty;

                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    string apiHost = "https://secure.shippingapis.com/shippingapi.dll?API=CityStateLookup&XML=" + xml.ToString();
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (resp.ToUpper().Contains("<ERROR>"))
                    {
                        addr.Error = resp;
                    }
                    else
                    {
                        string[] tempArray = resp.Split('>');
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            if (tempArray[i].ToUpper().Contains("</STATE"))
                            {
                                addr.State = tempArray[i].Replace("</State", "");
                            }
                            if (tempArray[i].ToUpper().Contains("</CITY"))
                            {
                                addr.City = tempArray[i].Replace("</City", "");
                            }
                            if (tempArray[i].ToUpper().Contains("</ZIP5"))
                            {
                                addr.City = tempArray[i].Replace("</Zip5", "");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    addr.Error = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                addr.Error = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return addr;
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
                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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
