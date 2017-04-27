<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlanPanel.ascx.cs" Inherits="ResellerClub.WebUI.UserControl.PlanPanel" %>
  <div class="plan-features" style="border: #e7e4da 1px solid;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border: none;"
                         cellspacing="0">
                        <tr>
                            <th align="center" style="padding-left: 10px;" id="th1" runat="server">
                                <div class="plan_start" style="padding-left: 35px; padding-top: 5px;
                                    height: 30px; font-size: 14px; font-weight:2000;" align="left">
                                  <%=PlanHeading[0]%> </div>
                            </th>
                            <th id="th2" runat="server">
                                <div class="plan_standard" style="padding-left: 35px; padding-top: 5px;
                                    height: 30px; font-size: 14px; font-weight:2000;" align="left">
                                    <%=PlanHeading[1]%></div>
                            </th>
                            <th id="th3" runat="server">
                                <div  class="plan_premium" style="padding-left: 35px; padding-top: 5px;
                                    height: 30px; font-size: 14px; font-weight: 2000;" align="left">
                                   <%=PlanHeading[2]%> </div>
                            </th>
                            <th id="th4" runat="server">
                                <div  class="plan_e-business" style="padding-left: 35px;
                                    padding-top: 5px; height: 30px; font-size: 14px; font-weight: 2000;" align="left">
                                    <%=PlanHeading[3]%></div>
                            </th>
                        </tr>
                        <tr>
                            <td id="td1" runat="server">
                                <div class="bn-section-content-holder" <%=GetRightBorder(0)%>>
                                    <div class="plan-details">
                                     <%=PlanFeature[0]%>
                                    </div>
                                    <div class="container-price" align="left">
                                        <div>
                                            Start From :</div>
                                        <div class="price">
                                            <%=CurrencySymbol%>
                                            <%=PlanStartingPrice[0]%>
                                            <span>/ month</span>
                                        </div>
                                    </div>
                                    <div class="bn-sectionselect">
                                        Select Duration :<br />
                                        <asp:DropDownList ID="ddlPlan1" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <button id="Button4" name="btnPlan1" class="" type="submit">
                                    <span>Order</span></button>
                            </td>
                            <td id="td2" runat="server">
                                <div class="bn-section-content-holder" <%=GetRightBorder(1)%>>
                                    <div class="plan-details">
                                      <%=PlanFeature[1]%>
                                    </div>
                                    <div class="container-price" align="left">
                                        <div>
                                            Start From :</div>
                                        <div class="price">
                                            <%=CurrencySymbol%>
                                              <%=PlanStartingPrice[1]%>
                                            <span>/ month</span>
                                        </div>
                                    </div>
                                    <div class="bn-sectionselect">
                                        Select Duration :<br />
                                        <asp:DropDownList ID="ddlPlan2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <button id="Button5" name="btnPlan2" class="" type="submit">
                                    <span>Order</span></button>
                            </td>
                            <td id="td3" runat="server" >
                                <div class="bn-section-content-holder" <%=GetRightBorder(2)%>>
                                    <div class="plan-details">
                                     <%=PlanFeature[2]%>
                                    </div>
                                    <div class="container-price" align="left">
                                        <div>
                                            Start From :</div>
                                        <div class="price">
                                            <%=CurrencySymbol%>
                                             <%=PlanStartingPrice[2]%>
                                            <span>/ month</span>
                                        </div>
                                    </div>
                                    <div class="bn-sectionselect">
                                        Select Duration :<br />
                                        <asp:DropDownList ID="ddlPlan3" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <button id="Button6" name="btnPlan3" class="" type="submit">
                                    <span>Order</span></button>
                            </td>
                            <td id="td4" runat="server">
                                <div class="bn-section-content-holder" style="border-right-width: 0px;">
                                    <div class="plan-details">
                                       <%=PlanFeature[3]%>
                                    </div>
                                    <div class="container-price" align="left">
                                        <div>
                                            Start From :</div>
                                        <div class="price">
                                            <%=CurrencySymbol%>
                                            <%=PlanStartingPrice[3]%>
                                            <span>/ month</span>
                                        </div>
                                    </div>
                                    <div class="bn-sectionselect">
                                        Select Duration :<br />
                                        <asp:DropDownList ID="ddlPlan4" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <button id="Button7" name="btnPlan4" class="" type="submit">
                                    <span>Order</span></button>
                            </td>
                        </tr>
                    </table>
                </div>