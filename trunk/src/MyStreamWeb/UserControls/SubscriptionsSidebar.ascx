<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionsSidebar.ascx.cs" Inherits="MyStreamWeb.UserControls.SubscriptionsSidebar" %>

<li>
	<h2>Social Accounts</h2>
    <%
        if (Items.Count > 0)
        {
    %>
    	<ul>
            <%  foreach (var item in Items)
                {
            %>
            		<li>
                        <a href="<%= item.FriendlyUrl %>"><%= item.Title%></a>
                    </li>
            <%
                }
            %>
    	</ul>
    <%
        }
        else
        {
    %>
            <i>No accounts setup yet.</i>
    <%
        }
    %>
</li>