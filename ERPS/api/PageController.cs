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
using EVSTAR.DB.NET;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Antlr.Runtime.Tree;
using System.Web.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Web.UI;
using System.Web.Optimization;
using System.Diagnostics;

namespace ERPS.api
{
    public class PageController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            string result = string.Empty;
            try
            {
                string user = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["user"]);
                string customer = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["customer"]);
                string call = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["call"]);
                string language = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["language"]);
                string client = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["client"]);
                string name = DBHelper.GetStringValue(HttpContext.Current.Request.Params["name"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);

                int userID = 0;
                int customerID = 0;
                int callID = 0;
                int languageID = 0;
                int clientID = 0;

                Int32.TryParse(customer, out customerID);
                Int32.TryParse(call, out callID);
                Int32.TryParse(user, out userID);
                Int32.TryParse(client, out clientID);
                Int32.TryParse(language, out languageID);

                if (userID > 0)
                {
                    string errorMsg = string.Empty;
                    UserHelper uh = new UserHelper();
                    List<EVSTAR.Models.User> users = uh.Select(string.Empty, string.Empty, userID, clientCode, out errorMsg);
                    if (users.Count > 0 && users[0] != null)
                    {
                        string path = HostingEnvironment.MapPath(String.Format("~/{0}.html", name));
                        FileInfo fi = new FileInfo(path);
                        if (fi.Exists)
                        {
                            string fileData = string.Empty;

                            using (StreamReader sr = new StreamReader(path))
                            {
                                string html = sr.ReadToEnd();
                                html = html.Replace("<!DOCTYPE html>", "");
                                html = html.Replace("<html>", "");
                                html = html.Replace("<body>", "");
                                html = html.Replace("</html>", "");
                                html = html.Replace("</body>", "");
                                fileData = html.Trim();
                                sr.Close();
                            }

                            if (fileData.Length > 0)
                            {
                                Call theCall = null;
                                if (callID > 0)
                                {
                                    CallHelper ch = new CallHelper();
                                    List<Call> calls = ch.Select(callID, 0, 0, DateTime.MinValue, DateTime.MinValue, clientCode, out errorMsg);
                                    if (calls != null && calls.Count > 0)
                                        theCall = calls[0];
                                    else
                                        result = String.Format("Unable to retrieve the call information.");
                                }

                                switch (name.ToUpper())
                                {
                                    case "ACCOUNTNOTFOUND":
                                        if (theCall != null)
                                            result = LoadAccountNotFound(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CALLERNAME":
                                        result = LoadCallerName(users[0], fileData, languageID, clientID, clientCode);
                                        break;

                                    case "ACCOUNTSEARCH":
                                        if (theCall != null)
                                            result = LoadAccountSearch(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CALLERADDRESS":
                                        if (theCall != null)
                                            result = LoadCallerAddress(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CUSTOMEREMAIL":
                                        if (theCall != null)
                                            result = LoadCustomerEmail(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CUSTOMERMATCH":
                                        if (theCall != null)
                                            result = LoadCustomerMatch(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "STARTREPLACEMENT":
                                        if (theCall != null)
                                            result = LoadStartReplacement(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "FINISHREPLACEMENT":
                                        if (theCall != null)
                                            result = LoadFinishReplacement(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "REPLACEMENTEVENTDATE":
                                        if (theCall != null)
                                            result = LoadReplacementEventDate(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "REPLACEMENTEQUIPMENT":
                                        if (theCall != null)
                                            result = LoadReplacementEquipment(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "SELECTFULFILLMENT":
                                        if (theCall != null)
                                            result = LoadSelectFulfillment(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "FULFILLMENTINSTRUCTIONS":
                                        if (theCall != null)
                                            result = LoadFulfillmentInstructions(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "SUBMITCLAIM":
                                        if (theCall != null)
                                            result = LoadSubmitClaim(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CLAIMENDCALL":
                                        if (theCall != null)
                                            result = LoadClaimEndCall(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "ENDCALL":
                                        if (theCall != null)
                                            result = LoadEndCall(users[0], theCall, fileData, languageID, clientCode);
                                        break;

                                    case "CSRCALLS":
                                        result = LoadCSRCalls(users[0], clientCode);
                                        break;

                                    case "CUSTOMERCALLS":
                                        if (theCall != null)
                                            result = LoadCustomerCalls(users[0], theCall, clientCode);
                                        break;

                                    case "CUSTOMERCLAIMS":
                                        if (theCall != null)
                                            result = LoadCustomerClaims(users[0], theCall, clientCode);
                                        break;

                                    default: break;
                                }
                            }
                            else
                                result = String.Format("File {0}.html was empty.", name);
                        }
                        else
                            result = String.Format("Could not find file {0}.html", name);
                    }
                    else
                        result = errorMsg;
                }
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadCallerAddress(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();

                CustomerHelper ch = new CustomerHelper();
                List<Customer> custs = ch.Select(string.Empty, string.Empty, call.SearchMDN, 0, call.ClientID, clientCode, out errorMsg);
                if (custs != null && custs.Count > 0)
                {
                    call.CustomerID = custs[0].ID;
                    call.CallCustomer = custs[0];
                }

                if (call.CustomerID == 0 || call.CallCustomer == null)
                {
                    result = LoadAccountNotFound(user, call, fileData, languageID, clientCode);
                    result = result.Replace("[CUSTOMERTABLE]", "");
                }
                else
                {
                    Script script1 = sh.Select("SearchRecordsFound", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                    {
                        string callername = call.CallerName;
                        if (callername.Contains(" "))
                        {
                            string[] parts = callername.Split(' ');
                            if (parts.Length >= 1)
                                callername = parts[0];
                        }
                        string text = script1.Text.Replace("|CALLERNAME|", callername);
                        result = result.Replace("[CUSTOMERSCRIPT]", text);
                        result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                        CustomerHelper customerHelper = new CustomerHelper();
                        List<Customer> customers = customerHelper.Select(string.Empty, string.Empty, call.SearchMDN, 0, call.ClientID, clientCode, out errorMsg);
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            Script script2 = null;
                            if (customers != null && customers.Count > 0)
                            {
                                if (customers.Count == 1)
                                    script2 = sh.Select("OneCustomerFound", languageID, call.ClientID, clientCode, out errorMsg);
                                else
                                    script2 = sh.Select("CustomersFound", languageID, call.ClientID, clientCode, out errorMsg);

                                if (script2 != null)
                                {
                                    Script csrScript = sh.Select("InfoSearchRecordsFound", languageID, call.ClientID, clientCode, out errorMsg);
                                    if (string.IsNullOrEmpty(errorMsg) && csrScript != null)
                                    {
                                        result = result.Replace("[CSRSCRIPT]", csrScript.Text + "<br />" + script2.Text);
                                    }
                                }
                            }
                        }

                        StringBuilder custTable = new StringBuilder();
                        custTable.AppendLine("<table style=\"width:100%; border: 1px solid lightgray; border-radius: 6px;\" cellpadding=\"3\" cellspacing=\"0\" >");
                        custTable.AppendLine("<tr>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("<b>CUSTOMER&nbsp;NAME</b>");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("<b>ADDRESS</b>");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("<b>CITY,&nbsp;STATE&nbsp;ZIP</b>");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("<b>ACCOUNT&nbsp;#</b>");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("<th style=\"border:1px solid lightgray;text-align:center; vertical-align:top;\">");
                        custTable.AppendLine("<b>STATUS</b>");
                        custTable.AppendLine("</th>");
                        custTable.AppendLine("</tr>");

                        CoverageHelper covHelper = new CoverageHelper();
                        string emails = string.Empty;
                        foreach (Customer customer in customers)
                        {
                            string address = string.Empty;
                            string citystatezip = string.Empty;
                            emails += String.Format("{0}|{1},", customer.ID, customer.Email);
                            if (customer.BillingAddress != null)
                            {
                                address = string.Format("{0}{1}", customer.BillingAddress.Line1,
                                    String.IsNullOrEmpty(customer.BillingAddress.Line2) ? "" : "<br />" + customer.BillingAddress.Line2).Trim().Replace(" ", "&nbsp;");
                                citystatezip = string.Format("{0}, {1} {2}", customer.BillingAddress.City,
                                    customer.BillingAddress.State, customer.BillingAddress.PostalCode).Replace(" ", "&nbsp;");
                            }
                            List<Coverage> coverages = covHelper.Select(0, customer.ID, customer.ProgramID, 0, clientCode, out errorMsg);
                            Coverage activeCoverage = null;
                            if (coverages != null && coverages.Count > 0)
                            {
                                foreach (Coverage coverage in coverages)
                                {
                                    if (activeCoverage == null || (coverage.Status == "ENROLLED" && coverage.EffectiveDate > activeCoverage.EffectiveDate))
                                        activeCoverage = coverage;
                                }
                            }

                            // Name, Address, City/State/ZIP, AccountNumber, Coverage Status
                            custTable.AppendLine("<tr>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(String.Format("<input type=\"radio\" id=\"cust_{0}\" name=\"customers\" value=\"{0}\" {1} />", customer.ID, customers.Count == 1 ? "checked " : string.Empty));
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(customer.PrimaryName.Replace(" ", "&nbsp;"));
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(address.Trim());
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(citystatezip.Trim());
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(customer.AccountNumber);
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("<td style=\"border:1px solid lightgray;text-align:left; vertical-align:top;\">");
                            custTable.AppendLine(activeCoverage != null ? activeCoverage.Status : "ENROLLED");
                            custTable.AppendLine("</td>");
                            custTable.AppendLine("</tr>");

                        }
                        custTable.AppendLine("</table>");

                        result = result.Replace("[CUSTOMERTABLE]", custTable.ToString());
                        result = result.Replace("[CUSTOMEREMAIL]", emails);
                    }
                }
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadAccountNotFound(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("VFNoRecordFound", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string callername = call.CallerName;
                    if (callername.Contains(" "))
                    {
                        string[] parts = callername.Split(' ');
                        if (parts.Length >= 1)
                            callername = parts[0];
                    }
                    string text = script1.Text.Replace("|CALLERNAME|", callername);
                    text = text.Replace("|COMPANYNAME|", call.CurrClient.Name);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("EnrollmentConfirmedQuestion", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script2 != null)
                    {
                        result = result.Replace("[CSRSCRIPT]", script2.Text);
                        result = result.Replace("[CSRSCRIPTCSS]", script2.CssClass);
                    }
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadAccountSearch(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("CallStartGetMdn", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string callername = call.CallerName;
                    if (callername.Contains(" "))
                    {
                        string[] parts = callername.Split(' ');
                        if (parts.Length >= 1)
                            callername = parts[0];
                    }
                    string text = script1.Text.Replace("|CALLERNAME|", callername);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadCustomerEmail(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("CollectEmailAddress", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", call.CsrName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadCustomerMatch(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("MatchFound", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", call.CsrName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("SubscriptionSelectAction1", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                    {
                        text = script2.Text.Replace("|CSRNAME|", call.CsrName);
                        result = result.Replace("[CSRSCRIPT]", text);
                        result = result.Replace("[CSRSCRIPTCSS]", script2.CssClass);

                        CallActionHelper actionHelper = new CallActionHelper();
                        List<CallAction> actions = actionHelper.Select(0, out errorMsg);
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            StringBuilder actionList = new StringBuilder();
                            actionList.AppendLine("<select id=\"selAction\" style=\"width:100%; border: 1px solid lightgray; border-radius: 6px;font-size:18px;\">");

                            foreach (CallAction action in actions)
                            {
                                if (action.ClientID == call.ClientID)
                                {
                                    if ((call.NumOpenReq > 0 || call.NumApprovedReq > 0) && action.VisibleExisting)
                                    {
                                        actionList.AppendLine(String.Format("<option value=\"{0}|{1}\">{2}</option>", action.ActionID, action.ActionName, action.Description));
                                    }
                                    else if (call.NumOpenReq == 0 && action.VisibleNonExisting)
                                        actionList.AppendLine(String.Format("<option value=\"{0}|{1}\">{2}</option>", action.ActionID, action.ActionName, action.Description));
                                }
                            }
                            actionList.AppendLine("</select>");
                            result = result.Replace("[CUSTOMERACTIONS]", actionList.ToString());
                        }
                        else
                            result = errorMsg;
                    }
                    else
                        result = errorMsg;
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadStartReplacement(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("SubscriptionSelectAction2", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", call.CsrName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("SubscriptionSelectAction1", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script2 != null)
                    {
                        text = script2.Text.Replace("|CSRNAME|", call.CsrName);
                        result = result.Replace("[SCRIPT2]", text);
                        result = result.Replace("[SCRIPT2CSS]", script2.CssClass);

                        Script script3 = sh.Select("LossCircumstances", languageID, call.ClientID, clientCode, out errorMsg);
                        if (string.IsNullOrEmpty(errorMsg) && script3 != null)
                        {
                            text = script3.Text.Replace("|CSRNAME|", call.CsrName);
                            result = result.Replace("[SCRIPT3]", text);
                            result = result.Replace("[SCRIPT3CSS]", script3.CssClass);

                            PerilSubCategoryHelper subCategoryHelper = new PerilSubCategoryHelper();

                            CoveredPerilHelper perilHelper = new CoveredPerilHelper();
                            List<CoveredPeril> perils = perilHelper.Select(0, call.CurrCoverage.CoverageProduct.ProductCategoryID,
                                call.CurrCoverage.CoverageProgram.ProgramName, clientCode, out errorMsg);
                            if (string.IsNullOrEmpty(errorMsg))
                            {
                                StringBuilder perilList = new StringBuilder();
                                perilList.AppendLine("<select id=\"selPeril\" style=\"width:600px; border: 1px solid lightgray; border-radius: 6px;font-size: 1.2em;\">");

                                foreach (CoveredPeril coveredPeril in perils)
                                {
                                    perilList.AppendLine(String.Format("<option value=\"{0}\">{1}</option>", coveredPeril.ID, coveredPeril.Peril));
                                }
                                perilList.AppendLine("</select>");
                                result = result.Replace("[EVENTTYPES]", perilList.ToString());

                                List<PerilSubCategory> perilSubCats = subCategoryHelper.Select(0, clientCode, out errorMsg);
                                if (string.IsNullOrEmpty(errorMsg))
                                {
                                    StringBuilder subPerilList = new StringBuilder();
                                    subPerilList.AppendLine("<select id=\"selSubPeril\" style=\"width:600px; border: 1px solid lightgray; border-radius: 6px;font-size: 1.2em; display:none;\">");

                                    foreach (PerilSubCategory subCategory in perilSubCats)
                                    {
                                        if (subCategory.CoveredPerilID == perils[0].ID)
                                            subPerilList.AppendLine(String.Format("<option value=\"{0}\">{1}</option>", subCategory.ID, subCategory.Subcategory));
                                    }
                                    subPerilList.AppendLine("</select>");

                                    result = result.Replace("[SUBTYPES]", subPerilList.ToString());
                                }
                                else
                                    result = errorMsg;
                            }
                            else
                                result = errorMsg;
                        }
                        else
                            result = errorMsg;
                    }
                    else
                        result = errorMsg;
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadFinishReplacement(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;

                ClaimHelper claimHelper = new ClaimHelper();
                List<Claim> claims = claimHelper.Select(0, call.CustomerID, 0, clientCode, out errorMsg);
                Claim claim = null;
                if (claims != null && claims.Count > 0)
                {
                    foreach(Claim c in claims)
                    {
                        foreach (ClaimStatusHistory csh in c.StatusHistory)
                        {
                            if (csh.StatusID == 1) // open
                            {
                                claim = c; 
                                break;
                            }
                        }
                        if (claim != null)
                            break;
                    }
                }

                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("SubscriptionSelectAction2", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", call.CsrName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("SubscriptionSelectAction1", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script2 != null)
                    {
                        text = script2.Text.Replace("|CSRNAME|", call.CsrName);
                        result = result.Replace("[SCRIPT2]", text);
                        result = result.Replace("[SCRIPT2CSS]", script2.CssClass);

                        Script script3 = sh.Select("LossCircumstances", languageID, call.ClientID, clientCode, out errorMsg);
                        if (string.IsNullOrEmpty(errorMsg) && script3 != null)
                        {
                            text = script3.Text.Replace("|CSRNAME|", call.CsrName);
                            result = result.Replace("[SCRIPT3]", text);
                            result = result.Replace("[SCRIPT3CSS]", script3.CssClass);

                            PerilSubCategoryHelper subCategoryHelper = new PerilSubCategoryHelper();

                            CoveredPerilHelper perilHelper = new CoveredPerilHelper();
                            List<CoveredPeril> perils = perilHelper.Select(0, call.CurrCoverage.CoverageProduct.ProductCategoryID,
                                call.CurrCoverage.CoverageProgram.ProgramName, clientCode, out errorMsg);
                            if (string.IsNullOrEmpty(errorMsg))
                            {
                                StringBuilder perilList = new StringBuilder();
                                perilList.AppendLine("<select id=\"selPeril\" style=\"width:600px; border: 1px solid lightgray; border-radius: 6px;font-size: 1.2em;\">");

                                foreach (CoveredPeril coveredPeril in perils)
                                {
                                    perilList.AppendLine(String.Format("<option value=\"{0}\">{1}</option>", coveredPeril.ID, coveredPeril.Peril));
                                }
                                perilList.AppendLine("</select>");
                                result = result.Replace("[EVENTTYPES]", perilList.ToString());

                                List<PerilSubCategory> perilSubCats = subCategoryHelper.Select(0, clientCode, out errorMsg);
                                if (string.IsNullOrEmpty(errorMsg))
                                {
                                    StringBuilder subPerilList = new StringBuilder();
                                    subPerilList.AppendLine("<select id=\"selSubPeril\" style=\"width:600px; border: 1px solid lightgray; border-radius: 6px;font-size: 1.2em; display:none;\">");

                                    foreach (PerilSubCategory subCategory in perilSubCats)
                                    {
                                        if (subCategory.CoveredPerilID == perils[0].ID)
                                            subPerilList.AppendLine(String.Format("<option value=\"{0}\">{1}</option>", subCategory.ID, subCategory.Subcategory));
                                    }
                                    subPerilList.AppendLine("</select>");

                                    result = result.Replace("[SUBTYPES]", subPerilList.ToString());
                                }
                                else
                                    result = errorMsg;
                            }
                            else
                                result = errorMsg;
                        }
                        else
                            result = errorMsg;
                    }
                    else
                        result = errorMsg;
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadCallerName(User user, string fileData, int languageID, int clientID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("CallStartGetName", languageID, clientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", String.Format("{0}", user.FirstName));
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("InfoCallerName", languageID, clientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script2 != null)
                    {
                        result = result.Replace("[CSRSCRIPT]", script2.Text);
                        result = result.Replace("[CSRSCRIPTCSS]", script2.CssClass);
                    }
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadReplacementEventDate(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("ChooseDate", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", user.FirstName);
                    result = result.Replace("[SCRIPT]", text);
                    result = result.Replace("[SCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadReplacementEquipment(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = sh.Select("EquipToReplace", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", user.FirstName);
                    result = result.Replace("[SCRIPT]", text);
                    result = result.Replace("[SCRIPTCSS]", script1.CssClass);

                    Script script2 = sh.Select("EquipToReplaceCSR", languageID, call.ClientID, clientCode, out errorMsg);
                    if (string.IsNullOrEmpty(errorMsg) && script2 != null)
                    {
                        result = result.Replace("[SCRIPT2]", text);
                        result = result.Replace("[SCRIPT2CSS]", script2.CssClass);

                        if (call.CurrCoverage != null && call.CurrCoverage.CoverageProduct != null)
                        {
                            result = result.Replace("|ENROLLEDEQUIPMENTMAKE|", call.CurrCoverage.CoverageProduct.Manufacturer);
                            result = result.Replace("|ENROLLEDEQUIPMENTMODEL|", call.CurrCoverage.CoverageProduct.Model);
                        }

                        call.CallClaim.CoveredProductID = call.CurrCoverage.CoveredProductID;
                        if (call.CurrCoverage.CoverageProduct.IMEI != "")
                            call.CallClaim.ReplacedESN = call.CurrCoverage.CoverageProduct.IMEI;

                        StringBuilder equipmentList = new StringBuilder();
                        equipmentList.AppendLine("<select id=\"selEquipment\" style=\"width:600px; border: 1px solid lightgray; border-radius: 6px;font-size: 1.2em;\">");

                        EquipmentHelper eh = new EquipmentHelper();
                        List<Equipment> equipments = eh.Select(0, call.ClientID, clientCode, out errorMsg);
                        foreach (Equipment equipment in equipments)
                        {
                            if (equipment.ID == call.CurrCoverage.CoverageProduct.EquipmentID)
                                equipmentList.AppendLine(String.Format("<option value=\"{0}\" selected=\"selected\" >{1} {2}</option>", equipment.ID, equipment.Make, equipment.Model));
                            else
                                equipmentList.AppendLine(String.Format("<option value=\"{0}\">{1} {2}</option>", equipment.ID, equipment.Make, equipment.Model));
                        }
                        equipmentList.AppendLine("</select>");
                        result = result.Replace("[EQUIPMENT]", equipmentList.ToString());
                    }
                    else
                        result = errorMsg;
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadSelectFulfillment(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = null;
                if (call.CallClaim != null && call.CallClaim.ClaimPeril != null && !String.IsNullOrEmpty(call.CallClaim.ClaimPeril.Peril)
                    && call.CallClaim.ClaimPeril.Peril.ToUpper().Contains("LIQUID"))
                {
                    script1 = sh.Select("LiquidReimbursement", languageID, call.ClientID, clientCode, out errorMsg);
                }
                else
                {
                    script1 = sh.Select("SelectFulfillment", languageID, call.ClientID, clientCode, out errorMsg);
                }
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", user.FirstName);
                    text = text.Replace("|CLAIMNUMBER|", String.Format("{0}{1}", user.ParentClient.Name.Substring(0, 1), call.CallClaim.ID.ToString("D7")));
                    text = text.Replace("|CALLEREMAIL|", call.CallCustomer.Email);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadFulfillmentInstructions(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = null;
                CoveredPerilHelper cph = new CoveredPerilHelper();
                if (call.CallClaim.ClaimPeril.Peril.ToUpper().Contains("LIQUID"))
                {
                    script1 = sh.Select("ReimbursementMethod", languageID, call.ClientID, clientCode, out errorMsg);

                }
                else
                {
                    if (call.CallClaim.LocalRepair)
                        script1 = sh.Select("LocalRepairFulfillment", languageID, call.ClientID, clientCode, out errorMsg);
                    else
                        script1 = sh.Select("MailInFulfillment", languageID, call.ClientID, clientCode, out errorMsg);
                }
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CSRNAME|", user.FirstName);
                    text = text.Replace("|CALLEREMAIL|", call.CallCustomer.Email);
                    text = text.Replace("|CLAIMNUMBER|", String.Format("{0}{1}", user.ParentClient.Name.Substring(0, 1), call.CallClaim.ID.ToString("D7")));
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                    if (call.CallClaim.ClaimPeril.Peril.ToUpper().Contains("LIQUID"))
                    {
                        result = result.Replace("display:none;", "display:inline-block;");
                    }
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadSubmitClaim(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = null;
                script1 = sh.Select("ClaimProcessed", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CALLERNAME|", call.CallerName);
                    text = text.Replace("|CLAIMNUMBER|", String.Format("{0}{1}", user.ParentClient.Name.Substring(0, 1), call.CallClaim.ID.ToString("D7")));
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadClaimEndCall(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = null;
                script1 = sh.Select("EndCallThankYou", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CALLERNAME|", call.CallerName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadEndCall(User user, Call call, string fileData, int languageID, string clientCode)
        {
            string result = fileData;
            try
            {
                string errorMsg = string.Empty;
                ScriptHelper sh = new ScriptHelper();
                Script script1 = null;
                script1 = sh.Select("EndCallThankYou", languageID, call.ClientID, clientCode, out errorMsg);
                if (string.IsNullOrEmpty(errorMsg) && script1 != null)
                {
                    string text = script1.Text.Replace("|CALLERNAME|", call.CallerName);
                    result = result.Replace("[CUSTOMERSCRIPT]", text);
                    result = result.Replace("[CUSTOMERSCRIPTCSS]", script1.CssClass);
                }
                else
                    result = errorMsg;
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private string LoadCSRCalls(User user, string clientCode)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string errorMsg = string.Empty;
                if (user != null)
                {
                    result.AppendLine("<div style=\"height:210px; overflow-y:scroll;\">");
                    result.AppendLine("<table id=\"tblCSRCalls\" style=\"width:100%;border: 1px solid gray;\" cellpadding=\"2\" cellspacing=\"0\" >");
                    result.AppendLine("<tr class=\"widelistbold\" style=\"border: 1px solid gray;\">");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;display:none;\" >ID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;display:none;\" >CUSTOMERID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >CLIENT</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >MDN</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >NAME</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >REASON</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >RESULT</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >ESCALATED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >STARTED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >ENDED</th>");
                    result.AppendLine("</tr>");

                    CallHelper ch = new CallHelper();
                    List<Call> calls = ch.Select(0, 0, user.ID, DateTime.MinValue, DateTime.MaxValue, clientCode, out errorMsg);
                    if (calls != null)
                    {
                        foreach (Call c in calls)
                        {
                            result.AppendLine(String.Format("<tr style=\"border: 1px solid gray;\" id=\"tbrCall_{0}\">", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.CustomerID));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", user.ParentClient.Name));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\" onclick=\"javascript:loadcall({0});\"><span class=\"historylink\">{1}</span></td>", c.CustomerID, c.SearchMDN));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.CallerName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.ActionName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.ResultName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.Escalate ? "YES" : "NO"));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", (c.StartTime != DateTime.MinValue ? c.StartTime.ToString("G") : "&nbsp;")));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", (c.EndTime != DateTime.MinValue ? c.EndTime.ToString("G") : "&nbsp;")));
                            result.AppendLine("</tr>");
                        }
                    }
                    result.AppendLine("</table>");
                    result.AppendLine("</div>");
                }
            }
            catch (Exception ex)
            {
                result.Append(String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace));
            }
            return result.ToString();
        }

        private string LoadCustomerCalls(User user, Call call, string clientCode)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string errorMsg = string.Empty;
                if (user != null)
                {
                    result.AppendLine("<div style=\"height:210px; overflow-y:scroll;\">");
                    result.AppendLine("<table id=\"tblCustomerCalls\" style=\"width:100%;border: 1px solid gray;\" cellpadding=\"2\" cellspacing=\"0\" >");
                    result.AppendLine("<tr style=\"border: 1px solid gray;\" class=\"widelistbold\">");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;display:none;\" >ID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;display:none;\" >CUSTOMERID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >CLIENT</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >MDN</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >NAME</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >REASON</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >RESULT</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >ESCALATED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >STARTED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;border: 1px solid gray;\" >ENDED</th>");
                    result.AppendLine("</tr>");

                    CallHelper ch = new CallHelper();
                    List<Call> calls = ch.Select(call.CustomerID, call.CustomerID, 0, DateTime.MinValue, DateTime.MaxValue, clientCode, out errorMsg);
                    if (calls != null)
                    {
                        foreach (Call c in calls)
                        {
                            result.AppendLine(String.Format("<tr style=\"border: 1px solid gray;\" id=\"tbrCall_{0}\">", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.CustomerID));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", user.ParentClient.Name));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\" onclick=\"javascript:loadcall({0});\"><span class=\"historylink\">{1}</span></td>", c.CustomerID, c.SearchMDN));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.CallerName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.ActionName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.ResultName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.Escalate ? "YES" : "NO"));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", (c.StartTime != DateTime.MinValue ? c.StartTime.ToString("G") : "&nbsp;")));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", (c.EndTime != DateTime.MinValue ? c.EndTime.ToString("G") : "&nbsp;")));
                            result.AppendLine("</tr>");
                        }
                    }
                    result.AppendLine("</table>");
                    result.AppendLine("</div>");
                }
            }
            catch (Exception ex)
            {
                result.Append(String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace));
            }
            return result.ToString();
        }

        private string LoadCustomerClaims(User user, Call call, string clientCode)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string errorMsg = string.Empty;
                if (user != null)
                {
                    ClaimHelper ch = new ClaimHelper();
                    List<Claim> claims = ch.Select(0, call.CustomerID, 0, clientCode, out errorMsg);

                    result.AppendLine("<div style=\"height: 210px; overflow-y:scroll;\">");
                    result.AppendLine("<table id=\"tblClaims\" style=\"width:100%;border: 1px solid gray;\" cellpadding=\"2\" cellspacing=\"0\" >");
                    result.AppendLine("<tr class=\"widelistbold\" style=\"border: 1px solid gray;\">");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;display:none;\" >ID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"align:center;display:none;\" >CUSTOMERID</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >CSR</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >CLAIM #</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >LOSS TYPE</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >LOSS DATE</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >STATUS</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >OPENED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >APPROVED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >CLOSED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >DENIED</th>");
                    result.AppendLine("<th class=\"widelistbold\" style=\"border: 1px solid gray;align:center;\" >CANCELLED</th>");
                    result.AppendLine("</tr>");

                    if (claims != null)
                    {
                        foreach (Claim c in claims)
                        {
                            result.AppendLine(String.Format("<tr style=\"border: 1px solid gray;\" id=\"tbrClaim_{0}\">", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.ID));
                            result.AppendLine(String.Format("<td style=\"display:none;\">{0}</td>", c.CustomerID));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>", c.UserName));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\" onclick=\"javascript:loadclaim({0});\"><span class=\"historylink\">{1}{2}</span></td>", 
                                call.CallClaim.ID, user.ParentClient.Name.Substring(0, 1), call.CallClaim.ID.ToString("D7")));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</span></td>", c.ClaimPeril != null ? c.ClaimPeril.Peril : "&nbsp;"));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</span></td>", 
                                (c.DateOfLoss != DateTime.MinValue ? c.DateOfLoss.ToString("G") : "&nbsp;")));
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</span></td>", 
                                c.StatusHistory.LastOrDefault() != null ? c.StatusHistory.OrderBy(x => x.StatusDate).LastOrDefault().Status : "&bnsp;"));
                            ClaimStatusHistory hist = c.StatusHistory.Where(x => x.Status == "OPEN").FirstOrDefault();
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>",
                                (hist != null && hist.StatusDate > DateTime.MinValue ? hist.StatusDate.ToString("G") : "&nbsp;")));
                            hist = c.StatusHistory.Where(x => x.Status == "APPROVED").FirstOrDefault();
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>",
                                (hist != null && hist.StatusDate > DateTime.MinValue ? hist.StatusDate.ToString("G") : "&nbsp;")));
                            hist = c.StatusHistory.Where(x => x.Status == "CLOSED").FirstOrDefault();
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>",
                                (hist != null && hist.StatusDate > DateTime.MinValue ? hist.StatusDate.ToString("G") : "&nbsp;")));
                            hist = c.StatusHistory.Where(x => x.Status == "DENIED").FirstOrDefault();
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>",
                                (hist != null && hist.StatusDate > DateTime.MinValue ? hist.StatusDate.ToString("G") : "&nbsp;")));
                            hist = c.StatusHistory.Where(x => x.Status == "CANCELLED").FirstOrDefault();
                            result.AppendLine(String.Format("<td style=\"border: 1px solid gray;\">{0}</td>",
                                (hist != null && hist.StatusDate > DateTime.MinValue ? hist.StatusDate.ToString("G") : "&nbsp;")));
                            result.AppendLine("</tr>");
                        }
                    }
                    result.AppendLine("</table>");
                    result.AppendLine("</div>");
                }
            }
            catch (Exception ex)
            {
                result.Append(String.Format("ERROR:<br /><br />{0}<br /><br />{1}", ex.Message, ex.StackTrace));
            }
            return result.ToString();
        }

        //// POST api/<controller>
        //public FileContentResult Post()
        //{
        //    string path = "C:\\inetpub\\wwwroot\\gotechcycle.com\\Content\\775606979950.pdf";
        //    byte[] data = System.IO.File.ReadAllBytes(path);
        //    return new FileContentResult(data, "application/pdf");
        //}
    }
}
