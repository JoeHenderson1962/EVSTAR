<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site_SCA.Master" AutoEventWireup="true" CodeBehind="ClaimsAdmin.aspx.cs"
    Inherits="EVSTAR.Web.ClaimsAdmin" MaintainScrollPositionOnPostback="true" ClientIDMode="Static" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel CssClass="login-block-desktop" runat="server">
        <asp:Panel runat="server" CssClass="login-content-desktop" ID="divDesktop">
            <div class="login-content-highlights-desktop">
                Pending Claims
            </div>
            <div>
                <div id="divDobsonClaims" style="width:100%;">

                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:HiddenField ID="hidSessionID" runat="server" Value="" />
    <script src="https://unpkg.com/dropzone@5/dist/min/dropzone.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/dropzone@5/dist/min/dropzone.min.css" type="text/css" />
    <script src="/Scripts/claimsadmin.js" type="module"></script>
</asp:Content>
