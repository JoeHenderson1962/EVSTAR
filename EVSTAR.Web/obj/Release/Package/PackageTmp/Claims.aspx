<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Claims.aspx.cs" Inherits="Techcycle.Web.Claims"
    MasterPageFile="~/Site_Evstar.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
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
            </div>
        </div>
    </div>
    <div class="verify-block-desktop">
        <div class="verify-panel-desktop">
            <div class="verify-content-desktop">

                <table style="margin: 0 auto; width: 100%;">
                    <thead>
                        <tr>
                            <th style="font-weight: bold; text-align: left; width: 250px; padding: 8px;"></th>
                            <th style="text-align: right; padding: 8px;"></th>
                        </tr>
                    </thead>
                </table>
                <div id="divClaimsGrid" style="padding-top: 10px; font-size: 0.7em;"></div>
                <asp:HiddenField ID="hidCustID" runat="server" ClientIDMode="Static" />
                <table style="width: 100%;">
                    <tr>
                        <td style="padding-right: 10px; padding-top: 10px; text-align: center; vertical-align: top;">
                            <input type="button" class="btn btn-green" id="btnViewUsers" value="Manage Users" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
        </div>
    </div>
</asp:Content>

