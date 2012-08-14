<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mobile.CampusInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Campus Info</title>
    <link rel="stylesheet" type="text/css" href="../iPhoneHeader.css" />
    <link rel="stylesheet" type="text/css" href="../RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="../Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">
    <script type="text/javascript">
        function hideAddressbar() {
            window.scrollTo(0, 1);
        }
    </script>
    <asp:MultiView ID="Multiview" runat="server">

        <asp:View ID="DefaultView" runat="server">
            <div id="banner">
                <a href="../Default.aspx" class="first">
                    <img src="../Images/PocketCampusHome.png" alt="Hello" /></a> <a href="Timetable.aspx">
                        <img class="second" src="../Images/CampusInfoHome.png" alt="Hello" /></a>
                <h1 class="secondheader">
                    Campus Info</h1>
            </div>
            <div id="content">
                <ul>
                    <li><a href="?page=CampusRedevelopment">
                        <img class="icon" src="../Images/CampusRedevelopmentMobile.png" />Campus Redevelopment</a></li>
                    <li><a href="?page=Calvinos">
                        <img class="icon" src="../Images/CalvinosMobile.png" />Calvinos</a></li>
                    <li><a href="?page=Careers">
                        <img class="icon" src="../Images/CareersMobile.png" />Careers</a></li>
                    <li><a href="?page=DiningHallx">
                        <img class="icon" src="../Images/DiningRoomMobile.png" />Dining Hall</a></li>
                    <li><a href="?page=IT">
                        <img class="icon" src="../Images/ITServicesMobile.png" />IT Services</a></li>
                    <li><a href="?page=Library">
                        <img class="icon" src="../Images/LibraryMobile.png" />Library</a></li>
                    <li><a href="?page=OTE">
                        <img class="icon" src="../Images/OnTheEdgeMobile.png" />On The Edge</a></li>
                    <li><a href="?page=SU">
                        <img class="icon" src="../Images/StudentsUnionMobile.png" />Students Union</a></li>
                    <li><a href="?page=StudyAdvice">
                        <img class="icon" src="../Images/StudyAdviceMobile.png" />Study Advice</a></li>
                    <li><a href="?page=UsefulDates">
                        <img class="icon" src="../Images/UsefulDatesMobile.png" />Useful Dates</a></li>
                </ul>
                <ul>
                    <li><span class="copy">&copy; University of Hull Scarborough Campus</span></li></ul>
            </div>
        </asp:View>

        <asp:View ID="ContentView" runat="server">
        
            <div id="banner">
                <a href="../Default.aspx" class="first">
                    <img src="../Images/PocketCampusHome.png" alt="Hello" /></a> <a href="Timetable.aspx">
                        <img class="second" src="../Images/CampusInfoHome.png" alt="Hello" /></a>
                <h1 class="secondheader"><asp:Label ID="headerlbl" runat="server" /></h1>
            </div>
            <div id="content">
                <asp:Label ID="contentlbl" runat="server" />
            </div>
            
        </asp:View>

    </asp:MultiView>
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <script type="text/javascript">
        try {
            var pageTracker = _gat._getTracker("UA-3108410-12");
            pageTracker._trackPageview();
        } catch (err) { }</script>
</body>
</html>
