<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductionBooking._Default" MasterPageFile="~/GoldNoMenu.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    <form id="roomdateform" runat="server" action="Default.aspx">
    <div>
        
        <asp:DropDownList ID="Roomcmb" runat="server">
            <asp:ListItem>PS1</asp:ListItem>
            <asp:ListItem>PS2</asp:ListItem>
            <asp:ListItem>PS3</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="Weekscmb" runat="server" Width="270px">
        </asp:DropDownList>
        <asp:Button ID="Gocmd" runat="server" Text="Go" Width="59px" 
            onclick="Gocmd_Click" />
        
    </div>

    <div>
        <asp:Table ID="Timetable" runat="server" Width="980px"></asp:Table>  
    </div>
    </form>
</asp:Content>