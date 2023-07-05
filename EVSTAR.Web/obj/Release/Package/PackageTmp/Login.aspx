<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EVSTAR.Web.Login" %>

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
                <%--                <div class="btn header-button" id="btnMyAccount">
                    <span class="header-button-icon">
                        <i class="fa-regular fa-circle-user"></i></span>
                    <span class="header-button-content">My account</span>
                </div>--%>
            </div>
        </div>
    </div>
    <div class="login-block-desktop">
        <div class="login-panel-desktop">
            <div class="login-content-desktop">
                <div class="login-content-highlights-desktop" id="divLoginWithText" style="width: 700px;">
                    Log in with your email address and password.
               
                </div>

                <div class="login-content-text-field1-desktop" style="width: 700px;">
                    <span class="login-text-input-label-desktop" id="loginType" style="width: 700px;">Email<br />
                    </span>
                    <input id="txtLoginEmail" type="text" maxlength="128" class="login-text-input-desktop" />
                    <div class="helper-text-box-desktop">
                        <span class="helper-text-desktop" id="loginTypeDescription">The email on your plan
                        </span>
                    </div>
                </div>
                <div class="login-content-text-field2-desktop" style="width: 700px;">
                    <span class="login-text-input-label-desktop">Password<br />
                    </span>
                    <input id="txtLoginPassword" maxlength="50" type="password" class="login-text-input-desktop" />
                </div>
                <div class="login-button-row-desktop">
                    <div class="button-back-desktop" id="btnLoginCancel">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="button-desktop btn-default" id="btnLogin">
                        <span class="button-text-desktop"><span class="button-text-inner-desktop">Login</span></span>
                    </div>
                </div>
                <%--        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-12" style="text-align: left; align-content: start;">
                <p>
                    <a href="ResetPassword.aspx">Forgot Password?</a>
                </p>
            </div>
        </div>--%>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop">
                <label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>
