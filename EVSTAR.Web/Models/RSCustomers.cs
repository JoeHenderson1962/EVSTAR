using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Techcycle.Web.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Customers>(myJsonResponse); 
    public class CustomerAutoComplete
    {        
        public long id { get; set; }
        public string business_then_name { get; set; }

    }

    public class RSCustomer
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string business_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string pdf_url { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string notes { get; set; }
        public bool get_sms { get; set; }
        public bool opt_out { get; set; }
        public bool disabled { get; set; }
        public bool no_email { get; set; }
        public string location_name { get; set; }
        public Int64? location_id { get; set; }
        public Properties properties { get; set; }
        public string online_profile_url { get; set; }
        public object tax_rate_id { get; set; }
        public string notification_email { get; set; }
        public string invoice_cc_emails { get; set; }
        public Int64? invoice_term_id { get; set; }
        public string referred_by { get; set; }
        public Int64? ref_customer_id { get; set; }
        public string business_and_full_name { get; set; }
        public string business_then_name { get; set; }
        public List<Contact> contacts { get; set; }
    }

    public class Meta
    {
        public int total_pages { get; set; }
        public int total_entries { get; set; }
        public int per_page { get; set; }
        public int page { get; set; }
    }

    public class RSCustomers
    {
        public List<RSCustomer> customers { get; set; }
        public Meta meta { get; set; }
    }

    public class SingleCustomer
    {
        public RSCustomer customer { get; set; }
    }
}
