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

namespace EVSTAR.Web.api
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
                    string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["client"]);
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
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
    }
}
