<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayInvoice.aspx.cs" Inherits="EVSTAR.Web.PayInvoice" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
    <style>
        .ui-autocomplete {
            font-size: 11px;
            text-align: left;
        }
    </style>
    <div id="divWait" style="width: 100%; height: 100%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>

    <div class="text-center" style="width: 960px; background-color: azure;">
        <div id="divInvoiceNumber" class="text-center" style="align-content: center; margin: 0 auto; width: 960px; background-color: white;">
            <p>&nbsp;</p>
            <h5>EVSTAR Invoice Payments</h5>
            <table style="margin: 0 auto; border: 1px solid gray;">
                <tr>
                    <td style="font-weight: bold; text-align: left; width: 500px; padding: 8px;">Enter Your Invoice Number:</td>
                    <td style="text-align: left; padding: 8px;">
                        <input type="text" id="txtInvoiceNumber" maxlength="50" style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; text-align: left; width: 500px; padding: 8px;">Enter Your Email Address:</td>
                    <td style="text-align: left; padding: 8px;">
                        <input type="text" id="txtEmailAddress" maxlength="30" style="width: 600px;" /></td>
                </tr>
                <tr>
                    <td style="font-weight: bold; text-align: left; vertical-align: top; width: 500px; padding: 8px;"></td>
                    <td style="text-align: left; padding: 8px;">
                        <input type="button" class="btn btn-green" id="btnValidateInvoice" value="Verify" />
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
