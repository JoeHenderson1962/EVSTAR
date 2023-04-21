using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace EVSTAR.RepairShopr.API
{
    public class CustomersManager
    {
        private string RSToken = DBHelper.GetStringValue(ConfigurationManager.AppSettings["RepairShopr"]);

        public List<CustomerAutoComplete> Query(string query)
        {
            List<CustomerAutoComplete> result = new List<CustomerAutoComplete>();
            result = GetCustomerAutoComplete("https://techcyclesolutions.repairshopr.com/api/v1/customers/autocomplete?query=" + query);
            return result;
        }

        public string Validate(string email, string password)
        {

            string result = "FALSE";

            if (email.Length > 0)
            {
                //if (allCustomers.Count == 0)

                PortalUsers users = GetPortalUsers("https://techcyclesolutions.repairshopr.com/api/v1/portal_users?email=" + email);

                if (users != null && users.portal_users != null && users.portal_users.Count > 0)
                {
                    SingleCustomer cust = GetCustomerName(users.portal_users[0].customer_id);
                    if (cust != null)
                    {
                        if (cust.customer.properties.PortalPWToTCS.Equals(password))
                        {
                            result = JsonConvert.SerializeObject(cust) + "||" +
                                (cust.customer.contacts.Count > 0 ? cust.customer.contacts[0].id.ToString() : "null") + "||" +
                                users.portal_users[0].id;
                        }
                        else if (cust.customer.contacts != null && cust.customer.contacts.Count > 0)
                        {
                            foreach (Contact contact in cust.customer.contacts)
                            {
                                if (contact.email.ToUpper() == email.ToUpper() && contact.properties != null &&
                                    contact.properties.PortalPWToTCS != null && contact.properties.PortalPWToTCS.Equals(password))
                                    result = JsonConvert.SerializeObject(cust) + "||" + contact.id.ToString() + "||" + users.portal_users[0].id;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public string Update(RSCustomer customer)
        {
            var t = Task.Run(() => PutCustomer(customer, (int)customer.id));
            t.Wait();
            string result = t.Result;
            return result;
        }

        public async Task<string> PutCustomer(RSCustomer customer, int id)
        {
            string result = string.Empty;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
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

        public SingleCustomer GetCustomerName(int id)
        {
            string admins = ConfigurationManager.AppSettings["CODiAdminCustomerID"];

            SingleCustomer cust = GetCustomer(String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers/{0}", id));
            if (cust != null)
                cust.customer.properties.Admin = admins.Contains(cust.customer.id.ToString());

            //    customerName = (!String.IsNullOrEmpty(cust.customer.business_then_name) ? cust.customer.business_then_name : 
            //        !String.IsNullOrEmpty(cust.customer.business_and_full_name) ? cust.customer.business_and_full_name : cust.customer.business_name) 
            //        + "||" + cust.customer.id.ToString();
            return cust;
        }

        public PortalUsers GetPortalUsers(string url)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add("Authorization", String.Format("Bearer {0}", RSToken));
                client.Headers.Add("Content-Type", "application/json");
                var response = client.DownloadString(url);
                PortalUsers user = JsonConvert.DeserializeObject<PortalUsers>(response);
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public SingleCustomer GetCustomer(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", RSToken));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            SingleCustomer customer = JsonConvert.DeserializeObject<SingleCustomer>(response);
            return customer;
        }

        public List<CustomerAutoComplete> GetCustomerAutoComplete(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", RSToken));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            RSCustomers customers = JsonConvert.DeserializeObject<RSCustomers>(response);
            List<CustomerAutoComplete> cac = new List<CustomerAutoComplete>();
            foreach (RSCustomer customer in customers.customers)
            {
                cac.Add(new CustomerAutoComplete() { business_then_name = customer.business_then_name, id = customer.id });
            }
            return cac;
        }

        public async Task<RSCustomers> GetAllCustomers()
        {
            RSCustomers custs = new RSCustomers();
            
            try
            {
                int page = 1;
                int totalPages = 1;
                while (page <= totalPages)
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/customers?page={0}", page);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    string result = await httpResponseMessage.Content.ReadAsStringAsync();
                    custs = JsonConvert.DeserializeObject<RSCustomers>(result);
                    if (custs != null && custs.customers != null && custs.customers.Count > 0)
                    {
                        // If the customer has a PortalPWToTCS property,
                        //   check for customer information in Client table.
                        //   If not found, create new Client record.
                        foreach (RSCustomer cust in custs.customers)
                        {
                            if (cust.properties != null && !string.IsNullOrEmpty(cust.properties.PortalPWToTCS))
                            {

                                //   For all contacts attached to the customer, check for a User record in the Users table
                            }
                        }
                    }
                    page = custs.meta.page + 1;
                    totalPages = custs.meta.total_pages;
                    System.Threading.Thread.Sleep(60000); // wait one minute before making the next call.
                }
            }
            catch (Exception ex)
            {
                custs.error = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return custs;
        }
    }
}
