<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeployWebApi.aspx.cs" Inherits="DeploymentWebUI.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #wizHeader li .prevStep {
            background-color: #669966;
        }

            #wizHeader li .prevStep:after {
                border-left-color: #669966 !important;
            }

        #wizHeader li .currentStep {
            background-color: #C36615;
        }

            #wizHeader li .currentStep:after {
                border-left-color: #C36615 !important;
            }

        #wizHeader li .nextStep {
            background-color: #C2C2C2;
        }

            #wizHeader li .nextStep:after {
                border-left-color: #C2C2C2 !important;
            }

        #wizHeader {
            list-style: none;
            overflow: hidden;
            font: 18px Helvetica, Arial, Sans-Serif;
            margin: 0px;
            padding: 0px;
        }

            #wizHeader li {
                float: left;
            }

                #wizHeader li a {
                    color: white;
                    text-decoration: none;
                    padding: 10px 0 10px 55px;
                    background: brown; /* fallback color */
                    background: hsla(34,85%,35%,1);
                    position: relative;
                    display: block;
                    float: left;
                }

                    #wizHeader li a:after {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid hsla(34,85%,35%,1);
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        left: 100%;
                        z-index: 2;
                    }

                    #wizHeader li a:before {
                        content: " ";
                        display: block;
                        width: 0;
                        height: 0;
                        border-top: 50px solid transparent;
                        border-bottom: 50px solid transparent;
                        border-left: 30px solid white;
                        position: absolute;
                        top: 50%;
                        margin-top: -50px;
                        margin-left: 1px;
                        left: 100%;
                        z-index: 1;
                    }

                #wizHeader li:first-child a {
                    padding-left: 10px;
                }

                #wizHeader li:last-child {
                    padding-right: 50px;
                }

                #wizHeader li a:hover {
                    background: #FE9400;
                }

                    #wizHeader li a:hover:after {
                        border-left-color: #FE9400 !important;
                    }

        .content {
            height: 250px;
            padding-top: 75px;
            text-align: center;
            background-color: #F9F9F9;
            font-size: 28px;
        }

        .hideMe {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 100px; margin-top: 0px;">
            <div id="divMessage" runat="server" style="font-weight:bold"></div>
            <asp:Wizard ID="Wizard1"  runat="server" DisplaySideBar="False" OnNextButtonClick="Wizard1_NextButtonClick" OnFinishButtonClick="Wizard1_FinishButtonClick" OnCancelButtonClick="Wizard1_CancelButtonClick" ActiveStepIndex="0" DisplayCancelButton="True">
                <StartNextButtonStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="50px" Width="200px" />
                <StepNextButtonStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="50px" Width="200px" />
                <FinishCompleteButtonStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="50px" Width="200px" />
                <CancelButtonStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="50px" Width="200px" />
                <StepPreviousButtonStyle CssClass="hideMe" />
                <FinishPreviousButtonStyle CssClass="hideMe" />

                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Clean Deployment Branch">
                        <div id="divContent1" runat="server" class="content">Cleans deployment branch and get latest from upstream</div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Create Pull">
                        <div id="divContent2" runat="server" class="content">Create pull request aganist RsMahesh/MediaValetAPI deployment branch and click next
                           
                            <div style="margin-top: 100px;font-size:15px">Skip this step if you only have enter Enterprise Dlls to deploy</div>
                            
                            <asp:Button ID="btnSkipCreatePull" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="50px" Width="200px" runat="server" Text="Skip" OnClick="btnSkipCreatePull_Click"  />
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Merge Pull">
                        <div id="divContent3" runat="server" class="content"></div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Add Enterprise Dlls">
                        <div id="divContent4" runat="server" class="content"></div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep5" runat="server" Title="Build and Deploy to server">
                        <div id="divContent5" runat="server" class="content">Triggres Teamcity build on RsMahesh/MediaValetAPI deployment branch and deploy it to cloud.</div>
                    </asp:WizardStep>
                </WizardSteps>
                <HeaderTemplate>
                    <ul id="wizHeader">
                        <asp:Repeater ID="SideBarList" runat="server">
                            <ItemTemplate>
                                <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                    <%# Eval("Name")%></a> </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </HeaderTemplate>
            </asp:Wizard>
        </div>
    </form>
</body>
</html>
