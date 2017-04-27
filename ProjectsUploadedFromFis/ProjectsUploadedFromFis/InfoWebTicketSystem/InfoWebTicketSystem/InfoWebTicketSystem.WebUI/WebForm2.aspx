<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="InfoWebTicketSystem.WebUI.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <div id="maincorecontent">
         
                <!-- BEGIN DIALOG PROCESSING -->
                <div class="boxcontainer">
                    <div class="boxcontainerlabel">
                        <div style="float: right">
                            <div class="headerbuttongreen" onclick="javascript:window.location.href = 'http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Index/0';">
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
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Sort/lastactivity/asc">
                                        Last Update&nbsp;<img src="data:image/gif;base64,R0lGODlhBwAHAIABABEyZf///yH5BAEAAAEALAAAAAAHAAcAAAIMjB+ACWrN4oPIrVMAADs="
                                            border="0" /></a>
                                </td>
                                <td class="ticketlistheaderrow" align="center" valign="middle" width="">
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Sort/lastreplier/asc">
                                        Last Replier&nbsp;</a>
                                </td>
                                <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Sort/type/asc">
                                        Type&nbsp;</a>
                                </td>
                                <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Sort/status/asc">
                                        Status&nbsp;</a>
                                </td>
                                <td class="ticketlistheaderrow" align="center" valign="middle" width="120">
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/ViewList/Sort/priority/asc">
                                        Priority&nbsp;</a>
                                </td>
                            </tr>
                            <tr>
                                <td class="ticketlistsubject" align="left" valign="middle" colspan="7">
                                    <a href="http://support.resellerclub.com/helpdesk/index.php?/Tickets/Ticket/View/577871">
                                        API to Validate User</a>
                                </td>
                            </tr>
                            <tr class="ticketlistproperties" style="background: #b8b8b8;">
                                <td class="ticketlistpropertiescontainer" align="left" valign="middle">
                                    XOJ-540-93744
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                    19 January 2013 08:02:59 PM
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                    Rigved R
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                    Need Information
                                </td>
                                <td class="ticketlistpropertiescontainer" align="center" valign="middle">
                                    On Hold
                                </td>
                                <td class="ticketlistpropertiescontainer" style="background: ;" align="center" valign="middle">
                                    Normal
                                </td>
                            </tr>
                            <tr class="ticketlistpropertiesdivider">
                                <td colspan="7">
                                    &nbsp;
                                </td>
                            </tr>
                           
                                               </table>
                    </div>
                </div>

             

            </div>
</asp:Content>
