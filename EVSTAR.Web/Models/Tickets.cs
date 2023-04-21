using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techcycle.Web.Models
{
    // Tickets myDeserializedClass = JsonConvert.DeserializeObject<Tickets>(myJsonResponse); 

    public class Comment
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int ticket_id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string tech { get; set; }
        public bool hidden { get; set; }
        public int? user_id { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string group { get; set; }

        [JsonProperty("admin?")]
        public bool Admin { get; set; }
        public string color { get; set; }
    }

    public class Ticket
    {
        public int id { get; set; }
        public int? number { get; set; }
        public string subject { get; set; }
        public DateTime created_at { get; set; }
        public int customer_id { get; set; }
        public string customer_business_then_name { get; set; }
        public DateTime due_date { get; set; }
        public DateTime? resolved_at { get; set; }
        public DateTime? start_at { get; set; }
        public DateTime? end_at { get; set; }
        public int? location_id { get; set; }
        public string problem_type { get; set; }
        public string status { get; set; }
        public int ticket_type_id { get; set; }
        public Properties properties { get; set; }
        public int? user_id { get; set; }
        public DateTime updated_at { get; set; }
        public object pdf_url { get; set; }
        public string priority { get; set; }
        public List<Comment> comments { get; set; }
        public User user { get; set; }
        public List<Asset> assets { get; set; }
        public Customer customer { get; set; }
        public Contact contact { get; set; }
    }

    public class Tickets
    {
        public List<Ticket> tickets { get; set; }
        public Meta meta { get; set; }
    }

}