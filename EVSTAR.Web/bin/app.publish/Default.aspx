<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EVSTAR.Web.Dobson" %>

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
    <div class="hero-desktop">
        <div class="left-box-desktop">
            <div class="hero-textbox-desktop">
                <div class="connected-home-prot-desktop">
                    <label id="lblPlanName"></label>
                </div>
                <div class="hi-textbox-desktop">
                    Hi. How can we help you?
                </div>
                <div id="btnRegisterProduct" class="btn register-button-desktop">
                    <span class="register-button-content-desktop">
                        <i class="fa-regular fa-square-check"></i></span>
                    <div class="register-button-text-desktop">
                        <div class="register-text-desktop" id="btnRegisterProductText">Register a product
                        </div>
                    </div>
                </div>
                <div id="btnNewClaim" class="btn file-button-desktop">
                    <span class="file-button-text-desktop">
                        <span class="file-text-desktop">File a claim</span>
                    </span>
                </div>
            </div>
            <div class="lnk reg-code-link-desktop">
                <div class="reg-code-link-inner-desktop" id="lnkRegistrationCode">
                    <div class="reg-code-link-placeholder-desktop">
                        <span class="reg-code-link-icon-desktop">
                            <i class="fa-regular fa-envelope-open"></i>
                        </span>
                    </div>
                    <a href="Register.aspx" class="reg-code-link-text-desktop">I have a registration code</a>
                </div>
            </div>
        </div>
        <div class="hero-image-desktop">
        </div>
        <div class="body-text-block-desktop">
            <div class="head-body-desktop">
                <div class="head-body-text-desktop">
                    We offer the protection you can trust
                </div>
            </div>
            <div class="sub-body-desktop">
                <div class="sub-body-text-desktop">
                    Here's what you can expect
                </div>
            </div>
        </div>
        <div class="callout-desktop">
            <div class="callout1-desktop">
                <div class="callout-header-text-desktop">
                    <span class="callout-header-icr-desktop">
                        <span class="callout-header-icon-desktop">
                            <i class="fa-regular fa-circle-user"></i>
                        </span>
                    </span>
                    <div class="callout-header-frame-desktop">
                        <div class="callout-header-ftext-desktop">
                            Set Up Your Account
                        </div>
                    </div>
                </div>
                <span class="callout-text-desktop">With a quick few steps, you'll be able to log in. Once you're logged in, 
                    you can view your account.
                </span>
            </div>
            <div class="callout2-desktop">
                <div class="callout-header-text-desktop">
                    <span class="callout-header-icr-desktop">
                        <span class="callout-header-icon-desktop">
                            <i class="fa-regular fa-file-excel"></i>
                        </span>
                    </span>
                    <div class="callout-header-frame-desktop">
                        <div class="callout2-header-ftext-desktop">
                            Start a Claim
                        </div>
                    </div>
                </div>
                <span class="callout-text-desktop">You'll select your product and tell us what's wrong and when it happened. 
                    You'll pay your service fee, if there is one, and submit your claim.
                </span>
            </div>
            <div class="callout3-desktop">
                <div class="callout-header-text-desktop">
                    <span class="callout-header-icr-desktop">
                        <span class="callout-header-icon-desktop">
                            <i class="fa-regular fa-rectangle-list"></i>
                        </span>
                    </span>
                    <div class="callout-header-frame-desktop">
                        <div class="callout2-header-ftext-desktop">
                            View a Claim Status
                        </div>
                    </div>
                </div>
                <span class="callout-text-desktop">You can also view a claim status by accessing the dashboard 
                    and selecting your device.
                </span>
            </div>
            <div class="callout4-desktop">
                <div class="callout-header-text-desktop">
                    <span class="callout-header-icr-desktop">
                        <span class="callout-header-icon-desktop">
                            <i class="fa-regular fa-face-grin-beam"></i>
                        </span>
                    </span>
                    <div class="callout-header-frame-desktop">
                        <span class="callout2-header-ftext-desktop">Next Steps
                        </span>
                    </div>
                </div>
                <span class="callout-text-desktop">Depending on your device, you may see instructions on 
                    shipping your device back to us for repair, or you may 
                    be receiving a new device. We will make sure you're 
                    taken care of quickly and efficiently.
                </span>
            </div>
        </div>
        <div class="pinstripe"></div>
        <div class="bottom-text-body-desktop">
            <span class="bottom-text-desktop">Got questions?</span>
            <div class="bottom-link-body-desktop">
                <div class="bottom-link-desktop">
                    <a href="FAQ.aspx" class="bottom-link-text-desktop lnk">View our FAQs</a>
                </div>
            </div>
        </div>
    </div>
    <div class="footer-desktop">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
        </div>
    </div>
</asp:Content>
