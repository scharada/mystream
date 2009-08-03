<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RenderItems.ascx.cs" Inherits="MyStreamWeb.UserControls.RenderItems" %>

<%@ Import Namespace="MyStream.Utilities" %>

<%
    if(Items.Count > 0)
    {
        foreach (var item in Items)
        {
%>
        	<div class="post">
        		<p class="byline"><img class="item-icon" src="<%= item.Icon %>" /> &nbsp;<%= item.Timestamp.ToRelative() %> ago </p>
                <h2 class="title"><%= RenderTitle(item.Title, item.Url) %></h2>
        		
                <% 
                    if(!string.IsNullOrEmpty(item.Description))
                    {
                %>
        		        <div class="entry"><%= item.Description %></div>
                <%
                    } 
                %>
                
        		<div class="meta">
                    <!--
        			<p class="links"><a href="#" class="comments">Comments (32)</a></p>
                    -->
        		</div>
                
        	</div>
<%
        }
    }
%>