<%@ Page Title="" Language="C#" MasterPageFile="~/Red.Master" AutoEventWireup="true" CodeBehind="Notices.aspx.cs" Inherits="Communications.Notices" %>
<%@ Register src="Controls/Control_Notices.ascx" tagname="Control_Notices" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HomeButtonCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainArea" runat="server">
    <form id="form1" runat="server">
    <uc1:Control_Notices ID="Control_Notices1" runat="server" Mode="Add" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomText" runat="server">
</asp:Content>
