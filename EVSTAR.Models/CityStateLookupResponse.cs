using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome6
    {
        [JsonProperty("CityStateLookupResponse")]
        public CityStateLookupResponse CityStateLookupResponse { get; set; }
    }

    public partial class CityStateLookupResponse
    {
        [JsonProperty("ZipCode")]
        public ZipCode ZipCode { get; set; }
    }

    public partial class ZipCode
    {
        [JsonProperty("Zip5")]
        public long Zip5 { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }
    }

    public partial class Welcome6
    {
        public static Welcome6 FromJson(string json) => JsonConvert.DeserializeObject<Welcome6>(json, EVSTAR.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome6 self) => JsonConvert.SerializeObject(self, EVSTAR.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
