<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_evstar.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EVSTAR.Web.Register" %>

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
            <div class="verify-content-desktop" style="width: 1000px; height: 600px;">
                <div id="divRegister" class="container body-content" style="display: block; width: 900px; height:600px; overflow-x:hidden;">
                    <div class="row" style="margin-bottom: 30px; margin-left: 30px;">
                        <h2 style="color: black;">
                            <span>Register Your Account</span>
                        </h2>
                    </div>
                    <div id="divRegCode" style="display: none;">
                        <div class="row" style="margin-left: 30px; width: 100%;">
                            <div class="col-md-12">
                                <h5>Enter your Registration Code</h5>
                                <p>
                                    <input type="text" style="width: 500px;" id="txtRegCode" maxlength="128" placeholder="Registration Code" />
                                </p>
                            </div>
                        </div>
                        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
                            <div class="verify-spacer-desktop" style="order: 2;">
                            </div>
                            <div class="verify-button-row-desktop" style="position: absolute; top: 500px;">
                                <div class="button-back-desktop" id="btnValidateCodeBack">
                                    <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                                </div>
                                <div class="verify-button-desktop btn-default" id="btnValidateCode">
                                    <span class="verify-button-text-desktop"><span class="verify-button-text-inner-desktop">Validate</span></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divRegInfo">
                        <div class="row" style="margin-left: 30px; width: 100%;">
                            <div class="col-md-12">
                                <h5>Email</h5>
                                <p>
                                    <input type="text" style="width: 500px;" id="txtRegEmail" maxlength="128" placeholder="Email Address" />
                                </p>
                            </div>
                        </div>
                        <div class="row" style="margin-left: 30px; width: 100%;">
                            <div class="col-md-12">
                                <h5>Password</h5>
                                <p>
                                    <input style="width: 500px;" id="txtRegPassword1" maxlength="50" placeholder="Create Password" type="password" />
                                </p>
                            </div>
                        </div>
                        <div class="row" style="margin-left: 30px; width: 100%;">
                            <div class="col-md-12">
                                <h5>Confirm Password</h5>
                                <p>
                                    <input style="width: 500px;" id="txtRegPassword2" maxlength="50" placeholder="Confirm Password" type="password" />
                                </p>
                            </div>
                        </div>
                        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
                            <div class="verify-spacer-desktop" style="order: 2;">
                            </div>
                            <div class="verify-button-row-desktop" style="position: absolute; top: 500px;">
                                <div class="button-back-desktop" id="btnRegCancel">
                                    <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                                </div>
                                <div class="verify-button-desktop btn-default" id="btnRegister">
                                    <span class="verify-button-text-desktop"><span class="verify-button-text-inner-desktop">Continue</span></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
<%--                <span class="my-plan-text-desktop">My plan:</span>--%>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName" style="display:none;">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>
