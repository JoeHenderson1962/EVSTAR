using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;

namespace EVSTAR.Web.api
{
    public class VehicleController : ApiController
    {
        // GET api/<controller>
        public List<Vehicle> Get()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

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
            return vehicles;
        }
    }
}
