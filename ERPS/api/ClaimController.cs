﻿using System;
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
using EVSTAR.DB.NET;

namespace ERPS.api
{
    public class ClaimController : ApiController
    {
        // GET api/<controller>
        public List<Claim> Get()
        {
            List<Claim> claims = null;
            try
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                string customer = DBHelper.GetStringValue(HttpContext.Current.Request.Params["customer"]);
                string op = DBHelper.GetStringValue(HttpContext.Current.Request.Params["open"]);
                int customerID = 0;
                Int32.TryParse(customer, out customerID);

                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return claims;
                }
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
                claims = GetClaimsForCustomer(customerID, clientCode);
                List<Claim> openClaims = new List<Claim>();
                if (op.ToLower() == "open")
                {
                    foreach (Claim claim in claims) 
                    {
                        ClaimStatusHistory lastStatus = claim.StatusHistory.OrderByDescending(x => x.StatusDate).FirstOrDefault();
                        if (lastStatus != null && (lastStatus.Status.ToUpper() == "OPEN"))
                            openClaims.Add(claim);
                    }
                    claims = openClaims;
                }
            }
            catch (Exception ex)
            {
            }
            return claims;
        }

        private List<Claim> GetClaimsForCustomer(int customerID, string clientCode)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Claims WITH(NOLOCK) ");
                    sql.AppendLine("WHERE CustomerID = @CustomerID ORDER BY ID DESC");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Claim claim = new Claim(r);
                            claim.StatusHistory = GetClaimStatusHistory(claim.ID, clientCode);
                            claims.Add(claim);
                        }
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return claims;
        }

        private List<ClaimStatusHistory> GetClaimStatusHistory(int claimID, string clientCode)
        {
            List<ClaimStatusHistory> history = new List<ClaimStatusHistory>();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT csh.*, cs.Name as StatusName FROM ClaimStatusHistory csh WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN ClaimStatuses cs ON cs.ID = csh.StatusID ");
                    sql.AppendLine("WHERE csh.ClaimID = @ClaimID ORDER BY ID DESC");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ClaimID", claimID);
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ClaimStatusHistory csh = new ClaimStatusHistory(r);
                            history.Add(csh);
                        }
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return history;
        }

        // GET api/<controller>/id
        public Claim Get(int id)
        {
            Claim claim = null;
            try
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return claim;
                }
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
                claim = GetClaim(id, clientCode);
            }
            catch (Exception ex)
            {
            }
            return claim;
        }

        // POST api/<controller>
        public Claim Post([FromBody] Claim value)
        {
            Claim claim = null;
            try
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return null;
                }

                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
                claim = value; // (Address)JsonConvert.DeserializeObject(value);  
                if (claim != null && claim.CustomerID > 0 && claim.CoverageID > 0 && claim.CoveredProductID > 0)
                {
                    string errorMsg = string.Empty;
                    ClaimHelper claimHelper = new ClaimHelper();
                    claim = claimHelper.Insert(claim, clientCode, out errorMsg);
                }
            }
            catch (Exception ex)
            {
                claim.DenialReason = ex.Message + "\r\n" + ex.StackTrace;
                return claim;
            }
            return claim;
        }

        // Put api/<controller>
        public Claim Put(int claimId, [FromBody] Claim value)
        {
            Claim claim = null;
            try
            {
                string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
                string email = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
                string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
                string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
                string provided = Encryption.MD5(code + email);
                if (hashed != provided)
                {
                    provided = Encryption.MD5(code + phone);
                    if (hashed != provided)
                        return null;
                }
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);

                claim = value; // (Address)JsonConvert.DeserializeObject(value);  
                string errorMsg = string.Empty;

                if (claim != null && claim.CustomerID > 0 && claim.CoverageID > 0 && claim.CoveredProductID > 0)
                {
                    ClaimHelper claimHelper = new ClaimHelper();
                    claim = claimHelper.Insert(claim, clientCode, out errorMsg);
                }
            }
            catch (Exception ex)
            {
                claim.DenialReason = ex.Message + "\r\n" + ex.StackTrace;
                return claim;
            }
            return claim;
        }

        //private ClaimStatusHistory SaveClaimStatus(ClaimStatusHistory status)
        //{
        //    try
        //    {
        //        if (status != null)
        //        {

        //            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
        //            using (SqlConnection con = new SqlConnection(constr))
        //            {
        //                con.Open();
        //                StringBuilder sql = new StringBuilder();
        //                sql.AppendLine("INSERT INTO ClaimStatusHistory ");
        //                sql.AppendLine("(ClaimID, StatusID, StatusDate, UserName) ");
        //                sql.AppendLine("VALUES (@ClaimID, @StatusID, @StatusDate, @UserName); ");
        //                sql.AppendLine("SELECT SCOPE_IDENTITY() ");
        //                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
        //                {
        //                    cmd.CommandType = CommandType.Text;
        //                    cmd.Parameters.AddWithValue("@ClaimID", status.ClaimID);
        //                    cmd.Parameters.AddWithValue("@StatusID", status.StatusID);
        //                    cmd.Parameters.AddWithValue("@StatusDate", status.StatusDate);
        //                    cmd.Parameters.AddWithValue("@UserName", status.UserName);
        //                    status.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
        //                }

        //                sql.Clear();
        //                sql.AppendLine("UPDATE Claims SET LastUpdated = GETDATE() WHERE ID=@ClaimID");
        //                using (SqlCommand cmd2 = new SqlCommand(sql.ToString(), con))
        //                {
        //                    cmd2.CommandType = CommandType.Text;
        //                    cmd2.Parameters.AddWithValue("@ClaimID", status.ClaimID);
        //                    cmd2.ExecuteNonQuery();
        //                }
        //                con.Close();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return status;
        //    }
        //    return status;
        //}

        private Claim GetClaim(int claimID, string statusCode)
        {
            Claim claim = null;

            string constr = ConfigurationManager.ConnectionStrings[statusCode].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Claims WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", claimID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        claim = new Claim(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return claim;
        }
    }
}
