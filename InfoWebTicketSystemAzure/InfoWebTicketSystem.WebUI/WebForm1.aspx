<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="InfoWebTicketSystem.WebUI.WebForm1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div>
        <div>
            <asp:Button ID="btnPostReply" runat="server" Text="Post Reply" OnClick="btnPostReply_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        </div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="background-color: #c7c7c7">
                    <div class="ticketgeneralcontainer">
                        <div style="background-color: #c7c7c7" class="ticketgeneralproperties">
                            <div class="ticketgeneralpropertiesobject">
                                <div class="ticketgeneralpropertiestitle">
                                    TYPE
                                </div>
                                <div class="ticketgeneralpropertiescontent">
                                    Need Information
                                </div>
                            </div>
                            <div class="ticketgeneralpropertiesdivider">
                                <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg==" />
                            </div>
                            <div class="ticketgeneralpropertiesobject">
                                <div class="ticketgeneralpropertiestitle">
                                    STATUS
                                </div>
                                <div class="ticketgeneralpropertiesselect">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="OP">OPEN</asp:ListItem>
                                        <asp:ListItem Value="CS">Closed</asp:ListItem>
                                        <asp:ListItem Value="OH">On Hold</asp:ListItem>
                                        <asp:ListItem Value="AW">Awaiting Client Update</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="ticketgeneralpropertiesdivider">
                                <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg==" />
                            </div>
                            <div class="ticketgeneralpropertiesobject">
                                <div class="ticketgeneralpropertiestitle">
                                    PRIORITY
                                </div>
                                <div class="ticketgeneralpropertiesselect">
                                    <asp:DropDownList ID="ddlPriority" runat="server">
                                        <asp:ListItem Value="NR">Normal</asp:ListItem>
                                        <asp:ListItem Value="EM">Emergency</asp:ListItem>
                                        <asp:ListItem Value="CR">Critical</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="ticketgeneralpropertiesdivider">
                                <img border="0" align="middle" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAAwCAQAAAB6OtgKAAAAN0lEQVQI113GiQkAIBADwXhqE1Zk/+2cL5GICMOymNVQDFmS+oli6hZYcEBTxqOzjXO7+TPnfQHCiBfALhHbGgAAAABJRU5ErkJggg==" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
     
        <div style="display: none">
            <asp:TextBox ID="txtMessage" Columns="70" Rows="25" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </div>
    <div>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table border="0">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <div style="border: solid 1px #CCCCCC; margin-top: 30px;">
                            <table>
                                <tr>
                                    <td style="vertical-align: top; background-color: #FFF7EF">
                                        <div style="color: #404040; vertical-align: top; text-align: center; min-width: 220px;
                                            font-weight: bold; font-size: 15px; padding-top: 10px;">
                                            <%#GetUser(Eval("UserId"))%></div>
                                        <div style="margin-left: 70px; margin-top: 10px; background: url(images/badge1blue.gif) no-repeat left top;
                                            width: 55px; height: 20px; vertical-align: middle; padding-left: 20px; padding-top: 3px;
                                            font-size: 11px;">
                                            Staff</div>
                                    </td>
                                    <td style="vertical-align: top; padding-top: 10px;">
                                        <div style="padding: 2px; background-color: #9CBEC6; margin-left: -30px; width: 500px;
                                            color: White">
                                            Posted on: 24 December 2012 11:30:50 PM</div>
                                        <div style="width: 500px; min-height: 340px; padding-top: 20px;">
                                            <%#Eval("Message")%>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div></asp:Content>
