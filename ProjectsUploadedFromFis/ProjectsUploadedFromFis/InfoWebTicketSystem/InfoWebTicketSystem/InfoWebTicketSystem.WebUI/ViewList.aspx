<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ViewList.aspx.cs" Inherits="InfoWebTicketSystem.WebUI.ViewList" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <!-- BEGIN DIALOG PROCESSING -->
        <div class="boxcontainer">
            <div class="boxcontainerlabel">
                <div style="float: right">
                    <div class="headerbuttongreen" style="display:none" onclick="">
                        Hide Resolved Tickets</div>
                </div>
                View Tickets</div>
            <div class="boxcontainercontent">
                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                        <td class="ticketlistheaderrow" align="left" valign="middle" width="150">
                            Ticket ID
                        </td>
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="200">
                            <a href="#">
                                Last Update&nbsp;<img src="data:image/gif;base64,R0lGODlhBwAHAIABABEyZf///yH5BAEAAAEALAAAAAAHAAcAAAIMjB+ACWrN4oPIrVMAADs="
                                    border="0" /></a>
                        </td>
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="">
                            <a href="#">
                                Last Replier&nbsp;</a>
                        </td>
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="">
                            <a href="#">
                                Department&nbsp;</a>
                        </td>
                   
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                            <a href="#">
                                Type&nbsp;</a>
                        </td>
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                            <a href="#">
                                Status&nbsp;</a>
                        </td>
                        <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                            <a href="#">
                                Priority&nbsp;</a>
                        </td>
                    </tr>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="ticketlistsubject" align="left" valign="middle" colspan="7">
                                    
                                    <a href="View.aspx?ticketid=<%#Eval("FID")%>">
                                       <%#Eval("Subject")%></a>
                                </td>
                            </tr>
                            <tr class="ticketlistproperties" style="background: #b8b8b8;">
                                <td class="ticketlistpropertiescontainer" align="left" valign="middle">
                                   <%#GetTicketNumber(Eval("TicketNumber"))%>
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                     <%#((DateTime)Eval("LastUpdate")).ToString("dd MMMM yyyy HH:mm:ss tt")%>
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                     <%#GetLastReplier(Eval("LastReplier"))%>
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                     <%#GetDepartment(Eval("Department"))%>
                                </td>
                          
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                    <%#GetTicketType(Eval("Type"))%>
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                      <%#GetStatus(Eval("Status"))%>
                                </td>
                                <td class="ticketlistpropertiescontainer" style="background: ;" align="center" valign="middle">
                                     <%#GetPriority(Eval("Priority"))%>
                                </td>
                            </tr>
                            <tr class="ticketlistpropertiesdivider">
                                <td colspan="7">
                                    &nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
