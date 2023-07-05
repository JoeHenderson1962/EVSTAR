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

namespace EVSTAR.Web.api
{
    public class SmsController : ApiController
    {
        Random random = new Random();

        // GET api/<controller>
        public string Get()
        {
            string result = string.Empty;
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["hashed"]);
            string provided = Encryption.MD5(code + phone);
            return provided == hashed ? "TRUE" : "FALSE";
        }

        // POST api/<controller>
        public string Post([FromBody] string value)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]);

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
                string code = random.Next(10000).ToString("0000");
                result = Encryption.MD5(code + phone);
                string msgBody = string.Format("Your EVSTAR verification code is {0}.", code);
                NotifyViaTwilio(msgBody, phone);
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

        private string NotifyViaTwilio(string msgBody, string number)
        {
            string result = string.Empty;
            try
            {
                // send messages
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSID"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                string twilioMobile = ConfigurationManager.AppSettings["TwilioMobileNumber"];

                if (!String.IsNullOrEmpty(twilioMobile) && !String.IsNullOrEmpty(accountSid) && !String.IsNullOrEmpty(authToken) && !String.IsNullOrEmpty(number))
                {
                    TwilioClient.Init(accountSid, authToken);
                    string smsNo = number;
                    if (smsNo.Length < 11)
                        smsNo = '1' + smsNo;
                    if (smsNo.Substring(0, 1) != "+")
                        smsNo = "+" + smsNo;
                    var message = MessageResource.Create(
                        body: msgBody,
                        from: new Twilio.Types.PhoneNumber(twilioMobile),
                        to: new Twilio.Types.PhoneNumber(smsNo)
                        );
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
