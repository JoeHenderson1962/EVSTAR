<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="VerifyCode.aspx.cs" Inherits="EVSTAR.Web.VerifyCode" %>

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
                <div class="verify-content-highlights-desktop">
                    Please enter your code and select Continue.
                </div>
                <div class="login-content-text-field1-desktop">
                    <span class="login-text-input-label-desktop">Verification code
                    </span>
                    <input id="txtVerificationCode" type="text" maxlength="6" class="login-text-input-desktop" />
                </div>
                <div class="lnk verify-code-link-desktop">
                    <div class="verify-code-link-inner-desktop">
                        <a href="Support.aspx" class="verify-code-link-text-desktop">Could not receive your code?</a>
                    </div>
                </div>
                <div class="verify-spacer-desktop">
                </div>
                <div class="verify-button-row-desktop">
                    <div class="button-back-desktop" id="btnVerifyCodeBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="verify-button-desktop btn-default" id="btnVerifyCode">
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
