<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="Default_old.aspx.cs" Inherits="Techcycle.Web._Default_old" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>

    <div id="divLanding" class="container body-content" style="display: block; width: 960px;">
        <div style="font-size: 1.7em; font-weight: 500; text-align: center; width: 100%; padding-top: 36px; padding-bottom: 36px;">
            Tell us what you'd like to do...
            <div class="row" style="font-size: 0.8em; align-content: center; margin: 0 auto; padding-top: 20px; text-align: center;" id="divWholeHomeIcons">
                <div class="col-md-4">
                    <div style="border-radius: 10px; border-width: 2px; border-style: solid; height: 240px; width: 240px; padding-top: 5px; text-align: center; margin: 0 auto;">
                        <a id="lnkRegisterIcon" href="Login.aspx">
                            <img src="Content/images/register.png" alt="Register Product" style="height: 180px; width: 215px;" /></a>
                        <p>
                            Register Product
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div style="border-radius: 10px; border-width: 2px; border-style: solid; height: 240px; width: 240px; padding-top: 5px; text-align: center; margin: 0 auto;">
                        <a href="Login.aspx">
                            <img src="Content/images/startclaim.png" alt="Start a Claim" style="height: 180px; width: 130px;" /></a>
                        <p>
                            Start a Claim
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div style="border-radius: 10px; border-width: 2px; border-style: solid; height: 240px; width: 240px; padding-top: 5px; text-align: center; margin: 0 auto;">
                        <a href="FAQ.aspx">
                            <img src="Content/images/viewcoverage.png" alt="View FAQs" style="height: 180px; width: 130px;" /></a>
                        <p>
                            View FAQs
                        </p>
                    </div>
                </div>
            </div>
            <div class="row" style="font-size: 0.8em; align-content: center; margin: 0 auto; padding-top: 20px; text-align: center; display: none;" id="divDobsonIcons">
                <div class="col-md-6">
                    <p>
                        <input type="button" class="btn btn-green" id="btnRegisterAccount" value="Register Your Account" />
                    </p>
                </div>
                <div class="col-md-6">
                    <p>
                        <input type="button" class="btn btn-green btn-default" id="btnLoginAccount" value="Login to Your Account" />
                    </p>
                </div>
            </div>
            <div class="row" style="width: 100%; height: 30px;">
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; margin: 0 auto;">
                </div>
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; margin: 0 auto;">
                </div>
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; margin: 0 auto;">
                </div>
            </div>
        </div>
        <div style="background-color: white; width: 100%; display: none;" id="divWholeHomeLanding">
            <div class="row" style="padding-top: 25px;">
                <div class="col-md-6" style="padding-top: 20px; padding-left: 50px; text-align: left; align-content: start;">
                    <hr style="width: 40%;" />
                    <p style="font-size: 1.4em; font-weight: 700;">
                        What happens next...
                    </p>
                    <h5>Register Your Account</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        With a quick few steps, you'll be able to log in. Once you're logged in, you can register and view your products.
                    </p>
                    <h5>Start a Claim</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        You’ll select your product and tell us what’s wrong and when it happened. You’ll pay your service fee, if there is one, and submit your claim.
                    </p>
                    <h5>View a Claim Status</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        You can also view a claim status by accessing the dashboard and selecting your device.
                    </p>
                    <h5 id="hdrNextSteps">Next Steps</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;" id="pNextSteps">
                        Depending on your device, you may see instructions on shipping your device back to us for repair, or you may be receiving a new device. 
                        We will make sure you’re taken care of quickly and efficiently.  
                    </p>
                    <h5>Need Help?</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        At any time in the process, let us know if you need assistance or have any questions.  We are here to help.  You can reach us at 866-772-2111.  
                    </p>
                </div>
                <div class="col-md-6" style="padding-top: 40px;">
                    <img src="Content/images/GOtechcycle_homepage_photo_small.jpg" alt="gotechcycle_homepage" />
                </div>
            </div>
        </div>
        <div style="background-color: white; width: 100%; display: none;" id="divEVSTARLanding">
            <div class="row" style="padding-top: 25px;">
                <div class="col-md-12" style="padding-top: 20px; padding-left: 120px; padding-right: 70px; text-align: left; align-content: start;">
                    <hr style="width: 40%;" />
                    <p style="font-size: 1.4em; font-weight: 700;">
                        What happens next...
                    </p>
                    <h5>First Time logging in?</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        Use the registration code provided in your welcome email to set up your new account.  With a quick few steps, you’ll be able to login and view your products.
                    </p>
                    <h5>Start a Claim</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        You’ll select your product and tell us what’s wrong and when it happened. Once you’ve verified your information, your claim will be initiated. 
                    </p>
                    <h5>View a Claim Status</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        You can also view a claim status by accessing the dashboard and selecting your device.
                    </p>
                    <h5>Next Steps</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        Depending on your device, you may see instructions on shipping your device back to us for repair, or you may be receiving a new device. 
                        We will make sure you’re taken care of quickly and efficiently.  
                    </p>
                    <h5>Need Help?</h5>
                    <p style="font-size: 1.0em; padding-left: 20px; padding-right: 20px; color: black;">
                        At any time in the process, let us know if you need assistance or have any questions.  We are here to help.  You can reach us at 866-772-2111.  
                    </p>
                </div>
            </div>
            <div class="row" style="padding-top: 5px;">
                <%--                <div class="col-md-12" style="padding-top: 20px; padding-left: 50px; text-align: left;">
                    <p style="font-size: 1.1em; font-weight: 700;">
                        GoTechcycle device protection is available through the following partners...
                    </p>
                </div>--%>
            </div>
            <div class="row" style="width: 100%; display: none;">
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; height: 100px; padding-top: 15px;">
                    <img src="Content/images/truckersave.png" alt="truckersave" />
                </div>
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; height: 100px;">
                    <img src="Content/images/benefit-hub-logo-nsa-image-1.png" alt="benefithub" />
                </div>
                <div class="col-md-4" style="text-align: center; align-content: center; vertical-align: middle; height: 100px; padding-top: 15px;">
                    <img src="Content/images/Loop_logo-blue-small.png" alt="loop" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
