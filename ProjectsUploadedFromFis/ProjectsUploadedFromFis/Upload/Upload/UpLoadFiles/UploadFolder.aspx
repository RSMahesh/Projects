<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFolder.aspx.cs" Inherits="UpLoadFiles.UploadFolder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 189px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Server Folder Root Path</td>
                <td>
                    <asp:TextBox ID="txtServerPath" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                
                <td class="style1">
                    Client Folder Root Path</td>
                <td>
                    <asp:TextBox ID="txtClientPath" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Files</td>
                <td>
                    <asp:TextBox ID="txtFiles" runat="server" Height="339px" TextMode="MultiLine" 
                        Width="513px"></asp:TextBox>
                </td>
            </tr>
           <tr>
           <td>
           <asp:FileUpload ID="FileUpload1" runat="server" />
           </td>
           <td>
           <asp:TextBox ID="txtCopyingFile" runat="server"></asp:TextBox>
           </td>
           </tr> 
            <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text="StartUpload" 
                    onclick="Button1_Click" />
            
                <asp:Button ID="btnUploadFile" runat="server" onclick="btnUploadFile_Click" 
                    Text="UploadFile" />
            
            </td>
            </tr>
        </table>
    
    </div>
    Left
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br />
    Done
    <br />
    <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
    </form>
</body>
<script>

//document.getElementById("FileUpload1").



</script>

</html>
