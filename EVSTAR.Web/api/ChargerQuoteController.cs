using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Threading.Tasks;
using EVSTAR.DB.NET;
using System.Web.Hosting;

namespace EVSTAR.Web.api
{
    public class ChargerQuoteController : ApiController
    {
        // GET api/<controller>
        public string Get(int id)
        {
            try 
            {
            }
            catch (Exception ex)
            {
            }
            return "";
        }


        // POST api/<controller>
        public ChargerQuoteRequest Post([FromBody] ChargerQuoteRequest value)
        {
            try
            {
                string folder = HostingEnvironment.MapPath("~/Uploads/");
                value.PhotoCloseUpMainPanelUrl = value.PhotoCloseUpMainPanelUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoCloseUpSubpanelUrl = value.PhotoCloseUpSubpanelUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoGarageInteriorUrl = value.PhotoGarageInteriorUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoGarageUrl = value.PhotoGarageUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoIdealChargerLocationUrl = value.PhotoIdealChargerLocationUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoMainPanelUrl= value.PhotoMainPanelUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoStreetUrl = value.PhotoStreetUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                value.PhotoSubpanelUrl = value.PhotoSubpanelUrl.Replace(@"C:\fakepath\", folder + value.SessionID + "_");
                string errorMsg;
                ChargerQuoteHelper cqh = new ChargerQuoteHelper();
                if (value.ID > 0)
                    cqh.Update(value, out errorMsg);
                else
                    cqh.Insert(value, out errorMsg);
            }
            catch (Exception ex)
            {
            }
            return value;
        }
    }
}
