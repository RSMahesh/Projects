<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="Support.aspx.cs" Inherits="ResellerClub.WebUI.Support" Title="Support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .row-indent
        {
            padding-left: 30px;
            padding-right: 30px;
        }
        .chk-icons .frm-field
        {
            margin: 0px;
        }
        .chk-icons .frm-select
        {
            margin: 0px;
        }
        .lfloat
        {
            float: left;
        }
        .rfloat
        {
            float: right;
        }
        .ui-heading
        {
            padding-bottom: 15px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font: bold 32px "Rokkitt" , serif;
            color: Gray;
            padding-top: 0px;
            text-shadow: 1px 1px 1px #ececec;
        }
        IMG
        {
            border-bottom: 0px;
            border-left: 0px;
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            border-top: 0px;
            border-right: 0px;
            padding-top: 0px;
        }
        FORM
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        A
        {
            color: #377ce4;
            text-decoration: none;
        }
        P
        {
            text-align: left;
            padding-bottom: 10px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font: 12px/18px Arial, Helvetica, sans-serif;
            padding-top: 10px;
        }
        BODY
        {
            width: 100%;
        }
        BODY
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        DIV
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        H4
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        H5
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        FORM
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        INPUT
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        TEXTAREA
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        P
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        TD
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        TABLE
        {
            border-spacing: 0;
            border-collapse: collapse;
        }
        IMG
        {
            border-bottom: 0px;
            border-left: 0px;
            border-top: 0px;
            border-right: 0px;
        }
        INPUT
        {
            font-style: inherit;
            font-family: inherit;
            font-size: inherit;
            font-weight: inherit;
        }
        TEXTAREA
        {
            font-style: inherit;
            font-family: inherit;
            font-size: inherit;
            font-weight: inherit;
        }
        SELECT
        {
            font-style: inherit;
            font-family: inherit;
            font-size: inherit;
            font-weight: inherit;
        }
        OPTION
        {
            font-style: inherit;
            font-family: inherit;
            font-size: inherit;
            font-weight: inherit;
        }
        BODY
        {
            background: url(images/body-bg.jpg) #fff repeat-x left top;
        }
        #page-container
        {
            margin: 0px auto;
            width: 1000px;
            background: #fff;
        }
        #page-wrapper
        {
            padding-bottom: 50px;
        }
        .uiButton
        {
            border-bottom: #5a8ddf 1px solid;
            text-align: center;
            border-left: #5a8ddf 1px solid;
            padding-bottom: 7px;
            background-color: #236fe2;
            padding-left: 12px;
            padding-right: 12px;
            display: inline-block;
            background-repeat: no-repeat;
            font: bold 17px/18px Arial, Helvetica, sans-serif;
            color: #fff;
            overflow: visible;
            border-top: #5a8ddf 1px solid;
            cursor: pointer;
            border-right: #5a8ddf 1px solid;
            text-decoration: none;
            padding-top: 7px;
            -moz-border-radius: 6px;
            text-shadow: 1px 1px 0px #2662b6;
            border-radius: 6px;
            -webkit-border-radius: 6px;
            box-shadow: inset 0px 1px 0px 0px #9acdf9;
            -webkit-box-shadow: inset 0px 1px 0px 0px #9acdf9;
            -moz-box-shadow: inset 0px 1px 0px 0px #9acdf9;
            -webkit-transition: background-position 0.2s linear;
            -moz-transition: background-position 0.2s linear;
            -ms-transition: background-position 0.2s linear;
            -o-transition: background-position 0.2s linear;
            transition: background-position 0.2s linear;
        }
        .red
        {
            color: #ff4444;
        }
        .txt-m
        {
            font: 14px/21px Arial, Helvetica, sans-serif;
        }
        .frm-label
        {
            font: 17px Arial, Helvetica, sans-serif;
            color: #626262;
        }
        .frm-field
        {
            border-bottom: #b2c4d4 1px solid;
            border-left: #b2c4d4 1px solid;
            padding-bottom: 6px;
            padding-left: 11px;
            padding-right: 11px;
            font: 14px Arial, Helvetica, sans-serif;
            background: #fff;
            color: #626262;
            border-top: #b2c4d4 1px solid;
            border-right: #b2c4d4 1px solid;
            padding-top: 6px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 1px 2px 2px #E5EAEF inset;
            -ms-border-radius: 4px;
            -webkit-box-shadow: 1px 1px 2px #E5EAEF inset;
            -moz-box-shadow: 1px 1px 2px #E5EAEF inset;
            -ms-box-shadow: 1px 1px 2px #E5EAEF inset;
            -o-border-radius: 4px;
            -webkit-transition: border linear 0.3s, box-shadow linear 0.3s;
            -moz-transition: border linear 0.3s, box-shadow linear 0.3s;
            transition: border linear 0.3s, box-shadow linear 0.3s;
            -o-box-shadow: 1px 1px 2px #E5EAEF inset;
        }
        .frm-select
        {
            border-bottom: #b2c4d4 1px solid;
            border-left: #b2c4d4 1px solid;
            padding-bottom: 6px;
            padding-left: 11px;
            padding-right: 11px;
            font: 14px Arial, Helvetica, sans-serif;
            background: #fff;
            color: #626262;
            border-top: #b2c4d4 1px solid;
            border-right: #b2c4d4 1px solid;
            padding-top: 6px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            box-shadow: 1px 2px 2px #E5EAEF inset;
            -ms-border-radius: 4px;
            -webkit-box-shadow: 1px 1px 2px #E5EAEF inset;
            -moz-box-shadow: 1px 1px 2px #E5EAEF inset;
            -ms-box-shadow: 1px 1px 2px #E5EAEF inset;
            -o-border-radius: 4px;
            -webkit-transition: border linear 0.3s, box-shadow linear 0.3s;
            -moz-transition: border linear 0.3s, box-shadow linear 0.3s;
            transition: border linear 0.3s, box-shadow linear 0.3s;
            -o-box-shadow: 1px 1px 2px #E5EAEF inset;
        }
        .data-form TD
        {
            padding-bottom: 8px;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 8px;
        }
        .div-spacer
        {
            height: 25px;
            clear: both;
        }
        .gray-blurb
        {
            border-bottom: #dedede 1px solid;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=        '#f9f9f9' , endColorstr= '#ffffff' );
            border-left: #dedede 1px solid;
            padding-bottom: 15px;
            background-color: #f9f9f9;
            padding-left: 30px;
            padding-right: 30px;
            border-top: #dedede 1px solid;
            border-right: #dedede 1px solid;
            padding-top: 15px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -ms-border-radius: 5px;
        }
        .gray-blurb P
        {
            padding-bottom: 10px;
            padding-left: 0px;
            padding-right: 0px;
            font: 13px Arial, Helvetica, sans-serif;
            color: #6a6a6a;
            padding-top: 10px;
        }
        .ic-search
        {
            width: 57px;
            display: inline-block;
            background: url(images/ic-search.png) no-repeat 0px 0px;
            height: 57px;
            margin-right: 10px;
        }
        .gray-blurb .ui-heading
        {
            padding-bottom: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font: bold 23px "Rokkitt" , serif;
            color: Gray;
            padding-top: 0px;
        }
        .border-sep
        {
            border-left: #d0d0d0 1px solid;
            padding-left: 20px;
        }
        .gray-blurb H5.ui-heading
        {
            color: Gray;
        }
        .img-posi
        {
            position: relative;
            top: 5px;
        }
        .uiButton
        {
            position: relative;
        }
        .uiButton
        {
            -pie-background: linear-gradient(#61aaef, #236fe2);
        }
        .frm-label
        {
            font: 13px Arial, Helvetica, sans-serif;
        }
        .frm-select
        {
            width: 176px;
        }
    </style>
    <div id="page-container">
        <div id="page-wrapper">
            <div style="padding-left: 52px; padding-right: 52px" class="row-indent">
                <%if (Request.QueryString["contactus"] == null)
                  { %>
                <div class="PageHeading">
                    <h3>
                        Need <span>Help?</span>
                    </h3>
                </div>
                <div class="gray-blurb">
                    <span class="ic-search lfloat"></span>
                    <h4 class="ui-heading">
                        Support Ticket
                    </h4>
                    <css3-container style="z-index: auto; position: absolute; direction: ltr; top: 575px;
                        left: 852px"><background style="POSITION: absolute; TOP: 0px; LEFT: 0px"><group2><shape style="POSITION: absolute; WIDTH: 194px; HEIGHT: 34px; TOP: 0px; BEHAVIOR: url(#default#VML); LEFT: 0px"><fill></fill><fill></fill></shape></group2></background><border style="POSITION: absolute; TOP: 0px; LEFT: 0px"><shape style="POSITION: absolute; WIDTH: 194px; HEIGHT: 34px; TOP: 0px; BEHAVIOR: url(#default#VML); LEFT: 0px"><stroke></stroke><stroke></stroke></shape></border></css3-container>
                    <button id="Button1" class="ui-button" style="float: right" value="CreateTicket"
                        name="button" type="button" onclick="OpenSupportTicket()">
                        <span><span>Create Support Ticket</span></span></button>
                    <p>
                        You can crete support ticket with your registerd email id
                    </p>
                </div>
                <div class="div-spacer">
                </div>
                <%}%>
                <div runat="server" id="divUserMsg">
                    <div class="dialogerrorsub">
                        <div runat="server" id="divUserMsgContent">
                            <%=message%>
                        </div>
                    </div>
                </div>
                <div class="gray-blurb">
                    <div id="query">
                        <h4 class="ui-heading">
                            Mail Us:
                        </h4>
                        <p>
                            Still haven't found an answer? Send us a mail and we will get back to you
                        </p>
                        <br />
                        <table id="contact-form" class="data-form chk-icons" border="0" cellspacing="0" cellpadding="0"
                            width="100%">
                            <tr>
                                <td class="frm-label" width="200">
                                    Name <span class="red">*</span>
                                </td>
                                <td width="600">
                                    <input class="frm-field" name="contact_name" value="<%=contact_name%>" maxlength="49" />
                                </td>
                            </tr>
                            <tr>
                                <td class="frm-label">
                                    Email Address <span class="red">*</span>
                                </td>
                                <td>
                                    <input class="frm-field" name="contact_email" value="<%=contact_email%>" maxlength="99" />
                                </td>
                            </tr>
                            <tr>
                                <td class="frm-label">
                                    Contact Number
                                </td>
                                <td>
                                    <input class="frm-field" name="contact_number" value="<%=contact_number%>" maxlength="49" />
                                </td>
                            </tr>
                            <tr>
                                <td class="frm-label" valign="top">
                                    Choose Department
                                </td>
                                <td>
                                    <select class="frm-select valid" name="contact_dept">
                                        <option value="SALE">Sales</option>
                                        <option value="BILL">Billing</option>
                                        <option value="TECH">Technical</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="frm-label" valign="top">
                                    Your Message
                                </td>
                                <td>
                                    <textarea class="frm-field" rows="5" cols="50" name="contact_msg"><%=contact_msg%></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <css3-container style="z-index: auto; position: absolute; direction: ltr; top: 1047px;
                                        left: 427px"><background style="POSITION: absolute; TOP: 0px; LEFT: 0px"><group2><shape style="POSITION: absolute; WIDTH: 140px; HEIGHT: 34px; TOP: 0px; BEHAVIOR: url(#default#VML); LEFT: 0px"><fill></fill><fill></fill></shape></group2></background><border style="POSITION: absolute; TOP: 0px; LEFT: 0px"><shape style="POSITION: absolute; WIDTH: 140px; HEIGHT: 34px; TOP: 0px; BEHAVIOR: url(#default#VML); LEFT: 0px"><stroke></stroke><stroke></stroke></shape></border></css3-container>
                                    <button id="existing_submit" class="ui-button" value="SendMail" name="button" type="submit">
                                        <span><span>Send us a mail</span></span></button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="div-spacer">
                </div>
                <div class="gray-blurb">
                    <table class="support-info" border="0" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td valign="top" width="20%">
                                <h4 class="ui-heading">
                                    Contact Us:
                                </h4>
                            </td>
                            <td style="border-bottom: medium none; border-left: medium none; border-top: medium none;
                                border-right: medium none" class="border-sep" valign="top" width="25%">
                                <h5 class="ui-heading">
                                    Sales
                                </h5>
                                <p>
                                    Phone: 091-9897911228
                                </p>
                                <p>
                                    Mail:<span style=""><b> billing@infowebservices.in</b></span>
                                    
                                </p>
                            </td>
                            <td class="border-sep" valign="top" width="25%">
                                <h5 class="ui-heading">
                                    Billing
                                </h5>
                                <p>
                                    Phone: 091-9897911228
                                </p>
                                <p>
                                    Mail:<span style=""><b> billing@infowebservices.in</b></span>
                                   
                                </p>
                            </td>
                            <td class="border-sep" valign="top" width="25%">
                                <h5 class="ui-heading">
                                    Technical
                                </h5>
                                <p>
                                    Phone: 091-9897911228
                                </p>
                                <p>
                                    Mail:<span style=""><b> billing@infowebservices.in</b></span>
                                    
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="div-spacer">
                </div>
                <div style="border-bottom: medium none; border-left: medium none; background: #fff;
                    border-top: medium none; border-right: medium none" class="gray-blurb">
                    <table class="data-form" width="100%">
                        <tr>
                            <td class="frm-label" valign="top" width="181">
                                <div class="ui-heading">
                                    Our Address:
                                </div>
                            </td>
                            <td class="frm-label txt-m" valign="top">
                                <b>InfoWeb Services</b><br>
                                Street No.11
                                <br>
                                Rajendra Nagar<br>
                                Kaulaghar Road<br>
                                Dehradun- 248001<br>
                                Uttaranchal, INDIA<br>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <script>
function OpenSupportTicket(){
window.open("http://www.infowebservices.in/Support");
}

    </script>

</asp:Content>
