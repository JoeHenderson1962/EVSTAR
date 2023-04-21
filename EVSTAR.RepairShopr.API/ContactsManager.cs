using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Configuration;

namespace EVSTAR.RepairShopr.API
{
    public class ContactsManager
    {
        private string RSToken = DBHelper.GetStringValue(ConfigurationManager.AppSettings["RepairShopr"]);

        CustomersManager custMgr = new CustomersManager();

        public async Task<Contacts> GetContactsForCustomer(long customerID)
        {
            Contacts result = new Contacts();
            result.contacts = new List<Contact>();
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                int page = 1;
                bool noMore = false;
                while (!noMore)
                {
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/contacts/?customer_id={0}&page={1}", customerID, page);
                    var uri = new Uri(apiHost);
                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    string res = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (res.Contains("message"))
                    {
                        noMore = true;
                    }
                    else
                    {
                        Contacts contacts = JsonConvert.DeserializeObject<Contacts>(res);
                        if (contacts != null)
                        {
                            result.contacts.AddRange(contacts.contacts);
                            noMore = contacts.meta.page == contacts.meta.total_pages;
                            page++;
                        }
                        else
                            noMore = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (result.contacts == null)
                    result.contacts = new List<Contact>();
                result.contacts.Add(new Contact() { name = "ERROR: " + ex.Message });
            }
            return result;
        }

        public async Task<Contact> GetContact(int contactId)
        {
            Contact result = null;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/contacts/{0}", contactId);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.GetAsync(uri);
                string res = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                result = new Contact();
                result.name = "ERROR: " + ex.Message;
            }
            return result;
        }

        public Contact CreateContact(Contact contact)
        {
            var t = Task.Run(() => PostContact(contact));
            t.Wait();
            int result = t.Result;
            if (result > 0)
                contact.id = result;
            return contact;
        }

        public async Task<int> PostContact(Contact contact)
        {
            int result = 0;
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(contact), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/contacts/");
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
                string res = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception(String.Format("[0} {1}", ex.Message, ex.StackTrace));
            }
            return result;
        }

        public string UpdateContact(Contact contact)
        {
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
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
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

        public PortalUsers GetPortalUsers(string url)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", String.Format("Bearer {0}", RSToken));
            client.Headers.Add("Content-Type", "application/json");
            var response = client.DownloadString(url);
            PortalUsers user = JsonConvert.DeserializeObject<PortalUsers>(response);
            return user;
        }
    }
}
