<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllFeature.ascx.cs"
    Inherits="ResellerClub.WebUI.UserControl.AllFeature" %>
<style>
    .all-feature-tbl
    {
    }
    .all-feature-tbl TD
    {
        vertical-align: top;
        border: solid 0px;
        border-right: solid 0px;
    }
    .all-feature-tbl TD DIV
    {
        font-weight: bold;
    }
    .all-feature-tbl TD UL LI
    {
        background: url(../images/gray-right.gif) no-repeat left center;
        list-style: none;
        padding-top: 5px;
        padding-bottom: 5px;
        color: #5b5b5b;
        padding-left: 20px;
    }
</style>

<script>

function ShowTab(id,classN,obj){
document.getElementById("trFeatures").style.display="none";
document.getElementById("trFaq").style.display="none";
document.getElementById(id).style.display="";
document.getElementById("tabs").className=classN;
obj.className = "fea-active";
}

</script>

<div style="padding-top: 40px">
    <div class="switch-nav" style="padding-top: 20px;">
        <div id="tabs" class="tab-wrp tab1">
            <div id="felist" class="first">
                <a class="fea-active" onclick="ShowTab('trFeatures','tab-wrp tab1',this)">Features</a>
            </div>
            <div id="faqlist" class="last">
                <a class="" onclick="ShowTab('trFaq','tab-wrp tab2', this)">FAQs</a>
            </div>
        </div>
    </div>
    <table width="100%" cellpadding="0" cellpadding="0">
        <tr id="trFeatures">
            <td>
                <%=GetAllFeature()%>
            </td>
        </tr>
        <tr id="trFaq" style="display: none">
            <td colspan="2">
                <%=GetFaq()%>
            </td>
        </tr>
    </table>
</div>
