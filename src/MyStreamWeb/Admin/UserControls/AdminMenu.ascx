<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="MyStreamWeb.Admin.UserControls.AdminMenu" %>

<ul>
    <%= Html.MenuItem("Back to Site", "../Default.aspx") %>
    <%= Html.MenuItem("Subscriptions", "Default.aspx") %>
    <%= Html.MenuItem("Settings", "Settings.aspx") %>
    <%= Html.MenuItem("Logout", "Logout.aspx") %>
</ul>