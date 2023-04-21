using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class LineItem
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int invoice_id { get; set; }
        public string item { get; set; }
        public string name { get; set; }
        public decimal? cost { get; set; }
        public decimal? price { get; set; }
        public decimal? quantity { get; set; }
        public int? product_id { get; set; }
        public bool taxable { get; set; }
        public object discount_percent { get; set; }
        public int? position { get; set; }
        public int? invoice_bundle_id { get; set; }
        public decimal? discount_dollars { get; set; }
        public string product_category { get; set; }
    }

    public class LineItems
    {
        public List<LineItem> line_items { get; set; }
        public Meta meta { get; set; }
    }

}
