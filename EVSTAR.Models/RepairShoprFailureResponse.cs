using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSTAR.Models
{
    public class Params
    {
        public string name { get; set; }
        public int customer_id { get; set; }
        public string asset_serial { get; set; }
    }

    public class RepairShoprFailureResponse
    {
        public string error { get; set; }
        public bool success { get; set; }
        public List<string> message { get; set; }
        public Params @params { get; set; }
    }

}