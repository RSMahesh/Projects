<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="View.aspx.cs" Inherits="InfoWebTicketSystem.WebUI.View" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <!-- BEGIN DIALOG PROCESSING -->
        <div class="boxcontainer">
            <div class="boxcontainerlabel">
                <div style="float: right">
                    <div class="headerbutton" onclick="javascript:UpdateTicket();">
                        Update</div>
                    <div class="headerbuttongreen" onclick="javascript: $('#postreplycontainer').show(); $('#replycontents').focus();">
                        Post Reply</div>
                </div>
                View Ticket: #NMU-<%=TicketNumber%>-XOJ</div>
            <div class="boxcontainercontenttight">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div class="ticketgeneralcontainer">
                                    <div class="ticketgeneraltitlecontainer">
                                        <div class="ticketgeneraldepartment" style="display: none">
                                            API</div>
                                        <div class="ticketgeneraltitle">
                                            <%=Subject%></div>
                                    </div>
                                    <div class="ticketgeneralinfocontainer">
                                        Created:<%=CreatedOn%>&nbsp;&nbsp;&nbsp;&nbsp;Updated:
                                        <%=UpdatedOn%>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-color: #c7c7c7;">
                                <div class="ticketgeneralcontainer">
                                    <div style="background-color: #c7c7c7;" class="ticketgeneralproperties">
                                        <div class="ticketgeneralpropertiesdivider">
                                            <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg=="></div>
                                        <div class="ticketgeneralpropertiesobject">
                                            <div class="ticketgeneralpropertiestitle">
                                                DEPARTMENT</div>
                                            <div class="ticketgeneralpropertiescontent">
                                                <%=Department%></div>
                                        </div>
                                     
                                        <div class="ticketgeneralpropertiesdivider">
                                            <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg=="></div>
                                        <div class="ticketgeneralpropertiesobject">
                                            <div class="ticketgeneralpropertiestitle">
                                                OWNER</div>
                                            <div class="ticketgeneralpropertiescontent">
                                                Unassigned</div>
                                        </div>
                                        <div class="ticketgeneralpropertiesdivider">
                                            <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg=="></div>
                                        <div class="ticketgeneralpropertiesobject">
                                            <div class="ticketgeneralpropertiestitle">
                                                TYPE</div>
                                            <div class="ticketgeneralpropertiescontent">
                                                <%=TicketType%></div>
                                        </div>
                                        <div class="ticketgeneralpropertiesdivider">
                                            <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg=="></div>
                                        <div class="ticketgeneralpropertiesobject">
                                            <div class="ticketgeneralpropertiestitle">
                                                STATUS</div>
                                            <div class="ticketgeneralpropertiesselect">
                                                <asp:DropDownList ID="ddlStatus" class="swiftselect" name="ticketstatusid" runat="server">
                                                    <asp:ListItem Value="OP">OPEN</asp:ListItem>
                                                    <asp:ListItem Value="CS">Closed</asp:ListItem>
                                                    <asp:ListItem Value="OH">On Hold</asp:ListItem>
                                                    <asp:ListItem Value="AW">Awaiting Client Update</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="ticketgeneralpropertiesdivider">
                                            <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg=="></div>
                                        <div class="ticketgeneralpropertiesobject" style="background-color: ;">
                                            <div class="ticketgeneralpropertiestitle">
                                                PRIORITY</div>
                                            <div class="ticketgeneralpropertiesselect">
                                                <asp:DropDownList ID="ddlPriority" class="swiftselect" name="ticketpriorityid" runat="server">
                                                    <asp:ListItem Value="NR">Normal</asp:ListItem>
                                                    <asp:ListItem Value="EM">Emergency</asp:ListItem>
                                                    <asp:ListItem Value="CR">Critical</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="viewticketcontentcontainer">
                    <br />
                </div>
                <div id="postreplycontainer" style="display: none;">
                    <div class="ticketpaddingcontainer">
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
                                <td colspan="2" align="left" valign="top">
                                    <asp:TextBox ID="txtMessage" Columns="25" Rows="15" runat="server" TextMode="MultiLine"
                                        class="swifttextareawide"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="hlineheader">
                            <tr>
                                <th rowspan="2" nowrap>
                                    Upload File(s) [<div class="addplus">
                                        <a href="#ticketattachmentcontainer" onclick="javascript: $('#ticketattachmentcontainer').show();">
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
                            <input class="rebuttonwide2" value="Send" type="submit" name="button" /></div>
                    </div>
                </div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="ticketpostsholder">
                            <div class="ticketpostcontainer">
                                <div class="ticketpostbar">
                                    <div class="ticketpostbarname">
                                        <%#GetUser(Eval("Staff"))%></div>
                                    <div class="ticketpostbarbadgeblue" >
                                        <div class="tpbadgetext">
                                            <%#GetUserType(Eval("Staff"))%></div>
                                    </div>
                                </div>
                                <div style="min-height: 340px;" class="ticketpostcontents">
                                    <div class="ticketpostcontentsbar">
                                        <div onclick="javascript: QuoteTicketPost('524123', '1136269');" class="ticketbarquote">
                                        </div>
                                        <div class="ticketbarcontents">
                                            Posted on:
                                            <%#((DateTime)Eval("InsertDate")).ToString("dd MMMM yyyy HH:mm:ss tt")%></div>
                                        <span class="ticketbardatefold"></span>
                                    </div>
                                    <div class="ticketpostcontentsdetails">
                                    
                                        <div class="ticketpostcontentsattachments" style="display:<%#IsAttachment(Eval("attachment"))%>" >
                                            <div style="background-image: url(images/mimeico_pic.gif)"
                                                class="ticketpostcontentsattachmentitem" onclick="javascript: PopupSmallWindow('<%#GetAttachemtFileNameRaw(Eval("attachment"))%>');">
                                                &nbsp;<%#GetAttachemtFileName(Eval("attachment"))%></div>
                                        </div>
                                    
                                        <div class="ticketpostcontentsholder">
                                            <div class="ticketpostcontentsdetailscontainer">
                                                <pre>
<%#Eval("Message")%>
</pre>
                                            </div>
                                        </div>
                                        <div class="ticketpostcontentsbottom">
                                            <span class="ticketpostbottomright">&nbsp;</span><div class="ticketpostbottomcontents">
                                                &nbsp;</div>
                                        </div>
                                    </div>
                                    <div class="ticketpostbarbottom">
                                        <div class="ticketpostbottomcontents">
                                            &nbsp;&nbsp;</div>
                                    </div>
                                    <div class="ticketpostclearer">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <script>
   function UpdateTicket(){
    var myform = document.forms[0];
    $(myform).append('<input type="hidden" name="button" value="Update" />');
    $(myform).submit();

   }
   
   
   function PopupSmallWindow(url) {
   window.open("UploadedFiles/" + url);
   }
   
    </script>

</asp:Content>
