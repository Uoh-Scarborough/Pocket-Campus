<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Comms.Default" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://communications.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/CommsLogo.png" alt="Communications" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">

    <h1>Scarborough Communications</h1>
    
    <p>You can Add or Edit Notices and Events from this page, as well as view a list of 
        the notices you have added to the system. Click one of the buttons below to 
        begin.</p>
    
    <ul class="buttonmenumain">
        <li><a href="Notices.aspx">View Notices</a></li>
        <li><a href="Notices.aspx?aid=1&nid=-1">Add Notice</a></li>
        <li><a href="Events.aspx">View Events</a></li>
        <li><a href="Events.aspx?aid=1&eid=-1">Add Event</a></li>
        <asp:Label ID="WelcomeScreenslbl" runat="server"></asp:Label>
        <asp:Label ID="PageManagerlbl" runat="server"></asp:Label>
        <li><a href="login.aspx">Log Out</a></li>
    </ul>
    
    <p><a href="http://laurel.scar.hull.ac.uk/HelpManager/index.php?sid=1">Communications Help</a></p>

</asp:Content>