<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="ProductAction.aspx.cs" Inherits="Techcycle.Web.ProductAction" %>

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
    <div class="newprod-panel-desktop">
        <div class="newprod-block-desktop">
            <div class="action-frame-506353-desktop">
                <div class="action-callout-dark-desktop">
                    <div class="newprod-frame-506346-desktop">
                        <div class="newprod-frame-506344-desktop">
                            <span class="newprod-callout-fee-text-desktop">Claim History
                            </span>
                        </div>
                    </div>
                    <div id="divClaims" class="action-frame-claims-desktop" style="width:100%;">
                    </div>
                </div>
            </div>
            <div class="newprod-content-desktop">
                <div class="newprod-content-highlights-desktop">
                    What would you like to do?
                </div>
                <div class="frame-506349-desktop">
                    <div id="divProduct" class="action-product-block-desktop">
                        <div id="divProductWidget" class="product-widget-non-selectable"></div>
                        <div class="action-device-info-desktop">
                            <div class="action-frame-506346-desktop">
                                <div class="action-frame-506344-desktop">
                                    <label id="lblActionDeviceType" class="action-device-type-desktop"></label>
                                </div>
                            </div>
                            <div class="action-frame-506347-desktop">
                                <div class="action-frame-506344-desktop">
                                    <label id="lblActionDeviceMake" class="action-device-values-desktop"></label>
                                </div>
                            </div>
                            <div class="action-frame-506348-desktop">
                                <div class="action-frame-506344-desktop">
                                    <label id="lblActionDeviceModel" class="action-device-values-desktop"></label>
                                </div>
                            </div>
                            <div class="action-frame-506349-desktop">
                                <div class="action-frame-506344-desktop">
                                    <label id="lblActionDeviceSerial" class="action-device-values-desktop"></label>
                                </div>
                            </div>
                            <div class="action-frame-506350-desktop">
                                <div class="action-frame-506344-desktop">
                                    <label id="lblActionDeviceAdded" class="action-device-date-desktop"></label>
                                </div>
                            </div>
                            <div id="divActionFrameLink" class="action-frame-link-desktop">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="newprod-button-row-desktop" style="gap:135px;">
                    <div class="button-back-desktop" id="btnActionBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnStartCustomerClaim" style="width: 150px;">
                        <span class="save-button-text-desktop" style="width: 100px;">
                            <span class="save-button-text-inner-desktop" id="btnStartCustomerClaimText" style="width: 100px;">Start a claim</span>

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
