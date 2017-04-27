<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs"
    Inherits="ResellerClub.WebUI.CheckOut" ValidateRequest="false" Title="CheckOut" %>

<%@ Register Src="UserControl/OderDetail.ascx" TagName="OderDetail" TagPrefix="uc1" %>
<%@ Register Src="UserControl/CustomerRegistration.ascx" TagName="CustomerRegistration"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .loginform
        {
            border-bottom: #d7d7d7 0px solid;
            border-left: #d7d7d7 1px solid;
            padding-bottom: 0px;
            margin: 20px 20px 0px 0px;
            padding-left: 5px;
            width: 323px;
            padding-right: 5px;
            float: left;
            border-top: #d7d7d7 0px solid;
            border-right: #d7d7d7 0px solid;
            padding-top: 6px;
        }
        .loginform DIV
        {
            margin: 5px 0px 15px 15px;
            clear: both;
        }
        .loginform INPUT
        {
            width: 217px;
        }
        .CartSection
        {
            text-align: left;
            padding-bottom: 10px;
            font-family: Arial, Helvetica, sans-serif;
            color: #555;
            clear: both;
        }
        .CartSection H3
        {
            border-bottom: #eee 2px solid;
            padding-bottom: 0px;
            line-height: 33px;
            margin: 6px 15px 0px;
            padding-left: 0px;
            padding-right: 0px;
            height: 33px;
            color: #707070;
            font-size: 18px;
            font-weight: normal;
            padding-top: 0px;
        }
        H3
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 15px;
            padding-right: 0px;
            color: #434343;
            font-size: 18px;
            font-weight: bold;
            padding-top: 5px;
        }
        .formError
        {
            border-bottom: #ff8080 1px solid;
            background: #ffe1e1;
            color: #333333;
            border-top: #ff8080 1px solid;
        }
        #rememberme LABEL
        {
            text-align: left;
            margin: 0px;
            width: auto;
            display: inline;
            float: none;
            clear: none;
            font-size: 12px;
            font-weight: normal;
        }
        #forgotpassword
        {
            margin: 0px 0px 0px 50px;
        }
        A
        {
            color: #0560a6;
            text-decoration: none;
        }
        .CartSection H2
        {
            padding-bottom: 0px;
            line-height: 43px;
            margin: 26px 0px 0px;
            padding-left: 15px;
            width: 682px;
            padding-right: 0px;
            height: 43px;
            font-size: 22px;
            font-weight: bold;
            padding-top: 0px;
        }
        .CartSection H2.HeadingActive
        {
            margin-top: 10px;
            background: url(images/bg_cart_headings.png) no-repeat left bottom;
            color: #2d5494;
        }
        .placeorderbox
        {
            background: url(images/back_content.png) no-repeat;
            margin-bottom: -10px;
        }
        .tb5a
        {
            border: 0;
            background-color: Transparent;
            margin-top: 3px;
            margin-left: 7px;
        }
        /* Gradient 1 */.tb10
        {
            background-image: url(images/form_bg.jpg);
            background-repeat: repeat-x;
            border: 1px solid #d1c7ac;
            width: 230px;
            color: #333333;
            padding: 3px;
            margin-right: 4px;
            margin-bottom: 8px;
            font-family: tahoma, arial, sans-serif;
        }
    </style>

    <script src="JavaScript/Global.js" type="text/javascript"></script>

    <table width="100%">
        <tr>
            <td align="left" style="vertical-align: top;" width="70%">
                <uc1:OderDetail ID="OderDetail1" runat="server"  />
            </td>
            <td width="30%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table class="CartSection">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="placeorderbox" id="divPlaceHolder">
                                <div style="color: #2e577a; padding-left: 10px; width: 100%; margin-bottom: 15px;
                                    padding-top: 20px; padding-bottom: 10px; font-weight: bold; font-size: 18px;
                                    background: url(/images/bg-gradient-long.gif);" id="divHeaderPlaceOrder" runat="server">
                                    Place Your Order
                                </div>
                                <table>
                                    <tr id="trUserOption" runat="server">
                                        <td style="vertical-align: top">
                                            <div id="Div1" class="loginform" style="border-left-width: 0px;">
                                                <h3>
                                                    New Users</h3>
                                                <p>
                                                    Create an account with InfowebServices get our services.</p>
                                                <div>
                                                    <button id="new_submit" class="ui-button" name="new_submit" type="button" onclick="window.location.href='Core/CustomerRegistration.aspx'">
                                                        <span><span>Create an Account in 10 seconds</span></span></button>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="Div2" class="loginform">
                                                <h3>
                                                    Existing Users</h3>
                                                <div id="divError" runat="server" class="formError">
                                                </div>
                                                <div>
                                                    <label id="label1" for="input_email">
                                                        Email Address</label>
                                                    <div class="tb10" style="margin-left: 0px;">
                                                        <input id="input_email" value="" class="tb5a" type="text"
                                                            name="input_email" />
                                                    </div>
                                                </div>
                                                <div style="display: ">
                                                    <label id="label2" for="input_password">
                                                        Password</label>
                                                    <div class="tb10" style="margin-left: 0px;">
                                                        <input id="input_password" class="tb5a" type="password" name="password">
                                                    </div>
                                                </div>
                                                <div id="rememberme" style="display: ">
                                                    <input id="existing_remember" style="width: 20px" type="checkbox" name="rememberme">
                                                    <label id="label_rememberme" for="existing_remember">
                                                        Remember Me</label>
                                                    <a id="forgotpassword" href="Core/ForgotPassword.aspx">Forgot your Password?</a>
                                                    <input value="validate_customer" type="hidden" name="action">
                                                </div>
                                                <div>
                                                    <button id="existing_submit" class="ui-button" value="abc" name="existing_submit"
                                                        type="submit">
                                                        <span><span>Login</span></span></button>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="trRegistrationForm" runat="server">
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="30%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td width="30%">
                &nbsp;
            </td>
        </tr>
    </table>

    <script>
   

document.getElementById("input_password").value = "MB248001";
     

function GetQueryStringParams(sParam)
{
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) 
    {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) 
        {
            return sParameterName[1];
        }
    }
    
    return "";
}​

  $(document).ready(function() {
    
//        $('#new_submit').bind('click', function(event) {
//            $("tr[id*=trRegistrationForm]").show();
//            $("tr[id*=trUserOption]").hide()
//          });
//    
//        if(GetQueryStringParams("login") != ""){
//        $("#divPlaceHolder").removeClass('placeorderbox');
//        }
//    
    });
 
    </script>

</asp:Content>
