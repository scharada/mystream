<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyStreamWeb.Default" %>

<%@ Register src="UserControls/SubscriptionsSidebar.ascx" tagname="SubscriptionsSidebar" tagprefix="uc1" %>
<%@ Register src="UserControls/RenderItems.ascx" tagname="RenderItems" tagprefix="uc2" %>
<%@ Register src="UserControls/Menu.ascx" tagname="Menu" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MenuContentPlaceHolder" runat="server"> 
    <uc3:Menu ID="Menu1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <uc2:RenderItems ID="RenderItems1" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
    <form id="form1" runat="server">
    <ul>
        <!--
    	<li id="search">
    		<h2>Search</h2>
    		<form method="get" action="">
    			<fieldset>
    			<input type="text" id="seach-text" name="s" value="" />
    			<input type="submit" id="search-submit" value="Search" />
    			</fieldset>
    		</form>
    	</li>
        -->
        
        <uc1:SubscriptionsSidebar ID="SubscriptionsSidebar1" runat="server" /></ul>
        
    </form>
</asp:Content>