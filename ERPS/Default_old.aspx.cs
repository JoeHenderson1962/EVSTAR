using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;
using Techcycle.DB.NET;
using Techcycle.Models;

public partial class _Default_old : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCustInfo_Click(object sender, ImageClickEventArgs e)
    {
        //btnNotes.ImageUrl = "./images/tabNotes.jpg";
        //btnCustInfo.ImageUrl = "./images/tabCustomerInfoActive.jpg";
        //btnCoverageHistory.ImageUrl = "./images/tabCoverageHistory.jpg";
        //btnRequest.ImageUrl = "./images/tabCurrentRequest.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];
        //    StringBuilder content = new StringBuilder(4096);
        //    content.AppendLine("<table id='tblCustInfo' name='tblCustInfo' width='100%' height='100%' class='widelist' border='1' cellpadding='2' cellspacing='0'>");
        //    if (Utility.GetInt32Value(call.CustomerID) > 0)
        //    {
        //        Customer cust = CustomerData.GetCustomerByID(Utility.GetInt32Value(call.CustomerID));
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Primary Name</td>");
        //        content.AppendLine("    <td>" + cust.PrimaryName.Trim() + "</td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Authorized User</td>");
        //        content.AppendLine("    <td>" + cust.AuthorizedName.Trim() + "</td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Wireless Number</td>");
        //        content.AppendLine("    <td>" + Utility.FormatUSPhoneNumber(cust.MDN.Trim()) + "</td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Sequence Number</td>");
        //        content.AppendLine("    <td>" + cust.SequenceNumber.Trim() + "</td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Account Number</td>");
        //        content.AppendLine("    <td>" + cust.AccountNumber.Trim() + "</td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Billing Address</td>");
        //        content.AppendLine("    <td>");
        //        if (cust.BillingAddress != null)
        //        {
        //            if (cust.BillingAddress.Lines != null)
        //            {
        //                if (cust.BillingAddress.Lines.Count > 0)
        //                    content.AppendLine(((string)cust.BillingAddress.Lines[0]).Trim() + "<br />");
        //                if (cust.BillingAddress.Lines.Count > 1)
        //                    content.AppendLine(((string)cust.BillingAddress.Lines[1]).Trim() + "<br />");
        //                if (cust.BillingAddress.Lines.Count > 2)
        //                    content.AppendLine(((string)cust.BillingAddress.Lines[2]).Trim() + "<br />");
        //            }
        //            if (cust.BillingAddress.City.Trim() != "")
        //                content.Append(cust.BillingAddress.City + ", ");
        //            if (cust.BillingAddress.State.Trim() != "")
        //                content.Append(cust.BillingAddress.State + " ");
        //            if (cust.BillingAddress.PostalCode.Trim() != "")
        //                content.Append(cust.BillingAddress.PostalCode);
        //        }
        //        content.AppendLine("    </td>");
        //        content.AppendLine("  </tr>");
        //        content.AppendLine("  <tr>");
        //        content.AppendLine("    <td class='widelistbold'>Market</td>");
        //        content.AppendLine("    <td>" + cust.MarketCode.Trim() + "</td>");
        //        content.AppendLine("  </tr>");

        //        Coverages coverages = CoverageData.GetCoverageByCustomerID(Utility.GetInt32Value(call.CustomerID));
        //        if (coverages != null)
        //        {
        //            Coverage cov = BuildCoverageHistory(coverages, DateTime.Now);
        //            if (cov != null)
        //            {
        //                if (cov.Feat != null && cov.Feat.Name == "")
        //                    cov.Feat = FeatureData.GetFeatureByID(cov.Feat.ID);
        //                Equipment equip = new Equipment();
        //                if (cov.CoveredEquip != null)
        //                {
        //                    if (cov.CoveredEquip.ID > 0)
        //                        cov.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(cov.CoveredEquip.ID);
        //                    if (cov.CoveredEquip != null)
        //                        equip = EquipmentData.GetEquipmentByID(cov.CoveredEquip.EquipmentID);
        //                    content.AppendLine("  <tr>");
        //                    content.AppendLine("    <td class='widelistbold'>Equipment</td>");
        //                    content.AppendLine("    <td>" + (equip != null ? equip.Make + " - " + equip.Model : "UNKNOWN") + "</td>");
        //                    content.AppendLine("  </tr>");
        //                    content.AppendLine("  <tr>");
        //                    content.AppendLine("  <tr>");
        //                    content.AppendLine("    <td class='widelistbold'>ESN/IMEI</td>");
        //                    content.AppendLine("    <td>" + (cov.CoveredEquip.ESN.Trim() != "" ? cov.CoveredEquip.ESN.Trim() : (cov.CoveredEquip.IMEI.Trim() != "" ? cov.CoveredEquip.IMEI.Trim() : "UNKNOWN")) + "</td>");
        //                    content.AppendLine("  </tr>");
        //                    content.AppendLine("  <tr>");
        //                }
        //            }
        //        }
        //    }
        //    content.AppendLine("</table>");

        //    Literal lit = new Literal();
        //    lit.Text = content.ToString();
        //    pnlCustInfo.Controls.Clear();
        //    pnlCustInfo.Controls.Add(lit);
        //}
    }

    protected void btnCoverageHistory_Click(object sender, ImageClickEventArgs e)
    {
        //btnNotes.ImageUrl = "./images/tabNotes.jpg";
        //btnCustInfo.ImageUrl = "./images/tabCustomerInfo.jpg";
        //btnCoverageHistory.ImageUrl = "./images/tabCoverageHistoryActive.jpg";
        //btnRequest.ImageUrl = "./images/tabCurrentRequest.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];
        //    StringBuilder content = new StringBuilder(4096);
        //    content.AppendLine("<table id='tblCoverageHistory' name='tblCoverageHistory' width='100%' class='widelistbold' border='1' cellpadding='2' cellspacing='0'>");
        //    content.AppendLine("<tr><td class='widelistbold' colspan='5'>COVERAGE HISTORY</td></tr>");
        //    content.AppendLine("  <tr>");
        //    content.AppendLine("    <th>FEATURE</th><th>EFF DATE</th><th>DROP DATE</th><th>MAX CLAIM DATE</th>");
        //    content.AppendLine("    <th>EQUIPMENT</th>");
        //    content.AppendLine("  </tr>");
        //    if (Utility.GetInt32Value(call.CustomerID) > 0)
        //    {
        //        Coverages coverages = CoverageData.GetCoverageByCustomerID(Utility.GetInt32Value(call.CustomerID));
        //        foreach (Coverage cov in coverages)
        //        {
        //            if (cov.Feat != null && cov.Feat.Name == "")
        //                cov.Feat = FeatureData.GetFeatureByID(cov.Feat.ID);
        //            Equipment equip = new Equipment();
        //            if (cov.CoveredEquip != null)
        //                if (cov.CoveredEquip.ID > 0)
        //                    cov.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(cov.CoveredEquip.ID);
        //            if (cov.CoveredEquip != null)
        //                equip = EquipmentData.GetEquipmentByID(cov.CoveredEquip.EquipmentID);
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelist'>" + (cov.Feat != null ? cov.Feat.FeatureCode.Trim() : "&nbsp;") + "</td>");
        //            content.AppendLine("    <td class='widelist'>" + (cov.EffectiveDate <= DateTime.Now ? cov.EffectiveDate.ToString("d") : "&nbsp;") + "</td>");
        //            content.AppendLine("    <td class='widelist'>" + (cov.DropDate > Convert.ToDateTime("01/01/1900") ? cov.DropDate.ToString("d") : "&nbsp;") + "</td>");
        //            content.AppendLine("    <td class='widelist'>" + (cov.MaxClaimDate > Convert.ToDateTime("01/01/1900") ? cov.MaxClaimDate.ToString("d") : "&nbsp;") + "</td>");
        //            content.AppendLine("    <td class='widelist'>" + (equip != null ? equip.Make + " - " + equip.Model : "UNKNOWN") + "</td>");
        //            content.AppendLine("  </tr>");
        //        }
        //    }
        //    content.AppendLine("</table><br /><br />");
        //    content.AppendLine("<table id='tblEquipmentHistory' name='tblEquipmentHistory' width='100%' class='widelistbold' border='1' cellpadding='2' cellspacing='0'>");
        //    content.AppendLine("<tr><td class='widelistbold' colspan='5'>EQUIPMENT HISTORY</td></tr>");
        //    content.AppendLine("  <tr>");
        //    content.AppendLine("    <th>MAKE</th><th>MODEL</th><th>ESN/IMEI</th><th>ACTIVATION DATE</th>");
        //    content.AppendLine("    <th>END DATE</th>");
        //    content.AppendLine("  </tr>");
        //    if (Utility.GetInt32Value(call.CustomerID) > 0)
        //    {
        //        CoveredEquipments equips = CoveredEquipmentData.GetCoveredEquipmentByCustomerID(Utility.GetInt32Value(call.CustomerID));
        //        if (equips != null)
        //            foreach (CoveredEquipment covequip in equips)
        //            {
        //                Equipment equip = EquipmentData.GetEquipmentByID(covequip.EquipmentID);
        //                if (equip != null)
        //                {
        //                    content.AppendLine("  <tr>");
        //                    content.AppendLine("    <td class='widelist'>" + (equip.Make.Trim() != "" ? equip.Make.Trim() : "&nbsp;") + "</td>");
        //                    content.AppendLine("    <td class='widelist'>" + (equip.Model.Trim() != "" ? equip.Model.Trim() : "&nbsp;") + "</td>");
        //                    content.AppendLine("    <td class='widelist'>" + (covequip.ESN.Trim() != "" ? covequip.ESN.Trim() : (covequip.IMEI.Trim() != "" ? covequip.IMEI.Trim() : "UNKNOWN")) + "</td>");
        //                    content.AppendLine("    <td class='widelist'>" + (covequip.ActivationDate > Convert.ToDateTime("01/01/1900") ? covequip.ActivationDate.ToString("d") : "&nbsp;") + "</td>");
        //                    content.AppendLine("    <td class='widelist'>" + (covequip.EndDate > Convert.ToDateTime("01/01/1900") ? covequip.EndDate.ToString("d") : "&nbsp;") + "</td>");
        //                    content.AppendLine("  </tr>");
        //                }
        //            }
        //    }
        //    content.AppendLine("</table>");

        //    Literal lit = new Literal();
        //    lit.Text = content.ToString();
        //    pnlCustInfo.Controls.Clear();
        //    pnlCustInfo.Controls.Add(lit);
        //}
    }

    protected void btnRequest_Click(object sender, ImageClickEventArgs e)
    {
        //btnNotes.ImageUrl = "./images/tabNotes.jpg";
        //btnCustInfo.ImageUrl = "./images/tabCustomerInfo.jpg";
        //btnCoverageHistory.ImageUrl = "./images/tabCoverageHistory.jpg";
        //btnRequest.ImageUrl = "./images/tabCurrentRequestActive.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];
        //    StringBuilder content = new StringBuilder(4096);
        //    content.AppendLine("<table id='tblRequestInfo' name='tblRequestInfo' width='100%' class='widelist' border='1' cellpadding='2' cellspacing='0'>");
        //    if (Utility.GetInt32Value(call.RequestID) > 0)
        //    {
        //        ReplacementRequest rreq = ReplacementRequestData.GetReplacementRequestByID(Utility.GetInt32Value(call.RequestID));
        //        if (rreq != null)
        //        {
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>Request #</td>");
        //            content.AppendLine("    <td>" + rreq.ID.ToString("0000000#") + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>LOSS TYPE</td>");
        //            content.AppendLine("    <td>" + (rreq.LossEvent != null && rreq.LossEvent.Name.Trim() != "" ? rreq.LossEvent.Name.Trim() : "&nbsp;") + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>LOSS DATE</td>");
        //            content.AppendLine("    <td>" + (rreq.EventDate > Convert.ToDateTime("01/01/1900") && (rreq.EventDate <= DateTime.Now) ? rreq.EventDate.ToString("d") : "&nbsp;") + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>POLICE REPORT</td>");
        //            content.AppendLine("    <td>" + rreq.PoliceReportInfo.Trim() + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>POLICE REPORT DATE</td>");
        //            content.AppendLine("    <td>" + (rreq.PoliceReportDate > Convert.ToDateTime("01/01/1900") && (rreq.PoliceReportDate <= DateTime.Now) ? rreq.PoliceReportDate.ToString("d") : "&nbsp;") + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>DESCRIPTION</td>");
        //            content.AppendLine("    <td>" + rreq.EventDescription.Trim() + "</td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>OPEN DATE</td>");
        //            content.AppendLine("    <td>");
        //            ReplacementStatusHistories hists = ReplacementRequestData.GetReplacementStatusHistoriesByRReqID(Utility.GetInt32Value(call.RequestID));
        //            foreach (ReplacementStatusHistory hist in hists)
        //                if (hist.StatusID == 1)
        //                {
        //                    content.AppendLine(hist.StatusDate.ToString("d"));
        //                    break;
        //                }
        //            content.AppendLine("    </td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>REPLACED EQUIPMENT</td>");
        //            content.AppendLine("    <td>");
        //            if (rreq.ReplacedEquipment != null)
        //            {
        //                content.AppendLine(rreq.ReplacedEquipment.Make + "-" + rreq.ReplacedEquipment.Model);
        //            }
        //            content.AppendLine("    </td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>REPLACED ESN/IMEI</td>");
        //            content.AppendLine("    <td>");
        //            content.AppendLine((rreq.ReplacedESN.Trim() != "" ? rreq.ReplacedESN.Trim() : ""));
        //            content.AppendLine("    </td>");
        //            content.AppendLine("  </tr>");
        //            content.AppendLine("  <tr>");
        //            content.AppendLine("    <td class='widelistbold'>REPLACEMENT EQUIPMENT</td>");
        //            content.AppendLine("    <td>");
        //            if (rreq.ReplacementEquipment != null)
        //            {
        //                Equipment equip = EquipmentData.GetEquipmentByID(rreq.ReplacementEquipment.ID);
        //                content.AppendLine(equip.Make + "-" + equip.Model);
        //            }
        //            content.AppendLine("    </td>");
        //            content.AppendLine("  </tr>");
        //        }
        //    }
        //    content.AppendLine("</table>");

        //    Literal lit = new Literal();
        //    lit.Text = content.ToString();
        //    pnlCustInfo.Controls.Clear();
        //    pnlCustInfo.Controls.Add(lit);
        //}
    }

    protected void btnNotes_Click(object sender, ImageClickEventArgs e)
    {
        //btnNotes.ImageUrl = "./images/tabNotesActive.jpg";
        //btnCustInfo.ImageUrl = "./images/tabCustomerInfo.jpg";
        //btnCoverageHistory.ImageUrl = "./images/tabCoverageHistory.jpg";
        //btnRequest.ImageUrl = "./images/tabCurrentRequest.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];
        //    StringBuilder content = new StringBuilder(4096);
        //    content.AppendLine("<table id='tblNotes' name='tblNotes' width='100%' class='widelist' border='1' cellpadding='2' cellspacing='0'>");
        //    if (Utility.GetInt32Value(call.CustomerID) > 0)
        //    {
        //        content.AppendLine("<tr>");
        //        content.AppendLine("<td class='widelist' width='100%' height='100%'>");
        //        Notes notes = NoteData.GetNotesByCustomerID(Utility.GetInt32Value(call.CustomerID));
        //        if (notes != null)
        //            foreach (Note note in notes)
        //            {
        //                content.AppendLine("[" + note.NoteDateTime.ToString("g") + " / " + note.UserName + "]" + note.NoteText + "<br /><br />");
        //            }
        //        content.AppendLine("</td>");
        //        content.AppendLine("</tr>");
        //    }
        //    content.AppendLine("</table>");

        //    Literal lit = new Literal();
        //    lit.Text = content.ToString();
        //    pnlCustInfo.Controls.Clear();
        //    pnlCustInfo.Controls.Add(lit);
        //}
    }

    //public Coverage BuildCoverageHistory(Coverages coverages, DateTime eventDate)
    //{
    //    Coverage activeCoverage = null;
    //    if (coverages != null)
    //    {
    //        foreach (Coverage coverage in coverages)
    //        {
    //            if ((coverage.Feat != null && coverage.Feat.ID > 0) &&
    //                (coverage.EffectiveDate <= eventDate && coverage.EffectiveDate > Convert.ToDateTime("01/01/1900")) &&
    //                (coverage.DropDate == Convert.ToDateTime("01/01/1900") || coverage.DropDate > eventDate) &&
    //                (coverage.CancelDate == Convert.ToDateTime("01/01/1900") || coverage.CancelDate > eventDate) &&
    //                (coverage.MaxClaimDate == Convert.ToDateTime("01/01/1900")) || coverage.MaxClaimDate > eventDate)
    //            {
    //                activeCoverage = coverage;
    //                activeCoverage.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(activeCoverage.CoveredEquip.ID);
    //                activeCoverage.Feat = FeatureData.GetFeatureByID(activeCoverage.Feat.ID);
    //            }
    //        }
    //    }
    //    return activeCoverage;
    //}

    protected void btnCalendarClick(object sender, ImageClickEventArgs e)
    {
    }

    protected void btnCSRCallsClick(object sender, ImageClickEventArgs e)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCallsActive.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCalls.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCalls.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (Session["Call"] != null)
        //{
        //    DateTime startDate = Utility.GetDateTimeValue(Request.QueryString["StartDate"]);
        //    DateTime endDate = Utility.GetDateTimeValue(Request.QueryString["EndDate"]);
        //    if (endDate < startDate || endDate <= Convert.ToDateTime("01/01/1900"))
        //        endDate = DateTime.Today.AddDays(1);
        //    if (startDate <= Convert.ToDateTime("01/01/1900"))
        //        startDate = DateTime.Today;
        //    Call call = (Call)Session["Call"];
        //    BuildCSRCallsTable(call.CsrName, startDate, endDate);
        //}
    }

    private void BuildCSRCallsTable(string csrName, DateTime startDate, DateTime endDate)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCallsActive.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCalls.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCalls.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (csrName != "")
        //{
        //    Table tblCSRCalls = new Table();
        //    tblCSRCalls.Width = Unit.Percentage(100);
        //    tblCSRCalls.ID = "tblCSRCalls";
        //    tblCSRCalls.BorderColor = Color.Black;
        //    tblCSRCalls.BorderStyle = BorderStyle.Solid;
        //    tblCSRCalls.BorderWidth = 1;
        //    tblCSRCalls.CellPadding = 2;
        //    tblCSRCalls.CellSpacing = 0;

        //    TableHeaderRow thrHeader = new TableHeaderRow();
        //    thrHeader.ID = "thrHeader";
        //    thrHeader.CssClass = "widelistbold";
        //    thrHeader.BorderColor = Color.Black;
        //    thrHeader.BorderStyle = BorderStyle.Solid;
        //    thrHeader.BorderWidth = 1;
        //    thrHeader.ForeColor = Color.White;
        //    thrHeader.BackColor = Color.PowderBlue;

        //    TableHeaderCell thcMDN = new TableHeaderCell();
        //    thcMDN.ID = "thcMDN";
        //    thcMDN.CssClass = "widelistbold";
        //    thcMDN.BorderColor = Color.Black;
        //    thcMDN.BorderStyle = BorderStyle.Solid;
        //    thcMDN.BorderWidth = 1;
        //    thcMDN.Text = "MDN";
        //    thcMDN.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcClient = new TableHeaderCell();
        //    thcClient.ID = "thcClient";
        //    thcClient.CssClass = "widelistbold";
        //    thcClient.BorderColor = Color.Black;
        //    thcClient.BorderStyle = BorderStyle.Solid;
        //    thcClient.BorderWidth = 1;
        //    thcClient.Text = "CLIENT";
        //    thcClient.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcName = new TableHeaderCell();
        //    thcName.ID = "thcName";
        //    thcName.CssClass = "widelistbold";
        //    thcName.BorderColor = Color.Black;
        //    thcName.BorderStyle = BorderStyle.Solid;
        //    thcName.BorderWidth = 1;
        //    thcName.Text = "NAME";
        //    thcName.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcAction = new TableHeaderCell();
        //    thcAction.ID = "thcAction";
        //    thcAction.CssClass = "widelistbold";
        //    thcAction.BorderColor = Color.Black;
        //    thcAction.BorderStyle = BorderStyle.Solid;
        //    thcAction.BorderWidth = 1;
        //    thcAction.Text = "ACTION";
        //    thcAction.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcResult = new TableHeaderCell();
        //    thcResult.ID = "thcResult";
        //    thcResult.CssClass = "widelistbold";
        //    thcResult.BorderColor = Color.Black;
        //    thcResult.BorderStyle = BorderStyle.Solid;
        //    thcResult.BorderWidth = 1;
        //    thcResult.Text = "RESULT";
        //    thcResult.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEscalated = new TableHeaderCell();
        //    thcEscalated.ID = "thcEscalated";
        //    thcEscalated.CssClass = "widelistbold";
        //    thcEscalated.BorderColor = Color.Black;
        //    thcEscalated.BorderStyle = BorderStyle.Solid;
        //    thcEscalated.BorderWidth = 1;
        //    thcEscalated.Text = "ESCALATED";
        //    thcEscalated.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcStart = new TableHeaderCell();
        //    thcStart.ID = "thcStart";
        //    thcStart.CssClass = "widelistbold";
        //    thcStart.BorderColor = Color.Black;
        //    thcStart.BorderStyle = BorderStyle.Solid;
        //    thcStart.BorderWidth = 1;
        //    thcStart.Text = "START";
        //    thcStart.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEnd = new TableHeaderCell();
        //    thcEnd.ID = "thcEnd";
        //    thcEnd.CssClass = "widelistbold";
        //    thcEnd.BorderColor = Color.Black;
        //    thcEnd.BorderStyle = BorderStyle.Solid;
        //    thcEnd.BorderWidth = 1;
        //    thcEnd.Text = "END";
        //    thcEnd.HorizontalAlign = HorizontalAlign.Center;

        //    thrHeader.Cells.Add(thcMDN);
        //    thrHeader.Cells.Add(thcClient);
        //    thrHeader.Cells.Add(thcName);
        //    thrHeader.Cells.Add(thcAction);
        //    thrHeader.Cells.Add(thcResult);
        //    thrHeader.Cells.Add(thcEscalated);
        //    thrHeader.Cells.Add(thcStart);
        //    thrHeader.Cells.Add(thcEnd);
        //    tblCSRCalls.Rows.Add(thrHeader);

        //    Calls calls = CallData.GetCallsByCSRNameAndDateRange(csrName, startDate, endDate);
        //    int i = 0;
        //    if (calls != null)
        //    {
        //        foreach (Call c in calls)
        //        {
        //            TableRow tbrCall = new TableRow();
        //            tbrCall.BorderColor = Color.Black;
        //            tbrCall.BorderStyle = BorderStyle.Solid;
        //            tbrCall.BorderWidth = 1;
        //            tbrCall.ID = "tbrCall" + i.ToString();

        //            TableCell tbcMDN = new TableCell();
        //            tbcMDN.ID = "tbcMDN" + i.ToString();
        //            tbcMDN.CssClass = "historylink";
        //            tbcMDN.BorderColor = Color.Black;
        //            tbcMDN.BorderStyle = BorderStyle.Solid;
        //            tbcMDN.BorderWidth = 1;
        //            tbcMDN.Text = "<span class='historylink'>" + c.SearchMDN.Trim() + "</span>";
        //            tbcMDN.HorizontalAlign = HorizontalAlign.Center;
        //            tbcMDN.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcClient = new TableCell();
        //            tbcClient.ID = "tbcClient" + i.ToString();
        //            tbcClient.CssClass = "widelist";
        //            tbcClient.BorderColor = Color.Black;
        //            tbcClient.BorderStyle = BorderStyle.Solid;
        //            tbcClient.BorderWidth = 1;
        //            tbcClient.Text = c.ClientCode.Trim();
        //            tbcClient.HorizontalAlign = HorizontalAlign.Center;
        //            tbcClient.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcName = new TableCell();
        //            tbcName.ID = "tbcName" + i.ToString();
        //            tbcName.CssClass = "widelist";
        //            tbcName.BorderColor = Color.Black;
        //            tbcName.BorderStyle = BorderStyle.Solid;
        //            tbcName.BorderWidth = 1;
        //            tbcName.Text = c.CallerName.Trim();
        //            tbcName.HorizontalAlign = HorizontalAlign.Left;
        //            tbcName.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcAction = new TableCell();
        //            tbcAction.ID = "tbcAction" + i.ToString();
        //            tbcAction.CssClass = "widelist";
        //            tbcAction.BorderColor = Color.Black;
        //            tbcAction.BorderStyle = BorderStyle.Solid;
        //            tbcAction.BorderWidth = 1;
        //            tbcAction.Text = c.ActionName.Trim();
        //            tbcAction.HorizontalAlign = HorizontalAlign.Left;
        //            tbcAction.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcResult = new TableCell();
        //            tbcResult.ID = "tbcResult" + i.ToString();
        //            tbcResult.CssClass = "widelist";
        //            tbcResult.BorderColor = Color.Black;
        //            tbcResult.BorderStyle = BorderStyle.Solid;
        //            tbcResult.BorderWidth = 1;
        //            tbcResult.Text = c.ResultName.Trim();
        //            tbcResult.HorizontalAlign = HorizontalAlign.Left;
        //            tbcResult.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEscalated = new TableCell();
        //            tbcEscalated.ID = "tbcEscalated" + i.ToString();
        //            tbcEscalated.CssClass = "widelist";
        //            tbcEscalated.BorderColor = Color.Black;
        //            tbcEscalated.BorderStyle = BorderStyle.Solid;
        //            tbcEscalated.BorderWidth = 1;
        //            tbcEscalated.Text = (c.EscalationReasonID > 0 ? "Y" : "N");
        //            tbcEscalated.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEscalated.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcStart = new TableCell();
        //            tbcStart.ID = "tbcStart" + i.ToString();
        //            tbcStart.CssClass = "widelist";
        //            tbcStart.BorderColor = Color.Black;
        //            tbcStart.BorderStyle = BorderStyle.Solid;
        //            tbcStart.BorderWidth = 1;
        //            tbcStart.Text = (c.StartTime != DateTime.MinValue ? c.StartTime.ToString("G") : "&nbsp;");
        //            tbcStart.HorizontalAlign = HorizontalAlign.Left;
        //            tbcStart.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEnd = new TableCell();
        //            tbcEnd.ID = "tbcEnd" + i.ToString();
        //            tbcEnd.CssClass = "widelist";
        //            tbcEnd.BorderColor = Color.Black;
        //            tbcEnd.BorderStyle = BorderStyle.Solid;
        //            tbcEnd.BorderWidth = 1;
        //            tbcEnd.Text = (c.EndTime != DateTime.MinValue ? c.EndTime.ToString("G") : "&nbsp;");
        //            tbcEnd.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEnd.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            tbrCall.Cells.Add(tbcMDN);
        //            tbrCall.Cells.Add(tbcClient);
        //            tbrCall.Cells.Add(tbcName);
        //            tbrCall.Cells.Add(tbcAction);
        //            tbrCall.Cells.Add(tbcResult);
        //            tbrCall.Cells.Add(tbcEscalated);
        //            tbrCall.Cells.Add(tbcStart);
        //            tbrCall.Cells.Add(tbcEnd);
        //            tblCSRCalls.Rows.Add(tbrCall);
        //            i++;
        //        }
        //    }
        //    pnlHistory.Controls.Clear();
        //    pnlHistory.Controls.Add(tblCSRCalls);
        //}
    }

    private void BuildEscalatedCallsTable(DateTime startDate, DateTime endDate)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCalls.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCallsActive.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCalls.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];

        //    Table tblEscalatedCalls = new Table();
        //    tblEscalatedCalls.Width = Unit.Percentage(100);
        //    tblEscalatedCalls.ID = "tblEscalatedCalls";
        //    tblEscalatedCalls.BorderColor = Color.Black;
        //    tblEscalatedCalls.BorderStyle = BorderStyle.Solid;
        //    tblEscalatedCalls.BorderWidth = 1;
        //    tblEscalatedCalls.CellPadding = 2;
        //    tblEscalatedCalls.CellSpacing = 0;

        //    TableHeaderRow thrHeader = new TableHeaderRow();
        //    thrHeader.ID = "thrHeader";
        //    thrHeader.CssClass = "widelistbold";
        //    thrHeader.BorderColor = Color.Black;
        //    thrHeader.BorderStyle = BorderStyle.Solid;
        //    thrHeader.BorderWidth = 1;
        //    thrHeader.ForeColor = Color.White;
        //    thrHeader.BackColor = Color.LightSalmon;

        //    TableHeaderCell thcMDN = new TableHeaderCell();
        //    thcMDN.ID = "thcMDN";
        //    thcMDN.CssClass = "widelistbold";
        //    thcMDN.BorderColor = Color.Black;
        //    thcMDN.BorderStyle = BorderStyle.Solid;
        //    thcMDN.BorderWidth = 1;
        //    thcMDN.Text = "MDN";
        //    thcMDN.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcClient = new TableHeaderCell();
        //    thcClient.ID = "thcClient";
        //    thcClient.CssClass = "widelistbold";
        //    thcClient.BorderColor = Color.Black;
        //    thcClient.BorderStyle = BorderStyle.Solid;
        //    thcClient.BorderWidth = 1;
        //    thcClient.Text = "CLIENT";
        //    thcClient.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcName = new TableHeaderCell();
        //    thcName.ID = "thcName";
        //    thcName.CssClass = "widelistbold";
        //    thcName.BorderColor = Color.Black;
        //    thcName.BorderStyle = BorderStyle.Solid;
        //    thcName.BorderWidth = 1;
        //    thcName.Text = "NAME";
        //    thcName.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcCSR = new TableHeaderCell();
        //    thcCSR.ID = "thcCSR";
        //    thcCSR.CssClass = "widelistbold";
        //    thcCSR.BorderColor = Color.Black;
        //    thcCSR.BorderStyle = BorderStyle.Solid;
        //    thcCSR.BorderWidth = 1;
        //    thcCSR.Text = "CSR";
        //    thcCSR.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcAction = new TableHeaderCell();
        //    thcAction.ID = "thcAction";
        //    thcAction.CssClass = "widelistbold";
        //    thcAction.BorderColor = Color.Black;
        //    thcAction.BorderStyle = BorderStyle.Solid;
        //    thcAction.BorderWidth = 1;
        //    thcAction.Text = "ACTION";
        //    thcAction.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcResult = new TableHeaderCell();
        //    thcResult.ID = "thcResult";
        //    thcResult.CssClass = "widelistbold";
        //    thcResult.BorderColor = Color.Black;
        //    thcResult.BorderStyle = BorderStyle.Solid;
        //    thcResult.BorderWidth = 1;
        //    thcResult.Text = "RESOLUTION";
        //    thcResult.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEscalated = new TableHeaderCell();
        //    thcEscalated.ID = "thcEscalated";
        //    thcEscalated.CssClass = "widelistbold";
        //    thcEscalated.BorderColor = Color.Black;
        //    thcEscalated.BorderStyle = BorderStyle.Solid;
        //    thcEscalated.BorderWidth = 1;
        //    thcEscalated.Text = "ESCALATION REASON";
        //    thcEscalated.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcStart = new TableHeaderCell();
        //    thcStart.ID = "thcStart";
        //    thcStart.CssClass = "widelistbold";
        //    thcStart.BorderColor = Color.Black;
        //    thcStart.BorderStyle = BorderStyle.Solid;
        //    thcStart.BorderWidth = 1;
        //    thcStart.Text = "START";
        //    thcStart.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEnd = new TableHeaderCell();
        //    thcEnd.ID = "thcEnd";
        //    thcEnd.CssClass = "widelistbold";
        //    thcEnd.BorderColor = Color.Black;
        //    thcEnd.BorderStyle = BorderStyle.Solid;
        //    thcEnd.BorderWidth = 1;
        //    thcEnd.Text = "END";
        //    thcEnd.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcRReqID = new TableHeaderCell();
        //    thcRReqID.ID = "thcRReqID";
        //    thcRReqID.CssClass = "widelistbold";
        //    thcRReqID.BorderColor = Color.Black;
        //    thcRReqID.BorderStyle = BorderStyle.Solid;
        //    thcRReqID.BorderWidth = 1;
        //    thcRReqID.Text = "RREQ ID";
        //    thcRReqID.HorizontalAlign = HorizontalAlign.Center;

        //    thrHeader.Cells.Add(thcMDN);
        //    thrHeader.Cells.Add(thcClient);
        //    thrHeader.Cells.Add(thcName);
        //    thrHeader.Cells.Add(thcCSR);
        //    thrHeader.Cells.Add(thcAction);
        //    thrHeader.Cells.Add(thcResult);
        //    thrHeader.Cells.Add(thcEscalated);
        //    thrHeader.Cells.Add(thcStart);
        //    thrHeader.Cells.Add(thcEnd);
        //    thrHeader.Cells.Add(thcRReqID);
        //    tblEscalatedCalls.Rows.Add(thrHeader);

        //    Calls calls = CallData.GetUnresolvedEscalatedCalls();
        //    int i = 0;
        //    if (calls != null)
        //    {
        //        foreach (Call c in calls)
        //        {
        //            TableRow tbrCall = new TableRow();
        //            tbrCall.BorderColor = Color.Black;
        //            tbrCall.BorderStyle = BorderStyle.Solid;
        //            tbrCall.BorderWidth = 1;
        //            tbrCall.ID = "tbrCall" + i.ToString();

        //            TableCell tbcMDN = new TableCell();
        //            tbcMDN.ID = "tbcMDN" + i.ToString();
        //            tbcMDN.CssClass = "historylink";
        //            tbcMDN.BorderColor = Color.Black;
        //            tbcMDN.BorderStyle = BorderStyle.Solid;
        //            tbcMDN.BorderWidth = 1;
        //            tbcMDN.Text = "<span class='historylink'>" + c.SearchMDN.Trim() + "</span>";
        //            tbcMDN.HorizontalAlign = HorizontalAlign.Center;
        //            tbcMDN.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcClient = new TableCell();
        //            tbcClient.ID = "tbcClient" + i.ToString();
        //            tbcClient.CssClass = "widelist";
        //            tbcClient.BorderColor = Color.Black;
        //            tbcClient.BorderStyle = BorderStyle.Solid;
        //            tbcClient.BorderWidth = 1;
        //            tbcClient.Text = c.ClientCode.Trim();
        //            tbcClient.HorizontalAlign = HorizontalAlign.Center;
        //            tbcClient.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcName = new TableCell();
        //            tbcName.ID = "tbcName" + i.ToString();
        //            tbcName.CssClass = "widelist";
        //            tbcName.BorderColor = Color.Black;
        //            tbcName.BorderStyle = BorderStyle.Solid;
        //            tbcName.BorderWidth = 1;
        //            tbcName.Text = c.CallerName.Trim();
        //            tbcName.HorizontalAlign = HorizontalAlign.Left;
        //            tbcName.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcCSR = new TableCell();
        //            tbcCSR.ID = "tbcCSR" + i.ToString();
        //            tbcCSR.CssClass = "widelist";
        //            tbcCSR.BorderColor = Color.Black;
        //            tbcCSR.BorderStyle = BorderStyle.Solid;
        //            tbcCSR.BorderWidth = 1;
        //            tbcCSR.Text = c.CsrName.Trim();
        //            tbcCSR.HorizontalAlign = HorizontalAlign.Left;
        //            tbcCSR.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcAction = new TableCell();
        //            tbcAction.ID = "tbcAction" + i.ToString();
        //            tbcAction.CssClass = "widelist";
        //            tbcAction.BorderColor = Color.Black;
        //            tbcAction.BorderStyle = BorderStyle.Solid;
        //            tbcAction.BorderWidth = 1;
        //            tbcAction.Text = c.ActionName.Trim();
        //            tbcAction.HorizontalAlign = HorizontalAlign.Left;
        //            tbcAction.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcResult = new TableCell();
        //            tbcResult.ID = "tbcResult" + i.ToString();
        //            tbcResult.CssClass = "widelist";
        //            tbcResult.BorderColor = Color.Black;
        //            tbcResult.BorderStyle = BorderStyle.Solid;
        //            tbcResult.BorderWidth = 1;
        //            tbcResult.Text = c.ResultName.Trim();
        //            tbcResult.HorizontalAlign = HorizontalAlign.Left;
        //            tbcResult.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEscalated = new TableCell();
        //            tbcEscalated.ID = "tbcEscalated" + i.ToString();
        //            tbcEscalated.CssClass = "widelist";
        //            tbcEscalated.BorderColor = Color.Black;
        //            tbcEscalated.BorderStyle = BorderStyle.Solid;
        //            tbcEscalated.BorderWidth = 1;
        //            tbcEscalated.Text = (c.EscalationReason.Trim() != "" ? c.EscalationReason.Trim() : "&nbsp;");
        //            tbcEscalated.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEscalated.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcStart = new TableCell();
        //            tbcStart.ID = "tbcStart" + i.ToString();
        //            tbcStart.CssClass = "widelist";
        //            tbcStart.BorderColor = Color.Black;
        //            tbcStart.BorderStyle = BorderStyle.Solid;
        //            tbcStart.BorderWidth = 1;
        //            tbcStart.Text = (c.StartTime != DateTime.MinValue ? c.StartTime.ToString("G") : "&nbsp;");
        //            tbcStart.HorizontalAlign = HorizontalAlign.Left;
        //            tbcStart.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEnd = new TableCell();
        //            tbcEnd.ID = "tbcEnd" + i.ToString();
        //            tbcEnd.CssClass = "widelist";
        //            tbcEnd.BorderColor = Color.Black;
        //            tbcEnd.BorderStyle = BorderStyle.Solid;
        //            tbcEnd.BorderWidth = 1;
        //            tbcEnd.Text = (c.EndTime != DateTime.MinValue ? c.EndTime.ToString("G") : "&nbsp;");
        //            tbcEnd.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEnd.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcRReqID = new TableCell();
        //            tbcRReqID.ID = "tbcRReqID" + i.ToString();
        //            tbcRReqID.CssClass = "widelist";
        //            tbcRReqID.BorderColor = Color.Black;
        //            tbcRReqID.BorderStyle = BorderStyle.Solid;
        //            tbcRReqID.BorderWidth = 1;
        //            tbcRReqID.Text = (c.RequestID == 0 ? "N/A" : c.RequestID.ToString("0000000#"));
        //            tbcRReqID.HorizontalAlign = HorizontalAlign.Left;
        //            tbcRReqID.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            tbrCall.Cells.Add(tbcMDN);
        //            tbrCall.Cells.Add(tbcClient);
        //            tbrCall.Cells.Add(tbcName);
        //            tbrCall.Cells.Add(tbcCSR);
        //            tbrCall.Cells.Add(tbcAction);
        //            tbrCall.Cells.Add(tbcResult);
        //            tbrCall.Cells.Add(tbcEscalated);
        //            tbrCall.Cells.Add(tbcStart);
        //            tbrCall.Cells.Add(tbcEnd);
        //            tbrCall.Cells.Add(tbcRReqID);
        //            tblEscalatedCalls.Rows.Add(tbrCall);
        //            i++;
        //        }
        //    }
        //    pnlHistory.Controls.Clear();
        //    pnlHistory.Controls.Add(tblEscalatedCalls);
        //}
    }

    protected void btnEscalatedCallsClick(object sender, ImageClickEventArgs e)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCalls.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCallsActive.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCalls.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (Session["Call"] != null)
        //{
        //    DateTime startDate = Utility.GetDateTimeValue(Request.QueryString["StartDate"]);
        //    DateTime endDate = Utility.GetDateTimeValue(Request.QueryString["EndDate"]);
        //    if (endDate < startDate || endDate <= Convert.ToDateTime("01/01/1900"))
        //        endDate = DateTime.Today.AddDays(1);
        //    if (startDate <= Convert.ToDateTime("01/01/1900"))
        //        startDate = DateTime.Today;
        //    Call call = (Call)Session["Call"];
        //    if (call != null)
        //        BuildEscalatedCallsTable(startDate, endDate);
        //}
    }

    protected void btnCustomerCallsClick(object sender, ImageClickEventArgs e)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCalls.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCalls.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCallsActive.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (Session["Call"] != null)
        //{
        //    DateTime startDate = Utility.GetDateTimeValue(Request.QueryString["StartDate"]);
        //    DateTime endDate = Utility.GetDateTimeValue(Request.QueryString["EndDate"]);
        //    if (endDate < startDate || endDate <= Convert.ToDateTime("01/01/1900"))
        //        endDate = DateTime.Today.AddDays(1);
        //    if (startDate <= Convert.ToDateTime("01/01/1900"))
        //        startDate = DateTime.Today;
        //    Call call = (Call)Session["Call"];
        //    if (call != null)
        //        BuildCustomerCallsTable(call.CustomerID, startDate, endDate);
        //}
    }

    private void BuildCustomerCallsTable(int customerID, DateTime startDate, DateTime endDate)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCalls.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCalls.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCallsActive.jpg";
        //btnRequests.ImageUrl = "./images/btnRequests.jpg";

        //if (customerID > 0)
        //{
        //    Table tblCustomerCalls = new Table();
        //    tblCustomerCalls.Width = Unit.Percentage(100);
        //    tblCustomerCalls.ID = "tblCustomerCalls";
        //    tblCustomerCalls.BorderColor = Color.Black;
        //    tblCustomerCalls.BorderStyle = BorderStyle.Solid;
        //    tblCustomerCalls.BorderWidth = 1;
        //    tblCustomerCalls.CellPadding = 2;
        //    tblCustomerCalls.CellSpacing = 0;

        //    TableHeaderRow thrHeader = new TableHeaderRow();
        //    thrHeader.ID = "thrHeader";
        //    thrHeader.CssClass = "widelistbold";
        //    thrHeader.BorderColor = Color.Black;
        //    thrHeader.BorderStyle = BorderStyle.Solid;
        //    thrHeader.BorderWidth = 1;
        //    thrHeader.ForeColor = Color.White;
        //    thrHeader.BackColor = Color.PaleGreen;

        //    TableHeaderCell thcMDN = new TableHeaderCell();
        //    thcMDN.ID = "thcMDN";
        //    thcMDN.CssClass = "widelistbold";
        //    thcMDN.BorderColor = Color.Black;
        //    thcMDN.BorderStyle = BorderStyle.Solid;
        //    thcMDN.BorderWidth = 1;
        //    thcMDN.Text = "MDN";
        //    thcMDN.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcClient = new TableHeaderCell();
        //    thcClient.ID = "thcClient";
        //    thcClient.CssClass = "widelistbold";
        //    thcClient.BorderColor = Color.Black;
        //    thcClient.BorderStyle = BorderStyle.Solid;
        //    thcClient.BorderWidth = 1;
        //    thcClient.Text = "CLIENT";
        //    thcClient.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcName = new TableHeaderCell();
        //    thcName.ID = "thcName";
        //    thcName.CssClass = "widelistbold";
        //    thcName.BorderColor = Color.Black;
        //    thcName.BorderStyle = BorderStyle.Solid;
        //    thcName.BorderWidth = 1;
        //    thcName.Text = "NAME";
        //    thcName.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcCSR = new TableHeaderCell();
        //    thcCSR.ID = "thcCSR";
        //    thcCSR.CssClass = "widelistbold";
        //    thcCSR.BorderColor = Color.Black;
        //    thcCSR.BorderStyle = BorderStyle.Solid;
        //    thcCSR.BorderWidth = 1;
        //    thcCSR.Text = "CSR";
        //    thcCSR.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcAction = new TableHeaderCell();
        //    thcAction.ID = "thcAction";
        //    thcAction.CssClass = "widelistbold";
        //    thcAction.BorderColor = Color.Black;
        //    thcAction.BorderStyle = BorderStyle.Solid;
        //    thcAction.BorderWidth = 1;
        //    thcAction.Text = "ACTION";
        //    thcAction.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcResult = new TableHeaderCell();
        //    thcResult.ID = "thcResult";
        //    thcResult.CssClass = "widelistbold";
        //    thcResult.BorderColor = Color.Black;
        //    thcResult.BorderStyle = BorderStyle.Solid;
        //    thcResult.BorderWidth = 1;
        //    thcResult.Text = "RESOLUTION";
        //    thcResult.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEscalated = new TableHeaderCell();
        //    thcEscalated.ID = "thcEscalated";
        //    thcEscalated.CssClass = "widelistbold";
        //    thcEscalated.BorderColor = Color.Black;
        //    thcEscalated.BorderStyle = BorderStyle.Solid;
        //    thcEscalated.BorderWidth = 1;
        //    thcEscalated.Text = "ESCALATION REASON";
        //    thcEscalated.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcStart = new TableHeaderCell();
        //    thcStart.ID = "thcStart";
        //    thcStart.CssClass = "widelistbold";
        //    thcStart.BorderColor = Color.Black;
        //    thcStart.BorderStyle = BorderStyle.Solid;
        //    thcStart.BorderWidth = 1;
        //    thcStart.Text = "START";
        //    thcStart.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcEnd = new TableHeaderCell();
        //    thcEnd.ID = "thcEnd";
        //    thcEnd.CssClass = "widelistbold";
        //    thcEnd.BorderColor = Color.Black;
        //    thcEnd.BorderStyle = BorderStyle.Solid;
        //    thcEnd.BorderWidth = 1;
        //    thcEnd.Text = "END";
        //    thcEnd.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcRReqID = new TableHeaderCell();
        //    thcRReqID.ID = "thcRReqID";
        //    thcRReqID.CssClass = "widelistbold";
        //    thcRReqID.BorderColor = Color.Black;
        //    thcRReqID.BorderStyle = BorderStyle.Solid;
        //    thcRReqID.BorderWidth = 1;
        //    thcRReqID.Text = "RREQ ID";
        //    thcRReqID.HorizontalAlign = HorizontalAlign.Center;

        //    thrHeader.Cells.Add(thcMDN);
        //    thrHeader.Cells.Add(thcClient);
        //    thrHeader.Cells.Add(thcName);
        //    thrHeader.Cells.Add(thcCSR);
        //    thrHeader.Cells.Add(thcAction);
        //    thrHeader.Cells.Add(thcResult);
        //    thrHeader.Cells.Add(thcEscalated);
        //    thrHeader.Cells.Add(thcStart);
        //    thrHeader.Cells.Add(thcEnd);
        //    thrHeader.Cells.Add(thcRReqID);
        //    tblCustomerCalls.Rows.Add(thrHeader);

        //    Calls calls = CallData.GetCallsByCustomerIDAndDateRange(Utility.GetInt32Value(customerID), startDate, endDate);
        //    Customer cust = CustomerData.GetCustomerByID(Utility.GetInt32Value(customerID));
        //    int i = 0;
        //    if (calls != null && cust != null)
        //    {
        //        foreach (Call c in calls)
        //        {
        //            TableRow tbrCall = new TableRow();
        //            tbrCall.BorderColor = Color.Black;
        //            tbrCall.BorderStyle = BorderStyle.Solid;
        //            tbrCall.BorderWidth = 1;
        //            tbrCall.ID = "tbrCall" + i.ToString();

        //            TableCell tbcMDN = new TableCell();
        //            tbcMDN.ID = "tbcMDN" + i.ToString();
        //            tbcMDN.CssClass = "historylink";
        //            tbcMDN.BorderColor = Color.Black;
        //            tbcMDN.BorderStyle = BorderStyle.Solid;
        //            tbcMDN.BorderWidth = 1;
        //            tbcMDN.Text = "<span class='historylink'>" + cust.MDN.Trim() + "</span>";
        //            tbcMDN.HorizontalAlign = HorizontalAlign.Center;
        //            tbcMDN.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcClient = new TableCell();
        //            tbcClient.ID = "tbcClient" + i.ToString();
        //            tbcClient.CssClass = "widelist";
        //            tbcClient.BorderColor = Color.Black;
        //            tbcClient.BorderStyle = BorderStyle.Solid;
        //            tbcClient.BorderWidth = 1;
        //            tbcClient.Text = c.ClientCode.Trim();
        //            tbcClient.HorizontalAlign = HorizontalAlign.Center;
        //            tbcClient.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcName = new TableCell();
        //            tbcName.ID = "tbcName" + i.ToString();
        //            tbcName.CssClass = "widelist";
        //            tbcName.BorderColor = Color.Black;
        //            tbcName.BorderStyle = BorderStyle.Solid;
        //            tbcName.BorderWidth = 1;
        //            tbcName.Text = c.CallerName.Trim();
        //            tbcName.HorizontalAlign = HorizontalAlign.Left;
        //            tbcName.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcCSR = new TableCell();
        //            tbcCSR.ID = "tbcCSR" + i.ToString();
        //            tbcCSR.CssClass = "widelist";
        //            tbcCSR.BorderColor = Color.Black;
        //            tbcCSR.BorderStyle = BorderStyle.Solid;
        //            tbcCSR.BorderWidth = 1;
        //            tbcCSR.Text = c.CsrName.Trim();
        //            tbcCSR.HorizontalAlign = HorizontalAlign.Left;
        //            tbcCSR.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcAction = new TableCell();
        //            tbcAction.ID = "tbcAction" + i.ToString();
        //            tbcAction.CssClass = "widelist";
        //            tbcAction.BorderColor = Color.Black;
        //            tbcAction.BorderStyle = BorderStyle.Solid;
        //            tbcAction.BorderWidth = 1;
        //            tbcAction.Text = c.ActionName.Trim();
        //            tbcAction.HorizontalAlign = HorizontalAlign.Left;
        //            tbcAction.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcResult = new TableCell();
        //            tbcResult.ID = "tbcResult" + i.ToString();
        //            tbcResult.CssClass = "widelist";
        //            tbcResult.BorderColor = Color.Black;
        //            tbcResult.BorderStyle = BorderStyle.Solid;
        //            tbcResult.BorderWidth = 1;
        //            tbcResult.Text = c.ResultName.Trim();
        //            tbcResult.HorizontalAlign = HorizontalAlign.Left;
        //            tbcResult.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEscalated = new TableCell();
        //            tbcEscalated.ID = "tbcEscalated" + i.ToString();
        //            tbcEscalated.CssClass = "widelist";
        //            tbcEscalated.BorderColor = Color.Black;
        //            tbcEscalated.BorderStyle = BorderStyle.Solid;
        //            tbcEscalated.BorderWidth = 1;
        //            tbcEscalated.Text = (c.EscalationReason.Trim() != "" ? c.EscalationReason.Trim() : "&nbsp;");
        //            tbcEscalated.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEscalated.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcStart = new TableCell();
        //            tbcStart.ID = "tbcStart" + i.ToString();
        //            tbcStart.CssClass = "widelist";
        //            tbcStart.BorderColor = Color.Black;
        //            tbcStart.BorderStyle = BorderStyle.Solid;
        //            tbcStart.BorderWidth = 1;
        //            tbcStart.Text = (c.StartTime != DateTime.MinValue ? c.StartTime.ToString("G") : "&nbsp;");
        //            tbcStart.HorizontalAlign = HorizontalAlign.Left;
        //            tbcStart.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcEnd = new TableCell();
        //            tbcEnd.ID = "tbcEnd" + i.ToString();
        //            tbcEnd.CssClass = "widelist";
        //            tbcEnd.BorderColor = Color.Black;
        //            tbcEnd.BorderStyle = BorderStyle.Solid;
        //            tbcEnd.BorderWidth = 1;
        //            tbcEnd.Text = (c.EndTime != DateTime.MinValue ? c.EndTime.ToString("G") : "&nbsp;");
        //            tbcEnd.HorizontalAlign = HorizontalAlign.Left;
        //            tbcEnd.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            TableCell tbcRReqID = new TableCell();
        //            tbcRReqID.ID = "tbcRReqID" + i.ToString();
        //            tbcRReqID.CssClass = "widelist";
        //            tbcRReqID.BorderColor = Color.Black;
        //            tbcRReqID.BorderStyle = BorderStyle.Solid;
        //            tbcRReqID.BorderWidth = 1;
        //            tbcRReqID.Text = (c.RequestID == 0 ? "N/A" : c.RequestID.ToString("0000000#"));
        //            tbcRReqID.HorizontalAlign = HorizontalAlign.Left;
        //            tbcRReqID.Attributes.Add("onclick", "loadcall('" + c.ID.ToString() + "')");

        //            tbrCall.Cells.Add(tbcMDN);
        //            tbrCall.Cells.Add(tbcClient);
        //            tbrCall.Cells.Add(tbcName);
        //            tbrCall.Cells.Add(tbcCSR);
        //            tbrCall.Cells.Add(tbcAction);
        //            tbrCall.Cells.Add(tbcResult);
        //            tbrCall.Cells.Add(tbcEscalated);
        //            tbrCall.Cells.Add(tbcStart);
        //            tbrCall.Cells.Add(tbcEnd);
        //            tbrCall.Cells.Add(tbcRReqID);
        //            tblCustomerCalls.Rows.Add(tbrCall);
        //            i++;
        //        }
        //    }
        //    pnlHistory.Controls.Clear();
        //    pnlHistory.Controls.Add(tblCustomerCalls);
        //}
    }

    protected void btnRequestsClick(object sender, ImageClickEventArgs e)
    {
        //btnCSRCalls.ImageUrl = "./images/btnCSRCalls.jpg";
        //btnEscalatedCalls.ImageUrl = "./images/btnEscalatedCalls.jpg";
        //btnCustomerCalls.ImageUrl = "./images/btnCustomerCalls.jpg";
        //btnRequests.ImageUrl = "./images/btnRequestsActive.jpg";

        //if (Session["Call"] != null)
        //{
        //    Call call = (Call)Session["Call"];

        //    Table tblRequests = new Table();
        //    tblRequests.Width = Unit.Percentage(100);
        //    tblRequests.ID = "tblRequests";
        //    tblRequests.BorderColor = Color.Black;
        //    tblRequests.BorderStyle = BorderStyle.Solid;
        //    tblRequests.BorderWidth = 1;
        //    tblRequests.CellPadding = 2;
        //    tblRequests.CellSpacing = 0;

        //    TableHeaderRow thrHeader = new TableHeaderRow();
        //    thrHeader.ID = "thrHeader";
        //    thrHeader.CssClass = "widelistbold";
        //    thrHeader.BorderColor = Color.Black;
        //    thrHeader.BorderStyle = BorderStyle.Solid;
        //    thrHeader.BorderWidth = 1;
        //    thrHeader.ForeColor = Color.White;
        //    thrHeader.BackColor = Color.Lavender;

        //    TableHeaderCell thcMDN = new TableHeaderCell();
        //    thcMDN.ID = "thcMDN";
        //    thcMDN.CssClass = "widelistbold";
        //    thcMDN.BorderColor = Color.Black;
        //    thcMDN.BorderStyle = BorderStyle.Solid;
        //    thcMDN.BorderWidth = 1;
        //    thcMDN.Text = "MDN";
        //    thcMDN.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcCSR = new TableHeaderCell();
        //    thcCSR.ID = "thcCSR";
        //    thcCSR.CssClass = "widelistbold";
        //    thcCSR.BorderColor = Color.Black;
        //    thcCSR.BorderStyle = BorderStyle.Solid;
        //    thcCSR.BorderWidth = 1;
        //    thcCSR.Text = "CSR";
        //    thcCSR.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcRReqID = new TableHeaderCell();
        //    thcRReqID.ID = "thcRReqID";
        //    thcRReqID.CssClass = "widelistbold";
        //    thcRReqID.BorderColor = Color.Black;
        //    thcRReqID.BorderStyle = BorderStyle.Solid;
        //    thcRReqID.BorderWidth = 1;
        //    thcRReqID.Text = "RREQ ID";
        //    thcRReqID.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcLossType = new TableHeaderCell();
        //    thcLossType.ID = "thcLossType";
        //    thcLossType.CssClass = "widelistbold";
        //    thcLossType.BorderColor = Color.Black;
        //    thcLossType.BorderStyle = BorderStyle.Solid;
        //    thcLossType.BorderWidth = 1;
        //    thcLossType.Text = "LOSS TYPE";
        //    thcLossType.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcLossDate = new TableHeaderCell();
        //    thcLossDate.ID = "thcLossDate";
        //    thcLossDate.CssClass = "widelistbold";
        //    thcLossDate.BorderColor = Color.Black;
        //    thcLossDate.BorderStyle = BorderStyle.Solid;
        //    thcLossDate.BorderWidth = 1;
        //    thcLossDate.Text = "LOSS DATE";
        //    thcLossDate.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcRReqStatus = new TableHeaderCell();
        //    thcRReqStatus.ID = "thcRReqStatus";
        //    thcRReqStatus.CssClass = "widelistbold";
        //    thcRReqStatus.BorderColor = Color.Black;
        //    thcRReqStatus.BorderStyle = BorderStyle.Solid;
        //    thcRReqStatus.BorderWidth = 1;
        //    thcRReqStatus.Text = "ACTION";
        //    thcRReqStatus.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcOpenDate = new TableHeaderCell();
        //    thcOpenDate.ID = "thcOpenDate";
        //    thcOpenDate.CssClass = "widelistbold";
        //    thcOpenDate.BorderColor = Color.Black;
        //    thcOpenDate.BorderStyle = BorderStyle.Solid;
        //    thcOpenDate.BorderWidth = 1;
        //    thcOpenDate.Text = "OPEN DATE";
        //    thcOpenDate.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcApprovedDate = new TableHeaderCell();
        //    thcApprovedDate.ID = "thcApprovedDate";
        //    thcApprovedDate.CssClass = "widelistbold";
        //    thcApprovedDate.BorderColor = Color.Black;
        //    thcApprovedDate.BorderStyle = BorderStyle.Solid;
        //    thcApprovedDate.BorderWidth = 1;
        //    thcApprovedDate.Text = "APPROVED DATE";
        //    thcApprovedDate.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcClosedDate = new TableHeaderCell();
        //    thcClosedDate.ID = "thcClosedDate";
        //    thcClosedDate.CssClass = "widelistbold";
        //    thcClosedDate.BorderColor = Color.Black;
        //    thcClosedDate.BorderStyle = BorderStyle.Solid;
        //    thcClosedDate.BorderWidth = 1;
        //    thcClosedDate.Text = "CLOSED DATE";
        //    thcClosedDate.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcDeniedDate = new TableHeaderCell();
        //    thcDeniedDate.ID = "thcDeniedDate";
        //    thcDeniedDate.CssClass = "widelistbold";
        //    thcDeniedDate.BorderColor = Color.Black;
        //    thcDeniedDate.BorderStyle = BorderStyle.Solid;
        //    thcDeniedDate.BorderWidth = 1;
        //    thcDeniedDate.Text = "DENIED DATE";
        //    thcDeniedDate.HorizontalAlign = HorizontalAlign.Center;

        //    TableHeaderCell thcCancelledDate = new TableHeaderCell();
        //    thcCancelledDate.ID = "thcCancelledDate";
        //    thcCancelledDate.CssClass = "widelistbold";
        //    thcCancelledDate.BorderColor = Color.Black;
        //    thcCancelledDate.BorderStyle = BorderStyle.Solid;
        //    thcCancelledDate.BorderWidth = 1;
        //    thcCancelledDate.Text = "DENIED DATE";
        //    thcCancelledDate.HorizontalAlign = HorizontalAlign.Center;

        //    thrHeader.Cells.Add(thcMDN);
        //    thrHeader.Cells.Add(thcCSR);
        //    thrHeader.Cells.Add(thcRReqID);
        //    thrHeader.Cells.Add(thcLossType);
        //    thrHeader.Cells.Add(thcLossDate);
        //    thrHeader.Cells.Add(thcRReqStatus);
        //    thrHeader.Cells.Add(thcOpenDate);
        //    thrHeader.Cells.Add(thcApprovedDate);
        //    thrHeader.Cells.Add(thcClosedDate);
        //    thrHeader.Cells.Add(thcDeniedDate);
        //    thrHeader.Cells.Add(thcCancelledDate);
        //    tblRequests.Rows.Add(thrHeader);

        //    Coverages coverages = CoverageData.GetCoverageByCustomerID(call.CustomerID);
        //    Coverage cov = null;
        //    ReplacementRequests rreqs = null;
        //    if (coverages != null)
        //    {
        //        cov = BuildCoverageHistory(coverages, DateTime.Now);
        //        if (cov == null)
        //            cov = FindLastActiveCoverage(coverages, DateTime.Now);
        //    }
        //    if (cov != null)
        //    {
        //        rreqs = ReplacementRequestData.GetReplacementRequestsByCoverageID(cov.ID, DateTime.Now);
        //        cov.Cust = CustomerData.GetCustomerByID(cov.Cust.ID);
        //        if (cov.Cust == null)
        //        {
        //            cov.Cust = new Customer();
        //            cov.Cust.ID = call.CustomerID;
        //            cov.Cust.ClientID = cov.ClientID;
        //        }
        //    }

        //    int i = 0;
        //    if (rreqs != null)
        //    {
        //        foreach (ReplacementRequest rreq in rreqs)
        //        {
        //            TableRow tbrRReq = new TableRow();
        //            tbrRReq.BorderColor = Color.Black;
        //            tbrRReq.BorderStyle = BorderStyle.Solid;
        //            tbrRReq.BorderWidth = 1;
        //            tbrRReq.ID = "tbrRReq" + i.ToString();

        //            TableCell tbcMDN = new TableCell();
        //            tbcMDN.ID = "tbcMDN" + i.ToString();
        //            tbcMDN.CssClass = "historylink";
        //            tbcMDN.BorderColor = Color.Black;
        //            tbcMDN.BorderStyle = BorderStyle.Solid;
        //            tbcMDN.BorderWidth = 1;
        //            tbcMDN.Text = "<span class='historylink'>" + (cov.Cust.MDN.Trim() != "" ? cov.Cust.MDN.Trim() : "&nbsp;") + "</span>";
        //            tbcMDN.HorizontalAlign = HorizontalAlign.Center;
        //            tbcMDN.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcCSR = new TableCell();
        //            tbcCSR.ID = "tbcCSR" + i.ToString();
        //            tbcCSR.CssClass = "widelist";
        //            tbcCSR.BorderColor = Color.Black;
        //            tbcCSR.BorderStyle = BorderStyle.Solid;
        //            tbcCSR.BorderWidth = 1;
        //            tbcCSR.Text = (rreq.UserName.Trim() != "" ? rreq.UserName.Trim() : "&nbsp;");
        //            tbcCSR.HorizontalAlign = HorizontalAlign.Left;
        //            tbcCSR.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcRReqID = new TableCell();
        //            tbcRReqID.ID = "tbcRReqID" + i.ToString();
        //            tbcRReqID.CssClass = "widelist";
        //            tbcRReqID.BorderColor = Color.Black;
        //            tbcRReqID.BorderStyle = BorderStyle.Solid;
        //            tbcRReqID.BorderWidth = 1;
        //            tbcRReqID.Text = (rreq.ID == 0 ? "&nbsp;" : rreq.ID.ToString("0000000#"));
        //            tbcRReqID.HorizontalAlign = HorizontalAlign.Left;
        //            tbcRReqID.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcLossType = new TableCell();
        //            tbcLossType.ID = "tbcLossType" + i.ToString();
        //            tbcLossType.CssClass = "widelist";
        //            tbcLossType.BorderColor = Color.Black;
        //            tbcLossType.BorderStyle = BorderStyle.Solid;
        //            tbcLossType.BorderWidth = 1;
        //            tbcLossType.Text = (rreq.LossEvent.Name.Trim() == "" ? "&nbsp;" : rreq.LossEvent.Name.Trim());
        //            tbcLossType.HorizontalAlign = HorizontalAlign.Center;
        //            tbcLossType.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcLossDate = new TableCell();
        //            tbcLossDate.ID = "tbcLossDate" + i.ToString();
        //            tbcLossDate.CssClass = "widelist";
        //            tbcLossDate.BorderColor = Color.Black;
        //            tbcLossDate.BorderStyle = BorderStyle.Solid;
        //            tbcLossDate.BorderWidth = 1;
        //            tbcLossDate.Text = (rreq.EventDate < DateTime.Now ? rreq.EventDate.ToString("g") : "&nbsp;");
        //            tbcLossDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcLossDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcRReqStatus = new TableCell();
        //            tbcRReqStatus.ID = "tbcRReqStatus" + i.ToString();
        //            tbcRReqStatus.CssClass = "widelist";
        //            tbcRReqStatus.BorderColor = Color.Black;
        //            tbcRReqStatus.BorderStyle = BorderStyle.Solid;
        //            tbcRReqStatus.BorderWidth = 1;
        //            tbcRReqStatus.Text = (rreq.Status.Name.Trim() == "" ? "&nbsp;" : rreq.Status.Name.Trim());
        //            tbcRReqStatus.HorizontalAlign = HorizontalAlign.Left;
        //            tbcRReqStatus.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcOpenDate = new TableCell();
        //            tbcOpenDate.ID = "tbcOpenDate" + i.ToString();
        //            tbcOpenDate.CssClass = "widelist";
        //            tbcOpenDate.BorderColor = Color.Black;
        //            tbcOpenDate.BorderStyle = BorderStyle.Solid;
        //            tbcOpenDate.BorderWidth = 1;
        //            tbcOpenDate.Text = (rreq.OpenDate < DateTime.Now ? rreq.OpenDate.ToString("g") : "&nbsp;");
        //            tbcOpenDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcOpenDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcApprovedDate = new TableCell();
        //            tbcApprovedDate.ID = "tbcApprovedDate" + i.ToString();
        //            tbcApprovedDate.CssClass = "widelist";
        //            tbcApprovedDate.BorderColor = Color.Black;
        //            tbcApprovedDate.BorderStyle = BorderStyle.Solid;
        //            tbcApprovedDate.BorderWidth = 1;
        //            tbcApprovedDate.Text = (rreq.ApprovedDate < DateTime.Now ? rreq.ApprovedDate.ToString("g") : "&nbsp;");
        //            tbcApprovedDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcApprovedDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcClosedDate = new TableCell();
        //            tbcClosedDate.ID = "tbcClosedDate" + i.ToString();
        //            tbcClosedDate.CssClass = "widelist";
        //            tbcClosedDate.BorderColor = Color.Black;
        //            tbcClosedDate.BorderStyle = BorderStyle.Solid;
        //            tbcClosedDate.BorderWidth = 1;
        //            tbcClosedDate.Text = (rreq.ClosedDate < DateTime.Now ? rreq.ClosedDate.ToString("g") : "&nbsp;");
        //            tbcClosedDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcClosedDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcDeniedDate = new TableCell();
        //            tbcDeniedDate.ID = "tbcDeniedDate" + i.ToString();
        //            tbcDeniedDate.CssClass = "widelist";
        //            tbcDeniedDate.BorderColor = Color.Black;
        //            tbcDeniedDate.BorderStyle = BorderStyle.Solid;
        //            tbcDeniedDate.BorderWidth = 1;
        //            tbcDeniedDate.Text = (rreq.DeniedDate < DateTime.Now ? rreq.DeniedDate.ToString("g") : "&nbsp;");
        //            tbcDeniedDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcDeniedDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            TableCell tbcCancelledDate = new TableCell();
        //            tbcCancelledDate.ID = "tbcCancelledDate" + i.ToString();
        //            tbcCancelledDate.CssClass = "widelist";
        //            tbcCancelledDate.BorderColor = Color.Black;
        //            tbcCancelledDate.BorderStyle = BorderStyle.Solid;
        //            tbcCancelledDate.BorderWidth = 1;
        //            tbcCancelledDate.Text = (rreq.CancelledDate < DateTime.Now ? rreq.CancelledDate.ToString("g") : "&nbsp;");
        //            tbcCancelledDate.HorizontalAlign = HorizontalAlign.Left;
        //            tbcCancelledDate.Attributes.Add("onclick", "loadcall('" + cov.Cust.ID.ToString() + "')");

        //            tbrRReq.Cells.Add(tbcMDN);
        //            tbrRReq.Cells.Add(tbcCSR);
        //            tbrRReq.Cells.Add(tbcRReqID);
        //            tbrRReq.Cells.Add(tbcLossType);
        //            tbrRReq.Cells.Add(tbcLossDate);
        //            tbrRReq.Cells.Add(tbcRReqStatus);
        //            tbrRReq.Cells.Add(tbcOpenDate);
        //            tbrRReq.Cells.Add(tbcApprovedDate);
        //            tbrRReq.Cells.Add(tbcClosedDate);
        //            tbrRReq.Cells.Add(tbcDeniedDate);
        //            tbrRReq.Cells.Add(tbcCancelledDate);
        //            tblRequests.Rows.Add(tbrRReq);
        //            i++;
        //        }
        //    }
        //    //pnlHistory.Controls.Clear();
        //    //pnlHistory.Controls.Add(tblRequests);
        //}
    }

    public Coverage BuildCoverageHistory(Coverages coverages, DateTime eventDate)
    {
        Coverage activeCoverage = null;
        //if (coverages != null)
        //{
        //    foreach (Coverage coverage in coverages)
        //    {
        //        if ((coverage.Feat != null && coverage.Feat.ID > 0) &&
        //            (coverage.EffectiveDate <= eventDate && coverage.EffectiveDate > Convert.ToDateTime("01/01/1900")) &&
        //            (coverage.DropDate == Convert.ToDateTime("01/01/1900") || coverage.DropDate > eventDate) &&
        //            (coverage.CancelDate == Convert.ToDateTime("01/01/1900") || coverage.CancelDate > eventDate) &&
        //            (coverage.MaxClaimDate == Convert.ToDateTime("01/01/1900")) || coverage.MaxClaimDate > eventDate)
        //        {
        //            activeCoverage = coverage;
        //            activeCoverage.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(activeCoverage.CoveredEquip.ID);
        //            activeCoverage.Feat = FeatureData.GetFeatureByID(activeCoverage.Feat.ID);
        //        }
        //    }
        //}
        return activeCoverage;
    }

    //public Coverage FindLastActiveCoverage(Coverages coverages, DateTime eventDate)
    //{
    //    Coverage lastCoverage = new Coverage();
    //    foreach (Coverage coverage in coverages)
    //    {
    //        if ((coverage.Feat != null && coverage.Feat.ID > 0)) //&&
    //                                                             //(coverage.EffectiveDate <= eventDate && coverage.EffectiveDate > Convert.ToDateTime("01/01/1900")) &&
    //                                                             //(coverage.DropDate > eventDate || coverage.DropDate == Convert.ToDateTime("01/01/1900")) && 
    //                                                             //(coverage.CancelDate > eventDate || coverage.CancelDate == Convert.ToDateTime("01/01/1900")) && 
    //                                                             //(coverage.MaxClaimDate > eventDate || coverage.MaxClaimDate == Convert.ToDateTime("01/01/1900")))
    //        {
    //            //        if (coverage.DropDate > LastDate || coverage.CancelDate > LastDate || coverage.MaxClaimDate > LastDate)
    //            //        {
    //            //            lastCoverage = coverage;
    //            //            lastCoverage.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(coverage.CoveredEquip.ID);
    //            //            lastCoverage.Feat = FeatureData.GetFeatureByID(lastCoverage.Feat.ID);

    //            //            LastDate = FindLatestDate(coverage.DropDate, FindLatestDate(coverage.MaxClaimDate, coverage.CancelDate));
    //            //        }
    //            if (coverage.ID > lastCoverage.ID)
    //                lastCoverage = coverage;
    //        }
    //    }
    //    if (lastCoverage.ID > 0)
    //    {
    //        if (lastCoverage.CoveredEquip != null)
    //            lastCoverage.CoveredEquip = CoveredEquipmentData.GetCoveredEquipmentByID(lastCoverage.CoveredEquip.ID);
    //        if (lastCoverage.Feat != null)
    //            lastCoverage.Feat = FeatureData.GetFeatureByID(lastCoverage.Feat.ID);
    //    }
    //    return lastCoverage;
    //}
}
