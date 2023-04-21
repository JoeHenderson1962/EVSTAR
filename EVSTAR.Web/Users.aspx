<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EVSTAR.Web.Users" MasterPageFile="~/Site_Evstar.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
    <div id="divWait" style="width: 100%; height: 200%; background-color: gray; opacity: 0.5; display: none; text-align: center; vertical-align: middle; font-size: 18pt; position: absolute; top: 0px; left: 0px; z-index: 200; color: white;">
        <img src="Content/circledots_32.gif" style="position: relative; top: 100px; left: 0px;" />
        Please wait...
    </div>

    <div class="text-center" style="width: 1200px;">
        <table style="margin: 0 auto; width: 100%;">
            <thead>
                <tr>
                    <th style="font-weight: bold; text-align: left; width: 250px; padding: 8px;"></th>
                    <th style="text-align: right; padding: 8px;"></th>
                </tr>
            </thead>
        </table>
        <div id="divUsersGrid" style="padding-top: 10px; margin-top: 30px; font-size: 0.7em;"></div>
        <table style="width: 100%;">
            <tr>
                <td style="padding-right: 10px; padding-top: 10px; text-align: center; vertical-align: top;">
                    <input type="button" class="btn btn-green" id="btnMyClaims" value="View Claims" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

