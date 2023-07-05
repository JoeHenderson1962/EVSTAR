<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="EVSTAR.Web.FAQ" %>

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
                    <a href="WhatsCovered.aspx" class="header-desktop-link-text">What's covered?</a>
                </div>
                <div class="header-desktop-link2">
                    <%--                    <a href="FAQ.aspx" class="header-desktop-link-text">FAQs</a>--%>
                </div>
                <div class="btn header-button" id="btnMyAccount">
                    <span class="header-button-icon">
                        <i class="fa-regular fa-circle-user"></i></span>
                    <span class="header-button-content">My account</span>
                </div>
            </div>
        </div>
    </div>
    <div class="newprod-panel-desktop">
        <div class="newprod-block-desktop">
            <div class="newprod-content-desktop" style="padding-top: 0px;width: 960px;display:none;" id="divFAQPhone">
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 0;height:80px;">
                    <p class="c2">
                        <span class="c3">Is there a limit on the number of claims I can make?</span>
                    </p>
                    <span class="c4">You may file two claims in a rolling 12-month period.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 1;height:80px;">
                    <p class="c2">
                        <span class="c3">Can I cancel?</span>
                    </p>
                    <span class="c4">Yes, you can cancel your plan at any time. Please contact your carrier to cancel your plan.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 2;height:80px;">
                    <p class="c2">
                        <span class="c3">Where can I get a copy of the Terms and Conditions?</span>
                    </p>
<%--                    <span class="c4"><a href="/Content/EVSTARTermsandConditions.pdf#toolbar=0&navpanes=0&scrollbar=0&statusbar=0&messages=0&scrollbar=0">Terms and Conditions</a></span>--%>
                    <span class="c4"><a href="ProductTerms.aspx">Terms and Conditions</a></span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 3;height:80px;">
                    <p class="c2">
                        <span class="c3">How do I file a claim?</span>
                    </p>
                    <span class="c4">You can file a claim online at <a href="https://claims.goevstar.com">claims.goevstar.com</a> 24/7 365 days a year.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 4;">
                    <p class="c2">
                        <span class="c3">When can I make a claim?</span>
                    </p>
                    <span class="c4">If you purchased your phone when you purchased the plan from your carrier, you can make a claim on day one.  
                        If you are using a previously owned phone, the wait period to make a claim is 30 days.
                    </span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 5;" id="divFAQPhoneFixed">
                    <p class="c2">
                        <span class="c3">After I file a claim, how does my phone get fixed?</span>
                    </p>
                    <span class="c4">Depending on the type of issue you have with your phone, you will either send your phone in for repair 
                        or take it to a local repair shop.  You’ll receive specific instructions on what you need to do. 
                    </span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 6;">
                    <p class="c2">
                        <span class="c3">What are the service fees?</span>
                    </p>
                    <span class="c4">Your service fee will vary depending on your phone model 
                        and issue. When you file your claim, you will be instructed on the amount to pay. 
                    </span>
                </div>
            </div>
            <div class="newprod-content-desktop" style="padding-top: 0px;width: 960px;display:none;" id="divFAQCharger">
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 0;height:80px;">
                    <p class="c2">
                        <span class="c3">Is there a limit on the number of claims I can make?</span>
                    </p>
                    <span class="c4">There are unlimited claims per the term with the liability limit being the retail price of your charger.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 1;height:80px;">
                    <p class="c2">
                        <span class="c3">Can I cancel?</span>
                    </p>
                    <span class="c4">Yes, you can cancel your plan at any time. Please contact EVSTAR to cancel your plan.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 2;height:80px;">
                    <p class="c2">
                        <span class="c3">Where can I get a copy of the Terms and Conditions?</span>
                    </p>
