<%@ Page Title="" Language="C#" MasterPageFile="~/Responsive.Master" AutoEventWireup="true" CodeBehind="Notices.aspx.cs" Inherits="Communications.Notices" %>

<%@ Register src="Controls/Control_Notice.ascx" tagname="Control_Notice" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainArea" runat="server">
    
    <uc2:Control_Notice ID="Control_Notice1" runat="server" />
    
</asp:Content>

