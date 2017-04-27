<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="InvoiceDetail.aspx.cs"
    Inherits="ResellerClub.WebUI.InvoiceDetail" Title="Invoice Detail" %>

<%@ Register src="UserControl/InvoiceDetail.ascx" tagname="InvoiceDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        H2
        {
            padding-bottom: 0px;
            line-height: 43px;
            margin: 30px 0px 0px;
            padding-left: 15px;
            width: 682px;
            padding-right: 0px;
            height: 43px;
            font-size: 22px;
            font-weight: bold;
            padding-top: 0px;
            background: url(images/bg_cart_headings.png) no-repeat 0px -5px;
            color: #acacac;
        }
        </style>
    <div>
        <h2>
            Invoice Summary <span style="float:right;font-size:10pt;color:#000;padding-right:15px;">Order Number:<%=OrderNumber%></span></h2>
            
    </div>
   <uc1:InvoiceDetail ID="InvoiceDetail1" runat="server" />
   
</asp:Content>
