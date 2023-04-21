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
    public class ProgramController : ApiController
    {
        // GET api/<controller>
        public List<Program> Get()
        {
            List<Program> programs = new List<Program>();

            //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            //string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);

            //string provided = Encryption.MD5(code + address);
            //if (hashed != provided)
            //{
            //    provided = Encryption.MD5(code + phone);
            //    if (hashed != provided)
            //        return clients;
            //}
            string programCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Program p WITH(NOLOCK) ");
                if (!string.IsNullOrEmpty(programCode))
                    sql.AppendLine("WHERE ProgramName=@ProgramName ");

                sql.AppendLine("ORDER BY [ProgramName]");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(programCode))
                        cmd.Parameters.AddWithValue("@ProgramName", programCode);

                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Program program = new Program(r);
                        program.ProgramFulfillmentType = GetFulfillmentTypeByID(program.FulfillmentTypeID);
                        programs.Add(program);
                    }
                    r.Close();
                }
            }

            return programs;
        }

        public Program Get(int id)
        {
            Program program = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Program WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID=@ID ");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        program = new Program(r);
                        program.ProgramFulfillmentType = GetFulfillmentTypeByID(program.FulfillmentTypeID);
                    }
                    r.Close();
                }
            }

            return program;
        }

        private FulfillmentType GetFulfillmentTypeByID(int id)
        {
            FulfillmentType ft = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM FulfillmentTypes WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID=@ID ");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        ft = new FulfillmentType(r);
                    }
                    r.Close();
                }
            }

            return ft;
        }
    }
}
