<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExistingBookings.aspx.cs"
    Inherits="KDLBookingMobile.ExistingBookings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <title>KDL Booking</title>
    <link rel="stylesheet" type="text/css" href="RoundedRectangle.css" />
    <link rel="apple-touch-icon" href="Images/PocketCampus.png" />
</head>
<body onload="hideAddressbar();">
    <form id="form1" runat="server">
    <div>
        <div id="banner">
            <a href="../Default.aspx" class="first">
                <img src="Images/PocketCampusHome.png" alt="Pocket Campus Home" /></a> <a href="Default.aspx">
                    <img class="second" src="Images/KDLBookingHome.png" alt="KDL Booking Home" /></a>
            <h1 class="secondheader">
                KDL Booking</h1>
        </div>
        <div id="content">

            <asp:MultiView ID="MultiView" runat="server">

                <asp:View ID="ListView" runat="server">

                    <h2>Your bookings:</h2>

                    <asp:Label runat="server" ID="Bookingslbl"/>

                </asp:View>

                <asp:View ID="OptionView" runat="server">
                
                    <h2>Modify bookings:</h2>

                    <ul>
                        <li>Location:<asp:Label ID="Locationlbl" runat="server"></asp:Label>
                            <br /></li>
                        <li>Date:<asp:Label ID="Datelbl" runat="server"></asp:Label>
                            <br /></li>
                        <li>Start Time:<asp:Label ID="StartTimelbl" runat="server"></asp:Label>
                            <br /></li>
                        <li>End Time:<asp:Label ID="EndTimelbl" runat="server"></asp:Label>
                            <br /></li>
                            <li><asp:ImageButton ID="EditBookingbtn" runat="server" 
                                    ImageUrl="Images/EditBookingButton.png" onclick="EditBookingbtn_Click" /></li>
                            <li><asp:ImageButton ID="DeleteButtonbtn" runat="server" 
                                    ImageUrl="Images/DeleteBookingButton.png" onclick="DeleteButtonbtn_Click" /></li>
                    </ul>

                </asp:View>

            </asp:MultiView>

        </div>
    </div>
    </form>
</body>
</html>
