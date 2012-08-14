<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProductionBookingMobile.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>Production Booking Mobile</title>
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">

     <script type="text/javascript">
         function hideAddressbar() {
             window.scrollTo(0, 1);
         }
    </script>

    <div id="banner">
        <a href="../Default.aspx" class="first"><img src="Images/PocketCampusHome.png" alt="Pocket Campus Home"/></a>
        <a href="Default.aspx"><img class="second" src="Images/ProductionBookingHome.png" alt="Production Booking Home"/></a>
        <h1 class="secondheader">Production Booking</h1>
    </div>

    <div id="content">

   

        <asp:MultiView ID="MultiView" runat="server">

            <asp:View ID="DefaultView" runat="server">
                <h1>Select a task</h1>

                <ul>
                    <li><a href="?View=True"><img class="arrow" src="Images/ArrowButton.jpg"/>New Booking</li>
                    <li><a href="ExistingBookings.aspx"><img class="arrow" src="Images/ArrowButton.jpg"/>Edit Booking</li>
                    <li><a href="Login.aspx"><img class="arrow" src="Images/ArrowButton.jpg"/>Logout</li>
                </ul>
            </asp:View>
        
            <asp:View ID="RoomList" runat="server">
                <h1>Please select a studio to book.</h1>
                
                <ul>
                    <li><a href="?rid=PS1"><img class="arrow" src="Images/ArrowButton.jpg"/>Performance Studio 1</a></li>
                    <li><a href="?rid=PS2"><img class="arrow" src="Images/ArrowButton.jpg"/>Performance Studio 2</a></li>
                    <li><a href="?rid=PS3"><img class="arrow" src="Images/ArrowButton.jpg"/>Performance Studio 3</a></li>
                </ul>

                <ul>
                    <li><a href="Default.aspx">Return to Task List</a></li>
                </ul>
                
            </asp:View>
            
            <asp:View ID="DatesList" runat="server">
                <h1>Please select a date to book.</h1>
                
                <asp:Label ID="DatesLabel" runat="server"></asp:Label>

                <ul>
                    <li><a href="Default.aspx?view=True">Return to Room List</a></li>
                </ul>
            </asp:View>
            
            <asp:View ID="RoomView" runat="server">
                <h1>Please select a time slot.</h1>
                <ul>
                    <li>Studio: <asp:Label runat=server ID="Roomlbl" /></li>
                </ul>

                <ul>
                    <li>
                        <asp:Table ID="RoomTable" runat="server">
                        </asp:Table>
                    </li>
                </ul>

                <ul>
                    <li><asp:Label runat="server" ID="ReturntoDateslbl" Text="Return to Dates List"/></li>
                </ul>
                
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
    } catch (err) { }</script>
    
</body>
</html>
