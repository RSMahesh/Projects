<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="Domain.aspx.cs" Inherits="ResellerClub.WebUI.Domain" Title="Get Domain"
    ValidateRequest="false" %>

<%@ Register Src="~/UserControl/DomainSearchControl.ascx" TagName="DomainSearchControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .ui-button
        {
            border-bottom: medium none;
            text-align: left;
            border-left: medium none;
            padding-bottom: 0px;
            line-height: 39px;
            margin: 0px;
            outline-style: none;
            outline-color: invert;
            padding-left: 0px;
            outline-width: medium;
            width: auto;
            padding-right: 0px;
            display: inline-block;
            white-space: nowrap;
            background: none transparent scroll repeat 0% 0%;
            height: 41px;
            color: #ffffff;
            font-size: 16px;
            overflow: visible;
            border-top: medium none;
            cursor: pointer;
            font-weight: bold;
            border-right: medium none;
            padding-top: 0px;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.25);
        }
        .ui-button SPAN
        {
            padding-bottom: 0px;
            padding-left: 5px;
            padding-right: 0px;
            display: block;
            background: url(images/sprite-input.gif) no-repeat left top;
            height: 39px;
            padding-top: 0px;
            _display: inline-block;
        }
        .ui-button SPAN SPAN
        {
            border-bottom: medium none;
            border-left: medium none;
            padding-bottom: 0px;
            line-height: 39px;
            padding-left: 10px;
            padding-right: 15px;
            display: block;
            background: url(images/sprite-input.gif) #a2d944 no-repeat right -40px;
            height: 39px;
            border-top: medium none;
            cursor: pointer;
            border-right: medium none;
            padding-top: 0px;
            _display: inline-block;
        }
        .ui-button-2
        {
            line-height: 12px;
            width: 160px;
            height: 55px;
            font-size: 12px;
        }
        .ui-button-2 SPAN
        {
            text-align: center;
            background-position: left -272px;
            height: 55px;
        }
        .ui-button-2 SPAN SPAN
        {
            line-height: 15px;
            background-position: right -329px;
            height: 45px;
            padding-top: 10px;
            _width: 120px;
        }
        .ui-button-2 SPAN STRONG
        {
            font-size: 18px;
        }
        .includedFree H4
        {
            padding-bottom: 0px;
            margin: 0px 0px 1px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        UL.includedFree
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            background: #fff;
            padding-top: 0px;
        }
        UL.includedFree LI
        {
            line-height: 24px;
            padding-left: 20px;
            list-style-type: none;
            background: url(images/Mooshak.png) #fff no-repeat left center;
            font-size: 14px;
        }
        .hideIncludedFree UL LI
        {
            display: none;
        }
        UL.includedFree LI.showmore
        {
            padding-left: 0px;
            background: none transparent scroll repeat 0% 0%;
            font-size: 12px;
        }
        UL.includedFree LI.showless
        {
            padding-left: 0px;
            background: none transparent scroll repeat 0% 0%;
            font-size: 12px;
        }
        .freeHighlight
        {
            color: #ec8c56;
        }
        .sidebar-head
        {
            border-bottom: #aeaeae 1px solid;
            margin-top: 20px;
            background: url(images/bg-new-sb-header-2.gif) #d7d7d7 repeat-x left top;
            height: 35px;
        }
        .rbtop DIV
        {
            background: url(images/sidebar-top-left-curve.gif) no-repeat left top;
        }
        .rbtop DIV SPAN
        {
            margin: 0px 6px;
            display: block;
            border-top: #d2d2d2 1px solid;
        }
        .rbtop
        {
            background: url(images/sidebar-top-right-curve.gif) no-repeat right top;
        }
        .rbtop DIV
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
        }
        .rbtop
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
        }
        .rbtop DIV
        {
            background: url(images/sidebar-top-left-curve.gif) no-repeat left top;
        }
        .rbtop DIV SPAN
        {
            margin: 0px 6px;
            display: block;
            background: #f6f6f6;
            height: 6px;
            border-top: #d2d2d2 1px solid;
        }
        .rbtop
        {
            background: url(images/sidebar-top-right-curve.gif) no-repeat right top;
        }
        .rbtop DIV
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
        }
        .rbtop
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
        }
        .sidebar-head H2
        {
            padding-bottom: 8px;
            padding-left: 11px;
            padding-right: 0px;
            background: none transparent scroll repeat 0% 0%;
            color: #333333;
            font-size: 15px;
            font-weight: bold;
            padding-top: 8px;
        }
        .new-sidebar-blurb
        {
            margin: 0px 0px 20px;
            background: url(images/side-bg_01.gif) no-repeat left top;
            width: 245px;
            height: 50px;
        }
        .sidebar-content P
        {
            padding-bottom: 8px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        .sidebar-head H2
        {
            border-left: #c9c9c9 1px solid;
            padding-bottom: 2px;
            padding-left: 11px;
            padding-right: 0px;
            color: #333333;
            font-size: 12px;
            font-weight: bold;
            border-right: #c9c9c9 1px solid;
            padding-top: 0px;
        }
        .sidebar-content
        {
            border-left: #c9c9c9 1px solid;
            padding-bottom: 0px;
            padding-left: 8px;
            padding-right: 8px;
            background: #f6f6f6;
            overflow: hidden;
            border-right: #c9c9c9 1px solid;
            margin-top: -15px;
            _height: 1%;
        }
        .rbbot
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
            background: url(images/sidebar-bottom-right-curve.gif) no-repeat right bottom;
        }
        .rbbot DIV
        {
            width: 100%;
            height: 7px;
            font-size: 1px;
            background: url(images/sidebar-bottom-left-curve.gif) no-repeat left bottom;
        }
        .rbbot DIV SPAN
        {
            border-bottom: #d6d6d6 1px solid;
            margin: 0px 6px;
            display: block;
            background: #f6f6f6;
            height: 6px;
        }
        .available
        {
            word-wrap: break-word;
            width: 175px;
            float: right;
            overflow: hidden;
        }
        .hidden
        {
            display: none;
        }
        .actionRow
        {
            border-bottom: #cecece 1px solid;
            text-align: right;
            border-left: #cecece 1px solid;
            padding-bottom: 10px;
            margin: 15px 0px 100px;
            padding-left: 10px;
            padding-right: 10px;
            background: url(images/bg_gradient_CCCEEE.png) #ccc repeat-x 0px 0px;
            height: 53px;
            clear: both;
            border-top: #cecece 1px solid;
            border-right: #cecece 1px solid;
            padding-top: 10px;
            -moz-border-radius: 6px;
            width: 96%;
        }
        P.PageTotal
        {
            text-align: right;
            padding-bottom: 5px;
            line-height: 23px;
            margin: 0px;
            padding-left: 5px;
            width: 510px;
            padding-right: 5px;
            float: left;
            color: #333;
            font-size: 13px;
            font-weight: bold;
            padding-top: 5px;
        }
        SPAN#total_curr
        {
            color: #0e4e5a;
            font-size: 18px;
        }
        .PageTotal SPAN#page_total
        {
            padding-left: 4px;
            color: #0e4e5a;
            font-size: 20px;
        }
        h3
        {
            color: Green;
            font-size: 14;
        }
    </style>

    <script>

        $(document).ready(function () {

            $("select[id*=_plan]").each(function () {
                $(this).change(function () {
                    GetTotalAmount();
                });
            }
       );


            $("select[id*=ddlPrimeDominPlan]").change(function () {
                GetTotalAmount();
            });


            GetTotalAmount();
        });


        function show_selected_domain_checked_in_down_panel(chk) {
            var sideBarRow = $(chk).parent();
            var ddlPlan = sideBarRow.find("select[id*=_sidebar]").clone(true);
            ddlPlan.attr("name", $(chk).val() + "_plan");
            ddlPlan.attr("id", $(chk).val() + "_plan");
            ddlPlan.show();
            $(ddlPlan).change(function () {
                GetTotalAmount();
            });

            sideBarRow.find("select[id*=_sidebarplan]").remove();

            var row = $('#tblSideBarTemplate tbody>tr:first').clone(true);
            row.insertAfter('#tblPrimeDomain tbody>tr:first');
            sideBarRow.parent().hide();

            var chkObj = $(document.createElement("input")).attr({
                id: $(chk).val() + '_chkDomain'
                        , name: "domainnamearr[]"
                        , value: $(chk).val()
                        , type: 'checkbox'
                        , checked: true
            })
                .click(function (event) {
                    var cbox = $(this)[0];
                    GetTotalAmount();
                })

            $(chkObj).val($(chk).val())
            row.find("td[id*=tdTemplateDomainName]").html(' ');
            row.find("td[id*=tdTemplateDomainName]").append(chkObj);
            row.find("td[id*=tdTemplateDomainName]").append($(chk).val());

            row.find("select[id*=dllTemplat_plan]").parent().append(ddlPlan);
            row.find("select[id*=dllTemplat_plan]").remove();
            var email = row.find(".email").html().replace('mydomain.com', $(chk).val());
            row.find(".email").html(email);
            GetTotalAmount();
            return false;
        }

        function ShowAllFreeItem(obj) {
            var p = $(obj).parent().parent();
            $(p).children(".hidden").toggle();
            $(p).children(".nothidden").toggle();
        }

        function GetTotalAmount() {
     
            var p = null;
            var ddl = null;
            var amount = 0;
            $("input[type=checkbox]").each(function () {
                if ($(this)[0].checked) {
                    ddl = $($(this).parent().parent()).find("select")
                    if ($(ddl).length != 0) {
                     amount += GetPlan($(ddl).val()).Price;
                    }
                }
            }

       );

            if( GetPlan($("select[id*=ddlPrimeDominPlan]").val())){
            amount += GetPlan($("select[id*=ddlPrimeDominPlan]").val()).Price;
            }
            
            $("#page_total").html(amount.toFixed(2));
        }
    
    </script>

    <script src="JavaScript/Global.js" type="text/javascript"></script>

    <div runat="server" id="divUserMsg">
        <div class="dialogerrorsub">
            <div runat="server" id="divUserMsgContent">
            </div>
        </div>
    </div>
    <table>
        <tr id="trDomainAvailable" runat="server" visible="false">
            <td id="td1" colspan="2" style="padding-bottom: 10px; font-size: 15px; font-weight: bold;
                color: Gray;" runat="server" width="100%">
                Your domain name <span style="color: #a50910;">
                    <%=SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString()%></span>
                is available
                <div style="width: 100%; border-bottom: #e7e4da 1px solid;">
                </div>
            </td>
        </tr>
        <tr id="trDomainNotAvailable" runat="server" visible="false">
            <td id="td2" colspan="2" style="padding-bottom: 10px; font-size: 15px; font-weight: bold;
                color: Gray;" runat="server" width="100%">
                Domain name <span style="color: #a50910;">
                    <%=SessionM["UserEnteredDomain"].ToString() + SessionM["UserSelectedTDLs"].ToString()%></span>
                is Taken
                <div style="width: 100%; border-bottom: #e7e4da 1px solid;">
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td colspan="2">
                <asp:PlaceHolder ID="plhSearchControl" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr id="trDisplaySearchResult" visible="false" runat="server">
            <td width="75%" style="vertical-align: top">
                <table id="tblPrimeDomain" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td id="tdDomainName" style="font-size: 14px; font-weight: bolder; color: Gray" runat="server"
                                        width="100%">
                                        cdfdfffdfdf.co
                                    </td>
                                    <td>
                                        Book it for:
                                        <asp:DropDownList ID="ddlPrimeDominPlan" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="display: none">
                                        <h4>
                                            <span class="freeHighlight">FREE!</span> with every domain (worth Rs. 5000)</h4>
                                        <ul class="fdns-sb-content includedFree">
                                            <li class="email">5 Email Accounts - you@ssdsdsdsdsdsdsds.com</strong></li>
                                            <li class="chatClient">Enterprise Chat at your Domain</li>
                                            <li class="domainForwarding">Domain Forwarding</li>
                                            <li class="privacyProtection">Privacy Protection <span>*</span></li>
                                            <li class="mailForwarding hidden">Mail Forwarding</li>
                                            <li class="urlMasking hidden">URL Masking</li>
                                            <li class="dnsManagement hidden">DNS Management</li>
                                            <li class="domainTheft hidden">Domain Theft Protection</li>
                                            <li class="bulkTools hidden">Bulk Tools</li>
                                            <li class="controlPanel hidden">Easy-to-use Control Panel</li>
                                            <li class="localSupport hidden">24/7 Local Support</li>
                                            <li class="showmore nothidden"><span style="cursor: pointer" onclick="ShowAllFreeItem(this)">
                                                <span class="FreeItemCart">and 7 more items. </span>See All Free Services »</span></li>
                                            <li class="showless hidden"><span style="cursor: pointer" onclick="ShowAllFreeItem(this)">
                                                « Less</span></li></ul>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:PlaceHolder ID="plhDomaninValibaleList" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; padding-left: 20px">
                <div id="Div1">
                    <div class="new-sidebar-blurb">
                        <div align="center" style="padding-left: 5px; padding-top: 10px; color: White; font-size: 14px;
                            font-weight: bold">
                            Free with every domain</div>
                    </div>
                    <h4>
                        <span class="freeHighlight">FREE!</span> with every domain (worth Rs. 5000)</h4>
                    <ul class="fdns-sb-content includedFree">
                        <li class="email">5 Email Accounts</li>
                        <li class="chatClient">Enterprise Chat at your Domain</li>
                        <li class="domainForwarding">Domain Forwarding</li>
                        <li class="privacyProtection">Privacy Protection <span>*</span></li>
                        <li class="mailForwarding hidden1">Mail Forwarding</li>
                        <li class="urlMasking hidden1">URL Masking</li>
                        <li class="dnsManagement hidden1">DNS Management</li>
                        <li class="domainTheft hidden1">Domain Theft Protection</li>
                        <li class="bulkTools hidden1">Bulk Tools</li>
                        <li class="controlPanel hidden1">Easy-to-use Control Panel</li>
                        <li class="localSupport hidden1">24/7 Local Support</li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divAmountStrip" runat="server" visible="false" class="">
                    <p class="PageTotal">
                        Total Amount
                        <br>
                        <span id="total_curr">
                            <%=CurrencySymbol%><span id="page_total">0.00</span></span>
                    </p>
                    <button id="btnContinue" style="font-size: 14px; height: 70px; width: 150px" name="btnContinue"
                        type="submit">
                        <span><span><strong>Continue</strong>
                            <br>
                            Proceed to cart</span></span></button>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tblSideBarTemplate" class="hidden">
        <tr>
            <td>
                <table>
                    <tr>
                        <td id="tdTemplateDomainName" style="font-size: 14px; font-weight: bold; color: Green"
                            runat="server" width="100%">
                            cdfdfffdfdf.co
                        </td>
                        <td>
                            Book it for:
                            <asp:DropDownList ID="dllTemplat_plan" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h4>
                                <span class="freeHighlight">FREE!</span> with every domain (worth Rs. 5000)</h4>
                            <ul class="fdns-sb-content includedFree">
                                <li class="email">5 Email Accounts - <strong>myemail@mydomain.com</strong></li>
                                <li class="chatClient hidden">Enterprise Chat at your Domain</li>
                                <li class="domainForwarding hidden">Domain Forwarding</li>
                                <li class="privacyProtection hidden">Privacy Protection <span>*</span></li>
                                <li class="mailForwarding hidden">Mail Forwarding</li>
                                <li class="urlMasking hidden">URL Masking</li>
                                <li class="dnsManagement hidden">DNS Management</li>
                                <li class="domainTheft hidden">Domain Theft Protection</li>
                                <li class="bulkTools hidden">Bulk Tools</li>
                                <li class="controlPanel hidden">Easy-to-use Control Panel</li>
                                <li class="localSupport hidden">24/7 Local Support</li>
                                <li class="showmore nothidden"><span style="cursor: pointer" onclick="ShowAllFreeItem(this)">
                                    <span class="FreeItemCart">and 7 more items. </span>See All Free Services »</span></li>
                                <li class="showless hidden"><span style="cursor: pointer" onclick="ShowAllFreeItem(this)">
                                    « Less</span></li></ul>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
