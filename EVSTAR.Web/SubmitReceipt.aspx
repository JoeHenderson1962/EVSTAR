﻿<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site_Evstar.Master" AutoEventWireup="true" CodeBehind="SubmitReceipt.aspx.cs" Inherits="EVSTAR.Web.SubmitReceipt" %>

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
                    <a href="WhatsCovered.aspx" class="header-desktop-link-text">What's covered?</a>
                </div>
                <div class="header-desktop-link2">
                    <a href="FAQ.aspx" class="header-desktop-link-text">FAQs</a>
                </div>
                <div class="btn header-button" id="btnMyAccount">
                    <span class="header-button-icon">
                        <i class="fa-regular fa-circle-user"></i></span>
                    <span class="header-button-content">My account</span>
                </div>
            </div>
        </div>
    </div>
    <div class="newprod-panel-desktop">
        <div class="newprod-block-desktop">
            <div class="newprod-content-desktop">
                <div class="newprod-content-highlights-desktop">
                    Submit Your Receipt
               
                </div>
                <div class="frame-506349-desktop">
                    <div id="pnlFacImport" class="drop-area">
                        <p><span style="font-size: smaller;">Upload your scanned receipt (PDF, JPG, or PNG) by selecting &quot;choose file&quot; and selecting 
                            a saved file from your computer or by dragging and dropping the file into the box.</span></p>
                        <input type="file" id="fileUpload" accept="application/pdf;image/*" onchange="javascript: handleFiles(this.files);" />
                        <textarea id="txtUploadStatus" class="UploadTextBox"></textarea><br />
                    </div>
                </div>
                <div class="newprod-button-row-desktop" style="gap: 135px;padding-top:200px;">
                    <div class="button-back-desktop" id="btnSubmitReceiptBack">
                        <span class="button-back-text-desktop"><span class="button-back-text-inner-desktop">Back</span></span>
                    </div>
                    <div class="save-button-desktop btn-default" id="btnSubmitReceiptContinue" style="width: 150px;">
                        <span class="save-button-text-desktop" style="width: 80px;">
                            <span class="save-button-text-inner-desktop" style="width: 80px;">Continue</span>

                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="my-plan-block-desktop2" style="top: 868px;">
        <div class="my-plan-pinstripe-desktop"></div>
        <div class="my-plan-line-desktop">
            <div class="my-plan-main-desktop">
                <span class="my-plan-text-desktop">My plan:</span>
            </div>
            <span class="my-plan-con-home-desktop"><label id="lblPlanName">Connected Home Protection</label></span>
        </div>
    </div>
    <div class="footer-desktop-login2" style="top: 943px;">
        <img src="Content/images/evstar-logo-white-small.png" alt="EVSTAR" class="footer-logo-desktop" />
        <div class="footer-copyright">
            EVSTAR &copy; 2023
       
        </div>
    </div>
</asp:Content>
