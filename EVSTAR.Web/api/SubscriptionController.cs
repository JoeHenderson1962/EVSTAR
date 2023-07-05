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
using TCModels = EVSTAR.Models;
using EVSTAR.DB;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using EVSTAR.DB.NET;
using EVSTAR.Models;

namespace EVSTAR.Web.api.reach
{
    public class SubscriptionController : ApiController
    {
        public SubscriptionResponse Post([FromBody] TCModels.Subscription subscription)
        {
            SubscriptionResponse result = new SubscriptionResponse();
            int id = 0;
            try
            {
                string authorization = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["Authorization"]);
                string[] parts = authorization.Split(' ');
                if (parts.Length == 2)
                {
                    string token = parts[1].Trim();
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] info = decodedString.Split(':');
                    if (info.Length == 2)
                    {
                        string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                        UserController uc = new UserController();
                        User user = uc.AuthenticateUser(info[0], info[1], clientCode);
                        if (user != null)
                        {
                            if (subscription != null)
                            {
                                SubscriptionHelper sh = new SubscriptionHelper();
                                string error = string.Empty;
                                id = sh.Insert(subscription, out error);
                                if (!string.IsNullOrEmpty(error))
                                {
                                    result.status = "Error saving to database.\r\n" + error;
                                    result.id = id;
                                    HttpContext.Current.Response.StatusCode = 500;
                                }
                                else
                                {
                                    result.status = "Success";
                                    result.id = id;
                                }
                            }
                            else
                            {
                                result.status = "Invalid or null subscription";
                                result.id = id;
                                HttpContext.Current.Response.StatusCode = 500;
                            }
                        }
                        else
                        {
                            result.status = "Authentication error.";
                            result.id = 0;
                            HttpContext.Current.Response.StatusCode = 403;
                        }
                    }
                    else
                    {
                        result.status = "Authentication error.";
                        result.id = 0;
                        HttpContext.Current.Response.StatusCode = 403;
                    }
                }
                else
                {
                    result.status = "Authentication error.";
                    result.id = 0;
                    HttpContext.Current.Response.StatusCode = 403;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                //string message = ex.Message + "\r\n" + ex.StackTrace + "\r\n" +
                //    ex.InnerException != null ? ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace : String.Empty;
                TCModels.DBHelper.LogMessage(message);
                result.status = message;
                result.id = id;
                HttpContext.Current.Response.StatusCode = 500;
            }

            TCModels.DBHelper.LogMessage(JsonConvert.SerializeObject(result));
            return result;
        }
    }
}