<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AvailableDomainList.ascx.cs"
    Inherits="ResellerClub.WebUI.AvailableDomainList" %>
<style>
    H2.selectFromRecommended
    {
        padding-bottom: 0px;
        margin: 0px;
        padding-left: 7px;
        padding-right: 0px;
        background: url(images/bg_recommend.gif) no-repeat left top;
        letter-spacing: 0.2px;
        height: 26px;
        color: #ec8c56;
        font-size: 12px;
        font-weight: normal;
        padding-top: 4px;
    }
    H2.selectFromRecommended A.hint
    {
        padding-right: 17px;
        background: url(images/small-help-icon-grey.gif) no-repeat right top;
        color: #777777;
        margin-left: 20px;
        font-size: 11px;
        cursor: help;
        text-decoration: none;
    }
</style>
<asp:Repeater ID="Repeater1" runat="server">
    <HeaderTemplate>
        <table border="0" width="100%">
            <tr>
                <td colspan="3">
                    <br />
                    <h2 class="selectFromRecommended">
                        Also Available <a class="hint" title="Registering multiple extensions allows you to protect your online brand from misuse and increases visitors to your website"
                            onclick="return false" href="#">Why do I need these?</a></h2>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr />
                </td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <%#GetCheckBox((ResellerClub.Interface.Messages.IDomainInfoMessage) Container.DataItem)%>
            </td>
            <td>
                <h3 style="color: Black">
                    <%#GetDominName((ResellerClub.Interface.Messages.IDomainInfoMessage)Container.DataItem)%>
                </h3>
            </td>
            <td align="right">
                <span>Book it for:</span>
                <%#GetPlanDdl((ResellerClub.Interface.Messages.IDomainInfoMessage)Container.DataItem)%>
                <br />
                You Save 14.20%
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
