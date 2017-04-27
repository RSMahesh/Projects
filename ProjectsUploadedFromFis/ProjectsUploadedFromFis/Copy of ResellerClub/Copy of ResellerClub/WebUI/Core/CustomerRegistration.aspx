<%@ Page Language="C#" MasterPageFile="~/MasterPage/Main.Master" AutoEventWireup="true"
    CodeBehind="CustomerRegistration.aspx.cs" Inherits="ResellerClub.WebUI.CustemRegistration"
    Title="Customer Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body
        {
            font-family: Verdana;
            font-size: 10px;
        }
        INPUT.textbox
        {
            border-bottom: #dddddd 1px solid;
            border-left: #c3c3c3 1px solid;
            padding-bottom: 4px;
            padding-left: 4px;
            width: 296px;
            padding-right: 4px;
            color: #444;
            border-top: #aaa 1px solid;
            border-right: #c3c3c3 1px solid;
            padding-top: 4px;
        }
        LABEL
        {
            margin: 0px 8px 4px 0px;
            display: block;
            color: #444;
            font-size: 12px;
            font-weight: bold;
        }
        TABLE.frmTable TR TD
        {
            padding-bottom: 12px;
            padding-left: 0px;
            padding-right: 0px;
            clear: both;
            vertical-align: top;
            padding-top: 12px;
        }
        .style2
        {
            width: 44px;
        }
        .formError
        {
            border-bottom: #ff8080 1px solid;
            background: #ffe1e1;
            color: #333333;
            border-top: #ff8080 1px solid;
        }
        TABLE.frmTable
        {
            margin: 10px;
            width: 90%;
            border: #e7e4da 1px solid;
            background: url(../images/back_content_top.png) #ffffff no-repeat;
        }
        .frmHint
        {
            padding-bottom: 0px;
            margin: 5px 0px 0px;
            padding-left: 0px;
            padding-right: 0px;
            color: #999;
            padding-top: 0px;
        }
        .aboveField
        {
            margin: -18px 27px 0px 5px;
            float: right;
        }
        .asterix
        {
            color: #f00;
        }
    </style>

    <script>
    
   var selectedState = '<%=Request[stateSelect.UniqueID]%>';
   var otherState = '<%=Request[input_otherState.UniqueID]%>';
    var stateSelect;
    $(document).ready(function() {
    OnCountryChange($("select[id*=country]").val());
    OnStateChange(selectedState);
    });
    
    
    function OnCountryChange(countryCode) {
    
    stateSelect = $('#<%=stateSelect.ClientID%>');
    
    $(stateSelect).empty();
     $(stateSelect).append('<option value=Loding>Loding State..Please Wait..</option>');


    $.ajax({
        type: "GET",
        url: "<%=Application["rootPath"]%>/HttpHandlers/AjaxHandler.ashx",
        data: 'GetCountryState='+countryCode,
        success: function(msg) {
            UpdateState(msg);
            
        }
    })
}

function UpdateState(msg){
    var stateList = eval(msg);
    $(stateSelect).empty();
    $(stateSelect).append('<option value=SelectState>Select State</option>');

    for (var i = 0, len = stateList.length; i < len; ++i) {
        $(stateSelect).append('<option value="'+stateList[i]+'">'+stateList[i]+'</option>');
    }
       $(stateSelect).append('<option value=other>Other</option>');
       
       if(selectedState !=""){
       $(stateSelect).val(selectedState);
       selectedState ="";
       }
}

