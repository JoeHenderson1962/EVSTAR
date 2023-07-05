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
    public class FileController : ApiController
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
        public void Post()
        {
            try
            {
                string sessionId = HttpContext.Current.Request.Headers["sessionId"];
                string folder = HostingEnvironment.MapPath("~/Uploads/");
                const int BufferSize = 65536;
                HttpFileCollection files = HttpContext.Current.Request.Files;
                foreach (string fileName in files)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                    string filePath = folder + sessionId + "_" + fileName.Replace(@"C:\fakepath\", "");

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        using (Stream reader = file.InputStream)
                        {
                            byte[] buffer = new byte[BufferSize];
                            int read = -1, pos = 0;
                            do
                            {
                                int len = (file.ContentLength < pos + BufferSize ?
                                    file.ContentLength - pos :
                                    BufferSize);
                                read = reader.Read(buffer, 0, len);
                                fs.Write(buffer, 0, len);
                                pos += read;
                            } while (read > 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
