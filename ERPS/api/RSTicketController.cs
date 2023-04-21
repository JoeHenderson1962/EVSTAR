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
    public class RSTicketController : ApiController
    {

        // GET api/<controller>
        public List<Ticket> Get()
        {
            List<Ticket> result = new List<Ticket>();
            string custidStr = DBHelper.GetStringValue(HttpContext.Current.Request.Params["customerid"]);
            int custID = DBHelper.GetInt32Value(custidStr);
            string ticketidStr = DBHelper.GetStringValue(HttpContext.Current.Request.Params["ticketid"]);
            int ticketID = DBHelper.GetInt32Value(ticketidStr);

            if (custID > 0)
            {
                var t = Task.Run(() => RetrieveCustomerTickets(custID));
                t.Wait();
                result = t.Result;
            }
            else if (ticketID > 0)
            {
                var t = Task.Run(() => RetrieveOneTicket(ticketID));
                t.Wait();
                Ticket ticket = t.Result;
                result.Add(ticket);
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                result.Add(tck);
            }

            return result;
        }

        // GET api/<controller>
        public async Task<List<Ticket>> RetrieveCustomerTickets(int custID)
        {
            List<Ticket> result = new List<Ticket>();
            if (custID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}", custID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Ticket>>(resp);
                    for (int i = 0; i < result.Count; i++)
                    {
                        var t = Task.Run(() => RetrieveOneTicket(result[i].id));
                        t.Wait();
                        result[i] = t.Result;
                    }
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    Ticket tck = new Ticket()
                    {
                        id = 0,
                        subject = resp
                    };
                    result.Add(tck);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                result.Add(tck);
            }

            return result;
        }

        // POST api/<controller>
        public Ticket Post([FromBody] Ticket cust_ticket)
        {
            var t = Task.Run(() => PostTicket(cust_ticket));
            t.Wait();
            Ticket newTicket = t.Result;
            string result = newTicket.subject;

            if (result.ToLower().Contains("\"success\":false"))
            {
                RepairShoprFailureResponse err = JsonConvert.DeserializeObject<RepairShoprFailureResponse>(result);

                return new Ticket()
                {
                    id = 0,
                    subject = "ERROR: " + err.message[0]
                };
            }
            else if (result.ToLower().Contains("\"error\":"))
            {
                RepairShoprErrorResponse err = JsonConvert.DeserializeObject<RepairShoprErrorResponse>(result);

                return new Ticket()
                {
                    id = 0,
                    subject = "ERROR: " + err.error
                };
            }
            else
            {
                //result = result.Replace("{\"ticket\":", "");
                //result = result.Substring(0, result.Length - 1);
                ////if (result.Contains("asset_ids"))
                ////{
                //    cust_ticket = JsonConvert.DeserializeObject<Ticket>(result);
                ////    var t2 = Task.Run(() => PutTicket(cust_ticket.id, ticket));
                ////    t2.Wait();
                ////    result = t2.Result;
                ////    if (result.Contains("\"success\":false"))
                ////    {
                ////        RepairShoprFailureResponse err = JsonConvert.DeserializeObject<RepairShoprFailureResponse>(result);

                ////        return new Ticket()
                ////        {
                ////            id = 0,
                ////            subject = "ERROR: " + err.message[0]
                ////        };
                ////    }
                ////    else if (result.Contains("\"error\":"))
                ////    {
                ////        RepairShoprErrorResponse err = JsonConvert.DeserializeObject<RepairShoprErrorResponse>(result);

                ////        return new Ticket()
                ////        {
                ////            id = 0,
                ////            subject = "ERROR: " + err.error
                ////        };
                ////    }
                ////    else
                ////    {
                ////        result = result.Replace("{\"ticket\":", "");
                ////        result = result.Substring(0, result.Length - 1);
                ////    }
                ////}
                ////Ticket updated_cust_ticket = JsonConvert.DeserializeObject<Ticket>(result.Replace("number\":null", "number\": 0"));
            }
            return newTicket;
        }

        private async Task<Ticket> PostTicket(Ticket ticket)
        {
            Ticket respTicket = ticket;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(ticket), System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = "https://techcyclesolutions.repairshopr.com/api/v1/tickets";
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
                string resp = await httpResponseMessage.Content.ReadAsStringAsync();
                if (resp.Contains("\"error\":"))
                {
                    respTicket = new Ticket();
                    respTicket.subject = resp;
                }
                else
                {
                    resp = resp.Replace("{\"ticket\":", "");
                    resp = resp.Substring(0, resp.Length - 1);
                    respTicket = JsonConvert.DeserializeObject<Ticket>(resp);
                }
            }
            catch (Exception ex)
            {
                respTicket.subject = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return respTicket;
        }

        // PUT api/<controller>
        public Ticket Put()
        {
            Ticket cust_ticket = null;
            string ticket = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["ticket"]);
            string ticket_id = DBHelper.GetStringValue(HttpContext.Current.Request.Params["ticket_id"]);
            var t = Task.Run(() => PutTicket(DBHelper.GetInt32Value(ticket_id), ticket));
            t.Wait();
            string result = t.Result;
            if (result.Contains("\"success\":false"))
            {
                RepairShoprFailureResponse err = JsonConvert.DeserializeObject<RepairShoprFailureResponse>(result);

                return new Ticket()
                {
                    id = 0,
                    subject = "ERROR: " + err.message[0]
                };
            }
            else if (result.Contains("\"error\":"))
            {
                RepairShoprErrorResponse err = JsonConvert.DeserializeObject<RepairShoprErrorResponse>(result);

                return new Ticket()
                {
                    id = 0,
                    subject = "ERROR: " + err.error
                };
            }
            else
            {
                result = result.Replace("{\"ticket\":", "");
                result = result.Substring(0, result.Length - 1);
                //if (result.Contains("asset_ids"))
                //{
                cust_ticket = JsonConvert.DeserializeObject<Ticket>(result);
                //    var t2 = Task.Run(() => PutTicket(cust_ticket.id, ticket));
                //    t2.Wait();
                //    result = t2.Result;
                //    if (result.Contains("\"success\":false"))
                //    {
                //        RepairShoprFailureResponse err = JsonConvert.DeserializeObject<RepairShoprFailureResponse>(result);

                //        return new Ticket()
                //        {
                //            id = 0,
                //            subject = "ERROR: " + err.message[0]
                //        };
                //    }
                //    else if (result.Contains("\"error\":"))
                //    {
                //        RepairShoprErrorResponse err = JsonConvert.DeserializeObject<RepairShoprErrorResponse>(result);

                //        return new Ticket()
                //        {
                //            id = 0,
                //            subject = "ERROR: " + err.error
                //        };
                //    }
                //    else
                //    {
                //        result = result.Replace("{\"ticket\":", "");
                //        result = result.Substring(0, result.Length - 1);
                //    }
                //}
                //Ticket updated_cust_ticket = JsonConvert.DeserializeObject<Ticket>(result.Replace("number\":null", "number\": 0"));
            }
            return cust_ticket;
        }

        public async Task<string> PutTicket(int ticket_id, string ticket)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(ticket, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets/{0}", ticket_id);
                var uri = new Uri(apiHost);

                var httpResponseMessage = await httpClient.PutAsync(uri, httpContent);
                resp = await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return resp;
        }

        public async Task<Ticket> RetrieveOneTicket(long ticketID)
        {
            Ticket result = null;
            if (ticketID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets/{0}", ticketID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    resp = resp.Replace("{\"ticket\":", "");
                    resp = resp.Substring(0, resp.Length - 1);
                    result = JsonConvert.DeserializeObject<Ticket>(resp);
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    Ticket tck = new Ticket()
                    {
                        id = 0,
                        subject = resp
                    };
                    result = tck;
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Ticket not found ({0})", ticketID)
                };
                result = tck;
            }

            return result;
        }
    }
}