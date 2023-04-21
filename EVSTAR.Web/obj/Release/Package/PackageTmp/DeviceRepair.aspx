<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="DeviceRepair.aspx.cs" Inherits="Techcycle.Web.DeviceRepair" %>

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
    <div class="login-block-desktop" style="width: 950px; height: 750px;">
        <div class="login-panel-desktop" style="width: 950px; height: 750px;">
            <div class="login-content-desktop" style="width: 950px; height: 750px;">
                <div class="login-content-highlights-desktop" style="width: 950px;">
                    Send in your device for repair
               
                </div>
                <div class="login-content-text-field1-desktop" style="order: 1; width: 100%; height: 150px;">
                    <span class="login-text-input-label-desktop" style="width: 100%;"><b>Shipping Address</b><br />
                        Please confirm your return address.
                    </span>
                    <br />
                    <label class="login-text-input-label-desktop" style="width: 100%;" id="lblAccountAddress"></label>
                    <br />
                    <span class="login-text-input-label-desktop" style="width: 100%; padding-top: 10px;">If your return address is different from the one associated with your account, 
                    please contact us at 913-717-7779 to update your address.
                    </span>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 2; width: 100%; height: 60px;">
                    <span class="login-text-input-label-desktop" style="width: 100%; height: 60px;"><b>Password/Security</b><br />
                        In order for our team to perform all of the necessary testing on your device, we need to make sure we 
                    are able to log in and access, so please select one of the following:
                    </span>
                </div>
                <div class="verify-content-rb1-desktop" style="order: 3; width: 100%;">
                    <input type="radio" name="security" value="disabled" class="verify-content-rb-desktop" checked>
                    <label class="verify-content-rb-text-desktop" style="width: 100%;">Disable your password so no password is needed to log in.</label>
                </div>
                <div class="verify-content-rb2-desktop" style="order: 4; width: 100%;">
                    <input type="radio" name="security" value="passcode" class="verify-content-rb-desktop">
                    <label class="verify-content-rb-text-desktop" style="width: 100%;">Provide your password so our team can access your device.</label>
                </div>
                <div class="login-content-text-field2-desktop" style="order: 5; width: 100%; height: 60px;">
                    <input type="text" id="txtPasscode" placeholder="Enter Passcode here" maxlength="16" class="dropdown-input-desktop" />
                </div>

                <div class="login-button-row-desktop" style="order: 6; gap: 118px; width: 100%;">
                    <div class="button-back-desktop" id="btnDeviceRepairBack">
                        <span class="button-back-text-desktop">
                            <span class="button-back-text-inner-desktop">Back</span>
                        </span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnCreateShippingLabel" style="width: 180px;">
                        <span class="save-button-text-desktop" style="width: 132px;">
                            <span id="btnDeviceRepair" class="save-button-text-inner-desktop" style="width: 120px;">Create Shipping Label</span>
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
