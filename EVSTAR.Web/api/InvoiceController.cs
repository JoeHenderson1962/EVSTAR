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
    public class InvoiceController : ApiController
    {
        // GET api/<controller>
        public Invoice Get()
        {
            Invoice invoice = null;
            try
            {
                string invoiceNum = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["invoice"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["email"]);
                invoice = GetInvoice(invoiceNum, email);
            }
            catch (Exception ex)
            {
            }
            return invoice;
        }
        public Invoice Get(int id)
        {
            Invoice invoice = null;
            try
            {
                //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                //string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                //string provided = Encryption.MD5(code + email);
                //if (hashed != provided)
                //{
                //    provided = Encryption.MD5(code + phone);
                //    if (hashed != provided)
                //        return invoice;
                //}
                invoice = GetInvoice(id);
            }
            catch (Exception ex)
            {
            }
            return invoice;
        }

        // POST api/<controller>
        public Invoice Post([FromBody] Invoice value)
        {
            Invoice invoice = null;
            try
            {
                //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                //string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                //string provided = Encryption.MD5(code + email);
                //if (hashed != provided)
                //{
                //    provided = Encryption.MD5(code + phone);
                //    if (hashed != provided)
                //        return null;
                //}

                invoice = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (invoice != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Invoices ");
                        sql.AppendLine("(CustomerID, SalesOrderID, InvoiceNumber, QuoteAmount, InvoiceAmount, AmountDue, AmountPaid, DatePaid) ");
                        sql.AppendLine("VALUES (@CustomerID, @SalesOrderID, @InvoiceNumber, @QuoteAmount, @InvoiceAmount, @AmountDue, @AmountPaid, @DatePaid); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CustomerID", invoice.CustomerID);
                            cmd.Parameters.AddWithValue("@SalesOrderID", invoice.SalesOrderID);
                            cmd.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                            cmd.Parameters.AddWithValue("@QuoteAmount", invoice.QuoteAmount);
                            cmd.Parameters.AddWithValue("@InvoiceAmount", invoice.InvoiceAmount);
                            cmd.Parameters.AddWithValue("@AmountDue", invoice.AmountDue);
                            cmd.Parameters.AddWithValue("@AmountPaid", invoice.AmountPaid);
                            if (invoice.DatePaid == DateTime.MinValue && invoice.AmountPaid > 0)
                                invoice.DatePaid = DateTime.Now;
                            if (invoice.DatePaid != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DatePaid", invoice.DatePaid);
                            else
                                cmd.Parameters.AddWithValue("@DatePaid", DBNull.Value);
                            invoice.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return invoice;
        }

        // PUT api/<controller>
        public Invoice Put([FromBody] Invoice value)
        {
            Invoice invoice = null;
            try
            {
                //string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                //string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                //string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                //string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                //string provided = Encryption.MD5(code + email);
                //if (hashed != provided)
                //{
                //    provided = Encryption.MD5(code + phone);
                //    if (hashed != provided)
                //        return null;
                //}

                invoice = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (invoice != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Invoices SET CustomerID=@CustomerID, SalesOrderID=@SalesOrderID, InvoiceNumber=@InvoiceNumber, ");
                        sql.AppendLine("QuoteAmount=@QuoteAmount, InvoiceAmount=@InvoiceAmount, AmountDue=@AmountDue, AmountPaid=@AmountPaid, DatePaid=@DatePaid ");
                        sql.AppendLine("WHERE ID=@ID ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ID", invoice.ID);
                            cmd.Parameters.AddWithValue("@CustomerID", invoice.CustomerID);
                            cmd.Parameters.AddWithValue("@SalesOrderID", invoice.SalesOrderID);
                            cmd.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                            cmd.Parameters.AddWithValue("@QuoteAmount", invoice.QuoteAmount);
                            cmd.Parameters.AddWithValue("@InvoiceAmount", invoice.InvoiceAmount);
                            cmd.Parameters.AddWithValue("@AmountDue", invoice.AmountDue);
                            cmd.Parameters.AddWithValue("@AmountPaid", invoice.AmountPaid);
                            if (invoice.DatePaid == DateTime.MinValue && invoice.AmountPaid > 0)
                                invoice.DatePaid = DateTime.Now;
                            if (invoice.DatePaid != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DatePaid", invoice.DatePaid);
                            else
                                cmd.Parameters.AddWithValue("@DatePaid", DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return invoice;
        }

        private Invoice GetInvoice(int invoiceID)
        {
            Invoice invoice = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Invoices WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", invoiceID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        invoice = new Invoice(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return invoice;
        }

        private Invoice GetInvoice(string invoiceNum, string email)
        {
            Invoice invoice = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT i.* FROM Invoices i WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN Customer c WITH(NOLOCK) ON c.ID = i.CustomerID ");
                sql.AppendLine("WHERE i.InvoiceNumber = @InvoiceNumber AND c.Email = @Email");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@InvoiceNumber", invoiceNum);
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        invoice = new Invoice(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return invoice;
        }
    }
}
