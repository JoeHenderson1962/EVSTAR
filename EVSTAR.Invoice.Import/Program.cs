using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;

namespace EVSTAR.Invoice.Import
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DirectoryInfo directoryInfo = new DirectoryInfo(args[0]);
            List<FileInfo> files = directoryInfo.EnumerateFiles("*.csv").ToList();
            foreach (FileInfo file in files)
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string data = sr.ReadToEnd();
                    string[] lines = data.Split('\n');
                    foreach (string line in lines)
                    {
                        string[] fields = line.Split('\t');
                        if (fields.Length > 6)
                        {
                            string email = fields[6];

                        }
                    }
                    sr.Close();
                }
        }

        public Customer LookupCustomerByEmail(string email)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                sql.AppendLine("WHERE c.Email = @Email");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Code", email);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        customer = new Customer(r);
                        //if (customer.BillingAddressID > 0)
                        //    customer.BillingAddress = FindAddress(customer.BillingAddressID);
                        //if (customer.ShippingAddressID > 0)
                        //    customer.ShippingAddress = FindAddress(customer.ShippingAddressID);
                        //if (customer.MailingAddressID > 0)
                        //    customer.MailingAddress = FindAddress(customer.MailingAddressID);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }
    }
}
