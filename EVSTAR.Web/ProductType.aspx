<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="ProductType.aspx.cs" Inherits="EVSTAR.Web.ProductType" %>

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
    <div id="divBanner" class="green-alert-banner-desktop" style="display:none;">
        <div class="banner-success-message-desktop">
            <label id="lblSuccessMessage"></label>
        </div>
        <div class="banner-close-desktop">
            <i id="btnCloseBanner" class="fa-solid fa-xmark"></i>
        </div>
    </div>
    <div class="newprod-panel-desktop">
        <div class="newprod-block-desktop">
            <div class="newprod-frame-506353-desktop">
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
            </div>
            <div class="newprod-content-desktop">
                <div class="newprod-content-highlights-desktop">
                    Tell us more about your device.
                </div>
                <div class="newprod-text-field1-desktop">
                    <span class="login-text-input-label-desktop" style="width:200px;">Manufacturer&nbsp;(* Required)
                    </span>
                    <input id="txtNewProductMake" type="text" maxlength="50" class="login-text-input-desktop" />
                </div>
                <div class="newprod-text-field2-desktop">
                    <span class="login-text-input-label-desktop" style="width:200px;">Model number&nbsp;(* Required)
                    </span>
                    <input id="txtNewProductModel" type="text" maxlength="50" class="login-text-input-desktop" />
                </div>
                <div class="newprod-text-field3-desktop">
                    <span class="login-text-input-label-desktop">Color
                    </span>
                    <input id="txtNewProductColor" type="text" maxlength="50" class="login-text-input-desktop" />
                </div>
                <div class="newprod-text-field4-desktop">
                    <span class="login-text-input-label-desktop" style="width:200px;">Serial number&nbsp;(* Required)
                    </span>
                    <input id="txtNewProductSerial" type="text" maxlength="50" class="login-text-input-desktop" />
                </div>
                <div class="newprod-button-row-desktop">
                    <div class="button-back-desktop" id="btnCancelDevice">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnSaveDevice">
                        <span class="save-button-text-desktop">
                            <span class="save-button-text-inner-desktop">Save device</span>

                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop2" style="top: 868px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login2" style="top: 943px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
        </div>
    </div>
</asp:Content>
