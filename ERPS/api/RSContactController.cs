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
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERPS.api
{
    public class RSContactController : ApiController
    {
        //List<Customer> allCustomers = new List<Customer>();

        // GET api/<controller>
        public List<string> Get()
        {
            string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["email"]);

            //if (allCustomers.Count == 0)
            //    GetCustomers("https://techcyclesolutions.repairshopr.com/api/v1/customers");

            //List<string> result = new List<string>();
            //result = GetCustomerNames(email);
            //return result;
            throw new NotImplementedException();
        }

        // POST api/<controller>
        public string Post(/*[FromBody] string pre*/)
        {

            string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["email"]);
            string password = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["code"]);
            string result = "FALSE";

            if (email.Length > 0)
            {
                //if (allCustomers.Count == 0)

                PortalUsers users = GetPortalUsers("https://techcyclesolutions.repairshopr.com/api/v1/portal_users?email=" + email);

                if (users.portal_users.Count > 0)
                {
                    SingleCustomer cust = GetCustomerName(users.portal_users[0].customer_id);
                    if (cust != null && cust.customer != null && cust.customer.properties != null)
                    {
                        if (cust.customer.contacts != null && cust.customer.contacts.Count > 0)
                        {
                            foreach (Contact contact in cust.customer.contacts)
                            {
                                if (contact.email.ToUpper() == email.ToUpper() && contact.properties != null && contact.properties.PortalPWToTCS.Equals(password))
                                    result = JsonConvert.SerializeObject(cust) + "||" +
                                        contact.id.ToString() + "||" + users.portal_users[0].id;
                            }
                        }
                        else if (cust.customer.properties.PortalPWToTCS.Equals(password))
                            result = JsonConvert.SerializeObject(cust) + "||" + 
                                (cust.customer.contacts.Count > 0 ? cust.customer.contacts[0].id.ToString() : "null") + "||" + 
                                users.portal_users[0].id;
                    }
                }
            }
            return result;
        }

        // PUT api/<controller>
        public string Put([FromBody] Contact contact)
        {
            //Customer customer = JsonConvert.DeserializeObject<Customer>(cust);
            var t = Task.Run(() => PutContact(contact, (int)contact.id));
            t.Wait();
            string result = t.Result;
            return result;
        }

        public async Task<string> PutContact(Contact contact, int id)
        {
            string result = string.Empty;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(contact), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/contacts/{0}", id);
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

        public SingleCustomer GetCustomerName(int id)
        {
            SingleCustomer cust = GetCustomer(String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers/{0}", id));
            //if (cust != null)
            //    customerName = (!String.IsNullOrEmpty(cust.customer.business_then_name) ? cust.customer.business_then_name : 
            //        !String.IsNullOrEmpty(cust.customer.business_and_full_name) ? cust.customer.business_and_full_name : cust.customer.business_name) 
            //        + "||" + cust.customer.id.ToString();
            return cust;
        }

        public PortalUsers GetPortalUsers(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            PortalUsers user = JsonConvert.DeserializeObject<PortalUsers>(response);
            return user;
        }

        public SingleCustomer GetCustomer(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            SingleCustomer customer = JsonConvert.DeserializeObject<SingleCustomer>(response);
            return customer;
        }

    }
}
