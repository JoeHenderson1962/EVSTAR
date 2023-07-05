<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="ProductTerms.aspx.cs" Inherits="EVSTAR.Web.ProductTerms" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                    <a href="FAQ.aspx" class="header-desktop-link-text">FAQs</a>
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
            <div class="newprod-content-desktop" style="padding-top: 0px; width: 960px;">
                <object data="/Content/EVSTARTermsandConditions.pdf#toolbar=0&navpanes=0&scrollbar=0&statusbar=0&messages=0&scrollbar=0" type="application/pdf" style="width:100%;height:600px;">
                    <p>Unable to display PDF file. <a href="/Content/EVSTARTermsandConditions.pdf#toolbar=0&navpanes=0&scrollbar=0&statusbar=0&messages=0&scrollbar=0">Download</a> instead.</p>
                </object>
            </div>
            <div class="newprod-button-row-desktop" style="position: absolute; top: 660px;">
                <div class="button-back-desktop" id="btnFAQBack">
                    <span class="button-back-text-desktop" style="width: 100px;"><span class="button-back-text-inner-desktop" style="width: 100px;">Back to Home</span></span>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop" style="top: 880px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 902px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>
