<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notices.aspx.cs" Inherits="PocketCampus.Notices" MasterPageFile="~/Red.Master" %>

<asp:Content ID="HomeButton" ContentPlaceHolderID="HomeButtonCPH" runat="server">

    <a href="http://pocketcampus.scar.hull.ac.uk"><img src="http://pocketcampusimages.scar.hull.ac.uk/NoticesLogo.png" alt="Notices" /></a>
    
</asp:Content>

<asp:Content ID="first" ContentPlaceHolderID="MainArea" Runat="Server">
    <h1>Notices</h1>
    <p>Listed below are the current Scarborough Campus Notices.</p>
    <p>If you have a notice you would like to add to the system, you can <a href="http://communications.scar.hull.ac.uk/notices.aspx?aid=1&amp;nid=-1">add a notice</a> by logging in.</p>
    <asp:Label ID="Contentlbl" runat="server" Text="Label"></asp:Label>
</asp:Content>

<asp:Content ID="BottomText" ContentPlaceHolderID="BottomText" runat="server">
    
    <img src="http://pocketcampusimages.scar.hull.ac.uk/NoticesText.png" style="width:1000px; padding:0px; margin:0px; border:0px; position:relative; left:-12px;" alt="Notices"/>

</asp:Content>