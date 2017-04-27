<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayInformation.aspx.cs"
    Inherits="ResellerClub.WebUI.DisplayInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Infomation</title>
    <link href="StyleSheet/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <div runat="server" id="divUserMsg">
        <div class="dialogerrorsub">
            <div runat="server" id="divUserMsgContent">
            </div>
        </div>
    </div>
    <div>
        <%=HTMLContent%>
    </div>
   
        <div style="position:absolute;left:40%;padding-top:20px;" id="divContinue" runat="server" >
            <button id="btnContinue" class="ui-button" value="submit" onclick="fun1()"  name="button" type="button">
                <span><span>Continue</span></span></button>
        </div>
   
<script>
function fun1(){
window.parent.OnContinueClickCallBack();
}
</script>
</body>
</html>
