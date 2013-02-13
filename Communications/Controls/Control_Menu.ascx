<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control_Menu.ascx.cs" Inherits="Communications.Controls.Control_Menu" %>

<asp:Panel ID="MenuPanel" runat="server" CssClass="adminmenu">
<p><b><asp:Label ID="AdminMenulbl" runat="server" CssClass="visiblespan makeinvisible">Admin</asp:Label> Menu:</b> 
    <asp:Label ID="AdminMenuOptionslbl" runat="server" CssClass="visiblespan makeinvisible">
        <asp:HyperLink ID="GroupsBtn" runat="server" 
            NavigateUrl="~/Groups.aspx">Groups Management </asp:HyperLink> | 
    </asp:Label>
    <asp:HyperLink ID="Calendarbtn" runat="server" 
        NavigateUrl="~/Default.aspx">Manage</asp:HyperLink> |
    <asp:HyperLink ID="Bookingbtn" runat="server" 
        NavigateUrl="~/Default.aspx?cmd=Notice">Add Notice</asp:HyperLink> |
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/Default.aspx?cmd=Event">Add Event</asp:HyperLink> |
    <asp:Label ID="MenuOptionslbl" runat="server" CssClass="visiblespan">
        <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="~/Default.aspx?cmd=Menu">Add Menu</asp:HyperLink> |
    </asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="visiblespan">
        <asp:HyperLink ID="HyperLink3" runat="server" 
        NavigateUrl="~/Default.aspx?cmd=Ticker">Add Ticker</asp:HyperLink> |
    </asp:Label>
    <asp:HyperLink ID="Logoutbtn" runat="server"
        NavigateURL="~/Login.aspx">Log Out</asp:HyperLink></p>
</asp:Panel>
