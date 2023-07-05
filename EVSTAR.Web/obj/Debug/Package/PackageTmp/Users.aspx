<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EVSTAR.Web.Users" MasterPageFile="~/Site_Evstar.Master" %>

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
    <div class="login-block-desktop">
        <div class="login-panel-desktop">
            <div class="login-content-desktop" style="width: 1200px; padding: 0px;">
                <div class="login-content-highlights-desktop" style="width: 900px; order: 0;">
               
                </div>
        <div id="divUsersGrid" style="padding-top: 10px; margin-top: 30px; font-size: 0.7em;"></div>
                <div class="login-button-row-desktop">
                    <asp:Button ID="btnMyClaims" runat="server" CssClass="button-desktop button-default" BorderStyle="None" Text="View Claims" ForeColor="White"
                        OnClick="btnMyClaims_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 2380px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>

