<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="CollectPayment.aspx.cs" Inherits="EVSTAR.Web.CollectPayment" %>

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
    <div id="divBanner" class="green-alert-banner-desktop" style="display: none;">
        <div class="banner-success-message-desktop">
            <label id="lblMessage"></label>
        </div>
        <div class="banner-close-desktop">
            <i id="btnCloseBanner" class="fa-solid fa-xmark"></i>
        </div>
    </div>
    <div class="login-block-desktop" style="width: 750px; height: 750px;">
        <div class="login-panel-desktop" style="width: 750px; height: 750px;">
            <div class="login-content-desktop" style="width: 750px; height: 750px;">
                <div class="login-content-highlights-desktop" style="width: 750px;">
                    Pay your service fee of&nbsp;<input type="text" id="txtAmountToPay" disabled="disabled" />
                </div>
                <div class="login-content-text-field1-desktop" style="height: 61px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Cardholder's Name
                    </span>
                    <input type="text" id="txtCardName" maxlength="100" style="width: 100%;" />
                </div>
                <div class="login-content-text-field2-desktop" style="height: 81px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Credit Card Type:
                    </span>
                    <div class="dropdown-container-desktop">
                        <select class="dropdown-input-desktop" id="ddlCardType" style="width: 100%;">
                            <option value="VISA">Visa</option>
                            <option value="MAST">Mastercard</option>
                            <option value="AMEX">American Express</option>
                            <option value="DISC">Discover</option>
                        </select>
                    </div>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 2; height: 61px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Credit Card Number
                    </span>
                    <input type="text" id="txtCardNumber" maxlength="16" style="width: 100%;" />
                </div>
                <div class="login-content-text-field2-desktop" style="order: 3; height: 61px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Card Expiration Date (MM/YY)
                    </span>
                    <span style="flex-direction: row;">
                        <input type="text" id="txtCardExpMonth" maxlength="2" style="width: 40px;" />/
                    <input type="text" id="txtCardExpYear" maxlength="2" style="width: 40px;" />
                    </span>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 4; height: 61px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Card Verification Code
                    </span>
                    <input type="text" id="txtCardVerifCode" maxlength="4" style="width: 100%;" />
                </div>
                <div class="login-content-text-field2-desktop" style="order: 5; height: 61px; width: 100%;">
                    <span class="login-text-input-label-desktop" style="width: 100%;">Billing ZIP Code
                    </span>
                    <input type="text" id="txtCardZIPCode" maxlength="5" style="width: 100%;" />
                </div>
                <div class="login-button-row-desktop" style="order: 6; gap: 118px;">
                    <div class="button-back-desktop" id="btnCollectPaymentBack">
                        <span class="button-back-text-desktop">
                            <span class="button-back-text-inner-desktop">Back</span>
                        </span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnCollectPayment" style="width: 180px;">
                        <span class="save-button-text-desktop" style="width: 132px;">
                            <span id="spnCollectPayment" class="save-button-text-inner-desktop" style="width: 120px;">Process Payment</span>
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
            EVSTAR &copy; 2022
        </div>
    </div>
</asp:Content>
