<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPage/Main.Master"
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="ResellerClub.WebUI.ForgotPassword"
    Title="Forgot Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #tblForgotPass TD
        {
            margin-top: 40px;
            vertical-align: middle;
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" id="divUserMsg">
        <div class="dialogerrorsub">
            <div runat="server" id="divUserMsgContent">
            </div>
        </div>
    </div>
    <div id="divContainer" style="width: 400px; left: 33%; padding-left: 10px; padding-bottom: 5px;
        position: absolute; background: url(../images/back_content_top.png) #ffffff no-repeat;
        border: #e7e4da 1px solid;">
        <div class="PageHeading">
            <h3>
                Forgot <span>Password</span>
            </h3>
        </div>
        <br />
        <table id="tblForgotPass">
            <tr>
                <td>
                    Email ID:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtEmailID" CssClass="fancy_input" Columns="40" runat="server"></asp:TextBox>
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
</asp:Content>
