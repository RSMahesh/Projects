<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SubmitConfirmation.aspx.cs"
    Inherits="InfoWebTicketSystem.WebUI.SubmitConfirmation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <div class="boxcontainer">
            <div class="boxcontainerlabel">
                Your request has been received</div>
            <div class="boxcontainercontent">
                We have received your request and will get back to you with further details shortly.
                You can login to the support center or check your mailbox for further updates.<br />
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            General Information
                        </th>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="hlinelower">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="4">
                    <tr>
                        <td width="200" align="left" valign="middle" class="zebraodd">
                            Ticket ID
                        </td>
                        <td>
                            <%=TicketNumber%>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="left" valign="middle" class="zebraodd">
                            Full Name
                        </td>
                        <td>
                            Yogesh Bailwal
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="middle" class="zebraodd">
                            Email
                        </td>
                        <td>
                            <%=UserEmail%>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            Subject:
                            <%=Subject%>
                        </th>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="hlinelower">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="4">
                    <tr>
                        <td align="left" valign="top">
                            <%=Message%>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
