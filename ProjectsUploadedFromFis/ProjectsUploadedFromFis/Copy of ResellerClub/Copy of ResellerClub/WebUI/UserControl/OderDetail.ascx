<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OderDetail.ascx.cs"
    Inherits="ResellerClub.WebUI.OderDetail" %>
<style>
    H3
    {
        padding-bottom: 0px;
        margin: 0px 0px 1px;
        padding-left: 0px;
        padding-right: 0px;
        padding-top: 0px;
        font-size: 14px;
    }
</style>
<div style="width: 100%; margin-bottom: 15px; padding-top: 20px; padding-bottom: 10px;
    font-weight: bold; font-size: 16px; background: url(/images/bg-gradient-long.gif);
    color: #b90913">
    Order Summary
</div>
<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
    <HeaderTemplate>
        <table border="0" width="682px">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td width="80%" style="vertical-align: top">
                <h3 style="color: #9d9d9d">
                    <asp:Label ID="lblItem" Text="" runat="server"></asp:Label>
                </h3>
                <div id="divSeprator" style="height: 5px;">
                </div>
                <a class="remove_img" style="vertical-align: middle; padding-left: 25px;" onclick="RemoveItem(this)"
                    name="<%#GetSubPlanIdAndDomain(Container.DataItem)%>" href="###"><span style="color: #00468e;
                        font-weight: normal">Remove</span></a> <span class="loading_new_img" style="height: 18px; width: 18px; display:none;
                            ">&nbsp;</span>
            </td>
            <td style="vertical-align: top">
                <asp:DropDownList ID="ddlPlan" runat="server">
                </asp:DropDownList>
            </td>
            <td style="vertical-align: top;" align="right">
                <asp:Label ID="lblPrice" Text="ghhnm.com" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="padding-top: 10px; padding-bottom: 12px;">
                <div style="height: 1px; width: 100%; border-bottom: #d7d7d7 1px solid;">
                    &nbsp;</div>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
<table width="682px">
    <tr>
        <td width="70%">
            &nbsp;
        </td>
        <td width="30%" align="right">
            <table width="100%">
                <tr>
                    <td align="right">
                        Sub Total:
                    </td>
                    <td align="right">
                        <span id="spnSamt">Rs 1234</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Tax(<%=ConfigurationManager.AppSettings["ServiceTax"]%>%):
                    </td>
                    <td align="right">
                        <span id="spnTax">Rs 1234</span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Total Amount:
                    </td>
                    <td align="right">
                        <span id="spnTamt"></span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script>
var OnItemDeleteCallBack;

    $(document).ready(function() {
        //$('#dialog').dialog();
        $('#dialog_link').click(function() {
            $('#dialog').dialog();
            return false;
        });


        $("select[id*=ddlPlan]").each(function() {
            $(this).change(function() {
              GetTotalAmount();
            });
          }
       );

    GetTotalAmount();
    });


    var itemRemoved = null;
    var currencySymbol = "";
    function RemoveItem(obj) {
        $(obj).next().css('display','inline-block');
        $(obj).toggle();
        itemRemoved = obj;
        
        
    $.ajax({
        type: "GET",
        url: "<%=Application["rootPath"]%>/HttpHandlers/AjaxHandler.ashx",
        data: 'RemoveItem=' + $(obj).attr('name'),
        success: function(msg) {
            RemoveRow(msg);
            
        }
    })
}

function RemoveRow(msg) {
    $(itemRemoved).parent().parent().next().hide();
    $(itemRemoved).parent().parent().hide();
    GetTotalAmount();
    DecreseCartItemCount();
}    

   function  ShowAllFreeItem(obj){
       var p = $(obj).parent().parent();
       $(p).children(".hidden").toggle();
       $(p).children(".nothidden").toggle();
    }
    
    
     function GetTotalAmount(){
        var p = null;
        var ddl = null;
        var amount =0;
        var plan;
        var tax = 0;
        var taxPer =<%=ConfigurationManager.AppSettings["ServiceTax"]%>;
        
        $("select[id*=ddlPlan]").each(function() {
            //amount += parseInt($(this).val().split('_')[1]);
           if( $(this).is(":visible"))
           { 
            plan = GetPlan($(this).val());
            currencySymbol = plan.CurrencySymbol;
            amount +=plan.Price;
            var par = $(this).parent().next();
            par.children(':first-child').html(plan.CurrencySymbol + plan.Price);
            }
        }

       );
        
        $("#spnSamt").html(currencySymbol +' ' + amount.toFixed(2)); 
        tax = GetPercentage(amount,taxPer);
        
        amount += tax;
        $("#spnTax").html(currencySymbol +' '+ tax.toFixed(2)); 
        $("#spnTamt").html(currencySymbol +' ' + amount.toFixed(2)); 
        
        try{
            OnItemDeleteCallBack(amount);
        }
        catch(ex){}
    }
    
    
    function GetPercentage(num, percentage){
        return (num*percentage/100);
    }
</script>

