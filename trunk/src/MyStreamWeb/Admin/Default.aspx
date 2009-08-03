<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyStreamWeb.Admin.Default" %>
<%@ Register src="UserControls/AdminMenu.ascx" tagname="AdminMenu" tagprefix="uc1" %>
<%@ Import Namespace="MyStream.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContentHolder" runat="server">
    <uc1:AdminMenu ID="AdminMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <table style="width: 100%; border: 0;">
        <tr>
            <td>
                 <h2>Add Subscription</h2>
            </td>
            <td style="text-align: right">
                <h3><asp:HyperLink NavigateUrl="~/Admin/Manage.aspx" runat="server">Manage Subscriptions</asp:HyperLink></h3>
            </td>
        </tr>
    </table>      
    <br /><br />   
    
    <%
        using(var facade = new Facade())
        {
            var plugins = facade.GetAllSocialServices();
            plugins.ForEach(p =>
            {
    %>
                <%= Html.AnchorWithIcon(p.GetFriendlyName(), "Add.aspx?Type=" + p.GetTypeName(), "../" + p.GetIconPath()) %> <br /><br />   
    <%
            });
        }
    %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
