<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="WhatsCovered.aspx.cs" Inherits="Techcycle.Web.WhatsCovered" %>

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
            <%--            <div class="newprod-frame-506353-desktop">
                <div class="newprod-callout-dark-desktop">
                    <div class="newprod-frame-506346-desktop">
                        <div class="newprod-frame-506344-desktop">
                            <span class="newprod-callout-fee-text-desktop">Device type / Service fee
                            </span>
                        </div>
                    </div>
                    <div class="newprod-frame-506353-2-desktop">
                        <span class="newprod-content-text-field1-desktop" style="width:200px;">
                            <label id="lblProductCategory"></label>
                        </span>
                        <span class="newprod-content-text-field2-desktop">
                            <label id="lblServiceFee"></label>
                        </span>
                    </div>
                </div>
            </div>--%>
            <div class="newprod-content-desktop">
                <div class="newprod-content-highlights-desktop">
                    What devices are covered?
               
                </div>
                <div class="newprod-text-field1-desktop" style="width: 800px;">
                    <p class="c2"><span class="c3">What devices are covered?</span></p>
                    <p class="c2"><span class="c4">We cover any make, model, age and size device. Items include:</span></p>
                    <p class="c2"><span class="c3">Mobile Devices</span></p>
                    <ul class="c8 lst-kix_ioadaa6x6asu-0 start">
                        <li class="c2 c5 li-bullet-0"><span class="c4">Laptops</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">Tablets</span></li>
                    </ul>
                    <p class="c0"><span class="c3"></span></p>
                    <p class="c2"><span class="c3">Home Office</span></p>
                    <ul class="c8 lst-kix_ceqdm5uj9axd-0 start">
                        <li class="c2 c5 li-bullet-0"><span class="c1">Keyboards</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Peripherals</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Printers</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Routers</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Speakers</span></li>
                    </ul>
                    <p class="c0"><span class="c3"></span></p>
                    <p class="c2"><span class="c3">Entertainment</span></p>
                    <ul class="c8 lst-kix_eo8gb6rr70vs-0 start">
                        <li class="c2 c5 li-bullet-0"><span class="c4">Bluetooth Speaker</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">Gaming Console</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">Health &amp; Fitness Tracker</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">Remotes</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">SmartWatches</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c4">Soundbars</span></li>
                    </ul>
                    <p class="c0"><span class="c3"></span></p>
                    <p class="c2"><span class="c3">Smart Home</span></p>
                    <ul class="c8 lst-kix_3oog3u6ga6c2-0 start">
                        <li class="c2 c5 li-bullet-0"><span class="c1">Smart Doorbells</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Smartlocks</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Smart Security Cameras</span></li>
                        <li class="c2 c5 li-bullet-0"><span class="c1">Smart Thermostats</span></li>
                    </ul>
                </div>
                <div class="newprod-button-row-desktop" style="position: absolute; top: 980px;">
                    <div class="button-back-desktop" id="btnFAQBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop" style="top: 1220px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login" style="top: 1242px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
       
        </div>
    </div>
</asp:Content>
