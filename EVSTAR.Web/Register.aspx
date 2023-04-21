<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EVSTAR.Web.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>

    <div id="divRegister" class="container body-content" style="display: block; width: 960px;">
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
                <div class="col-md-2" style="text-align: left; align-content: start;">
                    <p>
                        <input type="button" class="btn btn-green btn-default" id="btnValidateCode" value="Validate" />
                    </p>
                </div>
                <div class="col-md-4" style="text-align: left; align-content: start;">
                    <p>
                    </p>
                </div>
                <div class="col-md-6" style="text-align: center; align-content: center;">
                    <p>
                    </p>
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
                <div class="col-md-2" style="text-align: left; align-content: start;">
                    <p>
                        <input type="button" class="btn btn-green" id="btnRegCancel" value="Cancel" />
                    </p>
                </div>
                <div class="col-md-4" style="text-align: left; align-content: start;">
                    <p>
                        <input type="button" class="btn btn-green btn-default" id="btnRegister" value="Register" />
                    </p>
                </div>
                <div class="col-md-6" style="text-align: center; align-content: center;">
                    <p>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
