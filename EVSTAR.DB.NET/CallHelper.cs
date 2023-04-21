
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
    public class CallHelper
    {
        public List<Call> Select(int ID, int customerID, int userID, DateTime startDate, DateTime endDate, 
            string clientCode, out string errorMsg)
        {
            List<Call> result = new List<Call>();
            errorMsg = string.Empty;
            ClaimHelper claimHelper = new ClaimHelper();
            CallActionHelper actionHelper = new CallActionHelper();
            ClientHelper clientHelper = new ClientHelper();
            UserHelper userHelper = new UserHelper();
            CallResultHelper resultHelper = new CallResultHelper();
            CoverageHelper coverageHelper = new CoverageHelper();
            CustomerHelper custHelper = new CustomerHelper();

            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Calls WITH(NOLOCK) ");
                    if (ID > 0)
                        sql.AppendLine("WHERE ID=@ID ");
                    else
                    {
                        if (customerID > 0)
                            sql.AppendLine("WHERE CustomerID=@CustomerID ");
                        if (userID > 0)
                            if (customerID > 0)
                                sql.AppendLine("AND UserID=@UserID");
                            else
                                sql.AppendLine("WHERE UserID=@UserID");
                        if (startDate > DateTime.MinValue)
                            if (customerID > 0 || userID > 0)
                                sql.AppendLine("AND (StartTime>=@StartTime OR StartTime=NULL)");
                            else
                                sql.AppendLine("WHERE (StartTime>=@StartTime OR StartTime=NULL)");
                        if (endDate > DateTime.MinValue)
                            if (customerID > 0 || userID > 0 || startDate > DateTime.MinValue)
                                sql.AppendLine("AND (EndTime<=@EndTime OR EndTime=NULL)");
                            else
                                sql.AppendLine("WHERE (EndTime<=@EndTime OR EndTime=NULL)");
                    }
                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (ID > 0)
                        {
                            cmd.Parameters.AddWithValue("@ID", ID);
                        }
                        else
                        {
                            if (customerID > 0)
                                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                            if (userID > 0)
                                cmd.Parameters.AddWithValue("@UserID", userID);
                            if (startDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@StartTime", startDate);
                            if (endDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EndTime", endDate);
                        }

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Call call = new Call(r);
                            if (call.ActionID > 0)
                            {
                                List<CallAction> actions = actionHelper.Select(call.ActionID, out errorMsg);
                                if (actions != null && actions.Count > 0)
                                {
                                    call.CallAction = actions[0];
                                    call.ActionName = call.CallAction.ActionName;
                                }
                            }
                            if (call.ResultID > 0)
                            {
                                List<CallResult> callResults = resultHelper.Select(call.ResultID, out errorMsg);
                                if (callResults != null && callResults.Count > 0)
                                {
                                    call.CallResult = callResults[0];
                                    call.ResultName = call.CallResult.Result;
                                }
                            }
                            if (call.UserID > 0)
                            {
                                List<User> userResults = userHelper.Select(string.Empty, string.Empty, call.UserID, clientCode, out errorMsg);
                                if (userResults != null && userResults.Count > 0)
                                {
                                    call.CallUser = userResults[0];
                                    call.CsrName = call.CallUser.UserName;
                                }
                            }
                            if (call.ClaimID > 0)
                            {
                                List<Claim> claimResults = claimHelper.Select(call.ClaimID, 0, 0, clientCode, out errorMsg);
                                if (claimResults != null && claimResults.Count > 0)
                                {
                                    call.CallClaim = claimResults[0];
                                }
                            }
                            if (call.ClientID > 0)
                            {
                                List<Client> clientResults = clientHelper.Select(call.ClientID, out errorMsg);
                                if (clientResults != null && clientResults.Count > 0)
                                {
                                    call.CurrClient = clientResults[0];
                                    call.ClientCode = call.CurrClient.Code;
                                }
                            }
                            if (call.CustomerID > 0)
                            {
                                List<Customer> custResults = custHelper.Select(string.Empty, string.Empty, string.Empty, call.CustomerID, 
                                    call.ClientID, call.CurrClient.Code, out errorMsg);
                                if (custResults != null && custResults.Count > 0)
                                {
                                    call.CallCustomer = custResults[0];
                                }
                                List<Claim> customerClaims = claimHelper.Select(0, call.CustomerID, 0, clientCode, out errorMsg);
                                if (customerClaims != null && customerClaims.Count > 0)
                                {
                                    call.NumApprovedReq = 0;
                                    call.NumOpenReq = 0;
                                    foreach (Claim claim in customerClaims)
                                    {
                                        if (claim.StatusHistory != null)
                                        {
                                            ClaimStatusHistory hist = claim.StatusHistory.OrderByDescending(x => x.StatusDate).FirstOrDefault();
                                            if (hist != null)
                                            {
                                                if (hist.Status.ToUpper() == "OPEN" || hist.Status.ToUpper() == "RECEIPT SUBMITTED")
                                                    call.NumOpenReq++;
                                                if (hist.Status.ToUpper() == "APPROVED")
                                                    call.NumApprovedReq++;
                                            }
                                        }
                                    }
                                    call.CallClaim = customerClaims[0];
                                }

                                List<Coverage> coverageResults = coverageHelper.Select(0, call.CustomerID, 0, call.ClientID, clientCode, out errorMsg);
                                if (coverageResults != null && coverageResults.Count > 0)
                                {
                                    call.CurrCoverage = coverageResults[0];
                                }
                            }

                            if (call.ClaimID > 0)
                            {
                                List<Claim> customerClaims = claimHelper.Select(call.ClaimID, 0, 0, clientCode, out errorMsg);
                                if (customerClaims != null && customerClaims.Count > 0)
                                {
                                    call.CallClaim = customerClaims[0];
                                }
                            }
                            result.Add(call);
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

        public Call Insert(Call call, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (call != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Calls ");
                        sql.AppendLine("(CSRName, CallerName, SearchMDN, ClientID, CustomerID, ClaimID, ActionID, ResultID, ");
                        sql.AppendLine("LanguageID, StartTime, EndTime, EscalationReasonID, EscalationResolution, EscalationResolvedDate, UserID, Escalate) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@CSRName, @CallerName, @SearchMDN, @ClientID, @CustomerID, @ClaimID, @ActionID, @ResultID, ");
                        sql.AppendLine("@LanguageID, @StartTime, @EndTime, @EscalationReasonID, @EscalationResolution, @EscalationResolvedDate, @UserID, @Escalate) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CSRName", call.CsrName);
                            cmd.Parameters.AddWithValue("@CallerName", call.CallerName);
                            cmd.Parameters.AddWithValue("@SearchMDN", call.SearchMDN);
                            cmd.Parameters.AddWithValue("@ClientID", call.ClientID);
                            cmd.Parameters.AddWithValue("@CustomerID", call.CustomerID);
                            cmd.Parameters.AddWithValue("@ClaimID", call.ClaimID);
                            cmd.Parameters.AddWithValue("@ActionID", call.ActionID);
                            cmd.Parameters.AddWithValue("@ResultID", call.ResultID);
                            cmd.Parameters.AddWithValue("@LanguageID", call.LanguageID);
                            if (call.StartTime > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@StartTime", call.StartTime);
                            else
                                cmd.Parameters.AddWithValue("@StartTime", DateTime.Now);
                            if (call.EndTime > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@EndTime", call.EndTime);
                            else
                                cmd.Parameters.AddWithValue("@EndTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@EscalationReasonID", call.EscalationReasonID);
                            cmd.Parameters.AddWithValue("@EscalationResolution", call.EscalationResolution);
                            if (call.EscalationResolvedDate > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@EscalationResolvedDate", call.EscalationResolvedDate);
                            else
                                cmd.Parameters.AddWithValue("@EscalationResolvedDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserID", call.UserID);
                            cmd.Parameters.AddWithValue("@Escalate", call.Escalate);
                            call.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0\r\n{1}", ex.Message, ex.StackTrace);
            }
            return call;
        }

        public Call Update(Call call, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (call != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Calls ");
                        sql.AppendLine("SET CSRName=@CSRName, CallerName=@CallerName, SearchMDN=@SearchMDN, ClientID=@ClientID, CustomerID=@CustomerID, ");
                        sql.AppendLine("ClaimID=@ClaimID, ActionID=@ActionID, ResultID=@ResultID, LanguageID=@LanguageID, StartTime=@StartTime, ");
                        sql.AppendLine("EndTime=@EndTime, EscalationReasonID=@EscalationReasonID, EscalationResolution=@EscalationResolution, EscalationResolvedDate=@EscalationResolvedDate, ");
                        sql.AppendLine("UserID=@UserID, Escalate=@Escalate ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CSRName", call.CsrName);
                            cmd.Parameters.AddWithValue("@CallerName", call.CallerName);
                            cmd.Parameters.AddWithValue("@SearchMDN", call.SearchMDN);
                            cmd.Parameters.AddWithValue("@ClientID", call.ClientID);
                            cmd.Parameters.AddWithValue("@CustomerID", call.CustomerID);
                            cmd.Parameters.AddWithValue("@ClaimID", call.ClaimID);
                            cmd.Parameters.AddWithValue("@ActionID", call.ActionID);
                            cmd.Parameters.AddWithValue("@ResultID", call.ResultID);
                            cmd.Parameters.AddWithValue("@LanguageID", call.LanguageID);
                            if (call.StartTime > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@StartTime", call.StartTime);
                            else
                                cmd.Parameters.AddWithValue("@StartTime", DBNull.Value);
                            if (call.EndTime > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@EndTime", call.EndTime);
                            else
                                cmd.Parameters.AddWithValue("@EndTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@EscalationReasonID", call.EscalationReasonID);
                            cmd.Parameters.AddWithValue("@EscalationResolution", call.EscalationResolution);
                            if (call.EscalationResolvedDate > Convert.ToDateTime("2022-01-01"))
                                cmd.Parameters.AddWithValue("@EscalationResolvedDate", call.EscalationResolvedDate);
                            else
                                cmd.Parameters.AddWithValue("@EscalationResolvedDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@UserID", call.UserID);
                            cmd.Parameters.AddWithValue("@Escalate", call.Escalate);
                            cmd.Parameters.AddWithValue("@ID", call.ID);
                            int i = cmd.ExecuteNonQuery();
                            Console.WriteLine("{0} records updated.", i);
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return call;
        }

    }
}
