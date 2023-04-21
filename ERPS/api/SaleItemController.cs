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
    public class SaleItemController : ApiController
    {

        // GET api/<controller>
        public List<SaleItem> Get()
        {
            List<SaleItem> result = new List<SaleItem>();
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["client"]);
            ClientController clientController = new ClientController();
            Client client = clientController.GetClientByCode(clientCode);

            if (client != null && client.ID > 0)
            {
                var t = Task.Run(() => RetrieveSaleItemsByClient(client.ID));
                t.Wait();

                result = t.Result;
            }
            else
            {
                SaleItem tck = new SaleItem()
                {
                    ID = 0,
                    Name = String.Format("ERROR: Sale Itesm not found ({0})", client.Name)
                };
                result.Add(tck);
            }

            return result;
        }

        public async Task<List<SaleItem>> RetrieveSaleItemsByClient(int clientID)
        {
            List<SaleItem> result = new List<SaleItem>();
            if (clientID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("SELECT * FROM SaleItems WITH(NOLOCK) ");
                        sql.AppendLine("WHERE ClientID=@ClientID ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", clientID);
                            Task<SqlDataReader> r = cmd.ExecuteReaderAsync();
                            r.Wait();
                            if (r.Result != null && r.IsCompleted && !r.IsFaulted)
                            while (r.Result.Read())
                            {
                                SaleItem item = new SaleItem(r.Result);
                                result.Add(item);
                            }
                            r.Result.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    SaleItem tck = new SaleItem()
                    {
                        ID = 0,
                        Name = resp
                    };
                    result.Add(tck);
                }
            }
            else
            {
                SaleItem tck = new SaleItem()
                {
                    ID = 0,
                    Name = String.Format("ERROR: Invalid client ID passed ({0})", clientID)
                };
                result.Add(tck);
            }

            return result;
        }

        //public async Task<Ticket> RetrieveOneTicket(long ticketID)
        //{
        //    Ticket result = null;
        //    if (ticketID > 0)
        //    {
        //        string resp = String.Empty;
        //        HttpClient httpClient = new HttpClient();
        //        try
        //        {
        //            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
        //            string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets/{0}", ticketID);
        //            var uri = new Uri(apiHost);

        //            var httpResponseMessage = await httpClient.GetAsync(uri);
        //            resp = await httpResponseMessage.Content.ReadAsStringAsync();
        //            resp = resp.Replace("{\"ticket\":", "");
        //            resp = resp.Substring(0, resp.Length - 1);
        //            result = JsonConvert.DeserializeObject<Ticket>(resp);
        //        }
        //        catch (Exception ex)
        //        {
        //            resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
        //            Ticket tck = new Ticket()
        //            {
        //                id = 0,
        //                subject = resp
        //            };
        //            result = tck;
        //        }
        //    }
        //    else
        //    {
        //        Ticket tck = new Ticket()
        //        {
        //            id = 0,
        //            subject = String.Format("ERROR: Ticket not found ({0})", ticketID)
        //        };
        //        result = tck;
        //    }

        //    return result;
        //}

        //// POST api/<controller>
        //public Ticket Post()
        //{
        //    Ticket cust_ticket = null;
        //    string ticket = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["ticket"]);
        //    var t = Task.Run(() => PostTicket(ticket));
        //    t.Wait();
        //    string result = t.Result;
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
        //        cust_ticket = JsonConvert.DeserializeObject<Ticket>(result);
        //    }
        //    return cust_ticket;
        //}

        //public async Task<string> PostTicket(string ticket)
        //{
        //    string resp = String.Empty;
        //    HttpClient httpClient = new HttpClient();
        //    try
        //    {
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var httpContent = new StringContent(ticket, System.Text.Encoding.UTF8, "application/json");
        //        httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
        //        string apiHost = "https://techcyclesolutions.repairshopr.com/api/v1/tickets";
        //        var uri = new Uri(apiHost);

        //        var httpResponseMessage = await httpClient.PostAsync(uri, httpContent);
        //        resp = await httpResponseMessage.Content.ReadAsStringAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
        //    }
        //    return resp;
        //}
    }
}
