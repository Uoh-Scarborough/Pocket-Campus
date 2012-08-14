<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlAdminMenu.ascx.cs" Inherits="KDLBooking.ControlAdminMenu" %>
<asp:Panel ID="MenuPanel" runat="server" >
<p><b>Admin Menu:</b> <asp:HyperLink ID="GroupsBtn" runat="server" 
        NavigateUrl="~/Groups.aspx">Groups Management </asp:HyperLink> | 
    <asp:HyperLink ID="Constraintsbtn" runat="server" 
        NavigateUrl="~/Constraints.aspx">Constraints Management</asp:HyperLink> |
        <asp:HyperLink ID="Bookingbtn" runat="server" 
        NavigateUrl="~/Default.aspx">Bookings</asp:HyperLink> |
    <asp:HyperLink ID="Logoutbtn" runat="server"
        NavigateURL="~/Login.aspx">Log Out</asp:HyperLink></p>
</asp:Panel>

