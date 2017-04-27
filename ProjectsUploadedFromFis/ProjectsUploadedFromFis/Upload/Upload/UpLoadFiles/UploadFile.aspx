<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="UpLoadFiles.UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 367px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr><td class="style1"><asp:FileUpload ID="FileUpload1" runat="server" /></td></tr>
    <tr><td class="style1"> <asp:TextBox ID="TextBox1" runat="server" Width="215px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        </td></tr>
    <tr><td class="style1"></td></tr>
    <tr><td class="style1">&nbsp;</td></tr>
    </table>
    
        
    <br />
   
    </div>
    
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
       onrowdatabound="GridView1_RowDataBound" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="FileName" HeaderText="Col1" />
           
        </Columns>
    </asp:GridView>
    
    
    </form>
</body>
</html>
