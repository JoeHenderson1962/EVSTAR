using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class SessionToken
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expires { get; set; }

        public SessionToken()
        {
            ID = 0;
            UserName = string.Empty;
            Token = new Guid().ToString();
            CreatedAt = DateTime.Now;
            Expires = DateTime.Now.AddHours(12);
        }
    }
}
