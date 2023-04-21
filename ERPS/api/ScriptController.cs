using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;
using EVSTAR.DB.NET;
using System.Reflection.Emit;
using System.Net.PeerToPeer;

namespace ERPS.api
{
    public class ScriptController : ApiController
    {
        public UserHelper userHelper = new UserHelper();

        // GET api/<controller>
        public Script Get()
        {
            Script script = null;
            try
            {
                string errorMsg = string.Empty;   
                string name = DBHelper.GetStringValue(HttpContext.Current.Request.Params["name"]);
                int client = DBHelper.GetInt32Value(HttpContext.Current.Request.Params["client"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);

                ScriptHelper scriptHelper = new ScriptHelper();
                script = scriptHelper.Select(name, 1, client, clientCode, out errorMsg);
            }
            catch (Exception ex)
            {
            }
            return script;
        }
    }
}
