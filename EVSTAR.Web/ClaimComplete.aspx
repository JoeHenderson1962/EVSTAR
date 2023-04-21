<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="ClaimComplete.aspx.cs" Inherits="EVSTAR.Web.ClaimComplete" %>

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
                <div class="btn header-button" id="btnMyAccount">
                    <span class="header-button-icon">
                        <i class="fa-regular fa-circle-user"></i></span>
                    <span class="header-button-content">My account</span>
                </div>
            </div>
        </div>
    </div>
    <div class="login-block-desktop" style="width: 750px; height: 750px;">
        <div class="login-panel-desktop" style="width: 750px; height: 750px;">
            <div class="login-content-desktop" style="width: 750px; height: 750px;">
                <div class="login-content-highlights-desktop" style="width: 750px;">
                    <div id="divLocalRepair" style="display: none;">
                        <span class="login-text-input-label-desktop">Your claim is submitted. Check your inbox for an email with specific instructions on your repair and how to submit your receipt.<br />
                        </span>
                    </div>
                    <div id="divMailRepair" style="display: none;">
                        <span class="login-text-input-label-desktop" style="line-height: 0.9em;">Your claim is submitted. You will receive an email with a link to your FedEx shipping label.  
                            Follow the instructions in the email to ship your device to us.<br /><br />
                            You may also click the following link to print your FedEx shipping label:
                            <br />
                        </span>
                        <div class="login-text-input-label-desktop">
                            <a id="lnkShipping" href="tmp.pdf" class="verify-code-link-desktop" target="link" style="cursor: pointer;">
                                <label id="lblShipping"></label>
                            </a>
                        </div>
                    </div>
                    <div id="divEVSTAR" style="display: none;">
                        <span class="login-text-input-label-desktop">Thank your for submitting your claim.
                        A new part or unit will be shipped to the home address you provided. Once we've confirmed delivery of the
                part or replacement unit, we will contact you to schedule the technician at a time that works for you.</span>
                    </div>
                    <div id="divHomeRepair" style="display: none;">
                        <span class="login-text-input-label-desktop">Thank your for submitting your claim.
                        Our repair partner will be contacting you for troubleshooting and to schedule a repair appointment if your issue
                            is not resolved. If you need to update your contact information on your account, please contact us at 913-717-7779.<br />
                            <br />
                            Thanks, the EVSTAR Team.
                        </span>
                    </div>
                </div>
                <div class="verify-spacer-desktop" style="order: 2;">
                </div>
                <div class="verify-button-row-desktop" style="position: absolute; top: 500px;">
                    <div class="verify-button-desktop btn-default" id="btnClaimCompleteContinue">
                        <span class="verify-button-text-desktop"><span class="verify-button-text-inner-desktop">Return to Home</span></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop" style="top: 892px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 984px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
        </div>
    </div>
</asp:Content>
