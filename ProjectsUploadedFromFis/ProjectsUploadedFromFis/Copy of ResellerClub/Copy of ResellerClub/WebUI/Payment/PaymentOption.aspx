<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="PaymentOption.aspx.cs" Inherits="ResellerClub.WebUI.PaymentOption"
    Title="Payment Option" %>

<%@ Register Src="~/UserControl/OderDetail.ascx" TagName="OderDetail" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/PaymentOption.ascx" TagName="PaymentOption" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JavaScript/Global.js" type="text/javascript"></script>

    <script src="../JavaScript/OverLayWindow.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="80%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <th>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <uc1:OderDetail ID="OderDetail1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <br />
                <uc2:PaymentOption ID="PaymentOption1" runat="server" />
            </td>
        </tr>
    </table>

    <script>
    var doPostBack = false;
    var paymentOptionBtn;
  
    function OnPayOnlineClickCallBack(selectedValue,btn){
        paymentOptionBtn = btn;
        if(doPostBack){
        return true;
        }
        DisplayPopup(selectedValue);
        return false;
    }  
    

    function OnContinueClickCallBack(){
        ShowLoading();
        doPostBack = true;
        paymentOptionBtn.click();
    }
  
  function DisplayPopup(selectedValue){
      var hieght =510;
      var width =800;
       var htmlFilePath="";
    switch(selectedValue){
        case "offline":
        htmlFilePath="Payment/OffLinePayment.htm";
        hieght=520;
        break;
        case "paypal":
        htmlFilePath="Payment/PayPal.htm";
        break;
        case "imps":
        case "netbanking":
        htmlFilePath="Payment/IMPS.htm";
        break;
    }
    
    if(htmlFilePath == ""){
    alert("Please Select Payment Option");
    return false;
    }
    
     ShowModalPopUpURL('DisplayUserInformation.aspx?Htmlfile='+htmlFilePath,hieght,width,'User Info',null,null);
  }
    
    SetOverLayLoadingImagePath("../Images/loading_new.gif");
    SetOverLayCloseImagePath("../images/remove.gif");
    </script>

</asp:Content>
