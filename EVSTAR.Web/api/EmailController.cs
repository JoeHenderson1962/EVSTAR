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
using EVSTAR.Web;
using System.IO;
using EVSTAR.DB.NET;

namespace EVSTAR.Web.api
{
    public class EmailController : ApiController
    {
        Random random = new Random();

        // GET api/<controller>
        public string Get()
        {
            string result = string.Empty;
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string provided = Encryption.MD5(code + address);
            return provided == hashed ? "TRUE" : "FALSE";
        }

        // POST api/<controller>
        public string Post([FromBody] string value)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["address"]);

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
                    // +"&cat=" + encodeURIComponent(subcategorySelected) + "&claim=" + encodeURIComponent(claim.Number);
                    int devID = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["dev"]);
                    string dateSel = DBHelper.GetStringValue(HttpContext.Current.Request.Params["date"]);
                    string option = DBHelper.GetStringValue(HttpContext.Current.Request.Params["option"]);
                    int catSel = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["cat"]);
                    string label = DBHelper.GetStringValue(HttpContext.Current.Request.Params["label"]);
                    int claimNum = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["claim"]);
                    string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);

                    Customer cust = LookupCustomer(address, clientCode);
                    if (cust != null)
                    {
                        ClientHelper ch = new ClientHelper();
                        string errorMsg = string.Empty;
                        List<Client> client = ch.Select(cust.ClientID, out errorMsg);
                        CoveredProduct prod = LookupProduct(devID, clientCode);
                        if (prod != null && client != null && client.Count > 0)
                        {
                            CoveredPerilHelper cph = new CoveredPerilHelper();
                            List<CoveredPeril> perils = cph.SelectFull(0, 0, string.Empty, clientCode, out errorMsg);
                            CoveredPeril peril = perils.Where(x => x.SubcategoryID == catSel).FirstOrDefault();
                            string txt = string.Empty;
                            string subject = string.Empty;
                            if (option.ToUpper() == "LOCAL")
                            {
                                txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/EVSTARClaimInitiatedLocalRepair.html"));
                                //txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/reimbursement-template.html"));
                                subject = "Your {DEVICE NAME} claim is submitted, next step, local repair";
                            }
                            else
                            {
                                //txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/mail-in-template.html"));
                                txt = System.IO.File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/EVSTARClaimInitiatedDepotRepair.html"));
                                txt = txt.Replace("{LABEL LINK}", label);
                                subject = "Your {DEVICE NAME} claim is submitted, next step, shipping";
                            }
                            subject = subject.Replace("{DEVICE NAME}", prod.Manufacturer + " " + prod.Model);
                            txt = txt.Replace("{MAKE}", prod.Manufacturer).Replace("{MODEL}", prod.Model).Replace("{SNTYPE}", prod.IMEI.Length > 0 ? "IMEI" : "");
                            txt = txt.Replace("{DEVICE NAME}", prod.Manufacturer + " " + prod.Model);
                            txt = txt.Replace("{CUSTOMER NAME}", cust.PrimaryName);
                            txt = txt.Replace("{CLAIM NUMBER}", client[0].Code.Substring(0, 1) + claimNum.ToString("D7"));
                            txt = txt.Replace("{SN}", prod.IMEI.Length > 0 ? prod.IMEI : prod.SerialNumber).Replace("{RAM}", prod.MemorySize);
                            txt = txt.Replace("{ISSUE}", peril != null ? peril.Subcategory : string.Empty); 
                            txt = txt.Replace("{DATEFILED}", dateSel);
                            txt = txt.Replace("{REPAIR LIMIT}", String.Format("{0:C}", cust.WarrantyProgram.MaxAmountPerClaim));
                            txt = txt.Replace("{PROGRAM PHONE}", client[0].ContactPhone1);
#if DEBUG
                            if (client[0].Code == "REACH")
                            {
                                txt = txt.Replace("{PROGRAM URL}", "http://testreachmobile.goevstar.com/");
                            }
                            else
                            {
                                txt = txt.Replace("{PROGRAM URL}", "http://testdobson.goevstar.com/");
                            }
#else
                            if (client[0].Code == "REACH")
                            {
                                txt = txt.Replace("{PROGRAM URL}", "https://reachmobile.goevstar.com/");
                            }
                            else
                            {
                                txt = txt.Replace("{PROGRAM URL}", "https://dobson.goevstar.com/");
                            }
#endif
                            SendEmail(address, subject, txt);
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

        private Customer LookupCustomer(string email, string clientCode)
        {
            Customer customer = null;
            string errorMsg = string.Empty;
            CustomerHelper customerHelper = new CustomerHelper();

            customer = customerHelper.Find(email, string.Empty, string.Empty, 0, 0, clientCode, out errorMsg);
            if (errorMsg != string.Empty)
                throw new Exception(errorMsg);

            return customer;
        }

        private CoveredProduct LookupProduct(int id, string clientCode)
        {
            CoveredProduct product = null;

            CoveredProductHelper helper = new CoveredProductHelper();
            string errorMsg = string.Empty;
            List<CoveredProduct> prods = helper.Select(id, clientCode, out errorMsg);
            if (errorMsg != string.Empty)
            {
                throw new Exception(errorMsg);
            }
            if (prods.Count > 0)
            {
                product = prods[0];
            }

            return product;
        }
    }
}
