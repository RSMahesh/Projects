<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="PaymentAwaitedOrderDetail.aspx.cs" Inherits="ResellerClub.WebUI.PaymentAwaitedOrderDetail"
    Title="Order Detail" %>

<%@ Register Src="~/UserControl/InvoiceDetail.ascx" TagName="InvoiceDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divOrderDetail" runat="server" style="width: 700px">
        <div id="divUserMsg" runat="server" class="dialoginfo">
            <div class="dialogerrorsub">
                <div runat="server" id="divUserMsgContent" class="dialoginfocontent">
                    <span id="spnOnlinePaymentMsg" runat="server">Your Order is saved. Mail has been sent
                        to your email address <b>
                            <%=Email%>
                        </b>
                        <br />
                        Please follow the instrucation in the email after making your payment. </span>
                    <span id="spnOfflinePaymentMsg" runat="server">Your Order is saved. Mail has been sent
                        to your email address <b>
                            <%=Email%>
                        </b>
                        <br />
                        We will execute your order after receving payment for the same. </span>
                </div>
            </div>
        </div>
        <div>
            Order Number : <b>
                <%=OrderNumber%></b></div>
        <uc1:InvoiceDetail ID="InvoiceDetail1" runat="server" />
    </div>
</asp:Content>
