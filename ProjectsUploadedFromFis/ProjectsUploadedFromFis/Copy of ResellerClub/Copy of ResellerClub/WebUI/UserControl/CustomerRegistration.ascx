<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerRegistration.ascx.cs" Inherits="ResellerClub.WebUI.CustomerRegistration" %>
 <style type="text/css">
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
            width: 70%;
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
       
       .headingTR
       {
       	font-size:12px;
       }
       
    </style>
    
    <table class="frmTable">
        <tr class="headingTR">
            <td>
                Create an Account
            </td>
            <td colspan="3" align="right">
                * All fields are required
            </td>
        </tr>
        <tr class="headingTR">
            <td colspan="4">
                <hr />
                Please fill out the form below to create an account and proceed to checkout
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
                        size="35" name="name" value="testName" runat="server" />
            </td>
            <td nowrap>
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Address<span class="asterix">*</span></label><input id="input_address1" class="textbox"
                        size="35" name="name" value="testName" runat="server" />
                <input id="input_address2" class="textbox" size="35" name="name" value="testName"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Company Name</label><input id="input_companyname" class="textbox" size="35" name="name"
                        value="testName" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Country<span class="asterix">*</span></label>
                <select  id="country" class="required" onchange="change_country(this.value)"
                    name="country" runat="server">
                    <option value="null">Select a Country</option>
                    <option selected value="AX">Aland Islands</option>
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
                    <option value="IN">India</option>
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
                    <option value="GS">South Georgia And The South Sandwich Islands</option>
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
                    Email<span class="asterix">*</span></label><span class="frmHint aboveField">Will be
                        used as your username.</span><input id="username" class="textbox" size="35" name="name"
                            value="testName" runat="server" />
            </td>
            <td >
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    State<span class="asterix">*</span></label>
                <select id="stateSelect" class="required" onchange="showHideOtherState(this);"
                    name="state" runat="server">
                    <option value="Select State">Select State</option>
                    <option value="Alabama">Alabama</option>
                    <option value="Alaska">Alaska</option>
                    <option selected value="American Samoa">American Samoa</option>
                    <option value="Arizona">Arizona</option>
                    <option value="Arkansas">Arkansas</option>
                    <option value="California">California</option>
                    <option value="Colorado">Colorado</option>
                    <option value="Connecticut">Connecticut</option>
                    <option value="Delaware">Delaware</option>
                    <option value="District of Columbia">District of Columbia</option>
                    <option value="Florida">Florida</option>
                    <option value="Georgia">Georgia</option>
                    <option value="Guam">Guam</option>
                    <option value="Hawaii">Hawaii</option>
                    <option value="Idaho">Idaho</option>
                    <option value="Illinois">Illinois</option>
                    <option value="Indiana">Indiana</option>
                    <option value="Iowa">Iowa</option>
                    <option value="Kansas">Kansas</option>
                    <option value="Kentucky">Kentucky</option>
                    <option value="Louisiana">Louisiana</option>
                    <option value="Maine">Maine</option>
                    <option value="Maryland">Maryland</option>
                    <option value="Massachusetts">Massachusetts</option>
                    <option value="Michigan">Michigan</option>
                    <option value="Minnesota">Minnesota</option>
                    <option value="Mississippi">Mississippi</option>
                    <option value="Missouri">Missouri</option>
                    <option value="Montana">Montana</option>
                    <option value="Nebraska">Nebraska</option>
                    <option value="Nevada">Nevada</option>
                    <option value="New Hampshire">New Hampshire</option>
                    <option value="New Jersey">New Jersey</option>
                    <option value="New Mexico">New Mexico</option>
                    <option value="New York">New York</option>
                    <option value="North Carolina">North Carolina</option>
                    <option value="North Dakota">North Dakota</option>
                    <option value="Northern Mariana Islands">Northern Mariana Islands</option>
                    <option value="Ohio">Ohio</option>
                    <option value="Oklahoma">Oklahoma</option>
                    <option value="Oregon">Oregon</option>
                    <option value="Pennsylvania">Pennsylvania</option>
                    <option value="Puerto Rico">Puerto Rico</option>
                    <option value="Rhode Island">Rhode Island</option>
                    <option value="South Carolina">South Carolina</option>
                    <option value="South Dakota">South Dakota</option>
                    <option value="Tennessee">Tennessee</option>
                    <option value="Texas">Texas</option>
                    <option value="United States Minor Outlying Ielands">United States Minor Outlying Ielands</option>
                    <option value="Utah">Utah</option>
                    <option value="Vermont">Vermont</option>
                    <option value="Virgin Islands">Virgin Islands</option>
                    <option value="Virginia">Virginia</option>
                    <option value="Washington">Washington</option>
                    <option value="West Virginia">West Virginia</option>
                    <option value="Wisconsin">Wisconsin</option>
                    <option value="Wyoming">Wyoming</option>
                    <option value="Other">Other</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Password<span class="asterix">*</span></label><input id="passwd" type="password" class="textbox"
                        size="35" name="name" value="testName" runat="server" />
                <p class="frmHint">
                    8 character minimum. No special characters.</p>
            </td>
            <td style="width:60px">
                &nbsp;
            </td>
            <td style="width:170px">
                <label>
                    City<span class="asterix">*</span></label><input style="width: 140px" id="select_city"
                        class="textbox" size="35" name="name" value="testName" runat="server" />
            </td>
            <td align="left" style="width:200px">
                <label>
                    Zip<span class="asterix">*</span></label><input style="width: 140px" id="input_zip"
                        class="textbox" size="35" name="name" value="testName" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    Confirm Password<span class="asterix">*</span></label><input type="password" id="conf_passwd" class="textbox"
                        size="35" name="name" value="testName" runat="server" />
                <p class="frmHint">
                    8 character minimum. No special characters.</p>
            </td>
            <td >
                &nbsp;
            </td>
            <td colspan="2">
                <label>
                    Phone<span class="asterix">*</span></label>
                <input id="input_phone_cc" style="width: 40px" class="textbox" size="3" name="name"
                    value="testName" runat="server" />
                <input id="input_phone" class="textbox" style="width: 240px" size="35" name="name" value="testName" runat="server" />
                <p class="frmHint">
                    Country Code + Phone Number</p>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <button id="register_submit_id" class="ui-button" name="register_submit_id" type="submit">
                    <span><span>Create Account</span></span></button>
            </td>
        </tr>
    </table>
