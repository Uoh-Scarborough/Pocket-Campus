﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mobile._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>

 <head>
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Pocket Campus :: Mobile</title>   
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="Images/PocketCampus.png" />
 </head>

 <body onload="hideAddressbar();">

     <script type="text/javascript">
        function hideAddressbar()
        {
	        window.scrollTo(0, 1);
        }
    </script>

    <div id="banner">
        <a href="Default.aspx"><img src="Images/PocketCampusHome.png"/></a>
        <h1>Pocket Campus</h1>
    </div>

    <div id="content"> 
        <ul>
            <li><a href="Timetables"><img class="icon" src="Images/TimetableMobile.png" />Timetables</a></li>
            <li><a href="StudentServices"><img class="icon" src="Images/StudentServicesMobile.png" />Student Services</a></li>
            <li><a href="http://m.productionbooking.scar.hull.ac.uk"><img class="icon" src="Images/ProductionBookingMobile.png" />Production Booking</a></li>
            <li><a href="http://m.studiobooking.scar.hull.ac.uk"><img class="icon" src="Images/StudioBookingMobile.png" />Studio Booking</a></li>
            <li><a href="Notices"><img class="icon" src="Images/NoticesMobile.png" />Notices</a></li>
            <li><a href="Events"><img class="icon" src="Images/EventsMobile.png" />Events</a></li>
            <li><a href="CampusInfo"><img class="icon" src="Images/CampusInfoMobile.png" />Campus Info</a></li>
            <li><a href="Links"><img class="icon" src="Images/LinksMobile.png" />Links</a></li>
        </ul>
        
        <ul><li>&copy; University of Hull Scarborough Campus</li></ul>
    </div>
    
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

