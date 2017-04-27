<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="UpdatePaymentReference.aspx.cs"
    Inherits="ResellerClub.WebUI.UpdatePaymentReference" Title="Payment Reference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
    <style>
        #tblForgotPass TD
        {
            margin-top: 40px;
            vertical-align: middle;
            height: 40px;
        }
    </style>

    <script src="../JavaScript/OverLayWindow.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div runat="server" id="divUserMsg">
        <div class="dialogerrorsub">
            <div  runat="server" id="divUserMsgContent">
            </div>
        </div>
    </div> 
    <div id="divContainer" style="width: 550px; left: 28%; padding-left: 10px; padding-bottom: 5px;
        position: absolute; background: url(../images/back_content_top.png) #ffffff no-repeat;
        border: #e7e4da 1px solid;">
        <div class="PageHeading">
            <h3>
                Update <span>Pyament Transcation</span> Number
            </h3>
        </div>
        <br />
        <table id="tblForgotPass">
            <tr>
               <td>Order No.:</td>
                <td><%=OrderNumber%><a href="#" onclick="ShowOrderDetail('<%=Request.QueryString["rid"]%>')">[View Detail]</a></td>
            </tr>
                 <tr>
               <td>User:</td>
                <td><%=UserEmail%></td>
            </tr
            <tr>
                <td>
                    Payment Transaction Number:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtTranscationNumber" CssClass="fancy_input" Columns="100" MaxLength="40" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <div style="padding-top: 10px;">
                        <button id="existing_submit" class="ui-button" value="submit" name="button" type="submit">
                            <span><span>Submit</span></span></button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
   <script>
   function ShowOrderDetail(orderId){
     ShowModalPopUpURL('PaymentAwaitedOrderDetail.aspx?orderId=' + orderId +"&ViewOrderDetail=1" ,500,750,'Order Detail',null,null);
   }
    SetOverLayLoadingImagePath("../Images/loading_new.gif");
    SetOverLayCloseImagePath("../images/remove.gif");
   </script> 
    
</asp:Content>
