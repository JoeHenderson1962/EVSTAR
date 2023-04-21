<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="PaymentOption.aspx.cs" Inherits="Techcycle.Web.PaymentOption" %>

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
                <div id="divPaymentInfo">
                    <div class="login-text-input-label-desktop" style="width: 750px; height: 80px;">
                        Thank you for uploading your receipt and any additional
                    documents. Our team will review your submission and when
                    authorization is complete, you will receive an email with next steps
                    regarding your reiumbursement payment.
                    </div>
                    <div id="divVenmo" style="display: none;">
                        <div class="login-content-text-field1-desktop" style="height: 91px; width: 825px;">
                            <span class="login-text-input-label-desktop" style="width: 800px;">Please provide your Venmo Account information in the field below.
                            </span>
                            <input type="text" id="txtVenmo" class="dropdown-input-desktop" style="width: 100%;" placeholder="Enter @username" />
                        </div>
                        <div class="login-content-text-field2-desktop" style="height: 91px; width: 825px;">
                            <span class="login-text-input-label-desktop" style="width: 800px;">Confirm username
                            </span>
                            <input type="text" id="txtVenmoConfirm" class="dropdown-input-desktop" style="width: 100%;" placeholder="Enter @username" />
                        </div>
                    </div>
                    <div id="divPayPal" style="display: none;">
                        <div class="login-content-text-field1-desktop" style="height: 91px; width: 825px;">
                            <span class="login-text-input-label-desktop" style="width: 800px;">Please provide your PayPal Account information in the field below.
                            </span>
                            <input type="text" id="txtPayPal" class="dropdown-input-desktop" style="width: 100%;" placeholder="Enter @username, phone number, or account name" />
                        </div>
                        <div class="login-content-text-field2-desktop" style="height: 91px; width: 825px;">
                            <span class="login-text-input-label-desktop" style="width: 800px;">Confirm username
                            </span>
                            <input type="text" id="txtPayPalConfirm" class="dropdown-input-desktop" style="width: 100%;" placeholder="Enter @username, phone number, or account name" />
                        </div>
                    </div>
                    <div id="divCheck" style="display: none;">
                        <div class="login-content-text-field1-desktop" style="height: 300px; width: 825px;">
                            <span class="login-text-input-label-desktop" style="width: 800px;">Once approved, your reimbursement payment will be sent to you at the address associated
                            with your account.<br />
                                <br />
                                <label id="lblAccountAddress" style="width: 825px;"></label>
                                <br />
                                <br />
                                If this address is not accurate, please call our customer care team at
                                <label id="lblProgramPhone"></label>
                                to update your information.
                            </span>
                        </div>
                    </div>
                </div>
                <div id="divPaymentInfoComplete" style="display:none;">
                    <div class="login-content-text-field1-desktop" style="height: 300px; width: 825px;">
                        <span class="login-text-input-label-desktop" style="width: 800px;">Thank you for submitting your payment information.  You’ll receive an email 
                            with the details of your reimbursement when the process is complete.  
                            If you have any questions or need help, please contact us at 
                                <label id="lblProgramPhone2"></label>
                            for assistance.
                        </span>
                    </div>
                </div>
                <div class="login-button-row-desktop" style="order: 5; gap: 150px;">
                    <div class="button-back-desktop" id="btnPaymentOptionBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnPaymentOption" style="width: 150px;">
                        <span class="save-button-text-desktop" style="width: 90px;">
                            <span class="save-button-text-inner-desktop" style="width: 90px;" id="btnPaymentOptionText">Continue</span>
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
