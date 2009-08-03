<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultPage.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="MyStreamWeb.Admin.Settings" %>

<%@ Register src="UserControls/AdminMenu.ascx" tagname="AdminMenu" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenuContentHolder" runat="server">
    <uc1:AdminMenu ID="AdminMenu1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h2>Site Information</h2><br />
    
    <form runat="server">
    
        <table cellpadding="0" cellspacing="3" style="border: 0; width: 100%">
            <tr>
                <td>Title:</td>
                <td>
                    <asp:TextBox ID="txtTitle" MaxLength="64" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td>Slogan</td>
                <td>
                    <asp:TextBox ID="txtSlogan" MaxLength="128" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="64" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Confirm Password:</td>
                <td>
                    <asp:TextBox ID="txtConfirmPass" TextMode="Password" MaxLength="64" runat="server" Width="300px"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="cvPassword" runat="server" 
                        ErrorMessage="Mismatched." ControlToCompare="txtPassword" 
                        ControlToValidate="txtConfirmPass" Display="Dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>Theme:</td>
                <td>
                    <asp:DropDownList ID="ddlThemes" runat="server"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Cache duration (seconds):</td>
                <td><asp:TextBox ID="txtCacheDuration" MaxLength="8" runat="server" Width="150px"></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvCache" runat="server" ControlToValidate="txtCacheDuration" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="submit" value="Save" class="button" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label runat="server" ID="lblChangesSaved" Font-Bold="true" ForeColor="Green" Visible="false">Changes saved.</asp:Label> </td>
                <td></td>
            </tr>
        </table>
        
    </form>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SidebarContentPlaceHolder" runat="server">
</asp:Content>
