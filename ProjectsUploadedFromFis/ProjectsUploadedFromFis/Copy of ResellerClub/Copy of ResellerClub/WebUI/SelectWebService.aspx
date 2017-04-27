<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="SelectWebService.aspx.cs"
    Inherits="ResellerClub.WebUI.SelectWebService" Title="Select Web Service" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .TD1
        {
            border-bottom: none;
            border-top: none;
            border-left: none;
            border-right: none;
            padding-top: 8px;
            padding-right: 12px;
            padding-left: 80px;
            vertical-align: top;
            width: 240px;
        }
        .TD2
        {
            border-bottom: none;
            border-top: none;
            border-left: none;
            border-right: #e7e4da 1px solid;
            padding-top: 8px;
            padding-right: 12px;
            padding-left: 80px;
            vertical-align: top;
            width: 300px;
        }
        .comman-features
        {
            padding-top: 10px;
            width: 100%;
            background: url(images/back_content_bottom.png) #ffffff left bottom no-repeat;
            border: #e7e4da 1px solid;
        }
        .comman-features TD
        {
            height: 100px;
        }
        button
        {
            background-image: url(images/button.gif);
            line-height: 20px;
            margin-top: 5px;
            width: 94px;
            background-repeat: no-repeat;
            font-family: arial;
            float: left;
            height: 23px;
            color: #b70a12;
            font-size: 12px;
            border: 0px;
            font-weight: bold;
            margin-left: 50px;
            margin-bottom: 10px;
        }
    </style>
    <style>
        .bannerheading
        {
            border-bottom: 0px;
            border-left: 0px;
            border-collapse: collapse;
            border-top: 0px;
            border-right: 0px;
        }
        .bannerheading TABLE
        {
            border-bottom: 0px;
            border-left: 0px;
            border-collapse: collapse;
            border-top: 0px;
            border-right: 0px;
        }
        .bannerheading TD
        {
            padding-bottom: 0px;
            padding-left: 0px;
            padding-right: 0px;
            vertical-align: top;
            padding-top: 0px;
        }
        .bannerheading IMG
        {
            border-bottom: 0px;
            border-left: 0px;
            border-top: 0px;
            border-right: 0px;
        }
        #table_nav_holder
        {
            background-image: url(images/nav_top_bg.png);
            position: absolute;
            width: 960px;
            background-repeat: repeat-x;
            background-position: 50% top;
            margin-top: -5px;
        }
        #cell_nav_main
        {
            background-image: url(images/nav_bg_main.png);
            text-align: center;
            width: 475px;
            background-repeat: repeat-x;
            padding-top: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="comman-features" style="border-width: 1px;">
        <div style="padding-bottom: 50px; border: solid 0px; margin-top: -7px;">
            <table id="table_nav_holder" class="bannerheading" cellspacing="0">
                <tr>
                    <td style="width: 200px;">
                        &nbsp;
                    </td>
                    <td>
                        <table id="table_logo_nav">
                            <tr>
                                <td id="cell_nav_left">
                                    <img alt="web design background" src="images/nav_bg_left.jpg" width="32" height="47" />
                                </td>
                                <td id="cell_nav_main" style="">
                                    YOU CAN ALSO BUY ANY OF FOLLOWING ALONG WITH YOUR DOMAIN
                                </td>
                                <td id="cell_nav_right">
                                    <img alt="web strategy development" src="images/nav_bg_right.jpg" width="32" height="47" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" style="border: none; padding-left: 60px; height: 100px;" cellspacing="0">
            <tr>
                <td style="vertical-align: top;">
                    <div style="background: url(images/webdesign1.png) no-repeat; font-size: 15px; padding-top: 55px;
                        height: 22px; width: 300px;">
                    </div>
                    <p style="width: 250px">
                        <span>Get </span><span style="color: #b70a12;">WebSite</span>
                    </p>
                    <p>
                        Includes web & email hosting
                    </p>
                    <p>
                        Plans start at <%=startingPriceWebSite%>/month
                    </p>
                    <div style="padding-top: 10px; width: 50%" align="center">
                        <asp:Button ID="btnWebSite" runat="server" Text="View Plan" OnClick="btnWebSite_Click" />
                    </div>
                </td>
                <td style="vertical-align: top">
                    <div style="background: url(images/icon_server.png) no-repeat; font-size: 15px; padding-top: 55px;
                        height: 22px; width: 300px;">
                    </div>
                    <p style="width: 250px">
                        <span style="padding-left: 0px;">Get </span><span style="color: #b70a12;">Web Hosting</span>
                    </p>
                    <p>
                        Includes email hosting
                    </p>
                    <p>
                        Plans start at <%=startingPriceLinuxHosting%>/month
                    </p>
                    <div style="padding-top: 10px; width: 50%" align="center">
                        <asp:Button ID="btnHosting" runat="server" Text="View Plan" OnClick="btnHosting_Click" />
                    </div>
                </td>
                <td style="vertical-align: top; padding-bottom: 5px;">
                    <div style="background: url(images/3591.png) no-repeat; font-size: 15px; padding-top: 55px;
                        height: 22px; width: 300px;">
                    </div>
                    <p style="width: 250px">
                        <span style="">Get </span><span style="color: #b70a12;">Email Hosting</span>
                    </p>
                    <p>
                        &nbsp;
                    </p>
                    <p>
                        Plans start at <%=startingPriceEmailHosting%>/month
                    </p>
                    <div style="padding-top: 10px; width: 50%" align="center">
                        <asp:Button ID="btnEmail" runat="server" Text="View Plan" OnClick="btnEmail_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="padding-top: 40px; padding-bottom: 20px;">
        <a href="CheckOut.aspx">No Thanks, Procede to check out</a>
    </div>
</asp:Content>
