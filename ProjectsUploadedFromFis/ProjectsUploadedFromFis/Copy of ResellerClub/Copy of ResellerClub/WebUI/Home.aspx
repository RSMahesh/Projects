<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs"
    Inherits="ResellerClub.WebUI.Home" Title="InfowebServices" %>

<%@ Register Src="UserControl/DomainSearchControl.ascx" TagName="DomainSearchControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link rel="stylesheet" type="text/css" href="StyleSheet/<%=GetLanguage()%>_HOMEPAGE.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellspacing="0" id="tblHomeContainer">
        <tr>
            <td>
            </td>
            <td style="padding-right: 15px;">
                <div style="border-right: #e7e4da 1px solid; height: 240px;">
                    <div class="heading1">
                        <%=__("HOMEPAGE_LEFT_HEADING_PART1")%> <span><%=__("HOMEPAGE_LEFT_HEADING_PART2")%></span></div>
                    <uc1:DomainSearchControl ID="DomainSearchControl1" runat="server" />
                </div>
            </td>
            <td style="padding-left: 15px">
                <div>
                    <a href="WebSite/WebsiteBuilder.aspx" style="text-decoration: none; font-weight: normal"><span
                        class="heading1" style="color: #3173ab;"><%=__("HOMEPAGE_RIGHT_HEADING")%></span> </a>
                    <div class="website-feature">
                        <ul>
                            <li><%=__("HOMEPAGE_RIGHT_SUBHEADING1")%> </li>
                            <li><%=__("HOMEPAGE_RIGHT_SUBHEADING2")%> </li>
                            <li><%=__("HOMEPAGE_RIGHT_SUBHEADING3")%> </li>
                            <li><%=__("HOMEPAGE_RIGHT_SUBHEADING4")%> </li>
                        </ul>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="padding-top: 25px; padding-bottom: 5px;">
                <div id="wrapper" class="fix">
                    <div id="container" class="fix ">
                        <div class="fboxes fix">
                            <div class="fboxdividers fix">
                                <div class="fbox">
                                    <div class="fboxcopy">
                                        <div class="fboxtitle" style="background: url(images/linux-windows.png) no-repeat left top;
                                            height: 50px">
                                            <h3 style="padding-left: 70px; padding-top: 10px">
                                                <%=__("HOMEPAGE_WEBHOSTING")%>
                                            </h3>
                                        </div>
                                        <div class="fboxtext">
                                            <%=__("HOMEPAGE_WEBHOSTING_DESCRIPTION")%>
                                            <ul class="package">
                                                <li><%=__("HOMEPAGE_WEBHOSTING_FEATURE1")%> </li>
                                                <li><%=__("HOMEPAGE_WEBHOSTING_FEATURE2")%></li>
                                                <li><%=__("HOMEPAGE_WEBHOSTING_FEATURE3")%></li>
                                            </ul>
                                        </div>
                                        <button onclick="javascript:window.location='Hosting/LinuxHostingPlan.aspx'; return false;">
                                            <%=__("HOMEPAGE_ViewPlan")%></button>
                                    </div>
                                </div>
                                <div class="fbox">
                                    <div class="fboxcopy">
                                        <div class="fboxtitle" style="height: 50px; background: url(images/email1.png) no-repeat left top">
                                            <h3 style="padding-left: 55px; padding-top: 10px;">
                                                <%=__("HOMEPAGE_EMAILHOSTING")%>
                                            </h3>
                                        </div>
                                        <div class="fboxtext">
                                            <%=__("HOMEPAGE_EMAILHOSTING_DESCRIPTION")%>
                                            <ul class="package">
                                                <li><%=__("HOMEPAGE_EMAILHOSTING_FEATURE1")%></li>
                                                <li><%=__("HOMEPAGE_EMAILHOSTING_FEATURE2")%></li>
                                                <li><%=__("HOMEPAGE_EMAILHOSTING_FEATURE3")%></li>
                                            </ul>
                                        </div>
                                        <button onclick="javascript:window.location='Hosting/EmailHosting.aspx'; return false;">
                                            <%=__("HOMEPAGE_ViewPlan")%></button>
                                    </div>
                                </div>
                                <div class="fbox">
                                    <div class="fboxcopy">
                                        <div class="fboxtitle" style="height: 50px; background: url(images/ssl_lock.png) no-repeat left top">
                                            <h3 style="padding-left: 52px; padding-top: 10px;">
                                                <%=__("HOMEPAGE_SSL")%>
                                            </h3>
                                        </div>
                                        <div class="fboxtext">
                                            <%=__("HOMEPAGE_SSL_DESCRIPTION")%>
                                            <ul class="package">
                                                <li><%=__("HOMEPAGE_SSL_FEATURE1")%></li>
                                                <li><%=__("HOMEPAGE_SSL_FEATURE2")%></li>
                                                <li><%=__("HOMEPAGE_SSL_FEATURE3")%></li>
                                            </ul>
                                        </div>
                                        <button onclick="javascript:window.location='DigitalCertificates.aspx'; return false;">
                                            <%=__("HOMEPAGE_ViewPlan")%></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
