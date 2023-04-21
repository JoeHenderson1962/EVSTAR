<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DobsonPortal.aspx.cs" Inherits="EVSTAR.Web.DobsonPortal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>
    <div id="divLogin" class="container body-content" style="display: block; width: 960px; padding-top: 100px;">
        <%--        <div class="row" style="margin-bottom: 30px; margin-left: 30px;">
            <h2 style="color: black;">
                <span>Log in with your user name and password</span>
            </h2>
        </div>--%>

        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="text" style="width: 500px;" id="txtLoginUser" maxlength="128" placeholder="User Name" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input style="width: 500px;" id="txtLoginPassword" maxlength="50" placeholder="Password" type="password" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green btn-default" id="btnLogin" value="Submit" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <a href="ResetPassword.aspx">Forgot Password?</a>
                </p>
            </div>
        </div>
    </div>
    <div id="divStartDobson" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-6" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnNewCustomer" value="Create New Customer" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnBeginClaim" value="Begin Claim" />
                </p>
            </div>
        </div>
    </div>
    <div id="divCreateCustomer" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: center; align-content: center; color: white;">
                <p>
                    Enter Customer Information
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtFirstName" maxlength="50" placeholder="First Name" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtLastName" maxlength="50" placeholder="Last Name" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtAddress1" maxlength="50" placeholder="Address Line 1" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtAddress2" maxlength="50" placeholder="Address Line 2" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtCity" maxlength="50" placeholder="City" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtPostalCode" maxlength="10" placeholder="ZIP Code" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtEmail" maxlength="50" placeholder="Email Address" type="email" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtPhone" maxlength="50" placeholder="Phone Number XXX-XXX-XXXX" type="tel" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnCreateCustomer" value="Submit" />
                </p>
            </div>
        </div>
    </div>
    <div id="divVerifyCustomer" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: center; align-content: center; color: white;">
                <p>
                    Verify Customer Information
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtFirstNameV" maxlength="50" placeholder="First Name" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtLastNameV" maxlength="50" placeholder="Last Name" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtAddress1V" maxlength="50" placeholder="Address Line 1" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtAddress2V" maxlength="50" placeholder="Address Line 2" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtCityV" maxlength="50" placeholder="City" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtPostalCodeV" maxlength="10" placeholder="ZIP Code" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtEmailV" maxlength="50" placeholder="Email Address" type="email" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtPhoneV" maxlength="50" placeholder="Phone Number XXX-XXX-XXXX" type="tel" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnVerifyCustomer" value="Next" />
                </p>
            </div>
        </div>
    </div>
    <div id="divSelectRouter" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: center; align-content: center; color: white;">
                <p>
                    Select Router
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 400px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <label style="color: white;">
                        <input type="radio" name="router" value="Nokia">&nbsp;&nbsp;Nokia</label>
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 400px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <label style="color: white;">
                        <input type="radio" name="router" value="Calix">&nbsp;&nbsp;Calix</label>
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnSelectRouter" value="Next" />
                </p>
            </div>
        </div>
    </div>
    <div id="divRouterShipping" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start; color: white; font-weight: bold;">
                <p>
                    Router Shipping To:
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 320px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start; color: white;">
                <p>
                    <label id="lblName"></label>
                    <br />
                    <label id="lblAddress1"></label>
                    <br />
                    <label id="lblAddress2"></label>
                    <br />
                    <label id="lblCity"></label>
                    ,
                    <label id="lblState"></label>
                    &nbsp;<label id="lblPostalCode"></label>
                </p>
                <p>
                    <label id="lblPhone"></label>
                    <br />
                    <label id="lblEmail"></label>
                </p>
                <p>
                    <label id="lblRouter"></label>
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnSubmitRouter" value="Next" />
                </p>
            </div>
        </div>
    </div>
    <div id="divOrderSubmitted" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start; color: white; font-weight: bold;">
                <p>
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 200px; width: 590px;">
            <div class="col-md-12" style="text-align: center; align-content: center; color: black; background-color: white; font-weight: bold; display: none;" id="divOrderSuccess">
                <p>
                    Your order has been submitted. If it is prior to 4pm Central Time, your order will ship overnight today. If it is after 4pm Central,
                    your order will ship overnight on the next business day.<br />
                    <br />
                    Select OK to return to the start page.
                </p>
                <p>
                    <input type="button" class="btn btn-green" id="btnFinished" value="OK" />
                </p>
            </div>
        </div>
    </div>
    <div id="divNewClaim" class="container body-content" style="display: block; width: 960px; padding-top: 100px; display: none;">
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: center; align-content: center; color: white;">
                <p>
                    Enter Customer Name or Phone Number
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtFirstNameClaim" maxlength="50" placeholder="First Name" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: left; align-content: start;">
                <p>
                    <input type="text" style="width: 205px;" id="txtLastNameClaim" maxlength="50" placeholder="Last Name" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 220px; width: 550px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <input style="width: 480px; max-width: 480px;" id="txtPhoneClaim" maxlength="50" placeholder="Phone Number XXX-XXX-XXXX" type="tel" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: center; align-content: center;">
                <p>
                    <input type="button" class="btn btn-green" id="btnFindCustomer" value="Submit" />
                </p>
            </div>
        </div>
    </div>
</asp:Content>
