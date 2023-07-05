using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace EVSTAR.Web.api
{
    public class UspsController : ApiController
    {
        // GET api/<controller>
        public async Task<Address> Get()
        {
            Address addr = new Address();
            try
            {
                string uspsUser = ConfigurationManager.AppSettings["USPSUser"];
                string uspsPassword = ConfigurationManager.AppSettings["USPSPassword"];

                string postalCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["postalCode"]);
                string street = DBHelper.GetStringValue(HttpContext.Current.Request.Params["street"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);

                StringBuilder xml = new StringBuilder();
                xml.Append(String.Format("<CityStateLookupRequest USERID=\"{0}\">", uspsUser));
                //xml.Append("<Revision>1</Revision>");
                xml.Append("<ZipCode ID=\"0\">");
                xml.Append(String.Format("<Zip5>{0}</Zip5>", postalCode.Substring(0, 5)));
                xml.Append("</ZipCode>");
                xml.Append("</CityStateLookupRequest>");
                string resp = String.Empty;

                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                    string apiHost = "https://secure.shippingapis.com/shippingapi.dll?API=CityStateLookup&XML=" + xml.ToString();
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    if (resp.ToUpper().Contains("<ERROR>"))
                    {
                        addr.Error = resp;
                    }
                    else
                    {
                        string[] tempArray = resp.Split('>');
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            if (tempArray[i].ToUpper().Contains("</STATE"))
                            {
                                addr.State = tempArray[i].Replace("</State", "");
                            }
                            if (tempArray[i].ToUpper().Contains("</CITY"))
                            {
                                addr.City = tempArray[i].Replace("</City", "");
                            }
                            if (tempArray[i].ToUpper().Contains("</ZIP5"))
                            {
                                addr.City = tempArray[i].Replace("</Zip5", "");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    addr.Error = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                addr.Error = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return addr;
        }
    }
}
