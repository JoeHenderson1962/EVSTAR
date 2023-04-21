using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EVSTAR.Models
{
    public class SaleItem
    {
        public int ID { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public int QtyOnHand { get; set; }
        public int ClientID { get; set; }
        public string Period { get; set; }

        public SaleItem()
        {
            ID = 0;
            Name = string.Empty;
            Description = string.Empty; 
            UnitCost = 0;
            QtyOnHand = 0;
            ClientID = 0;
            Period = string.Empty;
        }

        public SaleItem(SqlDataReader r) : base()
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            Name = DBHelper.GetStringValue(r["Name"]);
            Description = DBHelper.GetStringValue(r["Description"]);
            UnitCost = DBHelper.GetDecimalValue(r["UnitCost"]);
            QtyOnHand = DBHelper.GetInt32Value(r["QtyOnHand"]);
            ClientID = DBHelper.GetInt32Value(r["ClientID"]);
            Period = DBHelper.GetStringValue(r["Period"]);
        }
    }
}
