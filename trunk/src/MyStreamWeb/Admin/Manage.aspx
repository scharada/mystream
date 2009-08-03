<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="MyStreamWeb.Admin.Manage" %>
<%@ Register src="UserControls/AdminMenu.ascx" tagname="AdminMenu" tagprefix="uc1" %>
<%@ Import Namespace="MyStream.Business" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script>
        function deleteSubscription(id, title) 
        {
            if (confirm('Are you sure you want to delete "' + title + '"')) 
            {
                document.getElementById('divItem' + id).style.display = 'none';
                MyStreamWeb.Services.StreamService.DeleteSubscription(id, function(result) { }, null, null, null);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContentHolder" runat="server">
    <uc1:AdminMenu ID="AdminMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Services/StreamService.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>

    <%
        using(var facade = new Facade())
        {
            var subscriptions = facade.GetAllSubscriptions();
            subscriptions.ForEach(s =>
            {
    %>
                <div id="divItem<%= s.ID %>">
                    <a id="anc<%= s.ID %>" href="javascript:;" style="vertical-align: super;" onclick="deleteSubscription('<%= s.ID %>', '<%= s.Title %>');">Delete</a>
                    <span style="vertical-align: super;"> | </span>
                    <img src="<%= "/" + s.Icon %>" class="item-icon" style="float: none;" /> 

                    <a style="vertical-align: super;" href="<%= s.FriendlyUrl %>"><%= s.Title %></a>
                    <br /><br />   
                </div>
    <%
            });

            if (subscriptions.Count < 1)
            {
    %>
                <i>No subscriptions found.</i> <a href="Default.aspx">Click here to add one now</a>.
    <%
            }
        }
    %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
