<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainSerachControl2.ascx.cs" Inherits="ResellerClub.WebUI.UserControl.DomainSerachControl2" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainSearchControl.ascx.cs"
    Inherits="ResellerClub.WebUI.DomainSearchControl" %>
<style>
    DIV
    {
        font-family: arial, helvetica, sanserif;
    }
    TABLE
    {
        font-family: arial, helvetica, sanserif;
    }
    TD
    {
        font-family: arial, helvetica, sanserif;
    }
    .inp_iphone
    {
        -webkit-appearance: none;
    }
    #domain_search_maindiv
    {
        position: relative;
        width: 1000px;
        background: url(images/63675_header.jpg) no-repeat;
        height: 254px;
    }
    #domain_search_input
    {
        border-bottom: 0px;
        border-left: 0px;
        background-color: transparent;
        outline-style: none;
        outline-color: invert;
        outline-width: medium;
        width: 330px;
        height: 28px;
        font-size: 20px;
        border-top: 0px;
        border-right: 0px;
    }
    #domain_search_button
    {
        position: relative;
        background-color: transparent;
        width: 64px;
        height: 42px;
        cursor: pointer;
    }
    #domain_search_tld
    {
        border-bottom: 0px;
        border-left: 0px;
        background-color: transparent;
        outline-style: none;
        outline-color: invert;
        padding-left: 3px;
        outline-width: medium;
        width: 63px;
        height: 18px;
        font-size: 16px;
        border-top: 0px;
        border-right: 0px;
    }
    .tld_search_sprite
    {
        background-image: url(images/51497_searchsprite2.png);
        background-repeat: no-repeat;
    }
    .tldDiv
    {
        position: absolute;
        width: 115px;
        display: none;
        height: 150px;
        top: 81px;
        left: 368px;
    }
    .tld_search_bg
    {
        background-image: url(http://img1.wsimg.com/fos/btn/1/47597_img_search_dropdn.png);
        background-repeat: no-repeat;
        height: 150px;
        _background-image: none;
    }
    .tld_search_inputdiv
    {
        width: 340px;
        background-position: 0px 0px;
        height: 42px;
    }
    .tld_search_tlddiv
    {
        width: 80px;
        background-position: -335px 0px;
        height: 42px;
    }
    .tld_search_buttondiv
    {
        background-position: 0px -104px;
    }
    .tld_arrow_div
    {
        background-color: transparent;
        width: 26px;
        float: left;
        height: 42px;
        font-size: 1px;
        cursor: pointer;
    }
    .tld_arrow_div_up
    {
        background-position: -96px -52px;
    }
    .tld_arrow_div_down
    {
        background-position: -60px -52px;
    }
    .tld_search_separatordiv
    {
        width: 10px;
        background-repeat: no-repeat;
        background-position: -40px -52px;
        height: 42px;
    }
</style>

<script type="text/javascript" src="JavaScript/jquery-1.6.1.min.js"></script>

<div class="domain-search-box">
    <div style="padding-bottom: 0px; background-color: #fff; margin: 0px auto; width: 100%;
        clear: both; padding-top: 0px" id="main" align="center">
        <table border="0" cellspacing="0" cellpadding="0" width="1000">
            <tbody>
                <tr>
                    <td style="text-align: left; width: 1000px; vertical-align: top">
                        <div id="search_header">
                            <div id="domain_search_maindiv">
                                <table border="0" cellspacing="0" cellpadding="0" width="1000">
                                    <tbody>
                                        <tr style="height: 82px" valign="bottom">
                                            <td width="555">
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tbody>
                                                        <tr valign="bottom">
                                                            <td>
                                                                <table border="0" cellspacing="0" cellpadding="0">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="padding-bottom: 0px; padding-left: 25px; width: 340px; padding-right: 0px;
                                                                                padding-top: 0px">
                                                                                <div class="tld_search_sprite tld_search_inputdiv">
                                                                                    <div style="padding-left: 7px; padding-top: 7px">
                                                                                        <input id="domain_search_input" class="inp_iphone" onkeyup="submitForSearch(event);"
                                                                                            maxlength="63" value="" />
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 10px">
                                                                                <div class="tld_search_sprite tld_search_separatordiv">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="tld_search_sprite tld_search_tlddiv">
                                                                                    <div style="padding-left: 3px; padding-top: 11px">
                                                                                        <input onblur="hidetldlist();" style="width: 63px" onkeydown="checkforscroll(this,event);"
                                                                                            id="domain_search_tld" class="inp_iphone" onfocus="showTLDs();" onkeyup="tldfilter(this, event);"
                                                                                            maxlength="8" value=".com" />
                                                                                    </div>
                                                                                </div>
                                                                                <!-- dropdown -->
                                                                                <div id="tldDiv" class="tldDiv">
                                                                                    <div style="height: 150px" id="tldBackground" class="tld_search_bg">
                                                                                        <div style="position: absolute; padding-bottom: 0px; overflow: auto; padding-left: 11px;
                                                                                            width: 95px; padding-right: 0px; max-height: 135px; overflow: hidden; padding-top: 5px;
                                                                                            height: 125px" id="inside1">
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com', this)">
                                                                                                .com
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".co" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.co', this)">
                                                                                                .co
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".info" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.info', this)">
                                                                                                .info
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net', this)">
                                                                                                .net
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org', this)">
                                                                                                .org
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".me" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.me', this)">
                                                                                                .me
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".mobi" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.mobi', this)">
                                                                                                .mobi
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".us" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.us', this)">
                                                                                                .us
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".biz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.biz', this)">
                                                                                                .biz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".ca" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.ca', this)">
                                                                                                .ca
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".mx" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.mx', this)">
                                                                                                .mx
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".tv" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.tv', this)">
                                                                                                .tv
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".ws" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.ws', this)">
                                                                                                .ws
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".ag" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.ag', this)">
                                                                                                .ag
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.ag" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.ag', this)">
                                                                                                .com.ag
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.ag" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.ag', this)">
                                                                                                .net.ag
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org.ag" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org.ag', this)">
                                                                                                .org.ag
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".am" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.am', this)">
                                                                                                .am
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".asia" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.asia', this)">
                                                                                                .asia
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".at" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.at', this)">
                                                                                                .at
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".be" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.be', this)">
                                                                                                .be
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.br" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.br', this)">
                                                                                                .com.br
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.br" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.br', this)">
                                                                                                .net.br
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".bz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.bz', this)">
                                                                                                .bz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.bz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.bz', this)">
                                                                                                .com.bz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.bz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.bz', this)">
                                                                                                .net.bz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".cc" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.cc', this)">
                                                                                                .cc
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.co" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.co', this)">
                                                                                                .com.co
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.co" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.co', this)">
                                                                                                .net.co
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".nom.co" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.nom.co', this)">
                                                                                                .nom.co
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".de" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.de', this)">
                                                                                                .de
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".es" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.es', this)">
                                                                                                .es
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.es" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.es', this)">
                                                                                                .com.es
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".nom.es" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.nom.es', this)">
                                                                                                .nom.es
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org.es" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org.es', this)">
                                                                                                .org.es
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".eu" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.eu', this)">
                                                                                                .eu
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".fm" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.fm', this)">
                                                                                                .fm
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".fr" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.fr', this)">
                                                                                                .fr
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".gs" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.gs', this)">
                                                                                                .gs
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.in', this)">
                                                                                                .in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".co.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.co.in', this)">
                                                                                                .co.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".firm.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.firm.in', this)">
                                                                                                .firm.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".gen.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.gen.in', this)">
                                                                                                .gen.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".ind.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.ind.in', this)">
                                                                                                .ind.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.in', this)">
                                                                                                .net.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org.in" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org.in', this)">
                                                                                                .org.in
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".it" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.it', this)">
                                                                                                .it
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".jobs" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.jobs', this)">
                                                                                                .jobs
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".jp" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.jp', this)">
                                                                                                .jp
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".ms" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.ms', this)">
                                                                                                .ms
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.mx" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.mx', this)">
                                                                                                .com.mx
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".nl" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.nl', this)">
                                                                                                .nl
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".nu" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.nu', this)">
                                                                                                .nu
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".co.nz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.co.nz', this)">
                                                                                                .co.nz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".net.nz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.net.nz', this)">
                                                                                                .net.nz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org.nz" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org.nz', this)">
                                                                                                .org.nz
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".se" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.se', this)">
                                                                                                .se
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".tc" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.tc', this)">
                                                                                                .tc
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".tk" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.tk', this)">
                                                                                                .tk
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".tw" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.tw', this)">
                                                                                                .tw
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".com.tw" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.com.tw', this)">
                                                                                                .com.tw
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".idv.tw" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.idv.tw', this)">
                                                                                                .idv.tw
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".co.uk" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.co.uk', this)">
                                                                                                .co.uk
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".me.uk" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.me.uk', this)">
                                                                                                .me.uk
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".org.uk" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.org.uk', this)">
                                                                                                .org.uk
                                                                                            </div>
                                                                                            <div style="padding-left: 2px; font-size: 15px; cursor: pointer" class="availTLD tldShown"
                                                                                                onmouseover="$(this).css('background-color','#e4efc7');" title=".vg" onmouseout="$(this).css('background-color','transparent');"
                                                                                                onclick="changeTLD('.vg', this)">
                                                                                                .vg
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <div id="arrow_container" class="tld_search_sprite tld_arrow_div tld_arrow_div_down"
                                                                    onclick="showTLDs();">
                                                                    &nbsp;
                                                                </div>
                                                            </td>
                                                            <td style="width: 72px" align="right">
                                                                <div id="domain_search_button" class="tld_search_sprite tld_search_buttondiv" onclick="validateTLD(); validateAndSearch();">
                                                                    &nbsp; &nbsp;
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    <script>
    
    function showTLDs() {
            if ($("#tldDiv").is(':hidden')) {
                $('#tldDiv').show();
                $("#arrow_container").removeClass("tld_arrow_div_down").addClass("tld_arrow_div_up");
                if ($.browser.version < 7) {
                    $("#inside1").css("height", "135px");
                    $("#tldBackground").css('z-index', 1);
                }
            }
            else {
                hidetldlist();
            }
        }
 
        function changeTLD(tld, elem) {
            $("#domain_search_tld").val(tld);
            hidetldlist();
        }
 
        function tldfilter(field, evt) {
            if ($("#tldDiv").is(':hidden')) { showTLDs(); }
 
            if ($("#domain_search_tld").val() != ".com") {
                var clean = $("#domain_search_tld").val();
                $("#domain_search_tld").val(clean.replace(/[^A-Za-z\.]/g, ''));
            }
            var keyCode = getKeyCode(evt)
            if ((keyCode == 40) || (keyCode == 38)) { } //do nothing
            else {
                var entered = $("#domain_search_tld").val();
                if (!initialTLD) {
                    $(".availTLD").each(function () {
                        if ($(this).html().indexOf(entered) >= 0)
                        { $(this).removeClass("tldhidden"); $(this).addClass("tldShown"); $(this).show(); }
                        else
                        { $(this).removeClass("tldShown"); $(this).addClass("tldhidden"); $(this).hide(); }
                    });
                }
                else {
                    initialTLD = false;
                }
            }
        }
 
        function checkforscroll(field, evt) {
            var keyCode = getKeyCode(evt)
            var highltd = $(".highlighted");
 
            if (highltd.length == 0) { $("div.tldShown").eq(0).addClass("highlighted"); }
            if (keyCode == 40 || keyCode == 38) {
                if (keyCode == 40) {
                    $("div.highlighted").removeClass("highlighted").nextAll(":visible").eq(0).addClass("highlighted");
                }
                if (keyCode == 38) {
                    $("div.highlighted").removeClass("highlighted").prevAll(":visible").eq(0).addClass("highlighted");
                }
                $("#domain_search_tld").val(($(".highlighted").html() == null) ? ".com" : $(".highlighted").html())
            } else if (keyCode == 13) {
                $(highltd).removeClass("highlighted");
                hidetldlist();
                $("#domain_search_input").select().focus();
            }
        }
 
        function hidetldlist() {
            $("#tldDiv").hide();
            $("#arrow_container").removeClass("tld_arrow_div_up").addClass("tld_arrow_div_down");
        }
 
        function validateTLD() {
            var entered = $("#selectedtld").val();
            var valid = false;
            $(".availtlds").each(function () { if ($(this).html() == entered) { valid = true; } });
            if (!valid) { $("#selectedtld").val(".com"); entered = ""; }
        }
 
        /******* END TLD SCRIPT *******/
 
 
 
        function validateAndSearch() {
            var domainToCheck = $('#domain_search_input').val().replace(/ /g, '');
            var tld = $('#domain_search_tld').val().replace('.', '');
 
            if (domainToCheck.indexOf('xn--') >= 0) {
              location.href = 'http://www.godaddy.com/domains/searchidn.aspx?ci=24121&isc=gssnin11&domainToCheck=' + escape(domainToCheck) + '&tld=' + escape(tld);
              return false;
            }
 
            var regExInvalidChars = /[^a-zA-Z0-9-\s.]+/g;
            var isInvalid = regExInvalidChars.test(domainToCheck);
            if (isInvalid) {
              domainToCheck = domainToCheck.replace(regExInvalidChars, '');
              $('#cleaned').val('true');
            }
 
            if (domainToCheck.length && tld.length) {
                if (ValidateDomainNames(ShowErrorMessage, domainToCheck)) {
                    $('#domainToCheck').val(domainToCheck);
                    $('#tld').val(tld);
                    $('#domain_search_form').submit();
                }
            }
        }
 
        function ValidateDomainNames(onFailed, domainToCheck) {
 
            if (domainToCheck.length == 0) {
                onFailed("Enter a domain name to search.");
                return false;
            }
            return true;
        }
 

    </script>
