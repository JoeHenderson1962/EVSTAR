﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="SubmitDetails.aspx.cs" Inherits="EVSTAR.Web.SubmitDetails" %>

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
                <div class="login-content-highlights-desktop" style="width: 750px; order: 0;">
                    Please review and confirm your details.
                </div>
                <div class="login-content-text-field1-desktop" style="order: 0; width: 750px;line-height: 110%;">
                    <span class="login-text-input-label-desktop"><b>What happened</b>
                    </span>
                    <label id="lblPerilToSubmit"></label>
                    <label id="lblSubcategoryToSubmit"></label>
                    <label id="lblDescriptionToSubmit"></label>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 1;width: 750px; height: 30px;">
                    <span class="login-text-input-label-desktop"><b>When</b>
                    </span>
                    <label id="lblDateToSubmit"></label>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 2;width: 750px;">
                    <span class="login-text-input-label-desktop"><b>Plan Info</b>
                    </span>
                    <table>
                        <tr>
                            <td style="vertical-align: top; text-align: left; padding-right: 15px;"><b>Name on plan:</b></td>
                            <td style="vertical-align: top; text-align: left;">
                                <label id="lblNameOnPlan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left; padding-right: 15px;"><b>Address on plan:</b></td>
                            <td style="vertical-align: top; text-align: left;">
                                <label id="lblAddressOnPlan"></label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 3;width: 750px;">
                    <span class="login-text-input-label-desktop">Contact Info
                    </span>
                    <table>
                        <tr>
                            <td style="vertical-align: top; text-align: left; padding-right: 15px;"><b>Phone number:</b></td>
                            <td style="vertical-align: top; text-align: left;">
                                <label id="lblPhoneNumberOnPlan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left; padding-right: 15px;"><b>Email address:</b></td>
                            <td style="vertical-align: top; text-align: left;">
                                <label id="lblEmailAddressOnPlan"></label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 4;width: 750px;">
                    <span class="login-text-input-label-desktop" style="width: 700px;">If everything looks correct, select <label id="lblSubmitText">Submit</label>.
                    </span>
                    <span class="login-text-input-label-desktop" style="width: 700px;">
                    If this information is incorrect or you need to update your information, please contact our customer service team for assistance at 
                        <label id="lblProgramPhone">913-717-7779</label> 
                    </span>
                </div>
                <div class="login-button-row-desktop" style="order: 5; gap: 150px;">
                    <div class="button-back-desktop" id="btnSubmitDetailsBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnSubmitDetails" style="width: 160px;">
                        <div class="save-button-text-desktop" id="spnSubmitButton" style="width:65px;">
                            <div class="save-button-text-inner-desktop" style="width: 65px;" id="lblSubmitButton">Submit</div>
                        </div>
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
