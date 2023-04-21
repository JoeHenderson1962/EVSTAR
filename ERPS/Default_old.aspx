<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_old.aspx.cs" Inherits="_Default_old" Title="Equipment Replacement Processing System" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Equipment Replacement Processing System</title>
    <link rel="stylesheet" type="text/css" href="Scripts/dialogbox/dialog.css" />
    <link rel="stylesheet" type="text/css" href="Scripts/dialogbox/demo/sunlight.default.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.structure.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.theme.min.css" />
<%--    <link rel="stylesheet" href="Content/bootstrap.css">--%>
    <link rel="stylesheet" href="./WHIPS.css" type="text/css" />
    <link rel="stylesheet" href="./Site-evstar.css" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,200,300,400,500,600,700,800' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="frmDefault" runat="server" name="frmDefault" onsubmit="return false;">
        <input type="hidden" id="hidCSRName" name="hidCSRName" value="x" />
        <input type="hidden" id="hidCallerName" name="hidCallerName" value="x" />
        <input type="hidden" id="hidCustomerID" name="hidCustomerID" value="x" />
        <input type="hidden" id="hidRReqID" name="hidRReqID" value="x" />
        <div class="body-wrapper">
            <div class="body-content">
                <div id="dialog" title="" style="text-align: center; vertical-align: middle; background-color: white; margin: 5px; border: 4px solid gray; display: none; border-radius: 6px;">
                    <div class="button-desktop btn-default" id="btnDialogOK">
                        <span class="button-text-inner-desktop">Close</span>
                    </div>
                </div>
                <div class="header-desktop">
                    <div class="header-desktop-inner">
                        <div class="header-desktop-main">
                            <div class="header-desktop-logo-outer">
                                <div class="header-desktop-logo-inner">
                                    <div class="header-desktop-logo" id="btnHome">
                                    </div>
                                </div>
                            </div>
                            <div class="header-desktop-frame"></div>
                            <div class="header-desktop-link1">
                            </div>
                            <div class="header-desktop-link2">
                            </div>
                            <%--                <div class="btn header-button" id="btnMyAccount">
                    <span class="header-button-icon">
                        <i class="fa-regular fa-circle-user"></i></span>
                    <span class="header-button-content">My account</span>
                </div>--%>
                        </div>
                    </div>
                </div>
                <div class="hero-desktop">
                    <div class="top-box-desktop">
                        <div class="left-box-desktop" id="divLogin">
                            <div class="login-content-text-field1-desktop" style="padding-top: 50px;">
                                <span class="login-text-input-label-desktop">Username<br />
                                </span>
                                <input id="txtLoginUsername" type="text" maxlength="128" class="login-text-input-desktop" />
                                <div class="helper-text-box-desktop">
                                    <span class="helper-text-desktop">Your user login<br />
                                    </span>
                                </div>
                            </div>
                            <div class="login-content-text-field2-desktop">
                                <span class="login-text-input-label-desktop">Password<br />
                                </span>
                                <input id="txtLoginPassword" maxlength="50" type="password" class="login-text-input-desktop" />
                            </div>
                            <div class="login-button-row-desktop">
                                <div class="button-desktop btn-default" id="btnLogin">
                                    <span class="button-text-inner-desktop">Login</span>
                                </div>
                            </div>
                        </div>
                        <div class="left-box-desktop" id="divContent" style="display:none;">
                        </div>
                        <div class="right-box-desktop">
                            <table style="width: 100%; height: 100%;">
                                <tr>
                                    <td height="22px">
                                        <table border="0" cellspacing="0" cellpadding="0" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;"
                                            width="100%">
                                            <tr>
                                                <td align="left">
                                                </td>
                                                <td align="right" class="helplink">
                                                    <%--                                <span class="helplink">HELP DESK</span>
                                                    --%>                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" align="left" valign="bottom" height="39px">
                                        <asp:ImageButton ID="btnCustInfo" ImageUrl="./images/tabCustomerInfoActive.jpg" runat="server"
                                            AlternateText="Customer Information" BorderWidth="0" ImageAlign="AbsBottom" OnClick="btnCustInfo_Click" />
                                        <asp:ImageButton ID="btnCoverageHistory" ImageUrl="./images/tabCoverageHistory.jpg"
                                            runat="server" AlternateText="Coverage History" BorderWidth="0" ImageAlign="AbsBottom" OnClick="btnCoverageHistory_Click" />
                                        <asp:ImageButton ID="btnRequest" ImageUrl="./images/tabCurrentRequest.jpg" runat="server"
                                            AlternateText="Current Request" BorderWidth="0" ImageAlign="AbsBottom" OnClick="btnRequest_Click" />
                                        <asp:ImageButton ID="btnNotes" ImageUrl="./images/tabNotes.jpg" runat="server" AlternateText="Notes"
                                            BorderWidth="0" ImageAlign="AbsBottom" OnClick="btnNotes_Click" />
                                        <asp:ImageButton ID="btnNewNote" ImageUrl="./images/btnNewNote.jpg" runat="server"
                                            AlternateText="New Note" OnClientClick="btnNewNoteClick()" BorderWidth="0" ImageAlign="AbsBottom" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlCustInfo" runat="server" Style="width: 100%; height: 100%;" ScrollBars="Vertical"
                                            BorderColor="Gray" BorderWidth="1" BorderStyle="Solid" BackColor="White">
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="bottom-box-desktop">
                        <table id="tblHistory" style="width: 100%; height: 100%;">
                            <tr>
                                <td id="celHistory" colspan="4" style="height: 100%">
                                    <asp:Panel ID="pnlHistory" runat="server" Style="width: 100%; height: 100%;" ScrollBars="Vertical"
                                        BorderColor="Gray" BorderWidth="1" BorderStyle="Solid">
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td height="30" bgcolor="white">
                                    <asp:ImageButton ID="btnCSRCalls" runat="server" ImageUrl="./images/btnCSRCallsActive.jpg"
                                        OnClick="btnCSRCallsClick" />
                                    <asp:ImageButton ID="btnEscalatedCalls" runat="server" ImageUrl="./images/btnEscalatedCalls.jpg"
                                        OnClick="btnEscalatedCallsClick" />
                                    <asp:ImageButton ID="btnCustomerCalls" runat="server" ImageUrl="./images/btnCustomerCalls.jpg"
                                        OnClick="btnCustomerCallsClick" />
                                    <asp:ImageButton ID="btnRequests" runat="server" ImageUrl="./images/btnRequests.jpg"
                                        OnClick="btnRequestsClick" />&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCalendar" runat="server" ImageUrl="./images/Calendar.gif"
                        OnClientClick="btnCalendarClick()" />
                                    <asp:Label CssClass="widelist" runat="server" ID="lblStart"></asp:Label>&nbsp;-&nbsp;<asp:Label CssClass="widelist" runat="server" ID="lblEnd"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="widelist" bgcolor="white" width="100px">CALLER:<br />
                                    <asp:Label ID="lblCaller" CssClass="widelistbold" runat="server"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="widelist" bgcolor="white" width="80px">MDN:<br />
                                    <asp:Label ID="lblMDN" CssClass="widelistbold" runat="server"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="widelist" bgcolor="white" width="80px">ACCOUNT:<br />
                                    <asp:Label ID="lblAccount" CssClass="widelistbold" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="footer-desktop">
                <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
                <div class="footer-copyright">
                    EVSTAR &copy; 2022
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript"
        src="https://code.jquery.com/jquery-3.6.2.min.js"
        integrity="sha256-2krYZKh//PcchRtd+H+VyyQoZ/e3EcrkxhM8ycwASPA="
        crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/bf24d734cd.js" crossorigin="anonymous" type="text/javascript"></script>
    <script type="text/javascript"
        src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"
        integrity="sha256-eTyxS0rkjpLEo16uXTS0uVCS4815lc40K2iVpWDvdSY="
        crossorigin="anonymous"></script>
    <script type="text/javascript" src="Scripts/dialogbox/demo/sunlight-min.js"></script>
    <script type="text/javascript" src="Scripts/dialogbox/demo/sunlight.javascript-min.js"></script>
    <script type="text/javascript" src="Scripts/dialogbox/jquery.dialog.js"></script>
    <script language="javascript" type="text/javascript">
        //function btnNotesClick(custid) {
        //    parent.frames['ifrCustInfo'].location.href = 'CustInfo.aspx?Action=NOTES';
        //}
    </script>
    <script type="text/javascript" src="Scripts/csr.js"></script>
</body>
</html>
