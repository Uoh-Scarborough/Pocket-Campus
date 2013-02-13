<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_Menus.ascx.cs" Inherits="Communications.Controls.Control_Menus" %>
<%@ Register src="Control_Base.ascx" tagname="Control_Base" tagprefix="uc1" %>

<h1><asp:Label ID="Titlelbl" runat="server" CssClass="visible"></asp:Label></h1>

<asp:MultiView ID="Multiview" runat="server">

    <asp:View ID="AddEditView" runat="server">

        <asp:ValidationSummary ID="ValidationSummary" runat="server"  HeaderText="The following fields are required:" />

        <uc1:Control_Base ID="Control_Base" runat="server"  OnBaseValidated="Base_Validated" OnBaseInvalidated="Base_Invalidated" ModuleBaseType="Menu"/>

        <asp:Button ID="Savecmd" runat="server" Text="Save" onclick="Savecmd_Click" /><asp:Button ID="Cancelcmd" runat="server" Text="Cancel" />

    </asp:View>

    <asp:View ID="DeleteView" runat="server">
    
        <asp:Label ID="Deletelbl" runat="server" CssClass="visible"></asp:Label>

        <asp:Button ID="DeleteYescmd" runat="server" Text="Yes" OnClick="Deletecmd_Click" CommandName="Yes"/><asp:Button ID="DeleteNocmd" runat="server" Text="No" OnClick="Deletecmd_Click" CommandName="No" />

    </asp:View>

    <asp:View ID="SavedView" runat="server">
    
        <asp:Label ID="Savelbl" runat="server" CssClass="visible"></asp:Label>

        <asp:Button ID="Okcmd" runat="server" Text="Ok" OnClick="Okcmd_Click"/>

    </asp:View>

</asp:MultiView>

