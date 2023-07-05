using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;

namespace Techcycle.RepairShopr.API
{
    public class TicketsManager
    {
        private string RSToken = DBHelper.GetStringValue(ConfigurationManager.AppSettings["RepairShopr"]);
        public Tickets GetTickets(long customerId, long ticketId, string query, DateTime createdDate)
        {
            Tickets result = new Tickets();

            if (query.Length > 0 || createdDate > DateTime.MinValue)
            {
                var t = Task.Run(() => RetrieveClientTickets(query, createdDate, customerId));
                t.Wait();
                Tickets temp = t.Result;
                result.tickets = new List<Ticket>();
                
                if (customerId > 0)
                {
                    foreach (Ticket ticket in temp.tickets)
                    {
                        if (ticket.customer_id == customerId)
                            result.tickets.Add(ticket);
                    }
                }
            }
            if (customerId > 0 && result.tickets.Count == 0)
            {
                var t = Task.Run(() => RetrieveCustomerTickets(customerId));
                t.Wait();
                result = t.Result;
            }
            else if (ticketId > 0)
            {
                var t = Task.Run(() => RetrieveOneTicket(ticketId));
                t.Wait();
                Ticket ticket = t.Result;
                result.tickets.Add(ticket);
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer, ticket, or query not found ({0} / {1} / {2})", customerId, ticketId, query)
                };
                result.tickets.Add(tck);
            }

            return result;
        }

        public async Task<Tickets> RetrieveCustomerTickets(long custID)
        {
            Tickets result = new Tickets();
            if (custID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}", custID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (resp.ToUpper().Contains("WE ARE SEEING"))
                    {
                        System.Threading.Thread.Sleep(300000);

                        httpResponseMessage = await httpClient.GetAsync(uri);
                        resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    }
                    result = JsonConvert.DeserializeObject<Tickets>(resp);
                    for (int i = 0; i < result.tickets.Count; i++)
                    {
                        var t = Task.Run(() => RetrieveOneTicket(result.tickets[i].id));
                        t.Wait();
                        result.tickets[i] = t.Result;
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
                    if (result.tickets == null)
                        result.tickets = new List<Ticket>();
                    result.tickets.Add(tck);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                if (result.tickets == null)
                    result.tickets = new List<Ticket>();
                result.tickets.Add(tck);
            }

            return result;
        }

        // GET api/<controller>
        public async Task<Tickets> RetrieveClientTickets(string query, DateTime createdDate, long custID)
        {
            Tickets result = new Tickets();
            result.tickets = new List<Ticket>();
            bool noMore = false;
            if (query.Length > 0 || createdDate > DateTime.MinValue)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    int page = 1;

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                    string apiHost = string.Empty;
                    while (!noMore)
                    {
                        if (!string.IsNullOrEmpty(query))
                            apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?query={0}&customer_id={1}&page={2}", query, custID, page);
                        else
                            apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?created_after={0}&customer_id={1}&page={2}", createdDate.ToString("yyyy-MM-dd"), custID, page);
                        var uri = new Uri(apiHost);

                        var httpResponseMessage = await httpClient.GetAsync(uri);
                        resp = await httpResponseMessage.Content.ReadAsStringAsync();
                        Tickets temp = JsonConvert.DeserializeObject<Tickets>(resp);
                        if (temp != null && temp.tickets != null && temp.tickets.Count > 0)
                        {
                            for (int i = 0; i < temp.tickets.Count; i++)
                            {
                                var t = Task.Run(() => RetrieveOneTicket(temp.tickets[i].id));
                                t.Wait();
                                temp.tickets[i] = t.Result;
                                System.Threading.Thread.Sleep(2000);
                            }
                            result.tickets.AddRange(temp.tickets);
                            noMore = temp.meta.page == temp.meta.total_pages;
                            page++;
                        }
                        else
                            noMore = true;
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
                    if (result.tickets == null)
                        result.tickets = new List<Ticket>();
                    result.tickets.Add(tck);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = "ERROR: Empty query."
                };
                if (result.tickets == null)
                    result.tickets = new List<Ticket>();
                result.tickets.Add(tck);
            }

            return result;
        }

        public Ticket CreateTicket(Ticket ticket)
        {
            Ticket cust_ticket = null;
            var t = Task.Run(() => PostTicket(JsonConvert.SerializeObject(ticket)));
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

        public async Task<string> PostTicket(string ticket)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(ticket, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
                string apiHost = "https://techcyclesolutions.repairshopr.com/api/v1/tickets";
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

        // PUT api/<controller>
        public Ticket UpdateTicket(Ticket ticket)
        {
            Ticket cust_ticket = null;
            var t = Task.Run(() => PutTicket(ticket.id, JsonConvert.SerializeObject(ticket)));
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

        public async Task<string> PutTicket(long ticket_id, string ticket)
        {
            string resp = String.Empty;
            HttpClient httpClient = new HttpClient();
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(ticket, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
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
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", RSToken));
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

        public Tickets GetRepairShoprTicketsSince(int custID, DateTime? lastUpdated)
        {
            Tickets result = new Tickets();

            if (custID > 0)
            {
                var t = Task.Run(() => RetrieveCustomerTicketsSince(custID, lastUpdated));
                t.Wait();
                result = t.Result;
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                if (result.tickets == null)
                    result.tickets = new List<Ticket>();
                result.tickets.Add(tck);
            }

            return result;
        }

        private async Task<Tickets> RetrieveCustomerTicketsSince(int custID, DateTime? lastUpdated)
        {
            Tickets result = new Tickets();
            if (custID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = "";
                    if (lastUpdated.HasValue)
                        apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}&since_updated_at={1}",
                            custID, lastUpdated.Value.ToString("yyyy-MM-dd"));
                    else
                        apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}", custID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Tickets>(resp);
                    if (result != null && result.tickets != null)
                        for (int i = 0; i < result.tickets.Count; i++)
                        {
                            var t = Task.Run(() => RetrieveOneTicket(result.tickets[i].id));
                            t.Wait();
                            result.tickets[i] = t.Result;
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
                    if (result.tickets == null)
                        result.tickets = new List<Ticket>();
                    result.tickets.Add(tck);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                if (result.tickets == null)
                    result.tickets = new List<Ticket>();
                result.tickets.Add(tck);
            }

            return result;
        }
    }
}
