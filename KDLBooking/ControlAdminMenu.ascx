<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlAdminMenu.ascx.cs" Inherits="KDLBooking.ControlAdminMenu" %>
<asp:Panel ID="MenuPanel" runat="server" CssClass="adminmenu">
<p><b><asp:Label ID="AdminMenulbl" runat="server" CssClass="visiblespan makeinvisible">Admin</asp:Label> Menu:</b> 
    <asp:Label ID="AdminMenuOptionslbl" runat="server" CssClass="visiblespan makeinvisible">
        <asp:HyperLink ID="GroupsBtn" runat="server" 
            NavigateUrl="~/Groups.aspx">Groups Management </asp:HyperLink> | 
        <asp:HyperLink ID="Constraintsbtn" runat="server" 
            NavigateUrl="~/Constraints.aspx">Constraints Management</asp:HyperLink> |
    </asp:Label>
    <asp:HyperLink ID="Calendarbtn" runat="server" 
        NavigateUrl="~/Default.aspx">Calendar</asp:HyperLink> |
    <asp:HyperLink ID="Bookingbtn" runat="server" 
        NavigateUrl="~/Booking.aspx">Bookings</asp:HyperLink> |
    <asp:HyperLink ID="Logoutbtn" runat="server"
        NavigateURL="~/Login.aspx">Log Out</asp:HyperLink></p>
</asp:Panel>

