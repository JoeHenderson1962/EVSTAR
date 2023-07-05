<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerSales.aspx.cs" Inherits="EVSTAR.Web.CustomerSales"
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
                Customer Sales Report
                <div class="login-content-text-field1-desktop" style="width: 900px; height: 260px;">
                    <table>
                        <tr>
                            <td style="width: 300px; vertical-align: top;">
                                <span class="login-text-input-label-desktop" style="width: 350px;">Client<br />
                                </span>
                            </td>
                            <td style="width: 300px; vertical-align: top;">
                                <span class="login-text-input-label-desktop" style="width: 350px;">Starting Date<br />
                                </span>
                            </td>
                            <td style="width: 300px; vertical-align: top;">
                                <span class="login-text-input-label-desktop" style="width: 350px;">Ending Date<br />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 300px; vertical-align: top;">
                                <asp:DropDownList ID="ddlClient" runat="server">
                                    <asp:ListItem Text="Dobson" Value="DOB"></asp:ListItem>
                                    <asp:ListItem Text="Reach" Value="REACH"></asp:ListItem>
                                    <asp:ListItem Text="EVSE" Value="EVSTAR"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 300px; vertical-align: top;">
                                <asp:Calendar runat="server" ID="calStartDate"></asp:Calendar>
                            </td>
                            <td style="width: 300px; vertical-align: top;">
                                <asp:Calendar runat="server" ID="calEndDate"></asp:Calendar>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="login-button-row-desktop" style="top: 660px;">
                    <asp:Button ID="btnCustomerSales" runat="server" CssClass="button-desktop button-default" BorderStyle="None" Text="Download for AIG" ForeColor="White"
                        OnClick="btnCustomerSales_Click" Width="200" />
                    <asp:Button ID="btnHornbeamSales" runat="server" CssClass="button-desktop button-default" BorderStyle="None" Text="Download for Hornbeam" ForeColor="White"
                        OnClick="btnHornbeamSales_Click" Width="200" />
                </div>
            </div>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>

