using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace EVSTAR.Models
{
    public class Comment
    {
        public long id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public long ticket_id { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string tech { get; set; }
        public bool hidden { get; set; }
        public long? user_id { get; set; }

        public Comment()
        {
        }

        public Comment(SqlDataReader r)
        {
            id = DBHelper.GetInt64Value(r["id"]);
            created_at = DBHelper.GetDateTimeValue(r["created_at"]).ToString("yyyy-MM-dd hh:mm:ss");
            updated_at = DBHelper.GetDateTimeValue(r["updated_at"]).ToString("yyyy-MM-dd hh:mm:ss");
            ticket_id = DBHelper.GetInt64Value(r["ticket_id"]);
            subject = DBHelper.GetStringValue(r["subject"]);
            body = DBHelper.GetStringValue(r["body"]);
            tech = DBHelper.GetStringValue(r["tech"]);
            hidden = DBHelper.GetBooleanValue(r["hidden"]);
            user_id = DBHelper.GetInt64Value(r["user_id"]);
        }
    }

    public class RSUser
    {
        public long id { get; set; }
        public string email { get; set; }
        public string full_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string group { get; set; }
        public bool admin { get; set; }
        public string color { get; set; }

        public RSUser ()
        {

        }

        public RSUser(SqlDataReader r)
        {
            id = DBHelper.GetInt64Value(r["id"]);
            created_at = DBHelper.GetDateTimeValue(r["created_at"]);
            updated_at = DBHelper.GetDateTimeValue(r["updated_at"]);
            email = DBHelper.GetStringValue(r["email"]);
            full_name = DBHelper.GetStringValue(r["full_name"]);
            color = DBHelper.GetStringValue(r["color"]);
            admin = DBHelper.GetBooleanValue(r["admin"]);
            group = DBHelper.GetStringValue(r["group"]);
        }
    }

    public class Ticket
    {
        public long id { get; set; }
        public int? number { get; set; }
        public string subject { get; set; }
        public string created_at { get; set; }
        public long? customer_id { get; set; }
        public string customer_business_then_name { get; set; }
        public DateTime due_date { get; set; }
        public DateTime? resolved_at { get; set; }
        public DateTime? start_at { get; set; }
        public DateTime? end_at { get; set; }
        public long? location_id { get; set; }
        public string problem_type { get; set; }
        public string status { get; set; }
        public long? ticket_type_id { get; set; }
        public Properties properties { get; set; }
        public long? user_id { get; set; }
        public string updated_at { get; set; }
        public string pdf_url { get; set; }
        public string priority { get; set; }
        public List<Comment> comments { get; set; }
        public RSUser user { get; set; }
        public List<Asset> assets { get; set; }
        public List<long> asset_ids { get; set; }
        public long? contact_id { get; set; }
        public RSCustomer customer { get; set; }
        public Contact contact { get; set; }
        public DateTime last_sent_to_servicebench { get; set; }

        public Ticket()
        {

        }

        public Ticket(SqlDataReader r)
        {
            id = DBHelper.GetInt64Value(r["id"]);
            number = DBHelper.GetInt32Value(r["number"]);
            subject = DBHelper.GetStringValue(r["subject"]);
            created_at = DBHelper.GetDateTimeValue(r["created_at"]).ToString("yyyy-MM-dd hh:mm:ss");
            updated_at = DBHelper.GetDateTimeValue(r["updated_at"]).ToString("yyyy-MM-dd hh:mm:ss");
            customer_id = DBHelper.GetInt64Value(r["customer_id"]);
            customer_business_then_name = DBHelper.GetStringValue(r["customer_business_then_name"]);
            due_date = DBHelper.GetDateTimeValue(r["due_date"]);
            resolved_at = DBHelper.GetDateTimeValue(r["resolved_at"]);
            start_at = DBHelper.GetDateTimeValue(r["start_at"]);
            end_at = DBHelper.GetDateTimeValue(r["end_at"]);
            location_id = DBHelper.GetInt64Value(r["location_id"]);
            problem_type = DBHelper.GetStringValue(r["problem_type"]);
            status = DBHelper.GetStringValue(r["status"]);
            ticket_type_id = DBHelper.GetInt64Value(r["ticket_type_id"]);
            properties = new Properties();
            user_id = DBHelper.GetInt64Value(r["user_id"]);
            pdf_url = DBHelper.GetStringValue(r["pdf_url"]);
            priority = DBHelper.GetStringValue(r["priority"]);
            comments = new List<Comment>();
            user = new RSUser();
            assets = new List<Asset>();
            asset_ids = new List<long>();
            contact_id = DBHelper.GetInt64Value(r["contact_id"]);
            customer = new RSCustomer();
            contact = new Contact();
            last_sent_to_servicebench = DBHelper.GetDateTimeValue(r["last_sent_to_servicebench"]);
        }
    }

    public class Tickets
    {
        public List<Ticket> tickets { get; set; }
        public Meta meta { get; set; }

        public string error { get; set; }
    }

}