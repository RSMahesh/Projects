<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="EmailHosting.aspx.cs" Inherits="ResellerClub.WebUI.EmailHosting"
    Title="Email Hosting" %>

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
    <div id="divPlanFeature" style="display: none">
        <div id="divPlanFeature1" runat="server">
            <ul>
                <li><strong>5</strong> Email accounts</li>
                <li><strong>Unlimited </strong>IMAP/POP</li>
                <li><strong>Unlimited </strong>Forwards</li>
                <li><strong>Unlimited </strong>Aliases</li>
            </ul>
        </div>
        <div id="divPlanFeature2" runat="server">
            <ul>
                <li><strong>25</strong> Email accounts</li>
                <li><strong>Unlimited </strong>IMAP/POP</li>
                <li><strong>Unlimited </strong>Forwards</li>
                <li><strong>Unlimited </strong>Aliases</li>
            </ul>
        </div>
        <div id="divPlanFeature3" runat="server">
            <ul>
                <li><strong>100 </strong>Email accounts</li>
                <li><strong>Unlimited </strong>IMAP/POP</li>
                <li><strong>Unlimited </strong>Forwards</li>
                <li><strong>Unlimited </strong>Aliases</li>
            </ul>
        </div>
        <div id="divPlanFeature4" runat="server">
            <ul>
                <li><strong>Unlimited</strong> Email accounts</li>
                <li><strong>Unlimited </strong>IMAP/POP</li>
                <li><strong>Unlimited </strong>Forwards</li>
                <li><strong>Unlimited </strong>Aliases</li>
            </ul>
        </div>
    </div>
    <%-- Header.ascx Content --%>
    <div id="divHeaderText" runat="server" visible="false">
        Business class <span>Email</span>
    </div>
    <div id="divHeaderFeature" runat="server" visible="false">
        <ul>
            <li>2 GB Space per Account</li>
            <li>Advanced Spam/Virus Protection</li>
            <li>30-Day Money-Back Guarantee</li>
        </ul>
    </div>
    <div id="divHeaderImage" runat="server" visible="false">
        <div style="padding-left: 310px;">
            <img src="../images/email-hosting.png" />
        </div>
    </div>
</asp:Content>