<%--                    <span class="c4"><a href="/Content/EVSTARTermsandConditions.pdf#toolbar=0&navpanes=0&scrollbar=0&statusbar=0&messages=0&scrollbar=0">Terms and Conditions</a></span>--%>
                    <span class="c4"><a href="ProductTerms.aspx">Terms and Conditions</a></span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 3;height:80px;">
                    <p class="c2">
                        <span class="c3">How do I file a claim?</span>
                    </p>
                    <span class="c4">You can file a claim online at <a href="https://claims.goevstar.com">claims.goevstar.com</a> 24/7 365 days a year.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 4;">
                    <p class="c2">
                        <span class="c3">When can I make a claim?</span>
                    </p>
                    <span class="c4"">You can make a claim at any time during the term of the plan.
                    </span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 5;">
                    <p class="c2">
                        <span class="c3">After I file a claim, how does my charger get fixed?</span>
                    </p>
                    <span class="c4">Depending on the type of issue you have with your charger, we will either send a replacement cable or charger to your home and then our team will schedule our expert to perform the installation of the part or the charger.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 6;">
                    <p class="c2">
                        <span class="c3">What are the service fees?</span>
                    </p>
                    <span class="c4" >There is no service fee for your EVSE plan.</span>
                </div>
            </div>
            <div class="newprod-content-desktop" style="padding-top: 0px;width: 960px;display:none;" id="divFAQHome">
<%--                <div class="newprod-text-field1-desktop" style="width: 900px; order: 0;height:80px;">
                    <p class="c2">
                        <span class="c3">Is there a limit on the number of claims I can make?</span>
                    </p>
                    <span class="c4" >There are unlimited claims per the term with the liability limit being the retail price of your charger.</span>
                </div>--%>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 3;height:80px;">
                    <p class="c2">
                        <span class="c3">Can I cancel?</span>
                    </p>
                    <span class="c4" >Yes, you can cancel your plan at any time. Please contact EVSTAR to cancel your plan.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 2;height:80px;">
                    <p class="c2">
                        <span class="c3">Where can I get a copy of the Terms and Conditions?</span>
                    </p>
<%--                    <span class="c4"><a href="/Content/EVSTARTermsandConditions.pdf#toolbar=0&navpanes=0&scrollbar=0&statusbar=0&messages=0&scrollbar=0">Terms and Conditions</a></span>--%>
                    <span class="c4"><a href="ProductTerms.aspx">Terms and Conditions</a></span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 0;height:80px;">
                    <p class="c2">
                        <span class="c3">How do I file a claim?</span>
                    </p>
                    <span class="c4">You can file a claim online at <a href="https://claims.goevstar.com">claims.goevstar.com</a> 24/7 365 days a year.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 1;">
                    <p class="c2">
                        <span class="c3">When can I make a claim?</span>
                    </p>
                    <span class="c4">You can make a claim 30 days after you purchase the plan.
                        There is a 30-day wait period from the time of purchase to make a claim.
                    </span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 4;">
                    <p class="c2">
                        <span class="c3">After I file a claim, how does my device get fixed?</span>
                    </p>
                    <span class="c4">Depending on the type of device and the nature of your issue, you will either send your device in for repair, take it to a local repair shop, 
                        or our team will schedule home repair. 
                        You’ll receive specific instructions on what you need to do.  
                        In specific instances, you may be reimbursed for your device or your device may be replaced with a like-make and model.</span>
                </div>
                <div class="newprod-text-field1-desktop" style="width: 900px; order: 5;">
                    <p class="c2">
                        <span class="c3">What are the service fees?</span>
                    </p>
                    <span class="c4">Your service fee will vary depending on your device. When you file your claim, you will be instructed on the amount to pay when making your claim.</span>
                </div>
            </div>
            <div class="newprod-button-row-desktop" style="position: absolute; top: 800px;">
                <div class="button-back-desktop" id="btnFAQBack">
                    <span class="button-back-text-desktop" style="width: 100px;"><span class="button-back-text-inner-desktop" style="width: 100px;">Back to Home</span></span>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop" style="top: 1020px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 1042px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>
