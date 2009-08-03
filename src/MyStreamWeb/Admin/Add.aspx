<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="MyStreamWeb.Admin.Add" %>
<%@ Register src="UserControls/AdminMenu.ascx" tagname="AdminMenu" tagprefix="uc1" %>
<%@ Import Namespace="MyStream.Business" %>
<%@ Import Namespace="MyStream.Plugins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validateInput() 
        {
            var isValid = true;
            var parameters = '<%= _ParametersList %>';
            var properties = parameters.split('|');
            var spnErrorMessage = document.getElementById('spnErrorMessage');
            var cookieString = '';
            
            for (var i = 0; i < properties.length; ++i) 
            {
                var propertyList = properties[i].split(':');
                
                var ctrl = document.getElementById(propertyList[0]);
                if (ctrl.value.trim() == '') 
                {
                    spnErrorMessage.innerHTML = propertyList[1] + " is required.";
                    isValid = false;
                }
                else
                {
                    cookieString += propertyList[0] + '=' + ctrl.value.trim() + '|';
                }
            }

            if (isValid) 
            {
                cookieString = cookieString.substr(0, cookieString.length - 1);
                document.cookie = 'mystream_subscription_add_cookie=' + cookieString;
                document.getElementById('<%= frmAdd.ClientID %>').submit();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContentHolder" runat="server">
    <uc1:AdminMenu ID="AdminMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <%
        using (var facade = new Facade())
        {
    %>            
            <h2>Add <%= _CurrentPlugin.GetShortName() %> Account</h2>    
            <br /><br />
            
            <form runat="server" id="frmAdd">
            <table cellpadding="2" cellspacing="3" style="border: 0; width: 100%" id="tabForm" runat="server">            
            </table>            
            </form>
            <asp:Panel runat="server" Visible="false" ID="pnlInvalidInfo">Invalid information provided. Please <a href="javascript:;" onclick="javascript:history.go(-1);">go back and try again</a>.</asp:Panel>
            <asp:Panel runat="server" Visible="false" ID="pnlSuccess">
                Subscription added successfully. <a href="../">View site for new items</a> | <a href="Default.aspx">Add more subscription</a>
            </asp:Panel>
    <%
        }
    %>
            


    <h2></h2>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
