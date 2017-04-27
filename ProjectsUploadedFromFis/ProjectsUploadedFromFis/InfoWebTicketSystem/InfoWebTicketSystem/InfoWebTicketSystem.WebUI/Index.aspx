<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs"
    Inherits="InfoWebTicketSystem.WebUI.Index" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
        <div style="display: none;" class="dialogerror">
            <div class="dialogerrorsub">
                <div class="dialogerrorcontent">
                </div>
            </div>
        </div>
        <!-- BEGIN DIALOG PROCESSING -->
        <div id="Custom">
            <div class="container">
                <h2>
                    Welcome to the Info Web Services Support HelpDesk</h2>
                <div class="cont-wrp">
                    <ol class="num">
                        <li>You can create a new Support Ticket by clicking on the "Submit a Ticket" button</li>
                        <li>You can view existing tickets by logging in with your Info Web Services credentials</li>
                        <li>If you do not have the password to access tickets created on this new desk, you
                            can click on Lost Password and request your password to be delivered to your registered email
                            address</li>
                    </ol>
                  
                    <br>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
