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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net.Mail;
using ERPS;
using System.IO;

namespace ERPS.api
{
    public class EmailController : ApiController
    {
        Random random = new Random();

        // GET api/<controller>
        public string Get()
        {
            string result = string.Empty;
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            return provided == hashed ? "TRUE" : "FALSE";
        }

        // POST api/<controller>
        public string Post([FromBody] string value)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);

            string func = HttpContext.Current.Request.Headers["func"];
            //string auth = HttpContext.Current.Request.Headers["auth"];
            //if (!String.IsNullOrEmpty(auth))
            //{
            //    string[] values = auth.Split('|');
            //    if (values.Length > 1)
            //    {
            //        User loggedIn = Common.MobileLogin(values[0], values[1]);
            //        if (loggedIn != null && String.IsNullOrEmpty(loggedIn.ErrorMsg))
            //        {
            try
            {
                if (func.ToUpper() == "CODE")
                {
                    string code = random.Next(10000).ToString("0000");
                    result = Encryption.MD5(code + address);
                    string msgBody = string.Format("Your EVSTAR verification code is {0}.", code);
                    SendEmail(address, "EVSTAR Verification Code", msgBody);
                    return result;
                }
                else if (func.ToUpper() == "EMAIL")
                {
                    // url = "/api/email?address=" + customer.Email + "&dev=" + productSelected.ID + "&date=" + encodeURIComponent(dateSelected) + "&option=" + repairOption
                    // +"&cat=" + encodeURIComponent(subcategorySelected);
                    int devID = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["dev"]);
                    string dateSel = DBHelper.GetStringValue(HttpContext.Current.Request.Params["date"]);
                    string option = DBHelper.GetStringValue(HttpContext.Current.Request.Params["option"]);
                    int catSel = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["cat"]);
                    string label = DBHelper.GetStringValue(HttpContext.Current.Request.Params["label"]);

                    Customer cust = LookupCustomer(address);
                    if (cust != null)
                    {
                        CoveredProduct prod = LookupProduct(devID);
                        if (prod != null)
                        {
                            List<CoveredPeril> perils = GetAllPerils();
                            CoveredPeril peril = perils.Where(x => x.SubcategoryID == catSel).FirstOrDefault();
                            string txt = string.Empty;

                            if (option.ToUpper() == "LOCAL")
                            {
                                txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/reimbursement-template.html"));
                            }
                            else
                            {
                                txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/mail-in-template.html"));
                                txt = txt.Replace("{LABEL}", label);
                            }
                            txt = txt.Replace("{MAKE}", prod.Manufacturer).Replace("{MODEL}", prod.Model).Replace("{SNTYPE}", prod.IMEI.Length > 0 ? "IMEI" : "");
                            txt = txt.Replace("{SN}", prod.IMEI.Length > 0 ? prod.IMEI : prod.SerialNumber).Replace("{RAM}", prod.MemorySize);
                            txt = txt.Replace("{ISSUE}", peril != null ? peril.Subcategory : string.Empty); 
                            txt = txt.Replace("{DATEFILED}", dateSel);
                            SendEmail(address, "About your repair with Techcycle Solutions", txt);
                        }
                    }
                }
                else
                {
                    string subj = HttpContext.Current.Request.Headers["subj"];
                    if (!string.IsNullOrEmpty(subj))
                    {
                        subj = Encryption.Base64Decode(subj);
                    }
                    string body = HttpContext.Current.Request.Headers["body"];
                    if (!string.IsNullOrEmpty(body))
                    {
                        body = Encryption.Base64Decode(body);
                        SendEmail(address, subj, body);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            //        }
            //        else if (loggedIn != null)
            //            result = loggedIn.ErrorMsg;
            //    }
            //}
            return result;
        }

        private string SendEmail(string recip, string subject, string message)
        {
            string result = string.Empty;

            string smtpReplyTo = ConfigurationManager.AppSettings["EmailReplyTo"];
            string smtpReplyToName = ConfigurationManager.AppSettings["EmailReplyToName"];
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string smtpUser = ConfigurationManager.AppSettings["SMTPUser"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            string smtpSecurity = ConfigurationManager.AppSettings["SMTPSecurity"];

            try
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(recip.Trim()));
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = message.Length >= 5 && message.Substring(0, 5).Equals("<HTML", StringComparison.OrdinalIgnoreCase);
                    msg.ReplyToList.Add(new MailAddress(smtpReplyTo, smtpReplyToName));
                    msg.Sender = new MailAddress(smtpUser, smtpReplyToName);
                    msg.From = new MailAddress(smtpUser, smtpReplyToName);

                    SmtpClient client = null;
                    if (!string.IsNullOrEmpty(smtpPort))
                        client = new SmtpClient(smtpHost, Convert.ToInt32(smtpPort));
                    else
                        client = new SmtpClient(smtpHost);
                    client.EnableSsl = !String.IsNullOrEmpty(smtpSecurity);
                    if (smtpUser.Length > 0 && smtpPassword.Length > 0)
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                    }
                    else
                        client.UseDefaultCredentials = true;
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private Customer LookupCustomer(string email)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN Addresses ba WITH(NOLOCK) ON ba.ID = c.BillingAddressID ");
                sql.AppendLine("LEFT JOIN Addresses sa WITH(NOLOCK) ON sa.ID = c.ShippingAddressID ");
                sql.AppendLine("LEFT JOIN Addresses ma WITH(NOLOCK) ON ma.ID = c.MailingAddressID ");
                sql.AppendLine("WHERE c.Email = @Email");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        customer = new Customer(r);
                        if (customer.BillingAddressID > 0)
                            customer.BillingAddress = GetAddress(customer.BillingAddressID);
                        if (customer.ShippingAddressID > 0)
                            customer.ShippingAddress = GetAddress(customer.ShippingAddressID);
                        if (customer.MailingAddressID > 0)
                            customer.MailingAddress = GetAddress(customer.MailingAddressID);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }

        private Address GetAddress(int addressID)
        {
            Address address = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Addresses WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", addressID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        address = new Address(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return address;
        }

        private CoveredProduct LookupProduct(int id)
        {
            CoveredProduct product = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT cp.*, pc.CategoryName FROM CoveredProducts cp WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN ProductCategories pc WITH(NOLOCK) ON pc.ID = cp.ProductCategoryID ");
                sql.AppendLine("WHERE cp.ID=@ID ");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        product = new CoveredProduct(r);
                    }
                    r.Close();
                }
            }

            return product;
        }

        private List<CoveredPeril> GetAllPerils()
        {
            List<CoveredPeril> perils = new List<CoveredPeril>();

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.ID, c.Peril, c.Description, s.ID as SubcategoryID, s.Subcategory, p.ProgramName ");
                sql.AppendLine("FROM CoveredPerils c WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN PerilSubcategories s WITH(NOLOCK) ON s.CoveredPerilID = c.ID ");
                sql.AppendLine("LEFT JOIN Program p WITH(NOLOCK) ON p.ID = c.ProgramID ");
                sql.AppendLine("ORDER BY s.ID ");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        CoveredPeril cat = new CoveredPeril(r);
                        perils.Add(cat);
                    }
                    r.Close();
                }
            }

            return perils;
        }
    }
}
