<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainSearchControl.ascx.cs"
    Inherits="ResellerClub.WebUI.DomainSearchControl" %>
<style>
    .domain-search-box
    {
        padding-bottom: 8px;
        padding-left: 5px;
        padding-right: 5px;
        background: url(images/input-form.gif) no-repeat -2px -7px;
        height: 54px;
        padding-top: 8px;
        width: 524px;
    }
    .domains-input
    {
        border-bottom: medium none;
        border-left: medium none;
        padding-bottom: 14px;
        padding-left: 16px;
        width: 330px;
        padding-right: 0px;
        background: url(images/input-form.gif) #ffffff no-repeat -3px -82px;
        color: #222222;
        font-size: 22px;
        border-top: medium none;
        border-right: medium none;
        padding-top: 14px;
    }
    .ie-fix
    {
        position: relative;
        margin-top: 13px;
        width: 90px;
        height: 28px;
        overflow: hidden;
    }
    .domain-submit
    {
        position: relative;
        left: 98px;
        border-bottom: medium none;
        border-left: medium none;
        margin-top: 6px;
        width: 65px;
        background: url(images/input-form.gif) no-repeat -3px -142px;
        height: 54px;
        color: #ffffff;
        font-size: 19px;
        border-top: medium none;
        cursor: pointer;
        font-weight: bold;
        border-right: medium none;
    }
    .domains-select
    {
        font-size: 18px;
        position: absolute;
        top: 153px;
        left: 380px;
        z-index: 2;
        display: none;
    }
    .domain-select-wrapper
    {
        padding-bottom: 0px;
        padding-left: 8px;
        padding-right: 5px;
        margin-top: -6px;
    }
    .tldDisplay
    {
        position: relative;
        top: -68px;
        left: 356px;
        border-bottom: medium none;
        border-left: #cccccc 1px solid;
        margin-top: 6px;
        width: 100px;
        background: url(images/input-form.gif) no-repeat -3px -82px;
        height: 54px;
        color: #000000;
        font-size: 19px;
        border-top: medium none;
        cursor: pointer;
        border-right: medium none;
        padding-left: 5px;
        padding-right: 5px;
        padding-top: 0px;
        vertical-align: middle;
        z-index: 0;
    }
    .tldDisplay12
    {
        border-bottom: medium none;
        border-left: medium none;
        border-top: medium none;
        border-right: medium none;
        font-size: 20px;
        background: url(images/input-form.gif) #ffffff no-repeat;
        background-position: -3px -82px;
        width: 50px;
        height: 50px;
        padding-top: 14px;
    }
    .inputwithdefulttextcolor
    {
        color: #878787;
    }
</style>
<div class="domain-search-box">
    <div class="domain-select-wrapper">
        <asp:TextBox ID="txtDomin" MaxLength="65" runat="server" CssClass="domains-input"
            Text="Enter Your Domain Name" Width="327px"></asp:TextBox>
        <asp:Button ID="btnGo" class="domain-submit" runat="server" OnClientClick="return btnGoClientClick(this)"
            OnClick="btnGo_Click" Text="" />
        <!--img id="imgLoadoing" src="images/ajax_busy.gif" style="position:absolute"/-->
    </div>
</div>
<div class="tldDisplay" id="divTdl">
    <table width="100%" style="height: 100%">
        <tr>
            <td id="tdTld" style="vertical-align: middle">
                .com
            </td>
            <td style="vertical-align: middle">
                <img style="float: right" src="images/icon-cust-dd.gif" />
            </td>
        </tr>
    </table>
</div>
<div id="divDomainSelect" class="domains-select">
    <asp:DropDownList ID="ddlTld" runat="server" Font-Size="25px">
    </asp:DropDownList>
</div>

<script>
 //$(this).css({'color':$(this).val()});

      $(document).ready(function() {

          $("#divTdl").bind('click', function(e) {
              DisplayTldDropDown();
              return false;
          });

          $("[id$=ddlTld]").change(function() {
              OnTldChange();
          });
          
          $("[id$=txtDomin]").focus(function() {
              ResetLabel(this, 'Enter Your Domain Name', '');
          });
          
          $("[id$=txtDomin]").blur(function() {
              ResetLabel(this, 'Enter Your Domain Name', '');
          });
        
     
          $("[id$=txtDomin]").addClass("inputwithdefulttextcolor");
     
     
          var left1  = $("[id$=divTdl]").offset().left;
          $('#divDomainSelect').css("left",left1);
          
      })

      function DisplayTldDropDown() {
          var len1 = $("[id$=ddlTld] option").length;
          $("[id$=ddlTld]").attr("size", len1-1);
          $('#divDomainSelect').toggle();
      }

      function OnTldChange() {
         
          $('#divDomainSelect').toggle();
          $('#tdTld').html($("[id$=ddlTld]").val());
       
      }
      
      function btnGoClientClick(obj){
              if($("[id$=txtDomin]").val() == "Enter Your Domain Name"){
              return false;
              }
      }
            
      function ShowLoading(obj)
      {
      return;
      $("[id$=btnGo]").val('');
        $(document).ready(function() {
     for (i=0;i<5;i++) {
       setTimeout('addDot()',i*200);
     }
   });
         
   
     return true; 
      }
      
      function addDot() {
      $("[id$=btnGo]").val($("[id$=btnGo]").val() + '.')
      if($("[id$=btnGo]").val() == "....."){
    ShowLoading();
      }
      
	 }
      
</script>

