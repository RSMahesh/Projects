<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deploy.aspx.cs" Inherits="DeploymentWebUI.Deploy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%;height:100%" align="center">
            <div align="left">
             <asp:Wizard ID="Wizard2" runat="server" ActiveStepIndex="0" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderWidth="1px" DisplayCancelButton="True" Font-Names="Verdana" Font-Size="0.8em" Height="316px" OnFinishButtonClick="Wizard2_FinishButtonClick" Width="824px" OnNextButtonClick="Wizard2_NextButtonClick">
            <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px" Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Center" />
            <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                 <StepPreviousButtonStyle Height="0px" Width="0px" />
            <SideBarButtonStyle ForeColor="White" BackColor="#507CD1" Font-Names="Verdana" />
            <SideBarStyle BackColor="#507CD1" Font-Size="1.2em" VerticalAlign="Top"  />
                 <StepStyle Font-Size="0.8em" ForeColor="#333333" BorderStyle="None" />
            <WizardSteps>
                <asp:WizardStep runat="server" Title="Clean Deployment Branch">
                    <asp:Literal ID="Literal0" runat="server"></asp:Literal>
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Create Pull">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Merge Pull">
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Enterprise Library Dlls">
                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                </asp:WizardStep>
                 <asp:WizardStep runat="server" Title="Deploy To Server">
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
        </div>
       </div>
    </form>
</body>
</html>
