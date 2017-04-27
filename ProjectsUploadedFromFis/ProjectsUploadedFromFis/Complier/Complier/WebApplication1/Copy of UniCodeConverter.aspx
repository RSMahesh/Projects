<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniCodeConverter.aspx.cs"
    Inherits="HindiUnicode.UniCodeConverter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="https://www.google.com/jsapi?key=ABQIAAAAQjfRAotG1YXq4slrHXtzXBT2yXp_ZAY8_ufC3CFXhHIE1NvwkxSRAMcE7RjLJucHtwY8j-MtXULGLA">
    </script>
    <script type="text/javascript">

      // Load the Google Transliterate API
      google.load("elements", "1", {
            packages: "transliteration"
          });

      function onLoad() {
        var options = {
            sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
            destinationLanguage:
                [google.elements.transliteration.LanguageCode.HINDI],
            shortcutKey: 'ctrl+g',
            transliterationEnabled: true
        };

        // Create an instance on TransliterationControl with the required
        // options.
        var control =
            new google.elements.transliteration.TransliterationControl(options);

        // Enable transliteration in the textbox with id
        // 'transliterateTextarea'.
        control.makeTransliteratable(['transliterateTextarea']);
      }
      google.setOnLoadCallback(onLoad);
    </script>
 
    <title > कुरूति देव से यूनिकोडे </title>
    <style>
        .KurtiFont
        {
            font-family: 'Kruti Dev 010';
            font-size: 18px;
        }
    </style>S
</head>
<body bgcolor="#d5eaff" style="font-size:10pt;font-family:Verdana">
    <form id="form1" runat="server">
   
   
        <table>
            <tr>
                <td  colspan="3">
                    &nbsp;fffffff
                </td>
             </tr>
             <tr>
             <td colspan="3" style="border-width:1px;border-bottom-style:solid;border-color:Gray;width:100px;background-image:url(/images/login_bar.jpg);background-repeat:repeat;">
             <input type="radio" name="language" onclick="ChangeLanguage('K')"  value="Kurti" />Type in Kurti Dev
              <input type="radio" name="language"  onclick="ChangeLanguage('E')"  value="English" />Type in English
             </td>
             </tr>
            <tr id="trKurti1" >
                <td style="width: 100px;">
                    &nbsp;
                </td>
                <td>
                <span style="font-size:11pt">पेस्ट या टाइप करे कुरूति देव फॉण्ट टेक्स्ट  </span>
                <span style="font-size:10pt;font-family:Verdana">(Past or Type Text in Kruti Dev)</span>
                
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr id="trKurti2">
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtKurti" runat="server" TextMode="MultiLine" CssClass="KurtiFont"
                        Height="232px" Width="728px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
            <td colspan="3" align="center">
            <div id ="divGoogle">
    <textarea style="height:232px;width:728px"  id="transliterateTextarea" ></textarea>
    </div>
            
            </td>
            </tr>
            <tr id="trKurti3">
                <td align="center" colspan="3">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="परिवर्तित्त करे यूनिकोडे में"
                        Width="219px" Font-Names="Verdana" Font-Size="11pt"  
                        ToolTip="Click to Convert to Unicode"/>
                </td>
               </tr>
            <tr id="trUniCod1">
            <td></td>
                <td>
                   <span id="span1" style="font-size:11pt" runat="server" visible="false">परिवर्तित्त यूनिकोडे टेक्स्ट  </span>
                <span id="span2" style="font-size:10pt;font-family:Verdana" runat="server" visible="false">(Converted Unicode Text)</span>
                
                </td>
            <td></td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtUnicode" runat="server" TextMode="MultiLine" ReadOnly="True"
                        BackColor="#EBEBEB" Visible="False" Font-Names="Verdana" Font-Size="12pt" Height="232px"
                        Width="728px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="btnCopy" OnClientClick="return CopyUniCode()" runat="server"  Text="कॉपी  यूनिकोडे"
                        Width="218px" Font-Names="Verdana" Font-Size="11pt" Visible="false" 
                        ToolTip="Click To Copy Unicode" />
                </td>
               </tr>
           </tr>
        </table>
     
    
    </form>
    <script>
    function CopyUniCode()
    {
    copyToClipboard(document.getElementById("txtUnicode").value);
    return false;
    }
    function copyToClipboard(text)
{
    if (window.clipboardData) // IE
    {  
        window.clipboardData.setData("Text", text);
    }
    else
    {  
        unsafeWindow.netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");  
        var  clipboardHelper = Components.classes["@mozilla.org/widget/clipboardhelper;1"].getService(Components.interfaces.nsIClipboardHelper);  
        clipboardHelper.copyString(text);
    }
}
    
 function ChangeLanguage(lang)
 {
    if(lang == 'E')
    {
     document.getElementById("trKurti1").style.display = "none";
     document.getElementById("trKurti2").style.display = "none";
     //document.getElementById("trKurti2").style.disa = "none";
     document.getElementById("divGoogle").style.display = "";
    }
    else
    {
     document.getElementById("trKurti1").style.display = "";
     document.getElementById("trKurti2").style.display = "";
     //document.getElementById("trKurti2").style.disa = "none";
     document.getElementById("divGoogle").style.display = "none";
    
    }
 
 }   
    
    </script>
</body>
</html>
