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
using EVSTAR.DB.NET;

namespace ERPS.api
{
    public class PerilController : ApiController
    {
        // GET api/<controller>
        public List<CoveredPeril> Get()
        {
            CoveredPerilHelper cph = new CoveredPerilHelper();

            List<CoveredPeril> perils = new List<CoveredPeril>();

            try
            {
                int category = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["category"]);
                int id = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["id"]);
                string program = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                string errorMsg = string.Empty;
                perils = cph.Select(id, category, program, clientCode, out errorMsg);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.Response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return perils;
        }
        public List<CoveredPeril> Get(int id)
        {
            CoveredPerilHelper cph = new CoveredPerilHelper();

            List<CoveredPeril> perils = new List<CoveredPeril>();

            try
            {
                int category = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["category"]);
                string program = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["program"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                string errorMsg = string.Empty;
                perils = cph.SelectFull(id, category, program, clientCode, out errorMsg);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.Response.StatusCode = 500;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return perils;
        }
    }
}
