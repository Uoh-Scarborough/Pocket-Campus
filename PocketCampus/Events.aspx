<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="PocketCampus.Events" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://pocketcampus.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/EventsLogo.png" alt="Events" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    <h1>Events</h1>
    <p>Listed below are up comming Scarborough Campus Events.</p>
    <p>If you have an event you would like to add to the system, you can <a href="http://communications.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=-1">add a event</a> by logging in.</p>
    <asp:Label ID="Contentlbl" runat="server" Text="Label"></asp:Label>
    <p>If you have an event you would like to add to the system, you can <a href="http://communications.scar.hull.ac.uk/events.aspx?aid=1&amp;eid=-1">add a event</a> by logging in.</p>
</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/EventsText.png" style="width:1000px; padding:0px; margin:0px; border:0px; position:relative; left:-12px;" alt="Events"/>

</asp:Content>