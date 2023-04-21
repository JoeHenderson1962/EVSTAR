<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="WhatsCovered.aspx.cs" Inherits="EVSTAR.Web.WhatsCovered" %>

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
                    <%--                    <a href="WhatsCovered.aspx" class="header-desktop-link-text">What's covered?</a>--%>
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
            <div class="newprod-content-desktop">
                <div class="newprod-content-highlights-desktop">
                    What's covered?
               
                </div>
                <div class="newprod-text-field1-desktop" style="width: 800px;">
                    <ul>
                        <li>Any make, model, customer owned or customer purchased handset</li>
                        <li>Accidental Damage from Handling (ADH)
                            <ul>
                                <li>Drops</li>
                                <li>Spills</li>
                                <li>Cracks</li>
                            </ul>
                        </li>
                        <li>Mechanical</li>
                        <li>Mechanical and Electrical Failure after OEM (Original Equipment Manufacturer) warranty expires</li>
                        <li>Power surge</li>
                    </ul>
                </div>
            </div>
            <div class="newprod-button-row-desktop" style="position: absolute; top: 400px;">
                <div class="button-back-desktop" id="btnFAQBack">
                    <span class="button-back-text-desktop" style="width: 100px;"><span class="button-back-text-inner-desktop" style="width: 100px;">Back to Home</span></span>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop" style="top: 620px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 642px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
       
        </div>
    </div>
</asp:Content>
