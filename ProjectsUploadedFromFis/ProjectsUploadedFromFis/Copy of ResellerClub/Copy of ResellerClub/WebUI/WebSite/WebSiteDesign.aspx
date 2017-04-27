<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="WebSiteDesign.aspx.cs"
    Inherits="ResellerClub.WebUI.WebSiteDesign" Title="WebSite Design" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  

    <script src="JueryControl/jquery-1.6.2.js"></script>

    <script type="text/javascript" src="JueryControl/Tab/jquery-ui-personalized-1.5.2.packed.js"></script>

    <script type="text/javascript" src="JueryControl/Tab/sprinkle.js"></script>

    <style>
        .divBanner
        {
            width: 1000px;
            color: black;
        }
        .highlight
        {
            background: url(images/desginurdesier.gif) no-repeat;
            padding-top: 63px;
        }
        .highlight LI
        {
            padding-left: 21px;
            line-height: 25px;
            font-size: 15px;
            margin-bottom: 5px;
            color: Gray;
            width: 300px;
            list-style : none;
            background: url(images/Bullet_Point.png) no-repeat left center;
        }
        .webSiteFeatures LI
        {
            padding-left: 21px;
            width: 309px;
            display: block;
            line-height: 20px;
            font-size: 12px;
            margin-bottom: 5px;
            background: url(images/plan-list1.png) no-repeat left center;
            color: #666;
        }
        .DPTD1
        {
            border-bottom: none;
            border-top: none;
            border-left: none;
            border-right: #e7e4da 0px solid;
            padding-top: 8px;
            padding-right: 12px;
            padding-left: 20px;
            padding-bottom: 5px;
            vertical-align: top;
            line-height: 18px;
            width: 240px;
            background: url(images/arrow1.png) no-repeat right center;
        }
        .TD2
        {
            border-bottom: none;
            border-top: none;
            border-left: none;
            border-right: #e7e4da 0px solid;
            padding-top: 8px;
            padding-right: 12px;
            padding-left: 10px;
            vertical-align: top;
            width: 240px;
        }
        TH
        {
            border-bottom: none;
            border-top: none;
            border-left: none;
            border-right: none;
        }
        .comman-features
        {
            width: 100%;
            background: url(images/back_content_top.png) #ffffff no-repeat;
            border: #e7e4da 1px solid;
            margin-bottom: 1px;
        }
        .comman-features UL LI
        {
            padding-left: 13px;
            line-height: 25px;
            list-style:none;
            background: url(images/arrow.gif) no-repeat left center;
        }
        .comman-features IMG
        {
            height: 25px;
            width: 25px;
        }
        .designprocess
        {
            width: 100%;
            margin-top: 20px;
            background: url(images/back_content_bottom.png) #ffffff no-repeat left bottom;
            border: #e7e4da 1px solid;
            margin-bottom: 1px;</style>
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
            width: 100%;
            background-repeat: repeat-x;
            background-position: 50% top;
            margin-top: -5px;
        }
        #cell_nav_main
        {
            background-image: url(images/nav_bg_main.png);
            text-align: center;
            width: 425px;
            background-repeat: repeat-x;
            padding-top: 15px;
        }
    </style>
    <style>
        .thirteenredbold
        {
            text-transform: capitalize;
            color: #A50910;
            font-weight: bold;
        }
        .twelveredbold
        {
            padding-bottom: 2px;
            color: #4a4949;
            font-size: 11px;
            font-weight: bold;
            padding-top: 3px;
        }
        P
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        .package2
        {
            font-size: 12px;
        }
        .v-sap
        {
            background-image: url(images/v-sap.gif);
            width: 1px;
            background-repeat: repeat-y;
        }
        .h-sap
        {
            background-image: url(images/h-sap.gif);
            background-repeat: repeat-x;
            height: 1px;
        }
        .itemname
        {
            background: url(images/arrow.gif) no-repeat left center;
            padding-left: 10px;
            color: #999999;
        }
        .itemavailable
        {
            font-size: 11px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--h2 style="color: Gray; padding-bottom: 10px;">
        Professional <span style="color: #a50910;">Website Design </span>
    </h2-->
    <table class="divBanner" cellpadding="0" cellspacing="0" border="0" style="border:solid 0px #e7e4da">
        <tr>
            <td align="right" style="padding-bottom: 10px; vertical-align: top; background: url(images/banner_website_design.jpg) #ffffff no-repeat;
                height: 270px;" colspan="3">
                <div class="highlight" style="width: 470px;" align="left">
                    <ul style="padding-left: 20px;">
                        <li>Expend your business globaly</li>
                        <li>Generate & convert leads </li>
                        <li>Decress advertsing costs</li>
                        <li>sell 24/7/365</li>
                        <li>Get feedback from our customers</li>
                        <li>within 2 weeks</li>
                    </ul>
                </div>
            </td>
        </tr>
        <!--tr>
        <td colspan="3">
         <div id="mycarousel" class="jcarousel-skin-ie7" style="width:90px; height: 150px">
                    <ul>
                         The content will be dynamically loaded in here 
                    </ul>
                </div>
                <br />
        </td>
        </tr> -->
        <tr>
            <td colspan="3">
                <div class="designprocess" style="padding-top: 0px">
                    <div align="center" style="height: 30px;border-bottom:dashed 1px #e7e4da;
                        font-weight: bold; font-size: 14px; padding-left: 10px; padding-top: 5px;">
                        5 Step <span style="color: #A50910">Design Process</span>
                    </div>
                    <table border="1" width="100%" style="border: none; padding-top: 15px; padding-bottom: 25px;"
                        cellspacing="0">
                        <tr>
                            <th align="center">
                                Step<span style="color: #A50910"> 1</span>
                            </th>
                            <th align="center">
                                Step<span style="color: #A50910"> 2</span>
                            </th>
                            <th align="center">
                                Step<span style="color: #A50910"> 3</span>
                            </th>
                            <th align="center">
                                Step<span style="color: #A50910"> 4</span>
                            </th>
                            <th align="center">
                                Step<span style="color: #A50910"> 5</span>
                            </th>
                        </tr>
                        <tr>
                            <td class="DPTD1" style="height: 70px;">
                                We will gather your requirements
                            </td>
                            <td class="DPTD1" style="padding-right: 25px;">
                                We will design a sitemap / navigation pages
                            </td>
                            <td class="DPTD1">
                                We will gather your content (text, brochures, images, logos, ...)
                            </td>
                            <td class="DPTD1" style="padding-right: 45px;">
                                We will create designs and a look-and-feel for your site
                            </td>
                            <td class="TD2">
                                We will build your website based on all your input
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div class="comman-features">
                    <table border="0" width="100%" style="border: none; padding-top: 8px;" cellspacing="0">
                        <tr>
                            <th style="padding-left: 30px">
                                <div align="left" style="height: 50px; font-size: 16px; padding-left: 60px; padding-top: 10px;
                                    background: url(images/professionalWebSite.png) no-repeat -1px -1px;">
                                    <span style="color: #A50910">Standard </span>Website
                                </div>
                            </th>
                            <th style="padding-left: 90px">
                                <div align="left" style="height: 50px; font-size: 16px; padding-left: 60px; padding-top: 10px;
                                    background: url(images/ecommerce.png) no-repeat -10px -13px;">
                                    <span style="color: #A50910">E commerece </span>Website
                                </div>
                            </th>
                        </tr>
                        <tr>
                            <td style="width: 43%; vertical-align: top;">
                                <div style="padding-left: 40px; padding-top: 7px; border-right: #e7e4da 1px solid;">
                                    <ul>
                                        <li>One-on-one consultations with a web designer</li>
                                        <li>Upto 10 professionally designed pages</li>
                                        <li>Professional Maintenance and Content Updates</li>
                                        <li>Get Your Website Up & Running within Days</li>
                                        <li>Dedicated Tech Support</li>
                                        <li>and much more...</li>
                                    </ul>
                                    <div style="padding-top: 68px; line-height: 20px; font-size: 14px;">
                                        <span style="color: #0e4e5a; font-weight: bold"><%=CurrencySymbol%>  <%=GetPlanStartingPrice("standard_bifm_plan", 1)%></span> * One-time-setup
                                        <br />
                                        <span style="color: #0e4e5a; font-weight: bold"><%=CurrencySymbol%>  <%=GetPlanStartingPrice("standard_bifm_plan", 1)%>/month </span>* Maintenance
                                        fee (Optional)</div>
                                </div>
                                <div align="center" style="padding-top: 10px; padding-bottom: 10px; padding-right: 40px;">
                                    <asp:Button ID="btnPwebsite" runat="server" Text="Order this" Style="padding: 4px;
                                        width: 80px;" onclick="btnPwebsite_Click" />
                                </div>
                            </td>
                            <td style="width: 50%; vertical-align: top;">
                                <div style="padding-left: 90px; padding-top: 7px;">
                                    <ul>
                                        <li>One-on-one consultations with a web designer</li>
                                        <li>15 professionally designed pages</li>
                                        <li>Accept payments via Credit Card/Debit Card/PayPal/Google</li>
                                        <li>Get Your Website Up & Running within Days</li>
                                        <li>Dedicated Tech Support</li>
                                        <li>FREE Setup for 20 Products (up to 200 products supported)</li>
                                        <li>Professional Maintenance and Content Updates</li>
                                        <li>and much more ...</li>
                                    </ul>
                                    <div style="padding-top: 20px; line-height: 20px; font-size: 14px;">
                                        <span style="color: #0e4e5a; font-weight: bold"><%=CurrencySymbol%> <%=GetPlanStartingPrice("ecommerce_bifm_plan", 1)%> </span> <span>* One-time-setup</span>
                                        <br />
                                        <span style="color: #0e4e5a; font-weight: bold"><%=CurrencySymbol%> <%=GetPlanStartingPrice("ecommerce_bifm_plan", 1)%>/month </span>* Maintenance
                                        fee (Optional)</div>
                                </div>
                                <div align="center" style="padding-top: 8px; padding-bottom: 10px;">
                                    <asp:Button ID="btnEwebsite" runat="server" Text="Order this" Style="padding: 4px;
                                        width: 80px;" onclick="btnEwebsite_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="tabvanilla" class="widget" style="width: 1000px; margin-top: 20px">
                    <ul class="tabnav" style="color: #A50910; font-size: 12px;">
                        <li><a href="#popular">Compare Plans</a></li>
                        <li><a href="#recent">FAQs</a></li>
                        <li><a href="#featured">Featured</a></li>
                    </ul>
                    <div id="popular" class="tabdiv">
                        <table border="0" cellspacing="2" cellpadding="2" width="70%">
                            <tr>
                                <td class="thirteenredbold" valign="top">
                                    &nbsp;
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="thirteenredbold" valign="top">
                                    <div align="center" style="width: 165px; font-size: 12px;">
                                        Standard Package
                                    </div>
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="thirteenredbold" valign="top" align="center">
                                    <div align="center" style="width: 165px; font-size: 12px;">
                                        Ecommerce Package
                                    </div>
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    10 Pages
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    15 Pages
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Domain Name
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Web Hosting
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Email Accounts
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Self edits once site is live
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Payment Gateway Integration
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Shopping Cart
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Online Shopper Registration
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                            <tr>
                                <td class="itemname" valign="top">
                                    Number of pages
                                </td>
                                <td class="v-sap" valign="top">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                                <td class="itemavailable" valign="top" align="center">
                                    <img src="images/tick.gif" />
                                </td>
                                <td class="v-sap" valign="top" align="center">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--/popular-->
                    <div id="recent" class="tabdiv">
                        <p>
                            <strong>Frequently Asked Questions about Infoweb Services's Website Design Packages</strong></p>
                        <ul>
                            <li style="">
                                <p>
                                    <strong>What is Infoweb Services's DesignCenter?</strong> DesignCenter is your very
                                    own custom portal where you can interact with your web designers, post comments,
                                    approve designs, and more. You will sent your DesignCenter login/password once your
                                    order has been provisioned.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Does my website include a domain name &amp; hosting? What if I want to host
                                        somewhere else?</strong> A domain name &amp; hosting package for your website
                                    must be purchased through us. You cannot host your site elsewhere or use a domain
                                    name purchased using a different provider. If you have already purchased a domain
                                    name and hosting package we can use this to provision your website.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Your Ecommerce package offers only 20 products. What if I need more?</strong>
                                    A total of 20 products can be added to the ecommerce website for free by the web
                                    designers. Requests for addition of additional products will incur a fee. Note that
                                    your ecommerce website supports a total of 200 products.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you resize/bevel/re-color my images?</strong> Our designers will use
                                    all the images you provide as-is. Besides basic cropping, we are unable to perform
                                    advanced manipulation on your images. Please ensure that you send us the images
                                    you want to use in a ready-to-upload format.
                                </p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you create an account with a Payment Gateway provider for me?</strong>
                                    You must provide details for the Payment Gateways to be integrated into your Ecommerce
                                    website. The web designers cannot create Payment Gateway accounts on your behalf.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you configure email accounts for me?</strong> Email accounts must be
                                    configured by you using your email hosting control panel. Infoweb Services's website
                                    designers cannot assist with email configuration.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>How will I interact with the web designers?</strong> Consultations with
                                    web designers will be conducted through the Infoweb Services's DesignCenter online
                                    control panel and/or the phone. Personal visits by the web designers are not possible.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>What do you mean by 'Site Build Time'?</strong> Site Build Time is calculated
                                    as the time taken to publish the website once all content and approvals have been
                                    provided by the customer. We will publish your website in under 2 weeks once all
                                    approvals are received.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>I do not have content for some pages on my website. Will you write content for
                                        me?</strong> Content writing services are not provided with this package. All
                                    content, images, logos, text, video, audio, etc... for the website must be provided
                                    by you without which the website design cannot proceed.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Why do you lock steps in Infoweb Services's DesignCenter?</strong> At each
                                    step of your design process we will confer with you and ensure that you are satisfied
                                    before you move on to the next step. For example, we will ensure that your site
                                    map is what you want before we move to collecting your information. Locking each
                                    step and permitting no more changes, after we obtain your permission, ensures that
                                    we can minimize rework and really be sure that you are ready to move on to the next
                                    stage.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>How much video / audio can I add to my website?</strong> A maximum of 10
                                    video files and 10 audio files of can be added to the website. All audio/video files
                                    must be hosted on a 3rd party site such as YouTube. No audio/video files can be
                                    uploaded to your website directly.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>I do not have a logo for my website, will you create one for me?</strong>
                                    Logo design is not included as part of this package. You must provide a logo for
                                    inclusion on the website if you would like one added to your website.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>How many customer contact forms can I add to my website?</strong> Only 1
                                    customer contact form can be added per website.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Can I contact you if I need changes to my website once it is published?</strong>
                                    Once your website is published consultations with the web designer are limited to
                                    1 revision within 30 days of site being published. A revision is counted as all
                                    changes in a single phone call / single online request using Infoweb Services's
                                    DesignCenter. If you will need ongoing maintenance please consider signing up for
                                    our annual maintenance packages.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Can I cancel my website design package?</strong> The website design package
                                    may be canceled prior to the creation of the first website design template for a
                                    cancellation fee of $45. Any cancellations after the website has been published
                                    will result in the loss of the entire setup fee.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you optimize my website to make it search engine friendly?</strong>
                                    At this time, no customized search engine optimizations are included as part of
                                    this package. We will offer specialized plans soon to cater to your SEO needs.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you create a Google Analytics account for my website?</strong> Your
                                    website design includes setting up analytics using Google. However, you must create
                                    and provide a Google analytics account to us. The web designers cannot create a
                                    Google analytics account for you or perform any advanced Google analytics setup
                                    as part of this package.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>The features on the website plan look great, but I would like to add a different
                                        widget that you don't provide?</strong> At this time, no additional website
                                    gadgets/widgets/customizations can be provided except the widgets and features offered.
                                    It is our constant endeavor to keep adding functionality.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you add Flash content / banners to my website?</strong> We typically
                                    advise against Flash banners - we offer scrolling image banners that are much better
                                    from a usability perspective for your website. However, if you give us the .swf
                                    file we will upload your Flash banner. Our designers cannot create a Flash banner
                                    for you.</p>
                            </li>
                        </ul>
                        <p>
                            <strong>Frequently Asked Questions about Infoweb Services's Website Maintenance Packages</strong></p>
                        <ul>
                            <li>
                                <p>
                                    <strong>What do you mean by 30 or 60 minutes a month of consultation in your maintenance
                                        packages?</strong> Our designers will allocate these number of minutes a month
                                    towards updating your website. This includes the time taken to interact with you
                                    and the time taken to update your website.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you add new widgets to my website after it has been built if I buy a maintenance
                                        plan?</strong> Sure we will. As long as the widgets belong to set of the widgets
                                    included in the corresponding design package. For example, we'll gladly add a contact
                                    form for you if that's what you want. If the widget is not included in the original
                                    design package then we won't be able to add it.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Can I carry over unused consultation time / additional product setup to the
                                        next month?</strong> Unused time or product setup credits cannot be carried
                                    over to the next month.</p>
                            </li>
                            <li>
                                <p>
                                    <strong>Will you redesign my website / change the theme?</strong> The maintenance
                                    pack allows you to modify the look-and-feel, colors, content, and customize your
                                    website to keep it fresh looking. We are unable to completely redesign your website
                                    / change its theme once it has been built.</p>
                            </li>
                        </ul>
                    </div>
                    <!--/recent-->
                    <div id="featured" class="tabdiv">
                        <ul>
                            <li><a href="#">Aliens Infiltrate Army Base In UK Town</a></li>
                            <li><a href="#">Are We Alone? A Look Into Space</a></li>
                            <li><a href="#">U2 Rocks New York's Central Park</a></li>
                            <li><a href="#">TA Soldiers Wear Uniforms To Work</a></li>
                            <li><a href="#">13 People Rescued From Flat Fire</a></li>
                            <li><a href="#">US Troops Abandon Afghan Outpost</a></li>
                            <li><a href="#">Sheep Rising From The Dead</a></li>
                            <li><a href="#">Blogosphere Daily Released!</a></li>
                            <li><a href="#">Apple iPhone 3G Released</a></li>
                            <li><a href="#">Welsh Zombie Sheep Invasion</a></li>
                        </ul>
                    </div>
                    <!--featured-->
                </div>
                <!--/widget-->
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
    </table>
</asp:Content>
