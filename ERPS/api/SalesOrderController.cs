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
using TCModels = EVSTAR.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERPS.api
{
    public class SalesOrderController : ApiController
    {

        // GET api/<controller>
        public string Get()
        {
            return "";
        }

        // GET api/<controller>/id
        public TCModels.SalesOrder Get(int id)
        {
            TCModels.SalesOrder order = new TCModels.SalesOrder();
            AddressController ac = new AddressController();
            RSCustomerController cc = new RSCustomerController();

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM SalesOrders s WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID=@ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        order = new TCModels.SalesOrder(r);
                        order.OrderLines = new List<TCModels.OrderLine>();
                        order.BillToAddress = ac.Get(order.BillToAddressID);
                        order.ShipToAddress = ac.Get(order.ShipToAddressID);
                        order.Customer = cc.Get(order.CustomerID);
                        List<TCModels.OrderLine> lines = GetLinesForOrder(id);
                        if (lines != null)
                            order.OrderLines.AddRange(lines);
                    }
                    r.Close();
                }
            }

            return order;
        }

        public List<TCModels.OrderLine> GetLinesForOrder(int orderID)
        {
            List<TCModels.OrderLine> orderLines = new List<TCModels.OrderLine>();

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT o.*, s.[Name] as ProductName FROM OrderLines o WITH(NOLOCK) ");
                sql.AppendLine("LEFT JOIN SaleItems s WITH(NOLOCK) ON s.ID = o.ProductID ");
                sql.AppendLine("WHERE SalesOrderID=@SalesOrderID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SalesOrderID", orderID);

                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        TCModels.OrderLine line = new TCModels.OrderLine(r);
                        orderLines.Add(line);
                    }
                    r.Close();
                }
            }

            return orderLines;
        }

        // POST api/<controller>
        public string Post([FromBody] TCModels.SalesOrder salesOrder)
        {
            string result = string.Empty;
            try
            {
                if (salesOrder.ClientID > 0)
                {
                    string clientCode = TCModels.DBHelper.GetStringValue(HttpContext.Current.Request.Params["client"]);
                    ClientController clientController = new ClientController();
                    TCModels.Client client = clientController.GetClientByCode(clientCode);

                    if (client != null && client.ID > 0)
                    {
                        salesOrder.ClientID = client.ID;
                    }
                }

                AddressController addressController = new AddressController();
                TCModels.Address address = addressController.Post(salesOrder.BillToAddress);
                if (address != null /*&& string.IsNullOrEmpty(errorMsg1)*/)
                {
                    salesOrder.BillToAddressID = address.ID;
                    salesOrder.BillToAddress.ID = address.ID;
                }

                TCModels.Address address2 = addressController.Post(salesOrder.ShipToAddress);
                if (address2 != null /*&& string.IsNullOrEmpty(errorMsg2)*/)
                {
                    salesOrder.ShipToAddressID = address2.ID;
                    salesOrder.ShipToAddress.ID = address2.ID;
                }

                salesOrder.ItemTotal = 0M;
                foreach (TCModels.OrderLine orderLine in salesOrder.OrderLines)
                {
                    salesOrder.ItemTotal += orderLine.UnitCost * orderLine.Quantity;
                    salesOrder.TotalTax += orderLine.Taxable ? orderLine.TaxAmount : 0M;
                }
                salesOrder.OrderTotal = salesOrder.ItemTotal + salesOrder.Shipping + salesOrder.TotalTax;

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO SalesOrders (ClientID, CustomerID, BillToAddressID, ShipToAddressID, OrderDateTime, ");
                sql.AppendLine("TotalTax, ItemTotal, OrderTotal, CCTransactionID, AmountPaid) ");
                sql.AppendLine("VALUES (@ClientID, @CustomerID, @BillToAddressID, @ShipToAddressID, @OrderDateTime, ");
                sql.AppendLine("@TotalTax, @ItemTotal, @OrderTotal, @CCTransactionID, @AmountPaid)" );
                sql.AppendLine("SELECT SCOPE_IDENTITY();");

                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(constr))
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@ClientID", salesOrder.ClientID);
                        cmd.Parameters.AddWithValue("@CustomerID", salesOrder.CustomerID);
                        cmd.Parameters.AddWithValue("@BillToAddressID", salesOrder.BillToAddressID);
                        cmd.Parameters.AddWithValue("@ShipToAddressID", salesOrder.ShipToAddressID);
                        cmd.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalTax", salesOrder.TotalTax);
                        cmd.Parameters.AddWithValue("@ItemTotal", salesOrder.ItemTotal);
                        cmd.Parameters.AddWithValue("@OrderTotal", salesOrder.OrderTotal);
                        cmd.Parameters.AddWithValue("@CCTransactionID", salesOrder.CCTransactionID);
                        cmd.Parameters.AddWithValue("@AmountPaid", salesOrder.AmountPaid);

                        salesOrder.ID = TCModels.DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        SaveOrderLines(salesOrder.OrderLines, salesOrder.ID);
                    }
                }
                result = JsonConvert.SerializeObject(salesOrder);
            }
            catch (Exception ex)
            {
                result = String.Format("Error: {0}", ex.Message);
            }

            return result;
        }

        // PUT api/<controller>
        public string Put([FromBody] TCModels.SalesOrder salesOrder)
        {
            string result = string.Empty;
            try
            {
                if (salesOrder.ClientID > 0)
                {
                    string clientCode = TCModels.DBHelper.GetStringValue(HttpContext.Current.Request.Params["client"]);
                    ClientController clientController = new ClientController();
                    TCModels.Client client = clientController.GetClientByCode(clientCode);

                    if (client != null && client.ID > 0)
                    {
                        salesOrder.ClientID = client.ID;
                    }
                }

                // TODO: Delete old addresses.

                AddressController addressController = new AddressController();
                TCModels.Address address = addressController.Post(salesOrder.BillToAddress);
                if (address != null /*&& string.IsNullOrEmpty(errorMsg1)*/)
                {
                    salesOrder.BillToAddressID = address.ID;
                    salesOrder.BillToAddress.ID = address.ID;
                }

                TCModels.Address address2 = addressController.Post(salesOrder.ShipToAddress);
                if (address2 != null /*&& string.IsNullOrEmpty(errorMsg2)*/)
                {
                    salesOrder.ShipToAddressID = address2.ID;
                    salesOrder.ShipToAddress.ID = address2.ID;
                }

                salesOrder.ItemTotal = 0M;
                foreach (TCModels.OrderLine orderLine in salesOrder.OrderLines)
                {
                    salesOrder.ItemTotal += orderLine.UnitCost * orderLine.Quantity;
                    salesOrder.TotalTax += orderLine.Taxable ? orderLine.TaxAmount : 0M;
                }
                salesOrder.OrderTotal = salesOrder.ItemTotal + salesOrder.Shipping + salesOrder.TotalTax;

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE SalesOrders SET ClientID=@ClientID, CustomerID=@CustomerID, BillToAddressID=@BillToAddressID, ");
                sql.AppendLine("ShipToAddressID=@ShipToAddressID, OrderDateTime=@OrderDateTime, TotalTax=@TotalTax, ");
                sql.AppendLine("ItemTotal=@ItemTotal, OrderTotal=@OrderTotal, CCTransactionID=@CCTransactionID, AmountPaid=@AmountPaid ");
                sql.AppendLine("WHERE ID=@ID");

                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
                using (SqlConnection sqlConn = new SqlConnection(constr))
                {
                    sqlConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@ID", salesOrder.ID);
                        cmd.Parameters.AddWithValue("@ClientID", salesOrder.ClientID);
                        cmd.Parameters.AddWithValue("@CustomerID", salesOrder.CustomerID);
                        cmd.Parameters.AddWithValue("@BillToAddressID", salesOrder.BillToAddressID);
                        cmd.Parameters.AddWithValue("@ShipToAddressID", salesOrder.ShipToAddressID);
                        cmd.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalTax", salesOrder.TotalTax);
                        cmd.Parameters.AddWithValue("@ItemTotal", salesOrder.ItemTotal);
                        cmd.Parameters.AddWithValue("@OrderTotal", salesOrder.OrderTotal);
                        cmd.Parameters.AddWithValue("@CCTransactionID", salesOrder.CCTransactionID);
                        cmd.Parameters.AddWithValue("@AmountPaid", salesOrder.AmountPaid);

                        cmd.ExecuteNonQuery();
                    }
                }

                DeleteOrderLines(salesOrder.ID);
                SaveOrderLines(salesOrder.OrderLines, salesOrder.ID);
                result = JsonConvert.SerializeObject(salesOrder);
            }
            catch (Exception ex)
            {
                result = String.Format("Error: {0}", ex.Message);
            }

            return result;
        }

        private void DeleteOrderLines(int salesOrderID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM OrderLines ");
            sql.AppendLine("WHERE SalesOrderID=@SalesOrderID");

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection sqlConn = new SqlConnection(constr))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                {
                    cmd.Parameters.AddWithValue("@SalesOrderID", salesOrderID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveOrderLines(List<TCModels.OrderLine> orderLines, int salesOrderID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO OrderLines (SalesOrderID, ProductID, Quantity, QuantityDelivered, UnitCost, ");
            sql.AppendLine("Taxable, TaxAmount, StudentName, AssetTag, SerialNumber) ");
            sql.AppendLine("VALUES (@SalesOrderID, @ProductID, @Quantity, @QuantityDelivered, @UnitCost, ");
            sql.AppendLine("@Taxable, @TaxAmount, @StudentName, @AssetTag, @SerialNumber);");
            sql.AppendLine("SELECT SCOPE_IDENTITY();");

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection sqlConn = new SqlConnection(constr))
            {
                sqlConn.Open();
                foreach (TCModels.OrderLine orderLine in orderLines)
                {
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), sqlConn))
                    {
                        cmd.Parameters.AddWithValue("@SalesOrderID", salesOrderID);
                        cmd.Parameters.AddWithValue("@ProductID", orderLine.ProductID);
                        cmd.Parameters.AddWithValue("@Quantity", orderLine.Quantity);
                        cmd.Parameters.AddWithValue("@QuantityDelivered", orderLine.QuantityDelivered);
                        cmd.Parameters.AddWithValue("@UnitCost", orderLine.UnitCost);
                        cmd.Parameters.AddWithValue("@Taxable", orderLine.Taxable);
                        cmd.Parameters.AddWithValue("@TaxAmount", orderLine.TaxAmount);
                        cmd.Parameters.AddWithValue("@StudentName", orderLine.StudentName);
                        cmd.Parameters.AddWithValue("@AssetTag", orderLine.AssetTag);
                        cmd.Parameters.AddWithValue("@SerialNumber", orderLine.SerialNumber);

                        cmd.ExecuteScalar();
                    }
                }
            }
        }
    }
}
