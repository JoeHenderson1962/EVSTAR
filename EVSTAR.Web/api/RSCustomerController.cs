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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EVSTAR.Models.FedEx;
using EVSTAR.Models;

namespace EVSTAR.Web.api
{
    public class RSCustomerController : ApiController
    {
        //List<Customer> allCustomers = new List<Customer>();

        // GET api/<controller>
        public List<TCModels.CustomerAutoComplete> Get()
        {
            string query = DBHelper.GetStringValue(HttpContext.Current.Request.Params["query"]);
            List<TCModels.CustomerAutoComplete> result = new List<TCModels.CustomerAutoComplete>();
            result = GetCustomerAutoComplete("https://techcyclesolutions.repairshopr.com/api/v1/customers/autocomplete?query=" + query);
            return result;
        }

        // GET api/<controller>
        public TCModels.Customer Get(int id)
        {
            TCModels.Customer result = new TCModels.Customer();
            return result;
        }

        // POST api/<controller>
        public async Task<TCModels.RSCustomer> Post([FromBody] TCModels.RSCustomer customer)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = "https://techcyclesolutions.repairshopr.com/api/v1/customers/";
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                if (result.ToLower().Contains("\"message\":"))
                {
                    customer.notes = result;
                }
            }
            catch (Exception ex)
            {
                string result = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return customer;
        }

        // PUT api/<controller>
        public string Put([FromBody] TCModels.RSCustomer customer)
        {
            //Customer customer = JsonConvert.DeserializeObject<Customer>(cust);
            var t = Task.Run(() => PutCustomer(customer, (int)customer.id));
            t.Wait();
            string result = t.Result;
            return result;
        }

        public async Task<string> PutCustomer(TCModels.RSCustomer customer, int id)
        {
            string result = string.Empty;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers/{0}", id);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PutAsync(uri, httpContent);
                result = await httpResponseMessage.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public TCModels.SingleCustomer GetCustomerName(int id)
        {
            TCModels.SingleCustomer cust = GetCustomer(String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers/{0}", id));
            //if (cust != null)
            //    customerName = (!String.IsNullOrEmpty(cust.customer.business_then_name) ? cust.customer.business_then_name : 
            //        !String.IsNullOrEmpty(cust.customer.business_and_full_name) ? cust.customer.business_and_full_name : cust.customer.business_name) 
            //        + "||" + cust.customer.id.ToString();
            return cust;
        }

        public TCModels.PortalUsers GetPortalUsers(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            TCModels.PortalUsers user = JsonConvert.DeserializeObject<TCModels.PortalUsers>(response);
            return user;
        }

        public TCModels.SingleCustomer GetCustomer(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            TCModels.SingleCustomer customer = JsonConvert.DeserializeObject<TCModels.SingleCustomer>(response);
            return customer;
        }

        public async Task<TCModels.RSCustomer> GetCustomerByEmail(string email)
        {
            RSCustomers custs = null;
            TCModels.RSCustomer cust = new TCModels.RSCustomer();
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers/?email={0}", email);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.GetAsync(uri);
                string result = await httpResponseMessage.Content.ReadAsStringAsync();
                custs = JsonConvert.DeserializeObject<TCModels.RSCustomers>(result);
                if (custs != null && custs.customers.Count > 0)
                {
                    cust = custs.customers[0];
                }
            }
            catch (Exception ex)
            {
                cust.id = 0;
                cust.fullname = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return cust;
        }

        public List<TCModels.CustomerAutoComplete> GetCustomerAutoComplete(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            TCModels.RSCustomers customers = JsonConvert.DeserializeObject<TCModels.RSCustomers>(response);
            List<TCModels.CustomerAutoComplete> cac = new List<TCModels.CustomerAutoComplete>();
            foreach (TCModels.RSCustomer customer in customers.customers)
            {
                cac.Add(new TCModels.CustomerAutoComplete() { business_then_name = customer.business_then_name, id = customer.id });
            }
            return cac;
        }

    }
}
