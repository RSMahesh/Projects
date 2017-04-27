<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SelectDepartment.aspx.cs"
    Inherits="InfoWebTicketSystem.WebUI.SelectDepartment" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="maincorecontent">
    
        <!-- BEGIN DIALOG PROCESSING -->
        <div class="boxcontainer">
            <div class="boxcontainerlabel">
                Select a department</div>
            <div class="boxcontainercontent">
                If you can't find a solution to your problem in our knowledgebase, you can submit
                a ticket by selecting the appropriate department below.
                <br />
                <br />
                <table class="hlineheader">
                    <tr>
                        <th rowspan="2" nowrap>
                            Departments
                        </th>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="hlinelower">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="4">
                    <tr>
                        <td width="16" align="left" valign="middle" class="zebraodd">
                            <input type="radio" name="departmentid" 
                                value="SALE" id="department_116" />
                        </td>
                        <td>
                            <label for="department_116">
                                Sales</label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16" align="left" valign="middle" class="zebraodd">
                            <input type="radio" name="departmentid" 
                                value="BILL" id="department_117" checked />
                        </td>
                        <td>
                            <label for="department_117">
                                Billing</label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16" align="left" valign="middle" class="zebraodd">
                            <input type="radio" name="departmentid" 
                                value="TECH" id="department_118" />
                        </td>
                        <td>
                            <label for="department_118">
                                Technical</label>
                        </td>
                    </tr>
                
                </table>
                <br />
                <div class="subcontent">
                    <input class="rebuttonwide2" value="Next &raquo;" type="submit" name="button" /></div>
            </div>
        </div>
    </div>
</asp:Content>
