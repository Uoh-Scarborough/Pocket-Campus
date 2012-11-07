<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlAdminMenu.ascx.cs" Inherits="StudioBooking.ControlAdminMenu" %>

<p><b>Menu:</b> <asp:Panel ID="MenuPanel" runat="server" style="display:inline" ><asp:HyperLink ID="GroupsBtn" runat="server" 
        NavigateUrl="~/Groups.aspx">Groups Management </asp:HyperLink> | 
    <asp:HyperLink ID="Constraintsbtn" runat="server" 
        NavigateUrl="~/Constraints.aspx">Constraints Management</asp:HyperLink> |
        </asp:Panel>
    <asp:HyperLink ID="Logoutbtn" runat="server"
        NavigateURL="~/Login.aspx">Log Out</asp:HyperLink></p>


