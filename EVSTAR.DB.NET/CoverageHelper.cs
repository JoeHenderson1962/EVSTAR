using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace EVSTAR.DB.NET
{
    public class CoverageHelper
    {
        public List<Coverage> Select(int id, int customerID, int programID, int clientID, string clientCode, out string errorMsg)
        {
            List<Coverage> result = new List<Coverage>();
            errorMsg = string.Empty;
            CustomerHelper customerHelper = new CustomerHelper();
            ClientHelper clientHelper = new ClientHelper();
            ProgramHelper programHelper = new ProgramHelper();
            ClaimHelper claimHelper = new ClaimHelper();
            CoveredProductHelper coveredProductHelper = new CoveredProductHelper();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Coverage WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");
                    if (customerID > 0 && programID > 0)
                        sql.AppendLine("WHERE CustomerID=@CustomerID AND ProgramID=@ProgramID");
                    if (customerID > 0 && clientID > 0)
                        sql.AppendLine("WHERE CustomerID=@CustomerID AND ClientID=@ClientID");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);
                        if (customerID > 0 && programID > 0)
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", customerID);
                            cmd.Parameters.AddWithValue("@ProgramID", programID);
                        }

                        if (customerID > 0 && clientID > 0)
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", customerID);
                            cmd.Parameters.AddWithValue("@ClientID", clientID);
                        }

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Coverage coverage = new Coverage(r);
                            if (coverage.ClientID > 0)
                            {
                                List<Client> clients = clientHelper.Select(coverage.ClientID, out errorMsg);
                                if (clients != null && clients.Count > 0)
                                {
                                    coverage.CoverageClient = clients[0];
                                }
                            }
                            if (coverage.CustomerID > 0)
                            {
                                List<Customer> customers = customerHelper.Select(string.Empty, string.Empty, string.Empty, 
                                    coverage.CustomerID, 0, clientCode, out errorMsg);
                                if (customers != null && customers.Count > 0)
                                {
                                    coverage.CoverageCustomer = customers[0];
                                }
                            }
                            if (coverage.ProgramID > 0)
                            {
                                List<Program> programs = programHelper.Select(coverage.ProgramID, clientCode, out errorMsg);
                                if (programs != null && programs.Count > 0)
                                {
                                    coverage.CoverageProgram = programs[0];
                                }
                            }
                            if (coverage.CoveredProductID > 0)
                            {
                                List<CoveredProduct> products = coveredProductHelper.Select(coverage.CoveredProductID, clientCode, out errorMsg);
                                if (products != null && products.Count > 0)
                                {
                                    coverage.CoverageProduct = products[0];
                                }
                            }
                            coverage.ClosedClaimsPastYear = claimHelper.SelectClosedClaimCountLastYear(coverage.ID, clientCode, out errorMsg);
                            result.Add(coverage);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public Coverage Insert(Coverage data, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Coverage ");
                        sql.AppendLine("(ClientID, CustomerID, ProgramID, CoveredProductID, TestCallDate, EffectiveDate, CancelDate, MaxClaimDate, ");
                        sql.AppendLine("UploadDate, DropDate, UpdatedBy, UpdatedOn) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@ClientID, @CustomerID, @ProgramID, @CoveredProductID, @TestCallDate, @EffectiveDate, @CancelDate, @MaxClaimDate, ");
                        sql.AppendLine("@UploadDate, @DropDate, @UpdatedBy, @UpdatedOn) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
                            cmd.Parameters.AddWithValue("@CoveredProductID", data.CoveredProductID);

                            if (data.TestCallDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@TestCallDate", data.TestCallDate);
                            else
                                cmd.Parameters.AddWithValue("@TestCallDate", DBNull.Value);

                            if (data.EffectiveDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EffectiveDate", data.EffectiveDate);
                            else
                                cmd.Parameters.AddWithValue("@EffectiveDate", DBNull.Value);

                            if (data.CancelDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@CancelDate", data.CancelDate);
                            else
                                cmd.Parameters.AddWithValue("@CancelDate", DBNull.Value);

                            if (data.MaxClaimDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@MaxClaimDate", data.MaxClaimDate);
                            else
                                cmd.Parameters.AddWithValue("@MaxClaimDate", DBNull.Value);

                            if (data.UploadDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@UploadDate", data.UploadDate);
                            else
                                cmd.Parameters.AddWithValue("@UploadDate", DBNull.Value);

                            if (data.DropDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DropDate", data.DropDate);
                            else
                                cmd.Parameters.AddWithValue("@DropDate", DBNull.Value);

                            if (data.UpdatedOn != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@UpdatedOn", data.UpdatedOn);
                            else
                                cmd.Parameters.AddWithValue("@UpdatedOn", DBNull.Value);

                            cmd.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                            data.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            return data;
        }

        public Coverage Update(Coverage data, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (data != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Coverage ");
                        sql.AppendLine("SET ClientID=@ClientID, CustomerID=@CustomerID, ProgramID=@ProgramID, CoveredProductID=@CoveredProductID, TestCallDate=@TestCallDate, ");
                        sql.AppendLine("EffectiveDate=@EffectiveDate, CancelDate=@CancelDate, MaxClaimDate=@MaxClaimDate, UploadDate=@UploadDate,");
                        sql.AppendLine("DropDate=@DropDate, UpdatedOn=@UpdatedOn, UpdatedBy=@UpdatedBy ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", data.ClientID);
                            cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                            cmd.Parameters.AddWithValue("@ProgramID", data.ProgramID);
                            cmd.Parameters.AddWithValue("@CoveredProductID", data.CoveredProductID);

                            if (data.TestCallDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@TestCallDate", data.TestCallDate);
                            else
                                cmd.Parameters.AddWithValue("@TestCallDate", DBNull.Value);

                            if (data.EffectiveDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EffectiveDate", data.EffectiveDate);
                            else
                                cmd.Parameters.AddWithValue("@EffectiveDate", DBNull.Value);

                            if (data.CancelDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@CancelDate", data.CancelDate);
                            else
                                cmd.Parameters.AddWithValue("@CancelDate", DBNull.Value);

                            if (data.MaxClaimDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@MaxClaimDate", data.MaxClaimDate);
                            else
                                cmd.Parameters.AddWithValue("@MaxClaimDate", DBNull.Value);

                            if (data.UploadDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@UploadDate", data.UploadDate);
                            else
                                cmd.Parameters.AddWithValue("@UploadDate", DBNull.Value);

                            if (data.DropDate != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DropDate", data.DropDate);
                            else
                                cmd.Parameters.AddWithValue("@DropDate", DBNull.Value);

                            if (data.UpdatedOn != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@UpdatedOn", data.UpdatedOn);
                            else
                                cmd.Parameters.AddWithValue("@UpdatedOn", DBNull.Value);

                            cmd.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            return data;
        }

    }
}
