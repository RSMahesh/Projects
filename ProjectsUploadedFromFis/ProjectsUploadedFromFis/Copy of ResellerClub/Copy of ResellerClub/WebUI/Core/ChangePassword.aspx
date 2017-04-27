<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="ResellerClub.WebUI.ChangePassword"
    Title="Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <style>
        #tblChangePass TD
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
    <div id="divContainer" style="width: 450px; left: 30%; padding-left: 10px; padding-bottom: 5px;
        position: absolute; background: url(../images/back_content_top.png) #ffffff no-repeat;
        border: #e7e4da 1px solid;">
        <div class="PageHeading">
            <h3>
                Change <span>Password</span>
            </h3>
        </div>
        <table id="tblChangePass">
            <tr>
            </tr>
            <tr>
                <td>
                    Email Id:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtEmailID" CssClass="fancy_input" Columns="40" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Old Password:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtOldPassword" CssClass="fancy_input" Columns="40" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 15px;">
                    New Password:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtPassword" Columns="40" CssClass="fancy_input" runat="server"
                            TextMode="Password"></asp:TextBox>
                        <span style="display: block; margin-top: -5px; font-size: 8.5pt">8 character minimum.
                            No special characters</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom: 15px;">
                    Confrim Password:
                </td>
                <td>
                    <div class="div_fancy_input">
                        <asp:TextBox ID="txtConfrim" Columns="40" CssClass="fancy_input" runat="server"></asp:TextBox>
                        <span style="display: block; margin-top: -5px; font-size: 8.5pt">8 character minimum.
                            No special characters</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <div style="padding-top: 20px;">
                        <button id="existing_submit" class="ui-button" value="submit" name="button" type="submit">
                            <span><span>Submit</span></span></button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
