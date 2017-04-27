<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DeploymentWebUI.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="BtnCleanBranch" runat="server" OnClick="BtnCleanBranch_Click" Text="Clean Deployment Branch" />
    
    </div>
        <p>
            <asp:Button ID="btnCeatePull" runat="server" Text="Create Pull Aganist Deployment Branch" />
        </p>
        <asp:Button ID="btnMergePull" runat="server" Text="Merge Pull" Width="276px" />
        <br />
        <asp:Button ID="btnUpdateReference" runat="server" OnClick="btnUpdateReference_Click" Text="Update Reference" />
        <br />
        <asp:TextBox ID="txtUpdateRefence" runat="server" Height="237px" TextMode="MultiLine" Width="790px"></asp:TextBox>
    </form>
</body>
</html>
