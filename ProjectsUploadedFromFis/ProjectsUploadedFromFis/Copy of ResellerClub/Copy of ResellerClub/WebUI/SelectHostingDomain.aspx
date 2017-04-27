<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true" CodeBehind="SelectHostingDomain.aspx.cs"
    Inherits="ResellerClub.WebUI.SelectHostingDomain" Title="Select Hosting Domain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            height: 35px;
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
        #domainoption H3
        {
            padding-bottom: 3px;
            line-height: 33px;
            margin: 6px 0px 10px;
            padding-left: 0px;
            padding-right: 0px;
            height: 25px;
            width: 200px;
            color: #0e4e5a;
            font-size: 14px;
            font-weight: bold;
            padding-top: 0px;
        }
        .input
        {
            border-bottom: #d5d5d5 1px solid;
            border-left: #d5d5d5 1px solid;
            padding-bottom: 4px;
            padding-left: 4px;
            padding-right: 4px;
            font-size: 16px;
            border-top: #d5d5d5 1px solid;
            margin-right: 5px;
            border-right: #d5d5d5 1px solid;
            padding-top: 4px;
        }
    </style>

    <script>
   
      $(document).ready(function() {
        $("select[id*=ddlExistingDomain]").change(function(obj) {
            if($(this)[0].value == "Other"){
                $("input[id*=txtExistingDomain]").show();
             }
             else{
                $("input[id*=txtExistingDomain]").hide();
             }
          });
        })



    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="domainoption" width="100%" cellspacing="0" style="height: 220px; background: url(images/back_content_bottom.png) #ffffff left bottom no-repeat;
        border: #e7e4da 1px solid;">
        <tr>
            <td colspan="2" style="background: url(images/headerbg.gif) repeat-x; height: 30px;
                font-weight: bold; padding-left: 10px;">
                Select Your <span style="color: #a50910">Domain Name</span>
            </td>
        </tr>
        <tr>
            <td width="300px">
                <div style="border-right: solid 1px #d7d7d7; height: 120px; padding-top: 20px; padding-left: 20px;">
                    <h3>
                        I have existing domain</h3>
                    <div style="width: 470px">
                        <asp:DropDownList ID="ddlExistingDomain" Width="350px" CssClass="input" Visible="false"
                            runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtExistingDomain" CssClass="input" Width="200px" runat="server"></asp:TextBox>
                        <asp:Button ID="btnContinue" runat="server" Text="Continue>>" OnClick="ContinueClicked" />
                    </div>
                </div>
            </td>
            <td width="600px;">
                <div style="height: 120px; padding-top: 20px; padding-left: 20px;">
                    <h3>
                        Search New Domain
                    </h3>
                    <asp:TextBox ID="txtDomainName" CssClass="input" Width="200px" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlTld" CssClass="input" runat="server">
                        <asp:ListItem>.com</asp:ListItem>
                        <asp:ListItem>.in</asp:ListItem>
                        <asp:ListItem>.org</asp:ListItem>
                        <asp:ListItem>.uk</asp:ListItem>
                        <asp:ListItem>.com</asp:ListItem>
                        <asp:ListItem>.in</asp:ListItem>
                        <asp:ListItem>.org</asp:ListItem>
                        <asp:ListItem>.uk</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search >>" OnClick="SearchClicked" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
