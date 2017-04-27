<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOption.ascx.cs"
    Inherits="ResellerClub.WebUI.UserControl.PaymentOption" %>
<table border="0">
    <tr>
        <td>
            <div>
                <h3 style="font-size: 18px; color: #707070">
                    Online Payment Options
                </h3>
                <p>
                    Please select from the following options to complete your purchase:
                </p>
                <ul style="list-style: none">
                    <li style="padding-top: 15px;">
                        <input type="radio" id="rdoImps" value="imps" name="rdoOnline" />
                        <label style="font-size: 14px;padding-bottom: 10px; padding-top: 10px; background: url(../images/mobile-icon.png)  no-repeat;
                            padding-left: 50px" onclick="SelectRdo('rdoImps')">
                            Pay using <span title="Interbank Mobile Payment Service" style="font-weight:bold">
                                IMPS</span> across India</label>
                    </li>
                    <li style="padding-top: 30px;">
                        <input type="radio" id="rdoNetBanking" value="netbanking" name="rdoOnline" />
                        <label onclick="SelectRdo('rdoNetBanking')" style="font-size: 14px; padding-bottom: 10px; padding-top: 10px; background: url(../images/mouse.jpg)  no-repeat;
                            padding-left: 50px">
                            Pay using <span title="Net-banking" style="font-weight:bold">Net-banking</span>
                            across India</label>
                    </li>
                      <li style="list-style: none; padding-top: 30px;display:none">
                        <input type="radio" id="rdoPaypal" value="paypal" name="rdoOnline" />
                        <label onclick="SelectRdo('rdoPaypal')" style="font-size: 14px; padding-left: 70px;background: url(../images/paypal-icon.png)  no-repeat;">
                            Pay using <span style="font-weight:bold">Paypal</span> </label>
                    </li>
                  
                </ul>
                <br />
                <button id="payonline_submit" class="ui-button" value="payonline" name="payonline_submit"
                    type="submit" onclick="return OnPayOnlineClick(this)">
                    <span><span>Pay Online</span></span></button>
            </div>
        </td>
        <td>
            <div style="height: 80px; border-right: dotted 1px; width: 7px">
            </div>
            <br />
            <span style="padding-top: 10px; padding-bottom: 10px; background-color: White">OR</span>
            <br>
            <div style="height: 80px; border-right: dotted 1px; width: 7px">
            </div>
        </td>
        <td style="vertical-align: top">
            <div style="padding-left: 20px;">
                <h3 style="font-size: 18px; color: #707070">
                    Offline Payment Options
                </h3>
                <p>
                    Add your order now and pay later using cheque/cash.
                </p>
                <p style="padding-top: 30px;">
                    <button id="payoffline_submit" class="ui-button" value="payoffline" name="payoffline_submit"
                        type="submit" onclick="return OnPayOfflineClick(this)">
                        <span><span>Pay Offline</span></span></button>
                </p>
            </div>
        </td>
    </tr>
</table>

<script>

function OnPayOnlineClick(btn){
    var val = $('input:radio[name=rdoOnline]:checked').val();
    return OnPayOnlineClickCallBack(val,btn);
}

function OnPayOfflineClick(btn){
    return OnPayOnlineClickCallBack("offline",btn);
}

function SelectRdo(rdo){
$("#"+rdo).attr('checked', true);
}
</script>

