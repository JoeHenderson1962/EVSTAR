using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class RSInvoice
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public string customer_business_then_name { get; set; }
        public string number { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string date { get; set; }
        public string due_date { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string tax { get; set; }
        public bool? verified_paid { get; set; }
        public bool? tech_marked_paid { get; set; }
        public int ticket_id { get; set; }
        public int? user_id { get; set; }
        public string pdf_url { get; set; }
        public bool is_paid { get; set; }
        public int? location_id { get; set; }
        public string po_number { get; set; }
        public int? contact_id { get; set; }
        public string note { get; set; }
        public string hardwarecost { get; set; }
        public LineItems line_items { get; set; }
    }

    public class RSInvoices
    {
        public List<RSInvoice> invoices { get; set; }
        public Meta meta { get; set; }
    }

}
