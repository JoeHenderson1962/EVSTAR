using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EVSTAR.Models;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Claims;
using System.Runtime.Remoting.Channels;

namespace EVSTAR.DB.NET
{
    public class ClaimHelper
    {
        public List<EVSTAR.Models.Claim> Select(int id, int customerId, int productId, string clientCode, out string errorMsg)
        {
            List<EVSTAR.Models.Claim> result = new List<EVSTAR.Models.Claim>();
            errorMsg = string.Empty;
            try
            {
                CustomerHelper customerHelper = new CustomerHelper();
                CoveredProductHelper coveredProductHelper = new CoveredProductHelper();
                AddressHelper addressHelper = new AddressHelper();
                CoveredPerilHelper perilHelper = new CoveredPerilHelper();
                PerilSubCategoryHelper perilSubHelper = new PerilSubCategoryHelper();
                CoverageHelper coverageHelper = new CoverageHelper();
                DenialReasonHelper denialReasonHelper = new DenialReasonHelper();
                ClaimStatusHistoryHelper statusHistoryHelper = new ClaimStatusHistoryHelper();

                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT c.*, pc.Subcategory, dr.[Name] AS DenialReason FROM Claims c WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN PerilSubCategories pc WITH(NOLOCK) ON pc.ID = c.PerilSubcategoryID ");
                    sql.AppendLine("LEFT JOIN DenialReasons dr WITH(NOLOCK) ON dr.ID = c.DenialReasonID ");
                    if (id > 0)
                        sql.AppendLine("WHERE c.ID=@ID ");

                    if (customerId > 0)
                        sql.AppendLine("WHERE c.CustomerID=@CustomerID ");

                    if (productId > 0)
                        sql.AppendLine("WHERE c.CoveredProductID=@CoveredProductID ");

                    sql.AppendLine("ORDER BY c.ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);
                        if (customerId > 0)
                            cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        if (productId > 0)
                            cmd.Parameters.AddWithValue("@CoveredProductID", productId);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            EVSTAR.Models.Claim claim = new EVSTAR.Models.Claim(r);
                            if (claim.CustomerID > 0)
                            {
                                List<Customer> customerResults = customerHelper.Select("", "", "", claim.CustomerID, 0,
                                    clientCode, out errorMsg);
                                if (customerResults != null && customerResults.Count > 0)
                                {
                                    claim.ClaimCustomer = customerResults[0];
                                }
                            }
                            if (claim.CoveredProductID > 0)
                            {
                                List<CoveredProduct> products = coveredProductHelper.Select(claim.CoveredProductID, clientCode, out errorMsg);
                                if (products != null && products.Count > 0)
                                {
                                    claim.ClaimProduct = products[0];
                                }
                            }
                            if (claim.AddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(claim.AddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                {
                                    claim.ClaimAddress = addresses[0];
                                }
                            }
                            if (claim.CoveredPerilID > 0)
                            {
                                List<CoveredPeril> coveredPerils = perilHelper.Select(claim.CoveredPerilID, 0, string.Empty, clientCode, out errorMsg);
                                if (coveredPerils != null && coveredPerils.Count > 0)
                                {
                                    claim.ClaimPeril = coveredPerils[0];
                                }
                            }
                            if (claim.PerilSubcategoryID > 0)
                            {
                                List<PerilSubCategory> subCategories = perilSubHelper.Select(claim.PerilSubcategoryID, clientCode, out errorMsg);
                                if (subCategories != null && subCategories.Count > 0)
                                {
                                    claim.ClaimPerilSubCategory = subCategories[0];
                                }
                            }
                            if (claim.CoverageID > 0)
                            {
                                List<Coverage> coverages = coverageHelper.Select(claim.CoverageID, 0, 0, 0, clientCode, out errorMsg);
                                if (coverages != null && coverages.Count > 0)
                                {
                                    claim.ClaimCoverage = coverages[0];
                                }
                            }
                            if (claim.DenialReasonID > 0)
                            {
                                List<DenialReason> denialReasons = denialReasonHelper.Select(claim.DenialReasonID, clientCode, out errorMsg);
                                if (denialReasons != null && denialReasons.Count > 0)
                                {
                                    claim.ClaimDenialReason = denialReasons[0];
                                }
                            }
                            List<ClaimStatusHistory> statusHistories = statusHistoryHelper.Select(0, claim.ID, clientCode, out errorMsg);
                            if (statusHistories != null && statusHistories.Count > 0)
                            {
                                claim.StatusHistory = statusHistories;
                            }
                            result.Add(claim);
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

        public int SelectClosedClaimCountLastYear(int coverageId, string clientCode, out string errorMsg)
        {
            int result = 0;
            errorMsg = string.Empty;
            try
            {

                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT DISTINCT rreq.ID  FROM Claims rreq ");
                    sql.AppendLine("LEFT JOIN ClaimStatusHistory rsh ON rsh.ClaimID=rreq.ID ");
                    sql.AppendLine("WHERE CoverageID=@CoverageID AND rsh.StatusID = 6 AND ");
                    sql.AppendLine("rsh.StatusDate > DATEADD(\"yy\", -1, GETDATE()) AND rsh.StatusDate <= GETDATE()");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@CoverageID", coverageId);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            result++;
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

        public EVSTAR.Models.Claim Insert(EVSTAR.Models.Claim data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("INSERT INTO Claims (");
                        sql.AppendLine("CustomerID, CoveredProductID, LocalRepair, AddressID, DateSubmitted, DateCompleted, DateReturnedToCustomer, ");
                        sql.AppendLine("DateReceivedAtTCS, CoveredPerilID, PasscodeDisabled, Passcode, DateOfLoss, RepairVendorID, InboundTrackingNumber, ");
                        sql.AppendLine("OutboundTrackingNumber, PerilSubcategoryID, DenialReason, DateDenied, RepairShoprTicketID, SendToRS, Deductible, ");
                        sql.AppendLine("CoverageID, EventDate, PoliceReportDate, PoliceReportInfo, EventDescription, UserName, DenialReasonID, StoreID, ");
                        sql.AppendLine("StoreRepID, DeductiblePaidDate, ReplacementProductID, ProgrammingFee, EquipmentCost, ActivationFee, ReplacedESN, ");
                        sql.AppendLine("ReimbursementMethod, ReimbursementAmount, ReimbursementAccount, DateNoPaid, DateCancelled ");
                        sql.AppendLine(") VALUES (");
                        sql.AppendLine("@CustomerID, @CoveredProductID, @LocalRepair, @AddressID, @DateSubmitted, @DateCompleted, @DateReturnedToCustomer, ");
                        sql.AppendLine("@DateReceivedAtTCS, @CoveredPerilID, @PasscodeDisabled, @Passcode, @DateOfLoss, @RepairVendorID, @InboundTrackingNumber, ");
                        sql.AppendLine("@OutboundTrackingNumber, @PerilSubcategoryID, @DenialReason, @DateDenied, @RepairShoprTicketID, @SendToRS, @Deductible, ");
                        sql.AppendLine("@CoverageID, @EventDate, @PoliceReportDate, @PoliceReportInfo, @EventDescription, @UserName, @DenialReasonID, @StoreID, ");
                        sql.AppendLine("@StoreRepID, @DeductiblePaidDate, @ReplacementProductID, @ProgrammingFee, @EquipmentCost, @ActivationFee, @ReplacedESN, ");
                        sql.AppendLine("@ReimbursementMethod, @ReimbursementAmount, @ReimbursementAccount, @DateNoPaid, @DateCancelled ");
                        sql.AppendLine("); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            if (data.CustomerID > 0)
                                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                            else
                                cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);
                            if (data.CoveredProductID > 0)
                                cmd.Parameters.AddWithValue("@CoveredProductID", data.CoveredProductID);
                            else
                                cmd.Parameters.AddWithValue("@CoveredProductID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@LocalRepair", data.LocalRepair);
                            if (data.AddressID > 0)
                                cmd.Parameters.AddWithValue("@AddressID", data.AddressID);
                            else
                                cmd.Parameters.AddWithValue("@AddressID", DBNull.Value);

                            if (data.DateSubmitted != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateSubmitted", data.DateSubmitted);
                            else
                                cmd.Parameters.AddWithValue("@DateSubmitted", DBNull.Value);

                            if (data.DateCompleted != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateCompleted", data.DateCompleted);
                            else
                                cmd.Parameters.AddWithValue("@DateCompleted", DBNull.Value);

                            if (data.DateReturnedToCustomer != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateReturnedToCustomer", data.DateReturnedToCustomer);
                            else
                                cmd.Parameters.AddWithValue("@DateReturnedToCustomer", DBNull.Value);

                            if (data.DateReceivedAtTCS != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateReceivedAtTCS", data.DateReceivedAtTCS);
                            else
                                cmd.Parameters.AddWithValue("@DateReceivedAtTCS", DBNull.Value);

                            if (data.DateNoPaid != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateNoPaid", data.DateNoPaid);
                            else
                                cmd.Parameters.AddWithValue("@DateNoPaid", DBNull.Value);

                            if (data.DateCancelled != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateCancelled", data.DateCancelled);
                            else
                                cmd.Parameters.AddWithValue("@DateCancelled", DBNull.Value);

                            if (data.CoveredPerilID > 0)
                                cmd.Parameters.AddWithValue("@CoveredPerilID", data.CoveredPerilID);
                            else
                                cmd.Parameters.AddWithValue("@CoveredPerilID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@PasscodeDisabled", data.PassCodeDisabled);
                            cmd.Parameters.AddWithValue("@Passcode", data.PassCode);

                            if (data.DateOfLoss != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateOfLoss", data.DateOfLoss);
                            else
                                cmd.Parameters.AddWithValue("@DateOfLoss", DBNull.Value);

                            if (data.RepairVendorID > 0)
                                cmd.Parameters.AddWithValue("@RepairVendorID", data.RepairVendorID);
                            else
                                cmd.Parameters.AddWithValue("@RepairVendorID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@InboundTrackingNumber", data.InboundTrackingNumber);
                            cmd.Parameters.AddWithValue("@OutboundTrackingNumber", data.OutboundTrackingNumber);
                            cmd.Parameters.AddWithValue("@PerilSubcategoryID", data.PerilSubcategoryID);
                            cmd.Parameters.AddWithValue("@DenialReason", data.DenialReason);

                            if (data.DateDenied != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateDenied", data.DateDenied);
                            else
                                cmd.Parameters.AddWithValue("@DateDenied", DBNull.Value);

                            if (data.RepairShoprTicketID > 0)
                                cmd.Parameters.AddWithValue("@RepairShoprTicketID", data.RepairShoprTicketID);
                            else
                                cmd.Parameters.AddWithValue("@RepairShoprTicketID", DBNull.Value);

                            cmd.Parameters.AddWithValue("@SendToRS", data.SendToRS);
                            cmd.Parameters.AddWithValue("@Deductible", data.Deductible);
                            cmd.Parameters.AddWithValue("@CoverageID", data.CoverageID);

                            if (data.DateOfLoss != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EventDate", data.DateOfLoss);
                            else
                                cmd.Parameters.AddWithValue("@EventDate", DBNull.Value);

                            if (data.PoliceReportDate.HasValue && data.PoliceReportDate.Value != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@PoliceReportDate", data.PoliceReportDate);
                            else
                                cmd.Parameters.AddWithValue("@PoliceReportDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@PoliceReportInfo", data.PoliceReportInfo);
                            cmd.Parameters.AddWithValue("@EventDescription", data.EventDescription);
                            cmd.Parameters.AddWithValue("@UserName", data.UserName);

                            if (data.DenialReasonID > 0)
                                cmd.Parameters.AddWithValue("@DenialReasonID", data.DenialReasonID);
                            else
                                cmd.Parameters.AddWithValue("@DenialReasonID", DBNull.Value);
                            if (data.StoreID > 0)
                                cmd.Parameters.AddWithValue("@StoreID", data.StoreID);
                            else
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@StoreRepID", data.StoreRepID);

                            if (data.DeductiblePaidDate.HasValue && data.DeductiblePaidDate.Value != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DeductiblePaidDate", data.DeductiblePaidDate);
                            else
                                cmd.Parameters.AddWithValue("@DeductiblePaidDate", DBNull.Value);

                            if (data.ReplacementProductID > 0)
                                cmd.Parameters.AddWithValue("@ReplacementProductID", data.ReplacementProductID);
                            else
                                cmd.Parameters.AddWithValue("@ReplacementProductID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ProgrammingFee", data.ProgrammingFee);
                            cmd.Parameters.AddWithValue("@EquipmentCost", data.EquipmentCost);
                            cmd.Parameters.AddWithValue("@ActivationFee", data.ActivationFee);
                            cmd.Parameters.AddWithValue("@ReimbursementMethod", data.ReimbursementMethod);
                            cmd.Parameters.AddWithValue("@ReimbursementAmount", data.ReimbursementAmount);
                            cmd.Parameters.AddWithValue("@ReplacedESN", data.ReplacedESN);
                            cmd.Parameters.AddWithValue("@ReimbursementAccount", data.ReimbursementAccount);
                            data.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                            data = UpdateClaimStatusHistory(data, clientCode, out errorMsg);
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return data;
            }
            return data;
        }

        private EVSTAR.Models.Claim UpdateClaimStatusHistory(EVSTAR.Models.Claim data, string clientCode, out string errorMsg)
        {
            errorMsg = String.Empty;
            ClaimStatusHistoryHelper cshh = new ClaimStatusHistoryHelper();
            List<ClaimStatusHistory> csh = cshh.Select(0, data.ID, clientCode, out errorMsg);
            ClaimStatusHistory oldHist = csh.OrderByDescending(x => x.StatusDate).FirstOrDefault();
            //ClaimStatusHistory curHist = data.StatusHistory != null && data.StatusHistory.Count > 0 ? 
            //    data.StatusHistory[data.StatusHistory.Count - 1] : null;

            //if (curHist != oldHist)
            //{
            //    if (data.DateSubmitted > DateTime.Parse("2020-01-01") && oldHist != null && 
            //        ((oldHist.StatusID == 1 && !data.LocalRepair) || (oldHist.StatusID == 7 && !data.LocalRepair))) // Open or Receipt Submitted
            //    {
            //        data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
            //        {
            //            ClaimID = data.ID,
            //            StatusID = 3,
            //            Status = "APPROVED",
            //            StatusDate = DateTime.Now,
            //            UserName = data.UserName
            //        }, out errorMsg));
            //    }
            //    else if (data.DateCompleted > DateTime.Parse("2020-01-01") && oldHist != null && oldHist.StatusID != 5) // Closed
            //    {
            //        data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
            //        {
            //            ClaimID = data.ID,
            //            StatusID = 5,
            //            Status = "CLOSED",
            //            StatusDate = DateTime.Now,
            //            UserName = data.UserName
            //        }, out errorMsg));
            //    }
            //    else if (data.DateDenied > DateTime.Parse("2020-01-01") && oldHist != null && oldHist.StatusID != 4) // Denied
            //    {
            //        data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
            //        {
            //            ClaimID = data.ID,
            //            StatusID = 4,
            //            Status = "DENIED",
            //            StatusDate = DateTime.Now,
            //            UserName = data.UserName
            //        }, out errorMsg));
            //    }
            //    else if (data.DateCancelled > DateTime.Parse("2020-01-01") && oldHist != null && oldHist.StatusID != 6) // Cancelled
            //    {
            //        data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
            //        {
            //            ClaimID = data.ID,
            //            StatusID = 6,
            //            Status = "CANCELLED",
            //            StatusDate = DateTime.Now,
            //            UserName = data.UserName
            //        }, out errorMsg));
            //    }
            //    else if (data.DateCancelled > DateTime.Parse("2020-01-01") && oldHist != null && oldHist.StatusID != 7) // Auto Nopaid
            //    {
            //        data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
            //        {
            //            ClaimID = data.ID,
            //            StatusID = 7,
            //            Status = "AUTO-NOPAY",
            //            StatusDate = DateTime.Now,
            //            UserName = data.UserName
            //        }, out errorMsg));
            //    }
            //}
            if (data.StatusHistory != null && data.StatusHistory.Count > 0)
            {
                ClaimStatusHistory hist = data.StatusHistory.OrderByDescending(x => x.StatusDate).FirstOrDefault();
                if (oldHist != null && hist.Status != oldHist.Status && hist.StatusID > oldHist.StatusID)
                {
                    data.StatusHistory[data.StatusHistory.Count - 1] = cshh.Insert(hist, clientCode, out errorMsg);
                }

                for (int i = 0; i < data.StatusHistory.Count; i++)
                {
                    if (data.StatusHistory[i].ID == 0)
                    {
                        data.StatusHistory[i] = cshh.Insert(data.StatusHistory[i], clientCode, out errorMsg);
                    }
                }
            }
            else
            {
                data.StatusHistory = new List<ClaimStatusHistory>();
                data.StatusHistory.Add(cshh.Insert(new ClaimStatusHistory()
                {
                    ClaimID = data.ID,
                    StatusID = 1,
                    Status = "OPEN",
                    StatusDate = DateTime.Now,
                    UserName = data.UserName
                }, clientCode, out errorMsg));
            }
            data.StatusHistory = cshh.Select(0, data.ID, clientCode, out errorMsg);

            return data;
        }

        public EVSTAR.Models.Claim Update(EVSTAR.Models.Claim data, string clientCode, out string errorMsg)
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
                        sql.AppendLine("UPDATE Claims SET");
                        sql.AppendLine("CustomerID=@CustomerID, CoveredProductID=@CoveredProductID, LocalRepair=@LocalRepair, ");
                        sql.AppendLine("AddressID=@AddressID, DateSubmitted=@DateSubmitted, DateCompleted=@DateCompleted, ");
                        sql.AppendLine("DateReturnedToCustomer=@DateReturnedToCustomer, DateReceivedAtTCS=@DateReceivedAtTCS, ");
                        sql.AppendLine("CoveredPerilID=@CoveredPerilID, PasscodeDisabled=@PasscodeDisabled, Passcode=@Passcode, ");
                        sql.AppendLine("DateOfLoss=@DateOfLoss, RepairVendorID=@RepairVendorID, InboundTrackingNumber=@InboundTrackingNumber, ");
                        sql.AppendLine("OutboundTrackingNumber=@OutboundTrackingNumber, PerilSubcategoryID=@PerilSubcategoryID, ");
                        sql.AppendLine("DenialReason=@DenialReason, DateDenied=@DateDenied, RepairShoprTicketID=@RepairShoprTicketID, ");
                        sql.AppendLine("SendToRS=@SendToRS, Deductible=@Deductible, CoverageID=@CoverageID, EventDate=@EventDate, ");
                        sql.AppendLine("PoliceReportDate=@PoliceReportDate, PoliceReportInfo=@PoliceReportInfo, EventDescription=@EventDescription, ");
                        sql.AppendLine("UserName=@UserName, DenialReasonID=@DenialReasonID, StoreID=@StoreID, StoreRepID=@StoreRepID, ");
                        sql.AppendLine("DeductiblePaidDate=@DeductiblePaidDate, ReplacementProductID=@ReplacementProductID, ");
                        sql.AppendLine("ProgrammingFee=@ProgrammingFee, EquipmentCost=@EquipmentCost, ActivationFee=@ActivationFee, ");
                        sql.AppendLine("ReplacedESN=@ReplacedESN, ReimbursementMethod=@ReimbursementMethod, ReimbursementAmount=@ReimbursementAmount, ");
                        sql.AppendLine("ReimbursementAccount=@ReimbursementAccount, DateNoPaid=@DateNoPaid, DateCancelled=@DateCancelled");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            if (data.CustomerID > 0)
                                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                            else
                                cmd.Parameters.AddWithValue("@CustomerID", DBNull.Value);
                            if (data.CoveredProductID > 0)
                                cmd.Parameters.AddWithValue("@CoveredProductID", data.CoveredProductID);
                            else
                                cmd.Parameters.AddWithValue("@CoveredProductID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@LocalRepair", data.LocalRepair);
                            if (data.AddressID > 0)
                                cmd.Parameters.AddWithValue("@AddressID", data.AddressID);
                            else
                                cmd.Parameters.AddWithValue("@AddressID", DBNull.Value);

                            if (data.DateSubmitted != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateSubmitted", data.DateSubmitted);
                            else
                                cmd.Parameters.AddWithValue("@DateCompleted", DBNull.Value);

                            if (data.DateCompleted != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateCompleted", data.DateCompleted);
                            else
                                cmd.Parameters.AddWithValue("@DateCompleted", DBNull.Value);

                            if (data.DateReturnedToCustomer != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateReturnedToCustomer", data.DateReturnedToCustomer);
                            else
                                cmd.Parameters.AddWithValue("@DateReturnedToCustomer", DBNull.Value);

                            if (data.DateReceivedAtTCS != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateReceivedAtTCS", data.DateReceivedAtTCS);
                            else
                                cmd.Parameters.AddWithValue("@DateReceivedAtTCS", DBNull.Value);

                            if (data.CoveredPerilID > 0)
                                cmd.Parameters.AddWithValue("@CoveredPerilID", data.CoveredPerilID);
                            else
                                cmd.Parameters.AddWithValue("@CoveredPerilID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@PasscodeDisabled", data.PassCodeDisabled);
                            cmd.Parameters.AddWithValue("@Passcode", data.PassCode);

                            if (data.DateOfLoss != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateOfLoss", data.DateOfLoss);
                            else
                                cmd.Parameters.AddWithValue("@DateOfLoss", DBNull.Value);

                            if (data.RepairVendorID > 0)
                                cmd.Parameters.AddWithValue("@RepairVendorID", data.RepairVendorID);
                            else
                                cmd.Parameters.AddWithValue("@RepairVendorID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@InboundTrackingNumber", data.InboundTrackingNumber);
                            cmd.Parameters.AddWithValue("@OutboundTrackingNumber", data.OutboundTrackingNumber);
                            cmd.Parameters.AddWithValue("@PerilSubcategoryID", data.PerilSubcategoryID);
                            cmd.Parameters.AddWithValue("@DenialReason", data.DenialReason);

                            if (data.DateNoPaid != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateNoPaid", data.DateNoPaid);
                            else
                                cmd.Parameters.AddWithValue("@DateNoPaid", DBNull.Value);

                            if (data.DateCancelled != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateCancelled", data.DateCancelled);
                            else
                                cmd.Parameters.AddWithValue("@DateCancelled", DBNull.Value);

                            if (data.DateDenied != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DateDenied", data.DateDenied);
                            else
                                cmd.Parameters.AddWithValue("@DateDenied", DBNull.Value);

                            if (data.RepairShoprTicketID > 0)
                                cmd.Parameters.AddWithValue("@RepairShoprTicketID", data.RepairShoprTicketID);
                            else
                                cmd.Parameters.AddWithValue("@RepairShoprTicketID", DBNull.Value);

                            cmd.Parameters.AddWithValue("@SendToRS", data.SendToRS);
                            cmd.Parameters.AddWithValue("@Deductible", data.Deductible);
                            cmd.Parameters.AddWithValue("@CoverageID", data.CoverageID);

                            if (data.EventDate.HasValue && data.EventDate.Value != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@EventDate", data.EventDate);
                            else
                                cmd.Parameters.AddWithValue("@EventDate", DBNull.Value);

                            if (data.PoliceReportDate.HasValue && data.PoliceReportDate.Value != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@PoliceReportDate", data.PoliceReportDate);
                            else
                                cmd.Parameters.AddWithValue("@PoliceReportDate", DBNull.Value);

                            cmd.Parameters.AddWithValue("@PoliceReportInfo", data.PoliceReportInfo);
                            cmd.Parameters.AddWithValue("@EventDescription", data.EventDescription);
                            cmd.Parameters.AddWithValue("@UserName", data.UserName);

                            if (data.DenialReasonID > 0)
                                cmd.Parameters.AddWithValue("@DenialReasonID", data.DenialReasonID);
                            else
                                cmd.Parameters.AddWithValue("@DenialReasonID", DBNull.Value);
                            if (data.StoreID > 0)
                                cmd.Parameters.AddWithValue("@StoreID", data.StoreID);
                            else
                                cmd.Parameters.AddWithValue("@StoreID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@StoreRepID", data.StoreRepID);

                            if (data.DeductiblePaidDate.HasValue && data.DeductiblePaidDate.Value != DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@DeductiblePaidDate", data.DeductiblePaidDate);
                            else
                                cmd.Parameters.AddWithValue("@DeductiblePaidDate", DBNull.Value);

                            if (data.ReplacementProductID > 0)
                                cmd.Parameters.AddWithValue("@ReplacementProductID", data.ReplacementProductID);
                            else
                                cmd.Parameters.AddWithValue("@ReplacementProductID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ProgrammingFee", data.ProgrammingFee);
                            cmd.Parameters.AddWithValue("@EquipmentCost", data.EquipmentCost);
                            cmd.Parameters.AddWithValue("@ActivationFee", data.ActivationFee);
                            cmd.Parameters.AddWithValue("@ReplacedESN", data.ReplacedESN);
                            cmd.Parameters.AddWithValue("@ReimbursementMethod", data.ReimbursementMethod);
                            cmd.Parameters.AddWithValue("@ReimbursementAmount", data.ReimbursementAmount);
                            cmd.Parameters.AddWithValue("@ReimbursementAccount", data.ReimbursementAccount);
                            cmd.Parameters.AddWithValue("@ID", data.ID);
                            cmd.ExecuteNonQuery();

                            data = UpdateClaimStatusHistory(data, clientCode, out errorMsg);

                            if (data != null && data.ClaimPeril == null && data.CoveredPerilID > 0)
                            {
                                CoveredPerilHelper cph = new CoveredPerilHelper();
                                data.ClaimPeril = cph.Select(data.CoveredPerilID, 0, string.Empty, clientCode, out errorMsg).FirstOrDefault();
                            }
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
