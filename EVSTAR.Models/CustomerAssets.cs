using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class AssetClaim
    {
        public string text { get; set; }
        public string href { get; set; }
    }

    public class CustomerAsset
    {
        public long id { get; set; }
        public string name { get; set; }
        public long? customer_id { get; set; }
        public long? contact_id { get; set; }
        public string asset_type { get; set; }
        public string asset_serial { get; set; }
        public List<AssetClaim> claims { get; set; }
    }
}
