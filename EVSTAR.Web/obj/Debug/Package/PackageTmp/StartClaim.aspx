﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="StartClaim.aspx.cs" Inherits="EVSTAR.Web.StartClaim" %>

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
                    Tell us what's wrong with your device.
                </div>
                <div class="login-content-text-field1-desktop" style="height: 81px;">
                    <span class="login-text-input-label-desktop" style="width: 300px;">Select one&nbsp;(* Required)
                    </span>
                    <div class="dropdown-container-desktop">
                        <select class="dropdown-input-desktop" id="ddlPerils"></select>
                    </div>
                </div>
                <div class="login-content-text-field2-desktop" style="height: 81px;" id="divSubcategories">
                    <span class="login-text-input-label-desktop" style="width: 300px;">Select one&nbsp;(* Required)
                    </span>
                    <div class="dropdown-container-desktop">
                        <select class="dropdown-input-desktop" id="ddlSubcategories"></select>
                    </div>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 3; height: 81px;">
                    <span class="login-text-input-label-desktop" style="width: 400px;">When did this happen?&nbsp;(* Required)
                    </span>
                    <input type="text" id="datepicker" class="dropdown-input-desktop" style="width: 100%;" />
                </div>
                <div class="login-content-text-field2-desktop" style="order: 4; height: 225px;">
                    <span class="login-text-input-label-desktop" style="width: 400px;">Describe what happened&nbsp;(* Required)
                    </span>
                    <textarea id="txtClaimDescription" name="txtClaimDescription" class="textarea-input-desktop" style="text-align: left;" rows="5">
                    </textarea>
                </div>
                <div class="login-button-row-desktop" style="order: 5; gap:150px;">
                    <div class="button-back-desktop" id="btnStartClaimBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnStartClaim" style="width: 150px;">
                        <span class="save-button-text-desktop" style="width: 50px;">
                            <span class="save-button-text-inner-desktop" style="width: 50px;">Next</span>
                        </span>
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
            EVSTAR &copy; 2023
        </div>
    </div>
</asp:Content>
