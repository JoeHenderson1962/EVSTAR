using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using net.authorize.sample;

namespace ERPS.api
{
    public class PaymentController : ApiController
    {
        // GET api/<controller>
        public string Get(int id)
        {
            try 
            {
            }
            catch (Exception ex)
            {
            }
            return "";
        }


        // POST api/<controller>
        public CCTransaction Post([FromBody] CCTransaction value)
        {
            string ApiLoginID = ConfigurationManager.AppSettings["AuthorizeNetAPILoginID"];
            string ApiTransactionKey = ConfigurationManager.AppSettings["AuthorizeNetTransactionKey"];
            string function = DBHelper.GetStringValue(HttpContext.Current.Request.Params["f"]).ToUpper();
            string salesOrder = DBHelper.GetStringValue(HttpContext.Current.Request.Params["s"]).ToUpper();
            int.TryParse(salesOrder, out int salesOrderID);

            if (function == "CHARGE" && value != null)
            {
                value.TransactionDateTime = DateTime.Now;

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO CCTransactions (FirstName, LastName, Address, City, State, PostalCode, Amount, ");
                sql.AppendLine("CardNumber, ExpDate, CardCode, RSCustomerID, RSContactID, RSInvoice, ClaimID, CustomerID) ");
                sql.AppendLine("VALUES (@FirstName, @LastName, @Address, @City, @State, @PostalCode, @Amount, ");
                sql.AppendLine("@CardNumber, @ExpDate, @CardCode, @RSCustomerID, @RSContactID, @RSInvoice, @ClaimID, @CustomerID); ");
                sql.AppendLine("SELECT SCOPE_IDENTITY();");

                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(constr))
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", value.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", value.LastName);
                        cmd.Parameters.AddWithValue("@Address", value.Address);
                        cmd.Parameters.AddWithValue("@City", value.City);
                        cmd.Parameters.AddWithValue("@State", value.State);
                        cmd.Parameters.AddWithValue("@PostalCode", value.PostalCode);
                        cmd.Parameters.AddWithValue("@Amount", value.Amount);
                        cmd.Parameters.AddWithValue("@CardNumber", Encryption.MD5(value.CardNumber));
                        cmd.Parameters.AddWithValue("@ExpDate", value.ExpDate);
                        cmd.Parameters.AddWithValue("@CardCode", Encryption.MD5(value.CardCode));
                        cmd.Parameters.AddWithValue("@RSCustomerID", value.RSCustomerID);
                        cmd.Parameters.AddWithValue("@RSContactID", value.RSContactID);
                        cmd.Parameters.AddWithValue("@RSInvoice", value.InvoiceID);
                        cmd.Parameters.AddWithValue("@ClaimID", value.ClaimID);
                        cmd.Parameters.AddWithValue("@CustomerID", value.CustomerID);

                        value.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());

                        if (salesOrderID > 0)
                        {
                            SalesOrderController soc = new SalesOrderController();
                            SalesOrder so = soc.Get(salesOrderID);
                            so.CCTransactionID = value.ID;
                            soc.Put(so);
                        }
                    }
                }

                ANetApiResponse response = ChargeCreditCard.Run(ApiLoginID, ApiTransactionKey, value.Amount, value.FirstName, value.LastName,
                    value.Address, value.City, value.PostalCode, value.CardNumber, value.ExpDate, value.CardCode);
                value.Response = JsonConvert.SerializeObject(response);
                UpdateCCTransaction(value);
                return value;
            }
            else
                return null;
        }

        private void UpdateCCTransaction(CCTransaction value)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE CCTransactions SET FirstName=@FirstName, LastName=@LastName, Address=@Address, City=@City, State=@State, ");
            sql.AppendLine("PostalCode=@PostalCode, Amount=@Amount, ");
            sql.AppendLine("CardNumber=@CardNumber, ExpDate=@ExpDate, CardCode=@CardCode, RSCustomerID=@RSCustomerID, ");
            sql.AppendLine("RSContactID=@RSContactID, RSInvoice=@RSInvoice, ");
            sql.AppendLine("TransactionDateTime=@TransactionDateTime, TransactionID=@TransactionID, Response=@Response, Status=@Status, AuthCode=@AuthCode, ");
            sql.AppendLine("ClaimID=@ClaimID, CustomerID=@CustomerID ");
            sql.AppendLine("WHERE ID=@ID ");

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection sqlConn = new SqlConnection(constr))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                {
                    cmd.Parameters.AddWithValue("@ID", value.ID);
                    cmd.Parameters.AddWithValue("@FirstName", value.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", value.LastName);
                    cmd.Parameters.AddWithValue("@Address", value.Address);
                    cmd.Parameters.AddWithValue("@City", value.City);
                    cmd.Parameters.AddWithValue("@State", value.State);
                    cmd.Parameters.AddWithValue("@PostalCode", value.PostalCode);
                    cmd.Parameters.AddWithValue("@Amount", value.Amount);
                    cmd.Parameters.AddWithValue("@CardNumber", Encryption.MD5(value.CardNumber));
                    cmd.Parameters.AddWithValue("@ExpDate", value.ExpDate);
                    cmd.Parameters.AddWithValue("@CardCode", Encryption.MD5(value.CardCode));
                    cmd.Parameters.AddWithValue("@RSCustomerID", value.RSCustomerID);
                    cmd.Parameters.AddWithValue("@RSContactID", value.RSContactID);
                    cmd.Parameters.AddWithValue("@RSInvoice", value.InvoiceID);
                    cmd.Parameters.AddWithValue("@TransactionDateTime", value.TransactionDateTime);
                    cmd.Parameters.AddWithValue("@TransactionID", value.TransactionID);
                    cmd.Parameters.AddWithValue("@Response", value.Response);
                    cmd.Parameters.AddWithValue("@Status", value.Status);
                    cmd.Parameters.AddWithValue("@AuthCode", value.AuthCode);
                    cmd.Parameters.AddWithValue("@ClaimID", value.ClaimID);
                    cmd.Parameters.AddWithValue("@CustomerID", value.CustomerID);

                    cmd.ExecuteNonQuery();

                }
            }

        }

        // PUT api/<controller>
        public void Put([FromBody] CCTransaction value)
        {
            UpdateCCTransaction(value);
            //string ApiLoginID = ConfigurationManager.AppSettings["AuthorizeNetAPILoginID"];
            //string ApiTransactionKey = ConfigurationManager.AppSettings["AuthorizeNetTransactionKey"];
            //string function = DBHelper.GetStringValue(HttpContext.Current.Request.Params["f"]).ToUpper();

            //if (function == "CHARGE" && value != null)
            //{
            //    ANetApiResponse response = ChargeCreditCard.Run(ApiLoginID, ApiTransactionKey, value.Amount, value.FirstName, value.LastName,
            //        value.Address, value.City, value.PostalCode, value.CardNumber, value.ExpDate, value.CardCode);
            //    if (response.messages.resultCode == 0)
            //    {
            //        if (response.messages != null)
            //        {
            //            value.TransactionDateTime = DateTime.Now;
            //        }
            //    }
            //    return response;
            //}
            //else
            //    return null;
        }
    }
}
