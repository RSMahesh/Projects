<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UniCodeConv.aspx.cs"
    Inherits="HindiUnicode.UniCodeConv" EnableViewState="false" EnableSessionState="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script type="text/javascript" src="JavaScript/tabber.js"></script>

    <link rel="stylesheet" href="Style/example.css" type="text/css" media="screen">
    <link rel="stylesheet" href="Style/example-print.css" type="text/css" media="print">

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

    <title>कुरूति देव से यूनिकोडे </title>
    <style>
        .KurtiFont
        {
            font-family: 'Kruti Dev 010';
            font-size: 18px;
        }
  
}
    </style>
    
</head>
<body bgcolor="WhiteSmoke" style="font-size: 10pt; font-family: Verdana" >
    <form id="form1" runat="server">
        
        <table width="100%">
        <tr>
        <td align=center width="100%">
        <span style="font-size:26pt;color:Gray;display:none">ॐ</span>  
        </td>
        </tr>
        </table>
        
        <div class="tabber" style="margin-left:-8px">
        
            <div class="tabbertab">
                <h2><pre>  कुरूति देव  </pre></h2>
                <table>
                    <tr>
                        <td colspan="3">
                            
                        </td>
                    </tr>
                    <tr id="trKurti1">
                        <td style="width: 100px;">
                            &nbsp;
                        </td>
                        <td>
                            <!-- span style="font-size: 10pt">पेस्ट या टाइप करे कुरूति देव फॉण्ट टेक्स्ट </span>
                            <span style="font-size: 10pt; font-family: Verdana">(Past or Type Text in Kruti Dev)</span -->
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trKurti2">
                      
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtKurti" runat="server" TextMode="MultiLine" CssClass="KurtiFont"
                                Height="232px" Width="102px"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr id="trKurti3">
                        <td align="center" colspan="3">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="परिवर्तित्त करे यूनिकोडे में"
                                Width="219px" Visible="false" Font-Names="Verdana" Font-Size="10pt" ToolTip="Click to Convert to Unicode" />
                        </td>
                    </tr>
                    <tr id="trUniCod1">
                        <td>
                        </td>
                        <td>
                            <span id="span1" style="font-size: 11pt" runat="server" visible="false">परिवर्तित्त
                                यूनिकोडे टेक्स्ट </span><span id="span2" style="font-size: 10pt; font-family: Verdana"
                                    runat="server" visible="false">(Converted Unicode Text)</span>
                        </td>
                        <td>
                        </td>
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
                            <asp:Button ID="btnCopy" OnClientClick="return CopyUniCode()" runat="server" Text="कॉपी  यूनिकोडे"
                                Width="218px"  Font-Names="Verdana" Font-Size="10pt" Visible="false" ToolTip="Click To Copy Unicode" />
                        </td>
                    </tr>
                    </tr>
                </table>
            </div>
            <div class="tabbertab">
                <h2><pre>  इंग्लिश   </pre></h2>
                <div id="divGoogle">
                <table><tr>
                <td style="width:100px">&nbsp;</td>
                <td>
                <br>
                Type a word in English and press SPACE to transliterate. 
                </td>
                <td></td>
                </tr>
                    <tr>
                 <td></td>
                 <td>
                    <textarea style="height: 232px; width: 728px" id="transliterateTextarea"></textarea>
                    <br/>
                    <br />
                    </td>
                    <td></td>
                    </tr></table>
                </div>
            </div>
           
        </div>
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
    
    
    function Init()
    {
    document.getElementById("txtKurti").style.width = document.body.clientWidth + 10;
 document.getElementById("txtKurti").style.marginLeft  =0;
    }
   
    Init();   
    
    </script>

</body>
</html>
