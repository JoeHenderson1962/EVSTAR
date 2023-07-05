<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ERPS.Default" ClientIDMode="Static" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Equipment Replacement Processing System</title>
    <link rel="stylesheet" type="text/css" href="Content/Scripts/dialogbox/dialog.css" />
    <link rel="stylesheet" type="text/css" href="Content/Scripts/dialogbox/demo/sunlight.default.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.structure.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui-1.13.2/jquery-ui.theme.min.css" />
    <%--    <link rel="stylesheet" href="~/Content/bootstrap.css">--%>
    <link rel="stylesheet" href="./WHIPS.css" type="text/css" />
    <link rel="stylesheet" href="./Site-evstar.css" type="text/css" />
    <link href='https://fonts.googleapis.com/css?family=Lato:100,200,300,400,500,600,700,800' rel='stylesheet' type='text/css' />
    <!--PQ Grid Office theme-->
    <link rel="stylesheet" href="grid/pqgrid.min.css" />
    <link rel="stylesheet" href="grid/themes/gray/pqgrid.css" />
    <style type="text/css">
        .ui-autocomplete {
            font-size: 11px;
            text-align: left;
        }

        div.pq-grid * {
            font-size: 12px;
        }

        button.delete_btn {
            margin: -2px 0px;
        }

        button.edit_btn {
            margin: -3px 0px;
        }

        .pq-grid-row .pq-grid-cell {
            color: #888;
        }

        .pq-row-edit > .pq-grid-cell {
            color: #000;
        }

        .pq-row-delete > .pq-grid-cell {
            text-decoration: line-through;
            background-color: pink;
        }

        .pq-grid-title {
            padding: 1.5px 2.0px;
        }
    </style>

    <script type="text/javascript"
        src="https://code.jquery.com/jquery-3.6.2.min.js"
        integrity="sha256-2krYZKh//PcchRtd+H+VyyQoZ/e3EcrkxhM8ycwASPA="
        crossorigin="anonymous"></script>
    <script type="text/javascript"
        src="https://code.jquery.com/ui/1.13.1/jquery-ui.min.js"
        integrity="sha256-eTyxS0rkjpLEo16uXTS0uVCS4815lc40K2iVpWDvdSY="
        crossorigin="anonymous"></script>
    <%--    <script type="text/javascript" src="~/Content/Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="~/Content/jquery-ui-1.13.2/jquery-ui.min.js"></script>--%>
    <script src="https://kit.fontawesome.com/bf24d734cd.js" crossorigin="anonymous" type="text/javascript"></script>
    <script type="text/javascript" src="Content/Scripts/dialogbox/demo/sunlight-min.js"></script>
    <script type="text/javascript" src="Content/Scripts/dialogbox/demo/sunlight.javascript-min.js"></script>
    <script type="text/javascript" src="Content/Scripts/dialogbox/jquery.dialog.js"></script>
    <script type="text/javascript" src="grid/pqgrid.dev.js"></script>
    <script type="text/javascript" src="Content/Scripts/csr.js"></script>
    <script language="javascript" type="text/javascript">
        //function btnNotesClick(custid) {
        //    parent.frames['ifrCustInfo'].location.href = 'CustInfo.aspx?Action=NOTES';
        //}
    </script>

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
                        <div class="left-box-desktop" id="divContent" style="display: none;">
                        </div>
                        <div class="right-box-desktop">
                            <table style="width: 100%; height: 100%;">
                                <tr>
                                    <td width="100%" align="left" valign="bottom" height="39px">
                                        <div class="login-button-row-desktop" style="gap: 1px; top: 0px; width: 400px;">
                                            <div class="button-desktop btn-default" id="btnCustInfo" style="border-radius: 10px 10px 0px 0px; width: 60px;">
                                                <span class="button-text-inner-desktop" style="width: 50px; font-size: 0.8em;">Customer Info</span>
                                            </div>
                                            <div class="button-desktop btn-default" id="btnCoverageHistory" style="border-radius: 10px 10px 0px 0px; width: 60px; opacity: 50%;">
                                                <span class="button-text-inner-desktop" style="width: 50px; font-size: 0.8em;">Coverage History</span>
                                            </div>
                                            <div class="button-desktop btn-default" id="btnRequest" style="border-radius: 10px 10px 0px 0px; width: 60px; opacity: 50%;">
                                                <span class="button-text-inner-desktop" style="width: 50px; font-size: 0.8em;">Current Claim</span>
                                            </div>
                                            <div class="button-desktop btn-default" id="btnNotes" style="border-radius: 10px 10px 0px 0px; width: 50px; opacity: 50%;">
                                                <span class="button-text-inner-desktop" style="width: 40px; font-size: 0.8em;">Notes</span>
                                            </div>
                                            <div class="button-desktop btn-default" id="btnNewNote" style="border-radius: 10px 10px 10px 10px; width: 60px;">
                                                <span class="button-text-inner-desktop" style="width: 50px; font-size: 0.8em;">New Note</span>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="celCustInfo" width="100%" align="left" valign="top" height="300px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="bottom-box-desktop">
                        <table id="tblHistory" style="width: 100%;">
                            <tr>
                                <td id="celHistory" colspan="5" style="height: 100%; height: 210px; border: solid 1px black; font-size: 0.8em; vertical-align:top;"></td>
                            </tr>
                            <tr>
                                <td height="24" bgcolor="white">
                                    <div class="login-button-row-desktop" style="gap: 1px; top: 0px">
                                        <div class="button-desktop btn-default" id="btnCSRCalls" style="border-radius: 0px 0px 10px 10px; width: 110px;">
                                            <span class="button-text-inner-desktop" style="width: 80px;">CSR Calls</span>
                                        </div>
                                        <div class="button-desktop btn-default" id="btnCustomerCalls" style="border-radius: 0px 0px 10px 10px; width: 110px; opacity: 50%;">
                                            <span class="button-text-inner-desktop" style="width: 80px;">Customer Calls</span>
                                        </div>
                                        <div class="button-desktop btn-default" id="btnRequests" style="border-radius: 0px 0px 10px 10px; width: 110px; opacity: 50%;">
                                            <span class="button-text-inner-desktop" style="width: 80px;">&nbsp;&nbsp;&nbsp;&nbsp;Claims</span>
                                        </div>
                                    </div>
                                </td>
                                <td align="left" valign="middle" class="widelist" bgcolor="white" width="100px">
                                    <asp:Label CssClass="widelist" runat="server" ID="lblStart"></asp:Label>&nbsp;-&nbsp;<asp:Label CssClass="widelist"
                                        runat="server" ID="lblEnd"></asp:Label>
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
                    EVSTAR &copy; 2023
                </div>
            </div>
        </div>
    </form>
</body>
</html>
