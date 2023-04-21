<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerInfo.aspx.cs" Inherits="Techcycle.Web.CustomerInfo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>

    <div id="divCustomer" class="container body-content" style="display: block; width: 960px;">
        <div class="row" style="margin-bottom: 30px; margin-left: 30px;">
            <h2 style="color: black;">
                <span>We need a few details to access your claims portal.</span>
            </h2>
        </div>

        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12">
                <h5>First Name</h5>
                <p>
                    <input type="text" style="width: 500px;" id="txtFirstName" maxlength="50" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12">
                <h5>Last Name</h5>
                <p>
                    <input type="text" style="width: 500px;" id="txtLastName" maxlength="50" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12">
                <h5>ZIP Code On Your Account</h5>
                <p>
                    <input type="text" style="width: 500px;" id="txtPostalCode" maxlength="5" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; width: 100%;">
            <div class="col-md-12">
                <h5>Phone Number On Your Account</h5>
                <p>
                    <input type="text" style="width: 500px;" id="txtPhoneNumber" maxlength="15" />
                </p>
            </div>
        </div>
        <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
            <div class="col-md-2" style="text-align: left; align-content: start;">
                <p>
                    <input type="button" class="btn btn-green" id="btnLookupCustomerBack" value="&laquo; Back" />
                </p>
            </div>
            <div class="col-md-4" style="text-align: left; align-content: start;">
                <p>
                    <input type="button" class="btn btn-green btn-default" id="btnLookupCustomer" value="Continue &raquo;" />
                </p>
            </div>
            <div class="col-md-6" style="text-align: center; align-content: center;">
                <p>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
