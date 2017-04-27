<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs"
    Inherits="InfoWebTicketSystem.WebUI.Error" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <table width="100%">
            <tr>
                <td width="100%" align="center">
                    <span style="font-family: Verdana; font-size: 12pt; color: RED">OOPS! Server incountered
                        some error while processing your request. Sorry for inconvience.</span>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
