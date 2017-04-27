<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="WebsiteBuilder.aspx.cs"
    Inherits="ResellerClub.WebUI.WebsiteBuilder" EnableEventValidation="false" ValidateRequest="false"
    Title="Website Builder" %>
<%@ Register Src="~/UserControl/PlanPanel.ascx" TagName="PlanPanel" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/AllFeature.ascx" TagName="AllFeature" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/Header.ascx" TagName="Header" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="maincontent" border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
        <tr>
            <td>
                <uc4:Header ID="Header1" runat="server" />
            </td>
        </tr>
        <tr>
            <td id="content" valign="top">
                <div style="padding-top: 0px;">
                    <div class="PageHeading" style="width: 183px; padding-top: 30px;">
                        Select Your <span>Plan</span>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:PlanPanel ID="PlanPanel1" runat="server" />
                <uc3:AllFeature ID="AllFeature1" runat="server" />
            </td>
        </tr>
    </table>
    <%--PlanPanel.ascx Content--%>
    <div id="divPlanFeature" style="display: none">
        <div id="divPlanFeature1" runat="server">
          <ul>
                <li><strong>1</strong> Page</li>
                <li><strong>1 GB</strong> Web Space</li>
                <li><strong>Unlimited</strong> Data Transfer</li>
                <li><strong>100<strong> Email Accounts</li>
            </ul>
        </div>
        <div id="divPlanFeature2" runat="server">
         <ul>
                <li><strong>50</strong> Pages</li>
                <li><strong>5 GB</strong> Web Space</li>
                <li><strong>Unlimited</strong> Data Transfer</li>
                <li><strong>Unlimited</strong> Email Accounts</li>
            </ul>
        </div>
        <div id="divPlanFeature3" runat="server">
        
          
        </div>
        <div id="divPlanFeature4" runat="server">
           
        </div>
    </div>
    <%-- Header.ascx Content --%>
    <div id="divHeaderText" runat="server" visible="false">
        Easy to use <span>Website Builder </span>
    </div>
    <div id="divHeaderFeature" runat="server" visible="false">
        <ul>
            <li>Choose from 175 themes & 85,000 images</li>
            <li>Easy Editing with a Drag & Drop Interface</li>
            <li>Go Live Instantly - Publish your website in minutes</li>
        </ul>
        <input type="button" value="view demo" />
    </div>
    <div id="divHeaderImage" runat="server" visible="false">
        <div style="padding-left: 210px;">
            <img src="../images/WebSideBuilderHeader.png" />
        </div>
    </div>
</asp:Content>
