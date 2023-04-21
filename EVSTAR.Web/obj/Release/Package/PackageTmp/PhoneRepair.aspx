<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="PhoneRepair.aspx.cs" Inherits="Techcycle.Web.PhoneRepair" %>

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
            <div class="verify-content-desktop" style="gap: 40px;">
                <div class="login-content-text-field1-desktop" style="order: 1; width: 800px;">
                    <span class="login-text-input-label-desktop" style="width:800px;">Great news, your claim has been started.
                    </span>
                    <div class="login-text-input-label-desktop" style="width:800px;display:none;" id="spnTwoOptions">There are two options to choose from to repair your device.</div>
                    <div class="login-text-input-label-desktop" style="width:800px;display:none;height: 100px;" id="spnMailInRepair"><b>Mail-in Repair Option</b><br />
                        You'll send your device to us for repair. After paying your
                        <label id="lblServiceFee" style="margin-bottom: 0rem;"></label>
                        service fee,
                        we will provide you with a prepaid label and instructions on how to send your device. When we receive your device,
                        our team of expert technicians will triage and repair your device. When the repair is complete, we will ship your
                        device back to you. This process usually takes 3 business days from start to finish depending on how quickly you
                        send the device.
                    </div>
                    <br />
                    <div class="login-text-input-label-desktop" style="width: 800px; height: 120px;display:none;" id="spnLocalRepair"><b>Local Repair Option</b><br />
                        You will receive an email with specific instructions at the email address associated with your account.  The next step, you’ll take your device to a certified, local repair shop. After your repair is complete, you will submit your
                        receipt for your repair and we will reimburse you for the repair minus your
                        <label id="lblServiceFee2" style="margin-bottom: 0rem;"></label>
                        service fee via PayPal, Venmo, or mailed check to the address on your account.<br />
                        <br />
                        <label id="lblLocalContinue">Please pick the option you want and select Continue.</label><br /><br />
                    </div>
                    <div class="radio-buttons" style="flex-direction: row;width:800px; vertical-align:middle;" id="divMailInRepair">
                        <input type="radio" name="repair" value="mailin" class="verify-content-rb-desktop" style="order: 0;"/>&nbsp;
                        <label id="lblRepairMailin" style="width: 700px; order: 1; vertical-align:middle;">
                            Mail-in Repair. We will provide specific instructions when you select this option.</label>
                    </div>
                    <div class="radio-buttons" style="flex-direction: row;width:800px; vertical-align:middle;display:none;" id="divLocalRepair">
                        <input type="radio" name="repair" value="local" class="verify-content-rb-desktop" checked style="order: 0;" />&nbsp;
                        <label id="lblRepairLocal" style="width: 700px; order: 1; vertical-align:middle;">
                            Local Repair. We will provide specific instructions when you select this option.</label>
                    </div>
                </div>
                <div class="verify-spacer-desktop" style="order: 2;">
                </div>
                <div class="verify-button-row-desktop" style="position: absolute; top: 500px;">
                    <div class="button-back-desktop" id="btnSelectRepairBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="verify-button-desktop btn-default" id="btnSelectRepair">
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
