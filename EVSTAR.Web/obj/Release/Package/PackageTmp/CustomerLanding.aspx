<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="CustomerLanding.aspx.cs" Inherits="EVSTAR.Web.CustomerLanding" %>

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
    <div class="landing-content-block">
        <div class="landing-product-block">
            <div class="landing-product-personalization">
                <div class="landing-product-personalization-header">
                    <span class="landing-product-personalization-text">Hi,&nbsp;<label id="lblLandingFirstName"></label>
                    </span>
                </div>
            </div>
            <div class="landing-product-message-block">
                <div class="landing-product-dev-total">
                    <div class="landing-product-dev-total-text">
                        <label style="font-weight: 700;" id="lblLandingTotalDevices">
                        </label>
                        <label id="lblSelectDevice">Select a device to continue.</label>
                    </div>
                    <div class="landing-product-dev-total-text2" style="width:825px" id="lblIncorrectProduct">
<%--                        If this information is incorrect or you need to update your information, please contact our customer service team for assistance at 
                        <label id="lblProgramPhone"></label>--%>
                    </div>
                </div>
                <div id="divProducts" style="margin-top: 40px; padding: 10px; height: 510px; width: 825px; overflow-x: hidden; overflow-y: auto;">
                </div>
                <div class="login-button-row-desktop" style="width: 600px;" id="divDobsonRouter">
                    <b>For any router issues or questions, please contact Dobson directly at 855.5.DOBSON (855.536.2766).</b>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop2">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login2">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
        </div>
    </div>
</asp:Content>
