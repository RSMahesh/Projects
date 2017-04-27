<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="DigitalCertificates.aspx.cs" Inherits="ResellerClub.WebUI.DigitalCertificates"
    Title="Digital Certificates" %>

<%@ Register Src="UserControl/PlanPanel.ascx" TagName="PlanPanel" TagPrefix="uc1" %>
<%@ Register Src="UserControl/AllFeature.ascx" TagName="AllFeature" TagPrefix="uc3" %>
<%@ Register Src="UserControl/Header.ascx" TagName="Header" TagPrefix="uc4" %>
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
    <div id="divPlanFeature" style="display: none">
        <div id="divPlanFeature1" runat="server">
            <ul>
                <li><strong>Internal</strong> Servers</li>
                <li><strong>Domain </strong>Validation </li>
                <li><strong>1</strong> Domain</li>
            </ul>
        </div>
        <div id="divPlanFeature2" runat="server">
            <ul>
                <li><strong>Public</strong> Websites</li>
                <li><strong>Organization </strong>Validation </li>
                <li><strong>1</strong> Domain</li>
            </ul>
        </div>
        <div id="divPlanFeature3" runat="server">
            <ul>
                <li><strong>E-commerce</strong> Websites</li>
                <li><strong>Full Organization </strong>Validation </li>
                <li><strong>1</strong> Domain</li>
            </ul>
        </div>
        <div id="divPlanFeature4" runat="server">
            <ul>
                <li><strong>Multiple</strong> Sub-Domains</li>
                <li><strong>Full Organization </strong>Validation </li>
                <li><strong>Unlimited</strong> Subdomains</li>
            </ul>
        </div>
    </div>
    <%-- Header.ascx Content --%>
    <div id="divHeaderText" runat="server" visible="false">
        Secure <span>SSL</span>
    </div>
    <div id="divHeaderFeature" runat="server" visible="false">
        <ul>
            <li>Upto 256-bit Encryption</li>
            <li>Free Reissues Included</li>
            <li>Maximum Browser Compatibility</li>
        </ul>
    </div>
    <div id="divHeaderImage" runat="server" visible="false">
        <div style="padding-left: 210px;">
            <img src="images/SSL.png" />
        </div>
    </div>
</asp:Content>
