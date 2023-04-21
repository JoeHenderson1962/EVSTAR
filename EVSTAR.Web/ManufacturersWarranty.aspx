<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManufacturersWarranty.aspx.cs" Inherits="EVSTAR.Web.ManufacturersWarranty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content" style="display: block; width: 960px;">
    </div>
    <div class="row" style="margin-bottom: 30px; margin-left: 30px; margin-right: 30px;">
        <h4 style="color: black;">
            <span style="color: black;">Based on our information, your device is still covered under the manufacturer's warranty.</span>
        </h4>
        <h4 style="color: black;">
            <span style="color: black;">
                For assistance with your device, please contact the manufacturer directly or the original seller of your device. If you have any other questions, 
                please feel free to contact us 800-555-5555, use the chat feature or email us at <a href="mailto:claims@gotechcycle.com.">claims@gotechcycle.com</a><br />
            </span>
        </h4>
    </div>
    <div class="row" style="margin-left: 30px; margin-top: 20px; margin-right: 30px;">
        <div class="col-md-2" style="text-align: left; align-content: start;">
            <p>
                <input type="button" class="btn btn-green btn-default" id="btnMfrWarr" value="Continue &raquo;" />
            </p>
        </div>
        <div class="col-md-4" style="text-align: left; align-content: start;">
        </div>
        <div class="col-md-6" style="text-align: center; align-content: center;">
            <p>
            </p>
        </div>
    </div>
</asp:Content>
