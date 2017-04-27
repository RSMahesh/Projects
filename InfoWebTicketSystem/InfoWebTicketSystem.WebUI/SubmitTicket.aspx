<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="SubmitTicket.aspx.cs" Inherits="InfoWebTicketSystem.WebUI.SubmitTicket"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <div style="display: none;" class="dialogerror">
            <div class="dialogerrorsub">
                <div class="dialogerrorcontent">
                </div>
            </div>
        </div>
        <!-- BEGIN DIALOG PROCESSING -->
        <div class="boxcontainer">
            <div class="boxcontainerlabel">
                Your ticket details</div>
            <div class="boxcontainercontent">
                Enter your ticket details below. If you are reporting a problem, please remember
                to provide as much information that is relevant to the issue as possible.<br />
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
                            Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" class="swiftselect" runat="server">
                                <asp:ListItem Value="NI">Need Information</asp:ListItem>
                                <asp:ListItem Value="ER">Error on Web Site</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            Ticket authentication details
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
                            Domain name:
                        </td>
                        <td>
                            <input name="domain" type="text" autocomplete="off" size="20" maxlength="190" class="swifttextlarge"
                                value="">
                            <br />
                            <span class="smalltext">Please enter a valid domain name for which you need support</span>
                        </td>
                    </tr>
                    <%if (!UserValidated())
                      {%>
                    <tr>
                        <td width="200" align="left" valign="middle" class="zebraodd">
                            Email ID:<span class="customfieldrequired">*</span>
                        </td>
                        <td>
                            <input name="email" type="text" autocomplete="off" size="20" maxlength="90" class="swifttextlarge"
                                value="">
                            <br />
                            <span class="smalltext">Please enter a valid email id for mail communication</span>
                        </td>
                    </tr>
                    <%} %>
                </table>
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            Message Details
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
                            Subject
                        </td>
                        <td width="">
                            <asp:TextBox ID="txtSubject" Columns="45" class="swifttextwide" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" valign="top">
                            <asp:TextBox ID="txtMessage" Columns="25" Rows="15" class="swifttextareawide" runat="server"
                                TextMode="MultiLine"></asp:TextBox><div id="irscontainer" class="irscontainer">
                                    <div class="irsui">
                                        <div class="irstitle">
                                            Loading knowledgebase suggestions...</div>
                                    </div>
                                </div>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            Upload File(s) [<div class="addplus">
                                <a href="#ticketattachmentcontainer" onclick="javascript:$('#ticketattachmentcontainer').show();">
                                    Add File</a></div>
                            ]
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
                <div id="ticketattachmentcontainer" style="display: none">
                    <div class="ticketattachmentitem">
                        <div class="ticketattachmentitemdelete">
                            &nbsp;</div>
                        <asp:FileUpload class="swifttextlarge" size="20" ID="FileUpload1" runat="server" />
                    </div>
                </div>
                <br />
                <div class="subcontent">
                    <input class="rebuttonwide2" value="Submit" type="submit" name="button" /><input
                        type="hidden" name="departmentid" value="117" /></div>
            </div>
        </div>
    </div>
</asp:Content>
