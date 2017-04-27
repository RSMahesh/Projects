<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceDetail.ascx.cs" Inherits="ResellerClub.WebUI.UserControl.InvoiceDetail" %>

  <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
        <HeaderTemplate>
            <table class="bluff" border="0" width="682px">
                <tr style="height: 50px">
                    <th>
                        Item
                    </th>
                    <th>
                        Decsiption
                    </th>
                    <th>
                        Invoice Number
                    </th>
                    <th align="right">
                        Amount
                    </th>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 200px">
                    <asp:Label ID="lblItem" Text="" runat="server"></asp:Label>
               
                </td>
                <td style="width: 300px">
                    <asp:Label ID="lblActionDecscription" Text="ghhnm.com" runat="server"></asp:Label>
                </td>
                <td style="width: 150px" align="center">
                    <asp:Label ID="lblInvoiceId" Text="ghhnm.com" runat="server"></asp:Label>
                </td>
                <td style="width: 100px;" align="right">
                    <asp:Label ID="lblPrice" Text="ghhnm.com" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <table width="682px">
        <tr>
            <td width="70%">
                &nbsp;
            </td>
            <td width="30%" align="right">
                <table width="100%">
                    <tr>
                        <td align="right">
                            Sub Total:
                        </td>
                        <td align="right">
                            <span id="spnSamt" runat="server">Rs 1234</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Tax(<%=ConfigurationManager.AppSettings["ServiceTax"]%>%):
                        </td>
                        <td align="right">
                            <span id="spnTax" runat="server">Rs 1234</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Total Amount:
                        </td>
                        <td align="right">
                            <span id="spnTamt" runat="server"></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>