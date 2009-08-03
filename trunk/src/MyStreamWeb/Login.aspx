<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyStreamWeb.Login" %>

<%@ Register src="UserControls/Menu.ascx" tagname="Menu" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function fireSubmit() 
        {
            var text = document.getElementById('<%= txtPassword.ClientID %>').value;
            
            if(text.length > 0)
                document.getElementById('<%= frmPassword.ClientID %>').submit();
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MenuContentHolder" runat="server"> 
    <uc3:Menu ID="Menu1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Administrator Login</h2><br />
    <form runat="server" id="frmPassword">
        Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" onkeypress="if(event.keyCode == 13){ fireSubmit(); }"></asp:TextBox> 
        <input type="button" value="Login" class="button" onclick="fireSubmit()" />
            
        <br /><br />
        <asp:Label ID="lblIncorrectPass" Visible="false" runat="server" ForeColor="Red" Text="Invalid password!"></asp:Label>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
