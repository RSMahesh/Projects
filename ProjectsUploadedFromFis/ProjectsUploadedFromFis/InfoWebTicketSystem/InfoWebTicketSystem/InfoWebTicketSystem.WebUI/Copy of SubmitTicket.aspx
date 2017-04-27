<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SubmitTicket.aspx.cs"
    Inherits="InfoWebTicketSystem.WebUI.SubmitTicket" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                Type</td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Value="NI">Need Information</asp:ListItem>
                    <asp:ListItem Value="ER">Error on Web Site</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Subject</td>
            <td>
                <asp:TextBox ID="txtSubject" Columns="70" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            
            <td colspan=2>
                <asp:TextBox ID="txtMessage" Columns="70" Rows="50" runat="server" 
                     TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
     <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />
</asp:Content>
