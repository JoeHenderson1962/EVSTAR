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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ERPS.api
{
    public class RSAssetController : ApiController
    {

        // GET api/<controller>
        public List<Asset> Get()
        {
            List<Asset> result = new List<Asset>();
            string custid = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["custid"]);
            var t = Task.Run(() => GetAssets(custid));
            t.Wait();
            string res = t.Result;
            if (!res.Contains("ERROR:"))
            {
                try
                {
                    Assets response = JsonConvert.DeserializeObject<Assets>(res);
                    if (response != null)
                    {
                        foreach (Asset asset in response.assets)
                        {
                            result.Add(asset);
                        }

                        int page = response.meta.page;
                        int total_pages = response.meta.total_pages;
                        while (page < total_pages)
                        {
                            t = Task.Run(() => GetAssets(custid));
                            t.Wait();
                            res = t.Result;
                            if (!res.Contains("ERROR:"))
                            {
                                response = JsonConvert.DeserializeObject<Assets>(res);
                                if (response != null)
                                {
                                    foreach (Asset asset in response.assets)
                                    {
                                        result.Add(asset);
                                    }

                                    page = response.meta.page;
                                }
                            }
                        }
                    }
                }
                catch 
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        // POST api/<controller>
        public Asset Post()
        {
            Asset cust_asset = null;
            string asset = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["asset"]);
            string custid = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["custid"]);
            string serialno = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["serialno"]);
            var t = Task.Run(() => PostAsset(asset));
            t.Wait();
            string result = t.Result;
            if (result.Contains("\"success\":false"))
            {
                RepairShoprFailureResponse err = JsonConvert.DeserializeObject<RepairShoprFailureResponse>(result);
                if (err.message[0] == "Asset serial must not be blank")
                {
                    err.message[0] += " or too short.";
                }
                else if (err.message[0] == "Asset serial has already been taken")
                {
                    t = Task.Run(() => GetAsset(custid, serialno));
                    t.Wait();
                    result = t.Result;
                    if (!result.Contains("\"success\":false"))
                    {
                        Assets cust_assets = JsonConvert.DeserializeObject<Assets>(result);
                        if (cust_assets != null && cust_assets.assets.Count > 0)
                        {
                            foreach (var casset in cust_assets.assets)
                            {
                                if (casset.asset_serial.ToUpper() == serialno.ToUpper())
                                {
                                    cust_asset = casset;
                                    break;
                                }
                            }
                            //err.message[0] = String.Format("Using asset already in RepairShopr: {0}", cust_asset.name);
                            return cust_asset;
                        }
                        else
                        {
                            err.message[0] += ". Unable to retrieve existing asset information:  " + result;
                        }
                    }
                }

                return new Asset()
                {
                    id = 0,
                    name = "ERROR: " + err.message[0]
                };
            }
            else
            {
                result = result.Replace("{\"asset\":", "");
                result = result.Substring(0, result.Length - 1);
                cust_asset = JsonConvert.DeserializeObject<Asset>(result);
            }
            return cust_asset;
        }

        public async Task<string> PostAsset(string asset)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(asset, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = "https://techcyclesolutions.repairshopr.com/api/v1/customer_assets";
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
                resp = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return resp;
        }

        public async Task<string> GetAsset(string custid, string serialno)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customer_assets?customer_id={0}&query={1}", custid, serialno);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.GetAsync(uri);
                resp = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return resp;
        }

        public async Task<string> GetAssets(string custid)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customer_assets?customer_id={0}", custid);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.GetAsync(uri);
                resp = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return resp;
        }
    }
}
