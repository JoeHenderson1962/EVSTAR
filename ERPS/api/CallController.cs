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
using EVSTAR.DB.NET;
using System.Runtime.InteropServices.ComTypes;
using System.Net.Http;

namespace ERPS.api
{
    public class CallController : ApiController
    {
        //string APIToken = ConfigurationManager.AppSettings["APIToken"];
        CallHelper callHelper = new CallHelper();

        // GET api/<controller>
        public List<Call> Get()
        {
            List<Call> calls = null;
            try
            {
                //UserHelper userHelper = new UserHelper();
                string call = DBHelper.GetStringValue(HttpContext.Current.Request.Params["call"]);
                string customer = DBHelper.GetStringValue(HttpContext.Current.Request.Params["customer"]);
                string user = DBHelper.GetStringValue(HttpContext.Current.Request.Params["user"]);
                string start = DBHelper.GetStringValue(HttpContext.Current.Request.Params["start"]);
                string end = DBHelper.GetStringValue(HttpContext.Current.Request.Params["end"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
                //string token = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["Authorization"]);

                int callID = 0;
                Int32.TryParse(call, out callID);

                int customerID = 0;
                Int32.TryParse(customer, out customerID);

                int userID = 0;
                Int32.TryParse(user, out userID);

                DateTime startDate = DateTime.Now.AddDays(-60);
                DateTime endDate = DateTime.Now.AddDays(1);

                if (!DateTime.TryParse(start, out startDate))
                    startDate = DateTime.Now.AddDays(-60);

                if (!DateTime.TryParse(start, out endDate))
                    endDate = DateTime.Now.AddDays(1);

                string errorMsg = string.Empty;
                calls = callHelper.Select(callID, customerID, userID, startDate, endDate, clientCode, out errorMsg);
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.Response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return calls;
        }

        // POST api/<controller>
        public Call Post([FromBody] Call call)
        {
            try
            {
                //string data = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["data"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                //call = JsonConvert.DeserializeObject<Call>(data);
                string errorMsg;
                call = callHelper.Insert(call, clientCode, out errorMsg);
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.Response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return call;
        }

        // Put api/<controller>
        public Call Put([FromBody] Call call)
        {
            ClaimHelper claimHelper = new ClaimHelper();
            try
            {
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                //string data = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["data"]);
                //call = JsonConvert.DeserializeObject<Call>(data);
                string errorMsg;

                if (call.CallClaim == null)
                    call.CallClaim = new Claim();

                if (call.CallClaim.CoverageID == 0 && call.CurrCoverage != null)
                {
                    call.CallClaim.CoverageID = call.CurrCoverage.ID;
                    call.CallClaim.CoveredProductID = call.CurrCoverage.CoveredProductID;
                    call.CallClaim.ClaimCoverage = call.CurrCoverage;
                }
                call.CallClaim.UserName = call.CallUser.UserName;

                call.CallClaim.CustomerID = call.CustomerID;
                if (call.CallCustomer != null)
                    call.CallClaim.AddressID = call.CallCustomer.BillingAddressID;

                if (call.CallClaim != null && call.CallClaim.ID == 0 && call.CallClaim.CustomerID > 0 && call.CallClaim.CoveredProductID > 0)
                {

                    Claim newClaim = claimHelper.Insert(call.CallClaim, clientCode, out errorMsg);
                    if (newClaim != null)
                    {
                        call.ClaimID = newClaim.ID;
                        call.CallClaim = newClaim;
                    }
                }
                else if (call.CallClaim != null && call.CallClaim.ID > 0 && call.CallClaim.CustomerID > 0 && call.CallClaim.CoveredProductID > 0)
                {
                    call.CallClaim.CustomerID = call.CustomerID;
                    if (call.CallCustomer != null)
                        call.CallClaim.AddressID = call.CallCustomer.BillingAddressID;
                    if (call.CallClaim.StatusHistory == null)
                        call.CallClaim.StatusHistory = new List<ClaimStatusHistory>();
                    if (call.CallClaim.StatusHistory.Count == 0)
                    {
                        call.CallClaim.StatusHistory.Add(new ClaimStatusHistory()
                        {
                            ClaimID = call.CallClaim.ID,
                            StatusID = 1,
                            Status = "OPEN",
                            StatusDate = DateTime.Now,
                            UserName = call.CallUser.UserName
                        });
                    };
                    call.CallClaim = claimHelper.Update(call.CallClaim, clientCode, out errorMsg);
                }
                call = callHelper.Update(call, clientCode, out errorMsg);
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.Response.StatusCode = 500;
                    HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                    response.ReasonPhrase = errorMsg;
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message + "\r\n" + ex.StackTrace;
                throw new HttpResponseException(response);
            }
            return call;
        }

    }
}