function OnStateChange(state){
    var txtOther = $("input[id*=input_otherState]");
    if(state == "other"){
        $(txtOther).show();
        $(txtOther).val(otherState);
        otherState="";
    }
    else{
        $(txtOther).hide();
    }
}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="frmTable" border="0" style="padding-left:5px;width:800px;margin-left:5%" >
        <tr>
            <td colspan="3" style="padding-top: 5px; padding-bottom: 5px;">
                <span style="color: #a50910; font-size: 15px;">Create an Account</span>
            </td>
            <td align="right" style="padding: 5px;">
                * All fields are required
            </td>
        </tr>
        <tr>
            <td colspan="4" style="padding: 0px">
                <div style="border-top: #e7e4da 1px solid; padding-top: 10px; width: 98%">
                    Please fill out the form below to create an account and proceed to checkout
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="divError" runat="server" class="formError">
                    <div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Name<span class="asterix">*</span></label><input id="input_fullname" class="textbox"
                        size="35" name="name" value="" runat="server" tabindex="1" />
            </td>
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Address<span class="asterix">*</span></label><input id="input_address1" class="textbox"
                        size="35" name="name" value="" runat="server" tabindex="5" />
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Company Name</label><input id="input_companyname" class="textbox" 
                    size="35" name="name"
                        value="" runat="server" tabindex="2" />
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Country<span class="asterix">*</span></label>
                <select id="country" class="required" 
                    style="padding-top: 3px; padding-bottom: 3px; padding-right: 5px;width:305px;" 
                    onchange="OnCountryChange(this.value)" name="country"
                    runat="server" tabindex="6">
                    <option value="null">Select a Country</option>
                    <option value="AX">Aland Islands</option>
                    <option value="AF">Afghanistan</option>
                    <option value="AL">Albania</option>
                    <option value="DZ">Algeria</option>
                    <option value="AS">American Samoa</option>
                    <option value="AD">Andorra</option>
                    <option value="AO">Angola</option>
                    <option value="AI">Anguilla</option>
                    <option value="AQ">Antarctica</option>
                    <option value="AG">Antigua And Barbuda</option>
                    <option value="AR">Argentina</option>
                    <option value="AM">Armenia</option>
                    <option value="AW">Aruba</option>
                    <option value="AU">Australia</option>
                    <option value="AT">Austria</option>
                    <option value="AZ">Azerbaijan</option>
                    <option value="BS">Bahamas</option>
                    <option value="BH">Bahrain</option>
                    <option value="BD">Bangladesh</option>
                    <option value="BB">Barbados</option>
                    <option value="BY">Belarus</option>
                    <option value="BE">Belgium</option>
                    <option value="BZ">Belize</option>
                    <option value="BJ">Benin</option>
                    <option value="BM">Bermuda</option>
                    <option value="BT">Bhutan</option>
                    <option value="BO">Bolivia</option>
                    <option value="BA">Bosnia and Herzegovina</option>
                    <option value="BW">Botswana</option>
                    <option value="BV">Bouvet Island</option>
                    <option value="BR">Brazil</option>
                    <option value="IO">British Indian Ocean Territory</option>
                    <option value="BN">Brunei</option>
                    <option value="BG">Bulgaria</option>
                    <option value="BF">Burkina Faso</option>
                    <option value="BI">Burundi</option>
                    <option value="KH">Cambodia</option>
                    <option value="CM">Cameroon</option>
                    <option value="CA">Canada</option>
                    <option value="CV">Cape Verde</option>
                    <option value="KY">Cayman Islands</option>
                    <option value="CF">Central African Republic</option>
                    <option value="TD">Chad</option>
                    <option value="CL">Chile</option>
                    <option value="CN">China</option>
                    <option value="CX">Christmas Island</option>
                    <option value="CC">Cocos (Keeling) Islands</option>
                    <option value="CO">Colombia</option>
                    <option value="KM">Comoros</option>
                    <option value="CG">Congo</option>
                    <option value="CD">Congo, Democractic Republic</option>
                    <option value="CK">Cook Islands</option>
                    <option value="CR">Costa Rica</option>
                    <option value="CI">Cote D'Ivoire (Ivory Coast)</option>
                    <option value="HR">Croatia (Hrvatska)</option>
                    <option value="CU">Cuba</option>
                    <option value="CY">Cyprus</option>
                    <option value="CZ">Czech Republic</option>
                    <option value="DK">Denmark</option>
                    <option value="DJ">Djibouti</option>
                    <option value="DM">Dominica</option>
                    <option value="DO">Dominican Republic</option>
                    <option value="TP">East Timor</option>
                    <option value="EC">Ecuador</option>
                    <option value="EG">Egypt</option>
                    <option value="SV">El Salvador</option>
                    <option value="GQ">Equatorial Guinea</option>
                    <option value="ER">Eritrea</option>
                    <option value="EE">Estonia</option>
                    <option value="ET">Ethiopia</option>
                    <option value="FK">Falkland Islands (Islas Malvinas)</option>
                    <option value="FO">Faroe Islands</option>
                    <option value="FJ">Fiji Islands</option>
                    <option value="FI">Finland</option>
                    <option value="FR">France</option>
                    <option value="FX">France, Metropolitan</option>
                    <option value="GF">French Guiana</option>
                    <option value="PF">French Polynesia</option>
                    <option value="TF">French Southern Territories</option>
                    <option value="GA">Gabon</option>
                    <option value="GM">Gambia, The</option>
                    <option value="GE">Georgia</option>
                    <option value="DE">Germany</option>
                    <option value="GH">Ghana</option>
                    <option value="GI">Gibraltar</option>
                    <option value="GR">Greece</option>
                    <option value="GL">Greenland</option>
                    <option value="GD">Grenada</option>
                    <option value="GP">Guadeloupe</option>
                    <option value="GU">Guam</option>
                    <option value="GT">Guatemala</option>
                    <option value="GG">Guernsey</option>
                    <option value="GN">Guinea</option>
                    <option value="GW">Guinea-Bissau</option>
                    <option value="GY">Guyana</option>
                    <option value="HT">Haiti</option>
                    <option value="HM">Heard and McDonald Islands</option>
                    <option value="HN">Honduras</option>
                    <option value="HK">Hong Kong S.A.R.</option>
                    <option value="HU">Hungary</option>
                    <option value="IS">Iceland</option>
                    <option value="IN" selected>India</option>
                    <option value="ID">Indonesia</option>
                    <option value="IR">Iran</option>
                    <option value="IQ">Iraq</option>
                    <option value="IE">Ireland</option>
                    <option value="IM">Isle of Man</option>
                    <option value="IL">Israel</option>
                    <option value="IT">Italy</option>
                    <option value="JM">Jamaica</option>
                    <option value="JP">Japan</option>
                    <option value="JE">Jersey</option>
                    <option value="JO">Jordan</option>
                    <option value="KZ">Kazakhstan</option>
                    <option value="KE">Kenya</option>
                    <option value="KI">Kiribati</option>
                    <option value="KR">Korea</option>
                    <option value="KP">Korea, North</option>
                    <option value="KW">Kuwait</option>
                    <option value="KG">Kyrgyzstan</option>
                    <option value="LA">Laos</option>
                    <option value="LV">Latvia</option>
                    <option value="LB">Lebanon</option>
                    <option value="LS">Lesotho</option>
                    <option value="LR">Liberia</option>
                    <option value="LY">Libya</option>
                    <option value="LI">Liechtenstein</option>
                    <option value="LT">Lithuania</option>
                    <option value="LU">Luxembourg</option>
                    <option value="MO">Macau S.A.R.</option>
                    <option value="MK">Macedonia</option>
                    <option value="MG">Madagascar</option>
                    <option value="MW">Malawi</option>
                    <option value="MY">Malaysia</option>
                    <option value="MV">Maldives</option>
                    <option value="ML">Mali</option>
                    <option value="MT">Malta</option>
                    <option value="MH">Marshall Islands</option>
                    <option value="MQ">Martinique</option>
                    <option value="MR">Mauritania</option>
                    <option value="MU">Mauritius</option>
                    <option value="YT">Mayotte</option>
                    <option value="MX">Mexico</option>
                    <option value="FM">Micronesia</option>
                    <option value="MD">Moldova</option>
                    <option value="MC">Monaco</option>
                    <option value="MN">Mongolia</option>
                    <option value="ME">Montenegro</option>
                    <option value="MS">Montserrat</option>
                    <option value="MA">Morocco</option>
                    <option value="MZ">Mozambique</option>
                    <option value="MM">Myanmar</option>
                    <option value="NA">Namibia</option>
                    <option value="NR">Nauru</option>
                    <option value="NP">Nepal</option>
                    <option value="NL">Netherlands</option>
                    <option value="AN">Netherlands Antilles</option>
                    <option value="NC">New Caledonia</option>
                    <option value="NZ">New Zealand</option>
                    <option value="NI">Nicaragua</option>
                    <option value="NE">Niger</option>
                    <option value="NG">Nigeria</option>
                    <option value="NU">Niue</option>
                    <option value="NF">Norfolk Island</option>
                    <option value="MP">Northern Mariana Islands</option>
                    <option value="NO">Norway</option>
                    <option value="OM">Oman</option>
                    <option value="PK">Pakistan</option>
                    <option value="PW">Palau</option>
                    <option value="PS">Palestinian Territory, Occupied</option>
                    <option value="PA">Panama</option>
                    <option value="PG">Papua new Guinea</option>
                    <option value="PY">Paraguay</option>
                    <option value="PE">Peru</option>
                    <option value="PH">Philippines</option>
                    <option value="PN">Pitcairn Island</option>
                    <option value="PL">Poland</option>
                    <option value="PT">Portugal</option>
                    <option value="PR">Puerto Rico</option>
                    <option value="QA">Qatar</option>
                    <option value="RE">Reunion</option>
                    <option value="RO">Romania</option>
                    <option value="RU">Russia</option>
                    <option value="RW">Rwanda</option>
                    <option value="SH">Saint Helena</option>
                    <option value="KN">Saint Kitts And Nevis</option>
                    <option value="LC">Saint Lucia</option>
                    <option value="PM">Saint Pierre and Miquelon</option>
                    <option value="VC">Saint Vincent And The Grenadines</option>
                    <option value="WS">Samoa</option>
                    <option value="SM">San Marino</option>
                    <option value="ST">Sao Tome and Principe</option>
                    <option value="SA">Saudi Arabia</option>
                    <option value="SN">Senegal</option>
                    <option value="RS">Serbia</option>
                    <option value="SC">Seychelles</option>
                    <option value="SL">Sierra Leone</option>
                    <option value="SG">Singapore</option>
                    <option value="SK">Slovakia</option>
                    <option value="SI">Slovenia</option>
                    <option value="SB">Solomon Islands</option>
                    <option value="SO">Somalia</option>
                    <option value="ZA">South Africa</option>
                    <option value="ES">Spain</option>
                    <option value="LK">Sri Lanka</option>
                    <option value="SD">Sudan</option>
                    <option value="SR">Suriname</option>
                    <option value="SJ">Svalbard And Jan Mayen Islands</option>
                    <option value="SZ">Swaziland</option>
                    <option value="SE">Sweden</option>
                    <option value="CH">Switzerland</option>
                    <option value="SY">Syria</option>
                    <option value="TW">Taiwan</option>
                    <option value="TJ">Tajikistan</option>
                    <option value="TZ">Tanzania</option>
                    <option value="TH">Thailand</option>
                    <option value="TL">Timor-Leste</option>
                    <option value="TG">Togo</option>
                    <option value="TK">Tokelau</option>
                    <option value="TO">Tonga</option>
                    <option value="TT">Trinidad And Tobago</option>
                    <option value="TN">Tunisia</option>
                    <option value="TR">Turkey</option>
                    <option value="TM">Turkmenistan</option>
                    <option value="TC">Turks And Caicos Islands</option>
                    <option value="TV">Tuvalu</option>
                    <option value="UG">Uganda</option>
                    <option value="UA">Ukraine</option>
                    <option value="AE">United Arab Emirates</option>
                    <option value="GB">United Kingdom</option>
                    <option value="US">United States</option>
                    <option value="UM">United States Minor Outlying Islands</option>
                    <option value="UY">Uruguay</option>
                    <option value="UZ">Uzbekistan</option>
                    <option value="VU">Vanuatu</option>
                    <option value="VA">Vatican City State (Holy See)</option>
                    <option value="VE">Venezuela</option>
                    <option value="VN">Vietnam</option>
                    <option value="VG">Virgin Islands (British)</option>
                    <option value="VI">Virgin Islands (US)</option>
                    <option value="WF">Wallis And Futuna Islands</option>
                    <option value="EH">WESTERN SAHARA</option>
                    <option value="YE">Yemen</option>
                    <option value="ZM">Zambia</option>
                    <option value="ZW">Zimbabwe</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Email<span class="asterix">*</span></label><span class="frmHint aboveField" style="padding-right:82px;">Will be
                        used as your username.</span><input id="username" class="textbox" 
                    size="35" name="name"
                            value="" runat="server" tabindex="3" />
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    State<span class="asterix">*</span></label>
                <select id="stateSelect" class="required" 
                    style="padding-top: 3px; padding-bottom: 3px; padding-right: 5px;width:305px;" 
                    onchange="OnStateChange(this.value);" name="state"
                    runat="server" tabindex="7">
                </select>
                <br />
                <input id="input_otherState" class="textbox" size="35" name="name" style="display: none"
                    value="" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Password<span class="asterix">*</span></label><input id="passwd" type="password"
                        class="textbox" size="35" name="name" value="" runat="server" 
                    tabindex="4" />
                <p class="frmHint">
                    8 character minimum. No special characters.</p>
            </td>
            <td style="width: 60px">
                &nbsp;
            </td>
            <td style="width: 180px">
                <label>
                    City<span class="asterix">*</span></label><input style="width: 150px" id="select_city"
                        class="textbox" size="35" name="name" value="" runat="server" 
                    tabindex="8" />
            </td>
            <td align="left" style="width: 190px">
                <label>
                    Zip<span class="asterix">*</span></label><input style="width: 114px" id="input_zip"
                        class="textbox" size="35" name="name" value="" runat="server" 
                    tabindex="9" />
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Confirm Password<span class="asterix">*</span></label><input 
                    type="password" id="conf_passwd"
                        class="textbox" size="35" name="name" value="" runat="server" 
                    tabindex="4" />
                <p class="frmHint">
                    8 character minimum. No special characters.</p>
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Phone<span class="asterix">*</span></label>
                <input id="input_phone_cc" style="width: 50px" class="textbox" size="3" name="name"
                    value="" runat="server" tabindex="11" /> -
                <input id="input_phone" class="textbox" style="width: 224px" size="35" 
                    name="name" value="" runat="server" tabindex="12" />
                <p class="frmHint">
                    Country Code + Phone Number</p>
            </td>
        </tr>
    </table>
    <table style="margin-left:5%">
        <tr>
            <td>
                By clicking 'Create Account' you agree to InfowebService's <a href="http://www.infowebservices.org/support/legal.php"
                    target="_blank">Terms & Conditions.</a>
            </td>
        </tr>
        <tr>
            <td>
                <button id="register_submit_id" class="ui-button" name="register_submit_id" type="submit">
                    <span><span>Create Account</span></span></button>
            </td>
        </tr>
    </table>
</asp:Content>
