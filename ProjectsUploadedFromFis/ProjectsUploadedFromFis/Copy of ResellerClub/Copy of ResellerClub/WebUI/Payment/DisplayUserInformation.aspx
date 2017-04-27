<%@ Page Language="C#" MasterPageFile="~/MasterPage/Blank.Master" AutoEventWireup="true"
    CodeBehind="DisplayUserInformation.aspx.cs" Inherits="ResellerClub.WebUI.Payment.DisplayUserInformation"
    Title="User Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../StyleSheet/Style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divUserMsg">
        <div class="dialogerrorsub">
            <div runat="server" id="divUserMsgContent">
            </div>
        </div>
    </div>
    <div>
        <%=HTMLContent%>
    </div>
    <div style="position: absolute; left: 40%; padding-top: 20px;" id="divContinue" runat="server">
        <button id="btnContinue" class="ui-button" value="submit" onclick="fun1()" name="button"
            type="button">
            <span><span>Continue</span></span></button>
    </div>

    <script>
function fun1(){
window.parent.OnContinueClickCallBack();
}
    </script>

</asp:Content>
