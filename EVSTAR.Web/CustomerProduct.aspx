<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="CustomerProduct.aspx.cs" Inherits="EVSTAR.Web.CustomerProduct" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>
    <div id="divProductType" class="container body-content" style="display: block; width: 960px;">
        <div class="row" style="margin-bottom: 20px; margin-left: 30px;">
            <h5>Select the device you need help with.</h5>
        </div>
        <div id="divProducts">
        </div>
        <div class="row" style="margin-top: 40px; margin-right: 30px;">
            <div class="col-md-2" style="text-align: left; align-content: start;">
                <p>
                    <input type="button" class="btn btn-green" id="btnProductBack" value="&laquo; Return to My Devices" />
                </p>
            </div>
            <div class="col-md-2" style="text-align: left; align-content: start;">
                <p>
                </p>
            </div>
            <div class="col-md-8" style="text-align: center; align-content: center;">
                <p>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
