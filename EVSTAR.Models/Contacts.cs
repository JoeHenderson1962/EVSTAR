﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Customers>(myJsonResponse); 

    public class Contact
    {
        public long id { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public long customer_id { get; set; }
        public long? account_id { get; set; }
        public string notes { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public long? vendor_id { get; set; }
        public Properties properties { get; set; }
        public bool opt_out { get; set; }
        public string extension { get; set; }
        public string processed_phone { get; set; }
        public string processed_mobile { get; set; }
        public string ticket_matching_emails { get; set; }
    }

    public class Contacts
    {
        public List<Contact> contacts { get; set; }
        public Meta meta { get; set; }
    }

}
