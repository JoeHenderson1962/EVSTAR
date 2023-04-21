using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVSTAR.Models
{
    // Tickets myDeserializedClass = JsonConvert.DeserializeObject<Tickets>(myJsonResponse); 
    public class RSClaim
    {
        public long id { get; set; }
        public long? number { get; set; }
        public string subject { get; set; }
        public string created_at { get; set; }
        public long? customer_id { get; set; }
        public string customer_name { get; set; }
        public string problem_type { get; set; }
        public string status { get; set; }
        public string asset_tag { get; set; }
        public string notes { get; set; }
    }

}