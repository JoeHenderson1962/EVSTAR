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
    public class ClientController : ApiController
    {
        // GET api/<controller>
        public List<Client> Get()
        {
            List<Client> clients = new List<Client>();

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
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["program"]);

            AddressController ac = new AddressController();
            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Client c WITH(NOLOCK) ");
                if (!string.IsNullOrEmpty(clientCode))
                    sql.AppendLine("WHERE Code=@Code ");

                sql.AppendLine("ORDER BY [Name]");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(clientCode))
                        cmd.Parameters.AddWithValue("@Code", clientCode);

                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Client client = new Client(r);
                        client.MailingAddress = ac.Get(client.AddressID);
                        client.Fulfillment = GetFulfillmentTypeByID(client.FulfillmentTypeID);
                        clients.Add(client);
                    }
                    r.Close();
                }
            }

            return clients;
        }

        public Client Get(int id)
        {
            Client client = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Client WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID=@ID ");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        client = new Client(r);
                        AddressController ac = new AddressController();
                        client.MailingAddress = ac.Get(client.AddressID);
                        client.Fulfillment = GetFulfillmentTypeByID(client.FulfillmentTypeID);
                    }
                    r.Close();
                }
            }

            return client;
        }

        public Client GetClientByCode(string clientCode)
        {
            Client client = null;
            AddressController ac = new AddressController();
            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Client c WITH(NOLOCK) ");
                sql.AppendLine("WHERE Code=@Code ");

                sql.AppendLine("ORDER BY [Name]");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(clientCode))
                        cmd.Parameters.AddWithValue("@Code", clientCode);

                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        client = new Client(r);
                        //client.MailingAddress = ac.Get(client.AddressID);
                        //client.Fulfillment = GetFulfillmentTypeByID(client.FulfillmentTypeID);
                    }
                    r.Close();
                }

            }
            return client;
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
