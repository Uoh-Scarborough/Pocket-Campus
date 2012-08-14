<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mobile.Timetable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Timetables</title>
    <link rel="stylesheet" type="text/css" href="../RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="../Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">

     <script type="text/javascript">
        function hideAddressbar()
        {
	        window.scrollTo(0, 1);
        }
    </script>

    <div id="banner">
        <a href="../Default.aspx" class="first"><img src="../Images/PocketCampusHome.png" alt="Pocket Campus Home"/></a>
        <a href="Default.aspx"><img class="second" src="../Images/TimetableHome.png" alt="Hello"/></a>
        <h1 class="secondheader">Timetables</h1>
    </div>

    <div id="content">

   

        <asp:MultiView ID="MultiView" runat="server">
        
            <asp:View ID="YearList" runat="server">
                <h1>Please select a year of study.</h1>
                
                <ul>
                    <li><a href="?yid=1"><img class="arrow" src="../Images/ArrowButton.jpg"/>Year 1</a></li>
                    <li><a href="?yid=2"><img class="arrow" src="../Images/ArrowButton.jpg"/>Year 2</a></li>
                    <li><a href="?yid=3"><img class="arrow" src="../Images/ArrowButton.jpg"/>Year 3</a></li>
                    <li><a href="?yid=4"><img class="arrow" src="../Images/ArrowButton.jpg"/>Foundation Year</a></li>
                    <li><a href="?yid=5"><img class="arrow" src="../Images/ArrowButton.jpg"/>MSC</a></li>
                    <li><a href="?yid=6"><img class="arrow" src="../Images/ArrowButton.jpg"/>Other</a></li>
                </ul>
                
                <asp:Label ID="MyTimetable" runat="server"></asp:Label>
            </asp:View>
            
            <asp:View ID="DepartmentList" runat="server">
                <h1>Please select you home departments / subject group.</h1>
                
                <asp:Label ID="DepartmentLabel" runat="server"></asp:Label>
                
              
            </asp:View>
            
            <asp:View ID="CourseList" runat="server">
                <h1>Please select a course.</h1>
                
                <asp:Label ID="CourseLabel" runat="server"></asp:Label>
            </asp:View>
            
            <asp:View ID="TimetableList" runat="server">
                <h1>Listed below is this weeks timetable.</h1>
                
                <asp:Label ID="NextWeeklbl" runat="server"></asp:Label>
                
                <asp:Label ID="TimetableLabel" runat="server"></asp:Label>
            </asp:View>
            
             <asp:View ID="SaveTimetable" runat="server">
                <h1>Timetable Saved</h1>
                
                <ul><li>Your timetable has been saved, return to the <a href="Default.aspx">Timetables home.</li></ul>
                
            </asp:View>
        
        </asp:MultiView>

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
} catch(err) {}</script>
    
</body>
</html>
