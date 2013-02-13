<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/Responsive.Master" Inherits="Communications._Default" %>

<%@ Register src="~/Controls/Control_List.ascx" tagname="Control_List" tagprefix="controls" %>

<%@ Register src="~/Controls/Control_Menu.ascx" tagname="Control_Menu" tagprefix="controls" %>

<%@ Register src="Controls/Control_Notice.ascx" tagname="Control_Notice" tagprefix="uc1" %>

<%@ Register src="Controls/Control_Event.ascx" tagname="Control_Event" tagprefix="uc1" %>

<%@ Register src="Controls/Control_Menus.ascx" tagname="Control_Menus" tagprefix="uc1" %>

<%@ Register src="Controls/Control_Ticker.ascx" tagname="Control_Ticker" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainArea" runat="server">

    <controls:control_menu ID="Control_Menu" runat="server" />
&nbsp;<asp:Multiview ID="Multiview" runat="server">

        <asp:View ID="Lists" runat="server">

            <h1>Manage</h1>

            <asp:Panel ID="pnl_Notices" runat="server">
            
            <h2>Notices</h2>
            
                <controls:Control_List ID="Notices_List" runat="server" type="Notices" />
            
            </asp:Panel>

            <asp:Panel ID="pnl_Events" runat="server">
            
            <h2>Events</h2>
            
                <controls:Control_List ID="Events_List" runat="server" type="Events" />
            
            </asp:Panel>

            <asp:Panel ID="pnl_Menu" runat="server">
            
            <h2>Menus</h2>
            
                <controls:Control_List ID="Menu_List" runat="server" type="Menus" />
            
            </asp:Panel>

            <asp:Panel ID="pnl_Ticker" runat="server">
            
            <h2>Tickers</h2>
            
                <controls:Control_List ID="Ticker_List" runat="server" type="Tickers" />
            
            </asp:Panel>
            

            
        </asp:View>

        <asp:View ID="AddEditNotice" runat="server">
        
        
            <uc1:Control_Notice ID="Control_Notice1" runat="server" />
        
        
        </asp:View>

        <asp:View ID="AddEditEvent" runat="server">
        
        
            <uc1:Control_Event ID="Control_Event1" runat="server" />
        
        
        </asp:View>

        <asp:View ID="AddEditMenu" runat="server">
        
        
            <uc1:Control_Menus ID="Control_Menu1" runat="server" />
        
        
        </asp:View>

        <asp:View ID="AddEditTicker" runat="server">
        
        
            <uc1:Control_Ticker ID="Control_Ticker1" runat="server" />
        
        
        </asp:View>
    
    
    </asp:Multiview>
   
</asp:Content>

