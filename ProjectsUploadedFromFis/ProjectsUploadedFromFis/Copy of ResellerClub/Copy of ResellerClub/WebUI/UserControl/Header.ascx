<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs"
    Inherits="ResellerClub.WebUI.UserControl.Header" %>
<style>
    .row-gray
    {
        padding-left: 10px;
        padding-right: 20px;
        padding-bottom: 5px;
    }
</style>

<div>

</div>


<table>

   <tr>
        <td>
        
            <div id="divHighLightedFeature" class="row-gray">
                <div class="highlight_faeture_heading">
                    <%=((HtmlGenericControl)Parent.FindControl("divHeaderText")).InnerHtml%>
                </div>
                <%=((HtmlGenericControl)Parent.FindControl("divHeaderFeature")).InnerHtml%>
       
        </td>
        <td>
           <%=((HtmlGenericControl)Parent.FindControl("divHeaderImage")).InnerHtml%>
        </td>
    </tr>
</table>
<%--<div style="padding-top: 0px; display:none">
    <div style="font-size: 11px;width: 100px; padding-top: 30px; padding-bottom: 5px">
       </div>
    <div>
        <img style="border-top: solid 1px #CCCCCC" src="images/linux-support.jpg" />
    </div>
</div>--%>
