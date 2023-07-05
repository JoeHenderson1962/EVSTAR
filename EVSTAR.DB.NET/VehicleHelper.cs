using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace EVSTAR.DB.NET
{
    public class VehicleHelper
    {
        public List<Vehicle> Select(int id, string clientCode, out string errorMsg)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            errorMsg= string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["EVSTAR"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT make.ID, make.Make, model.ID as ModelID, model.Model ");
                    sql.AppendLine("FROM ChargerQuoteCarMake make WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN ChargerQuoteCarModel model WITH(NOLOCK) ON make.ID = model.MakeID ");
                    sql.AppendLine("ORDER BY make.Make, model.Model ");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Vehicle vehicle = new Vehicle(r);
                            vehicles.Add(vehicle);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return vehicles;
        }
    }
}

