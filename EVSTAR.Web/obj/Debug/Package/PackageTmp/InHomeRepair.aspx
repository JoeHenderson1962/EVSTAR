﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="InHomeRepair.aspx.cs" Inherits="EVSTAR.Web.InHomeRepair" %>

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
            <div class="verify-content-desktop" style="gap: 40px;">
                <div class="login-content-text-field1-desktop" style="order: 1; width: 800px;">
                    <br />
                    <span class="login-text-input-label-desktop" style="width: 800px; height: 200px;">
                        Please confirm your contact number and home address for the repair appointment.<br />
                        <br />
                        <label id="lblPhoneNumber"></label><br />
                        <label id="lblAddress"></label><br />
                        <br />
                        If your information is different from the information associated with your account, please contact us at 913-717-7779 to update.
                    </span>
                    <br />
                </div>
                <div class="verify-spacer-desktop" style="order: 2;">
                </div>
                <div class="verify-button-row-desktop" style="position: absolute; top: 500px;">
                    <div class="button-back-desktop" id="btnInHomeRepairBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="verify-button-desktop btn-default" id="btnInHomeRepair">
                        <span class="verify-button-text-desktop"><span class="verify-button-text-inner-desktop">Continue</span></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
        </div>
    </div>
</asp:Content>
