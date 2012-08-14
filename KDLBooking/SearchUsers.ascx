<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchUsers.ascx.cs" Inherits="KDLBooking.SearchUsers" %>

<asp:Label ID="Label1" runat="server" Text="Student ID:"></asp:Label>
<asp:TextBox ID="StudentIDtxt" runat="server" style="margin-left: 45px;margin-right: 45px;" 
    Width="122px"></asp:TextBox>

Surname:
<asp:TextBox ID="Surnametxt" runat="server" style="margin-left: 45px;margin-right: 45px;" 
    Width="122px"></asp:TextBox>

<asp:Button ID="Searchcmd" runat="server" onclick="Searchcmd_Click" 
    Text="Search" />
<p>
    &nbsp;</p>

<asp:Table ID="UserTable" runat="server" Width="980px" Height="31px" style="padding:15px">
    <asp:TableHeaderRow BackColor="#91a23d" ForeColor="White">
        <asp:TableHeaderCell CssClass="tablename">Name</asp:TableHeaderCell>
        <asp:TableHeaderCell CssClass="tablename">Student Number</asp:TableHeaderCell>
        <asp:TableHeaderCell CssClass="tablerequest">Email</asp:TableHeaderCell>
        <asp:TableHeaderCell CssClass="tablecontrols">&nbsp;</asp:TableHeaderCell>
    </asp:TableHeaderRow>
</asp:Table>

