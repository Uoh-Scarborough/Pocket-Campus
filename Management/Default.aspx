<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Management._Default" MasterPageFile="~/Red.Master"%>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <h1>Kiosk Management</h1>
                
    <asp:Table ID="KiosksTable" runat="server" Width="700px" Height="31px">
        <asp:TableHeaderRow BackColor="#ae2b30" ForeColor="White">
            <asp:TableHeaderCell CssClass="tablename">Kiosk Name</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="tablenumber">Kiosk IP</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="tablerequest">Kiosk On</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="tablerequestdate">Todays Hit</asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="tablecontrols" ColumnSpan=2>Weeks Hits</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>

</asp:Content>
