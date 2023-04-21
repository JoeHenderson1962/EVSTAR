<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="PhoneRepairProviders.aspx.cs" Inherits="EVSTAR.Web.PhoneRepairProviders" %>

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
            </div>
        </div>
    </div>
    <div class="verify-block-desktop">
        <div class="verify-panel-desktop">
            <div class="verify-content-desktop" style="gap: 10px;">
                <div class="login-content-text-field1-desktop" style="order: 1; width: 800px; height: 70px;">
                    <span class="login-text-input-label-desktop" style="width: 800px;">
                        <label id="lblLocalRepairSelected" style="margin-bottom: 0rem;">
                            You have selected the local repair option.&nbsp;
                        </label>
                        We want your repair to be successful, so look for a repair shop that is certified for your
                        manufacturer and guarantees their repairs. This will ensure a quality repair is completed. 
                    </span>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 2; width: 800px; height: 60px;">
                    <span class="login-text-input-label-desktop" style="width: 800px;">To submit your receipt, you'll log
                        back into the claims portal and select "Finish my claim" for your device. You will upload a picture
                        of your receipt.
                    </span>
                </div>
                <div class="login-content-text-field3-desktop" style="order: 3; width: 800px;">
                    <span class="login-text-input-label-desktop" style="width: 800px;">Your printed store receipt must include
                        the following information requirements:
                    </span>
                    <ul id="ulReceipt">
                        <li>Name and Address of the repair shop</li>
                        <li>Customer Name</li>
                        <li>Date of Repair Payment</li>
                        <li>Description of Repair including IMEI of device</li>
                        <li>Total Amount Paid including Tax</li>
                    </ul>
                </div>
                <div class="login-content-text-field3-desktop" style="order: 4; width: 800px; height: 50px;">
                    <span class="login-text-input-label-desktop" style="width: 800px;">If the standard receipt does not include
                        this detail, please request an itemized invoice that includes the required fields. If all of the
                        required information is not provided, your reimbursement may be denied. For questions regarding claims 
                        limits, please refer to the Terms & Conditions of your plan.
                    </span>
                </div>
                <div class="verify-spacer-desktop" style="order: 5;">
                </div>
                <div class="verify-button-row-desktop" style="order: 6;">
                    <div class="button-back-desktop" id="btnSelectRepairProviderBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="verify-button-desktop btn-default" id="btnSelectRepairProvider">
                        <span class="verify-button-text-desktop"><span class="verify-button-text-inner-desktop">Continue</span></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2022
       
        </div>
    </div>
</asp:Content>
