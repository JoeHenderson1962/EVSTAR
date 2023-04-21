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

namespace EVSTAR.Web.api
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
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
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
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
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

        // POST api/<controller>
        //public Claim Post([FromBody] Claim value)
        //{
        //    Claim claim = null;
        //    try
        //    {
        //        string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
        //        string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
        //        string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
        //        string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
        //        string provided = Encryption.MD5(code + email);
        //        if (hashed != provided)
        //        {
        //            provided = Encryption.MD5(code + phone);
        //            if (hashed != provided)
        //                return null;
        //        }

        //        claim = value; // (Address)JsonConvert.DeserializeObject(value);  
        //        if (claim != null)
        //        {
        //            if (claim.StatusHistory == null)
        //            {
        //                claim.StatusHistory = new List<ClaimStatusHistory>();
        //            }

        //            if (claim.StatusHistory.Count == 0)
        //            {
        //                claim.StatusHistory.Add(new ClaimStatusHistory()
        //                {
        //                    StatusID = 1,
        //                    StatusDate = DateTime.Now,
        //                    UserName = !String.IsNullOrEmpty(email) ? email : phone,
        //                    Status = "OPEN"
        //                });
        //            }

        //            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
        //            using (SqlConnection con = new SqlConnection(constr))
        //            {
        //                con.Open();
        //                StringBuilder sql = new StringBuilder();
        //                sql.AppendLine("INSERT INTO Claims ");
        //                sql.AppendLine("(CustomerID, CoveredProductID, LocalRepair, AddressID, DateSubmitted, DateCompleted, DateReturnedToCustomer, DateReceivedAtTCS, " +
        //                    "CoveredPerilID, PasscodeDisabled, PassCode, RepairVendorID, InboundTrackingNumber, OutboundTrackingNumber, PerilSubcategoryID, DenialReason, " +
        //                    "DateDenied, RepairShoprTicketID, LastUpdated) ");
        //                sql.AppendLine("VALUES (@CustomerID, @CoveredProductID, @LocalRepair, @AddressID, @DateSubmitted, @DateCompleted, @DateReturnedToCustomer, @DateReceivedAtTCS, " +
        //                    "@CoveredPerilID, @PasscodeDisabled, @PassCode, @RepairVendorID, @InboundTrackingNumber, @OutboundTrackingNumber, @PerilSubcategoryID, @DenialReason, " +
        //                    "@DateDenied, @RepairShoprTicketID, @LastUpdated); ");
        //                sql.AppendLine("SELECT SCOPE_IDENTITY() ");
        //                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
        //                {
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.Parameters.AddWithValue("@CustomerID", claim.CustomerID);
        //                    cmd.Parameters.AddWithValue("@CoveredPerilID", claim.CoveredPerilID);
        //                    cmd.Parameters.AddWithValue("@CoveredProductID", claim.CoveredProductID);
        //                    if (claim.AddressID > 0)
        //                        cmd.Parameters.AddWithValue("@AddressID", claim.AddressID);
        //                    else
        //                        cmd.Parameters.AddWithValue("@AddressID", DBNull.Value);
        //                    cmd.Parameters.AddWithValue("@DateCompleted", claim.DateCompleted);
        //                    cmd.Parameters.AddWithValue("@DateReceivedAtTCS", claim.DateReceivedAtTCS);
        //                    cmd.Parameters.AddWithValue("@DateReturnedToCustomer", claim.DateReturnedToCustomer);
        //                    cmd.Parameters.AddWithValue("@DateSubmitted", claim.DateSubmitted);
        //                    cmd.Parameters.AddWithValue("@InboundTrackingNumber", claim.InboundTrackingNumber);
        //                    cmd.Parameters.AddWithValue("@LocalRepair", claim.LocalRepair);
        //                    cmd.Parameters.AddWithValue("@OutboundTrackingNumber", claim.OutboundTrackingNumber);
        //                    cmd.Parameters.AddWithValue("@PassCode", claim.PassCode);
        //                    cmd.Parameters.AddWithValue("@PasscodeDisabled", claim.PassCodeDisabled);
        //                    cmd.Parameters.AddWithValue("@RepairVendorID", claim.RepairVendorID);
        //                    cmd.Parameters.AddWithValue("@PerilSubcategoryID", claim.PerilSubcategoryID);
        //                    cmd.Parameters.AddWithValue("@DenialReason", claim.DenialReason);
        //                    cmd.Parameters.AddWithValue("@DateDenied", claim.DateDenied);
        //                    cmd.Parameters.AddWithValue("@RepairShoprTicketID", claim.RepairShoprTicketID);
        //                    cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
        //                    claim.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
        //                    claim.StatusHistory[claim.StatusHistory.Count - 1].ClaimID = claim.ID;
        //                    claim.StatusHistory[claim.StatusHistory.Count - 1] = SaveClaimStatus(claim.StatusHistory[claim.StatusHistory.Count - 1]);
        //                }
        //                con.Close();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        claim.DenialReason = ex.Message + "\r\n" + ex.StackTrace;
        //        return claim;
        //    }
        //    return claim;
        //}
    }
}
