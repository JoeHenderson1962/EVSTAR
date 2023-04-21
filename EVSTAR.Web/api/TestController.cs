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
using Microsoft.AspNetCore.Mvc;

namespace EVSTAR.Web.api
{
    public class TestController : ApiController
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
        public FileContentResult Post()
        {
            string path = "C:\\inetpub\\wwwroot\\gotechcycle.com\\Content\\775606979950.pdf";
            byte[] data = System.IO.File.ReadAllBytes(path);
            return new FileContentResult(data, "application/pdf");
        }
    }
}
