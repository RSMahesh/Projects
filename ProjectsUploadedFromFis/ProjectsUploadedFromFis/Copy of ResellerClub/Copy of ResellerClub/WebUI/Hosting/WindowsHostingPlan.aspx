<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="WindowsHostingPlan.aspx.cs"
    Inherits="ResellerClub.WebUI.WindowsHostingPlan" Title="Windows Hosting" %>

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
                <div class="install-software" style="padding-top: 30px; padding-bottom: 1px; padding-left: 10px;">
                    Install these <span>softwares</span> in just 1 - click!</div>
                <div style="padding-left: 10px;">
                    <img src="../images/linux-hosting-software.gif" />
                </div>
                <uc3:AllFeature ID="AllFeature1" runat="server" />
            </td>
        </tr>
    </table>
    <%--PlanPanel.ascx Content--%>
     <div id="divPlanFeature" style="display: none">
        <div id="divPlanFeature1" runat="server">
            <ul id="ul1">
                <li><strong>1 GB</strong> Disk space </li>
                <li><strong>Unlimited </strong> Email accounts</li>
                <li><strong>10 GB</strong> Data transfer </li>
            </ul>
        </div>
        <div id="divPlanFeature2" runat="server">
            <ul id="ul2">
                <li><strong>Unlimited</strong> Disk space </li>
                <li><strong>Unlimited</strong> Email accounts</li>
                <li><strong>Unlimited</strong> Data transfer </li>
            </ul>
        </div>
        <div id="divPlanFeature3" runat="server">
            <ul id="ul3">
            </ul>
        </div>
        <div id="divPlanFeature4" runat="server">
            <ul id="ul4">
            </ul>
        </div>
    </div>
    <%-- Header.ascx Content --%>
    <div id="divHeaderText" runat="server" visible="false">
        Powerful <span>Windows Hosting</span>
    </div>
    <div id="divHeaderFeature" runat="server" visible="false">
        <ul>
            <li>State-of-the-Art Hosting Infrastructure</li>
            <li>99.9% Uptime Guarantee</li>
            <li>30-Day Money-Back Guarantee</li>
        </ul>
    </div>
    <div id="divHeaderImage" runat="server" visible="false">
        <div style="padding-left: 250px;">
            <img src="../images/windows-hosting-server.png" />
        </div>
    </div>
</asp:Content>
